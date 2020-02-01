Shader "Custom/WallShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _NormalMap("Normal Map", 2D) = "bump" {}
        _EmissionMap("Emission Map", 2D) = "black" {}
        [HDR]_EmissionColor("Emission Color", Color) = (0,0,0,0)
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _MetallicMap("Metallic Map", 2D) = "white" {}
        _Metallic ("Metallic", Range(0,1)) = 0.0
        
        [Header(Cube Movement Variables)]
        _CubeIdentifier("Cube Identifier Map", 2D) = "white" {}
        _MaxCubeDistance("Max Cube Distance", float) = 0.4
        _CubeSpeed("Cube Speed", float) = 2
        _Seed("Seed", float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard vertex:vert fullforwardshadows addshadow
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _NormalMap;
        sampler2D _EmissionMap;
        sampler2D _MetallicMap;
        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        fixed4 _EmissionColor;

        //Cube movement stuff
        sampler2D _CubeIdentifier;
        float _MaxCubeDistance;
        float _CubeSpeed;
        float _Seed;

        struct Input
        {
            float2 uv_MainTex;
        };

        float fract(float val)
        {
            return val - floor(val);
        }

        float random(float2 st)
        {
            return fract(sin(dot(st.xy, float2(12.9898, 78.233))) * 43758.5453123);
        }

        void vert(inout appdata_full v)
        {
            float tex = tex2Dlod(_CubeIdentifier, v.texcoord);
            if (tex > 0)
            {
                float seed = _Seed * 9532.14;
                float offset = random(float2(seed * 79.341, seed * 23.523));
                float value = random(float2(tex * offset, tex * offset));

                float rand = random(float2(value, value));
                float sinVal = sin((_Time.z * _CubeSpeed) + (rand * 421));
                sinVal = (sinVal + 1) * 0.5;
                v.vertex.x += sinVal* _MaxCubeDistance;
            }
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_MainTex));
            o.Metallic = tex2D(_MetallicMap, IN.uv_MainTex) * _Metallic;
            o.Smoothness = _Glossiness;
            o.Emission = tex2D(_EmissionMap, IN.uv_MainTex) * _EmissionColor;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
