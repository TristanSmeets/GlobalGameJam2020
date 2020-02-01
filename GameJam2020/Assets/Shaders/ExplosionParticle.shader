Shader "Custom/ExplosionParticle"
{
    Properties
    {
        [HDR]_Color ("Color", Color) = (1,1,1,1)
        _NoiseTexture1("Noise Texture 1", 2D) = "white" {}
        _NoiseTexture2("Noise Texture 2", 2D) = "white" {}
        _MaskTexture("Mask Texture", 2D) = "white" {}

        [Header(Glitch Variables)]
        [HDR]_GlitchColor("Glitch Color", Color) = (1,1,1,1)
        _GlitchTiling("Glitch Tiling", float) = 20
        _GlitchSpeed("Glitch Speed", float) = 1
        _GlitchSpotAmount("Glitch Spot Amount", float) = 5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard vertex:vert fullforwardshadows alpha:fade
        #pragma target 3.0

        fixed4 _Color;
        fixed4 _GlitchColor;
        sampler2D _NoiseTexture1;
        sampler2D _NoiseTexture2;
        sampler2D _MaskTexture;

        //Glitch variables
        float _GlitchTiling;
        float _GlitchSpeed;
        float _GlitchSpotAmount;

        struct appdata
        {
            float4 vertex : POSITION;
            float3 normal : NORMAL;
            float3 texcoord : TEXCOORD0;
            float2 texcoord1 : TEXCOORD1;
            float2 texcoord2 : TEXCOORD2;
        };

        struct Input
        {
            float2 uv_NoiseTexture1;
            float agePercent;
            float3 pos;
        };

        void vert(inout appdata v, out Input IN)
        {
            UNITY_INITIALIZE_OUTPUT(Input, IN);
            IN.agePercent = v.texcoord.z;
            IN.pos = mul(unity_ObjectToWorld, v.vertex).xyz;
        }

        float fract(float val)
        {
            return val - floor(val);
        }

        float random(float2 st)
        {
            return fract(sin(dot(st.xy, float2(12.9898, 78.233))) * 43758.5453123);
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float agePercent = IN.agePercent;
            float3 pos = IN.pos;

            //Base uvs
            float2 uvs = IN.uv_NoiseTexture1.xy;

            //Glitch
            float block = random(float2(floor(pos.x * _GlitchSpotAmount), floor(pos.y * _GlitchSpotAmount))
                                + float2(_Time.x, _Time.x) * 0.000005);
            float ypos = random(float2(floor(pos.x * _GlitchTiling), floor(pos.y * _GlitchTiling))
                                + float2(_Time.x, _Time.x) * _GlitchSpeed);
            float isGlitching = step(ypos, 0.5) * step(block, 0.5);
            uvs += isGlitching;

            //Explosion stuff
            float2 halfUvs = uvs * 0.5;
            float4 noise1 = tex2D(_NoiseTexture1, uvs);
            float4 noise2 = tex2D(_NoiseTexture2, halfUvs);
            float4 noise = noise1 + noise2;
            float4 subNoise = noise - agePercent;
            float4 smoothStepNoise = smoothstep(agePercent, agePercent + 1, subNoise);
            float4 mask = tex2D(_MaskTexture, uvs);
            float4 saturatedMask = saturate((mask * (subNoise * float4(4, 2, 4, 0))) - 1);
            float4 color = _Color * smoothStepNoise;

            //Color change based on glitch
            color = lerp(color, _GlitchColor, isGlitching * agePercent);

            fixed4 c = saturate(color);
            o.Emission = color * (isGlitching * 2);
            o.Alpha = saturatedMask;
            o.Albedo = c.rgb;
            o.Metallic = 0;
            o.Smoothness = 0;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
