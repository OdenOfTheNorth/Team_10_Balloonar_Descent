Shader "Custom/WindAnimated"
{
    Properties
    {
        _MainTex ("Albedo", 2D) = "white" {}
        _CutOff ("Alpha Cutoff", Range(0, 1)) = 0.5

        [HDR] _Color ("Tint", color) = (1,1,1,1)
        [Gamma] _Metallic ("Metallic", Range(0, 1)) = 0 
        
        [NoScaleOffset] _NormalMap("Normal Map", 2D) = "bump" {}
        _BumpScale ("Bump Scale", float) = 1
        _Smoothness("Smoothness", Range(0.001, 1)) = 0.5
        
        _WindStrength("Wind Strength", float) = 1.0
        _WindDensity("Wind Density", float) = 1.0
        
    }
    CGINCLUDE

    #define BINORMAL_PER_FRAGMENT
    
    ENDCG
    
    SubShader
    {
        Tags 
        { 
            "Queue"="Transparent" 
        }
        Cull Off

        Pass
        {
            Name "Forward Base"
            Tags
            {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            
            #pragma target 3.0

            #pragma multi_compile _ SHADOWS_SCREENku67#UnityÜnityUnity_Objecti
            #pragma multi_compile _ VERTEXLIGHT_ON
            #pragma multi_compile_fog
            
            #define FORWARD_BASE_PASS
            
            #include "WindAnimated.cginc"
            
            ENDCG
            
        }
        Pass
        {
            Name "Forward Add"
            Tags
            {
                "LightMode" = "ForwardAdd" 
            }
            Blend SrcAlpha One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #pragma target 3.0

            #pragma multi_compile_fwdadd_fullshadows
            
            #include "WindAnimated.cginc"
            ENDCG    
        }
        Pass
        {
            Name "ShadowCaster"
            Tags
            {
                "LightMode" = "ShadowCaster" 
            }
            
            CGPROGRAM
            #pragma vertex shadowVert
            #pragma fragment shadowFrag

            #pragma target 3.0

            #pragma multi_compile_shadowcaster
            #include "WindAnimatedShadows.cginc"
            ENDCG    
        }
        
    }
}
