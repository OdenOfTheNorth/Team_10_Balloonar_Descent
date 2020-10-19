#if !defined(LIGHTING_INCLUDED)
#define LIGHTING_INCLUDED

#include "UnityPBSLighting.cginc"
#include "AutoLight.cginc"

struct vertexData
{
    float4 vertex : POSITION;
    float3 normal : NORMAL;
    float4 tangent : TANGENT;
    float2 uv : TEXCOORD0;
};

struct interpolators
{
    float4 pos : SV_POSITION;
    float2 uv : TEXCOORD0;
    float3 normal : TEXCOORD1;
    #if defined(BINORMAL_PER_FRAGMENT)
        float4 tangent : TEXCOORD2;
    #else
        float4 tangent : TEXCOORD2;
        float3 binormal : TEXCOORD3;    
    #endif
    
    float3 worldPos : TEXCOORD4;
    
    #if defined(VERTEXLIGHT_ON)
    float3 vertexLightColor : TEXCOORD5;
    #endif
    
    SHADOW_COORDS(6)
    UNITY_FOG_COORDS(7)
};

sampler2D _MainTex;
sampler2D _NormalMap;

float4 _MainTex_ST;
float4 _Color;
float _CutOff;
float _Smoothness;
float _BumpScale;
float4 _Metallic;
float4 _AmbientColor;

float _WindStrength;
float4 _WindDirection;
float _WindDensity;

float2 unity_gradientNoise_dir(float2 p)
{
    p = p % 289;
    float x = (34 * p.x + 1) * p.x % 289 + p.y;
    x = (34 * x + 1) * x % 289;
    x = frac(x / 41) * 2 - 1;
    return normalize(float2(x - floor(x + 0.5), abs(x) - 0.5));
}
float unity_gradientNoise(float2 p)
{
    float2 ip = floor(p);
    float2 fp = frac(p);
    float d00 = dot(unity_gradientNoise_dir(ip), fp);
    float d01 = dot(unity_gradientNoise_dir(ip + float2(0, 1)), fp - float2(0, 1));
    float d10 = dot(unity_gradientNoise_dir(ip + float2(1, 0)), fp - float2(1, 0));
    float d11 = dot(unity_gradientNoise_dir(ip + float2(1, 1)), fp - float2(1, 1));
    fp = fp * fp * fp * (fp * (fp * 6 - 15) + 10);
    return lerp(lerp(d00, d01, fp.y), lerp(d10, d11, fp.y), fp.x);
}

void Unity_GradientNoise_float(float2 UV, float Scale, out float Out)
{
    Out = unity_gradientNoise(UV * Scale) + 0.5;
}

float4 AnimateVertex(float4 pos, float2 uv)
{
    float4 vertexPos = pos;

    half2 halfUV = frac(uv * half2(4,2));
    float4 sway = _WindDirection;
    sway.y *=-1;
    float noise = 0.0f;
    Unity_GradientNoise_float(uv * _Time.xy, _WindDensity, noise);

    return vertexPos + (sway * noise * _WindStrength) * halfUV.y;
}

UnityLight CreateLight(interpolators i)
{
    UnityLight light;
    
    #if defined(POINT) || defined(POINT_COOKIE) || defined(SPOT)
        light.dir = normalize(_WorldSpaceLightPos0.xyz - i.worldPos);
    #else
        light.dir = _WorldSpaceLightPos0.xyz;
    #endif
    
    UNITY_LIGHT_ATTENUATION(attenuation, i, i.worldPos);
    light.color = _LightColor0.rgb * attenuation;
    light.ndotl = DotClamped(i.normal, light.dir);
    return light;
}

UnityIndirect CreateIndirectLight(interpolators i)
{
    UnityIndirect indirectLight;
    indirectLight.diffuse = 0;
    indirectLight.specular  = 0;

    #if defined(VERTEXLIGHT_ON)
        indirectLight.diffuse = i.vertexLightColor;
    #endif

    #if defined(FORWARD_BASE_PASS)
        indirectLight.diffuse += max(0, ShadeSH9(float4(i.normal, 1)));
    #endif
    
    return indirectLight;
}

float3 CreateBinormal(float3 normal, float3 tangent, float binormalSign)
{
    return cross(normal, tangent.xyz) * (binormalSign * unity_WorldTransformParams.w);
}

void InitializeFragmentNormal(inout interpolators i, fixed facing)
{
    float3 mainNormal =
        UnpackScaleNormal(tex2D(_NormalMap, i.uv.xy), _BumpScale);
    float3 tangentSpaceNormal = mainNormal;

    #if defined(BINORMAL_PER_FRAGMENT)
        float3 binormal = CreateBinormal(i.normal, i.tangent.xyz, i.tangent.w);
    #else
        float3 binormal = i.binormal;
    #endif
	
    i.normal = normalize(
        tangentSpaceNormal.x * i.tangent +
        tangentSpaceNormal.y * binormal +
        tangentSpaceNormal.z * i.normal
    );

    i.normal *= facing; // Flipped normals for backface
}

float GetAlpha(interpolators i)
{
    float alpha = _Color.a;
     #if !defined(_SMOOTHNESS_ABLEDO)
        alpha *= tex2D(_MainTex, i.uv).a;
     #endif
     return alpha;
}

interpolators vert (vertexData v)
{
    interpolators i;

    UNITY_INITIALIZE_OUTPUT(interpolators, i);
    
    i.pos = UnityObjectToClipPos(v.vertex);
    i.worldPos = mul(unity_ObjectToWorld, v.vertex);
    i.normal = UnityObjectToWorldNormal(v.normal);

    #if defined(BINORMAL_PER_FRAGMENT)
        i.tangent = float4(UnityObjectToWorldDir(v.tangent.xyz), i.tangent.w);
    #else
        i.tangent = UnityObjectToWorldDir(v.tangent.xyz);
        i.binormal = CreateBinormal(i.normal, i.tangent, v.tangent.w);
    #endif
    i.uv = TRANSFORM_TEX(v.uv, _MainTex);
    i.pos = AnimateVertex(i.pos, i.uv);
    
    TRANSFER_SHADOW(i);

    UNITY_TRANSFER_FOG(i,i.pos);

    #if defined(VERTEXLIGHT_ON)
     	float3 lightPos = float3(
     		unity_4LightPosX0.x, unity_4LightPosY0.x, unity_4LightPosZ0.x
     	);
     	float3 lightVec = lightPos - i.worldPos;
     	float3 lightDir = normalize(lightVec);
     	float ndotl = DotClamped(i.normal, lightDir);
     	float attenuation = 1 / (1 + dot(lightVec, lightVec));
     	i.vertexLightColor = unity_LightColor[0].rgb * ndotl * attenuation;
     	#endif

    return i;
}

float4 frag (interpolators i, fixed facing : VFACE) : SV_Target
{
    InitializeFragmentNormal(i, facing);
    
    // sample the textures
    float4 textureColor = tex2D(_MainTex, i.uv);
    float3 albedo = textureColor.rgb * _Color.rgb;
    
    float alpha = GetAlpha(i);
    clip(alpha - _CutOff);
    float3 specularTint;
    float oneMinusReflectivity;
    albedo = DiffuseAndSpecularFromMetallic(albedo, _Metallic, specularTint, oneMinusReflectivity);
    
    // cam
    float3 camPos = _WorldSpaceCameraPos.xyz;
    float3 fragToCam = camPos - i.worldPos;
    float3 viewDir = normalize(fragToCam);

    UnityLight light = CreateLight(i);
    UnityIndirect indirectLight = CreateIndirectLight(i);

    float4 finalColor = UNITY_BRDF_PBS(albedo, specularTint, oneMinusReflectivity, _Smoothness, i.normal, viewDir, light, indirectLight);
    // apply fog
    UNITY_APPLY_FOG(i.fogCoord, finalColor);
    return finalColor;
}
#endif