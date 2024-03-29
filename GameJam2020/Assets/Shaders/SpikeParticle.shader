﻿Shader "Custom/SpikeParticle"
{
    Properties
    {
        [HDR]_Color ("Color", Color) = (1,1,1,1)
        [HDR]_MidColor("Mid Color", Color) = (1,1,1,1)
        [HDR]_TipColor("Tip Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

        [Header(Glitch Variables)]
        [Toggle]_Glitching("Glitching", float) = 0
        _GlitchDistance("Glitch Distance", float) = 0.1
        _GlitchSpeed("Glitch Speed", float) = 1
        _Tess("Tessellation", float) = 16
        _Value("Time", Range(0, 1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
        CGPROGRAM
        #pragma require tessellation tessHW
        #pragma surface surf Standard vertex:vert tessellate:tess //finalcolor:finalColor
        #pragma target 3.0

        sampler2D _MainTex;
        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        fixed4 _TipColor;
        fixed4 _MidColor;

        //Glitch variables
        float _GlitchDistance;
        float _Glitching;
        float _GlitchSpeed;
        float _Tess;
        float _Value;

        struct Input
        {
            float2 uv_MainTex;
        };


        float4 tess(appdata_full v0, appdata_full v1, appdata_full v2)
        {
            if (_Glitching > 0.5)
                return _Tess;
            else
                return 1;
        }

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
            if (_Glitching > 0.5)
            {

                for (float i = 0; i < 5; i++)
                {
                    float j = i + (((_Value * 2) - 1) * 6);
                    float size = 0.05 + (random(float2(j * 423.219, j * 953.127)) * 0.3);
                    float halfSize = size * 0.5;

                    float midPoint = (0.125 + 0.175 + (j * 0.35)) - 1;
                    if (v.vertex.y > midPoint - halfSize && v.vertex.y < midPoint + halfSize)
                    {
                        float4 wpos = mul(unity_ObjectToWorld, float4(0,0,0,1));
                        float dist = random(float2(j * 923.219 + (_Time.z * _GlitchSpeed), j * 453.127 * floor(wpos.x * 10) * floor(wpos.z * 10)));
                        v.vertex.x += _GlitchDistance * ((dist * 2) - 1);
                    }
                }
            }
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            if (IN.uv_MainTex.y < 0.5)
                o.Emission = lerp(_Color, _MidColor, IN.uv_MainTex.y * 2);
            else
                o.Emission = lerp(_MidColor, _TipColor, (IN.uv_MainTex.y - 0.5) * 2);

            o.Alpha = c.a;
        }

        //void finalColor(Input IN, SurfaceOutputStandard o, inout fixed4 color)
        //{
        //    color = float4(1, 0, 0, 1);
        //}
        ENDCG
    }
    FallBack "Diffuse"
}
