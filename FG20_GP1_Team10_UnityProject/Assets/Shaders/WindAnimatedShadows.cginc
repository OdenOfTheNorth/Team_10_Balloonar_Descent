#if !defined(SHADOWS_INCLUDED)
#define SHADOWS_INCLUDED

#include "UnityCG.cginc"

struct shadowVertexData
{
    float4 position : POSITION;
    float3 normal : NORMAL;
};

#if defined(SHADOWS_CUBE)
    struct interpolators
    {
        float4 position : SV_POSITION;
        float3 lightVec : TEXCOORD0;
    };

    interpolators shadowVert(shadowVertexData v)
    {
        interpolators i;
        i.position = UnityObjectToClipPos(v.position);
        i.lightVec = mul(unity_ObjectToWorld, v.position).xyz - _LightPositionRange.xyz;
        return i;
    }
    float4 shadowFrag(interpolators i) : SV_TARGET
    {
        float depth = length(i.lightVec) + unity_LightShadowBias.x;
        depth *= _LightPositionRange.w;
        return UnityEncodeCubeShadowDepth(depth);
    }
#else
    float4 shadowVert(shadowVertexData v) : SV_POSITION 
    {
        float4 position = UnityClipSpaceShadowCasterPos(v.position.xyz, v.normal);
        return UnityApplyLinearShadowBias(position);
    }
    float4 shadowFrag() : SV_TARGET
    {
        return 0;
    }
#endif
#endif