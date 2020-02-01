Shader "Hidden/ScreenGlitching"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        [Header(Stripes)]
        _StripesSpeed("Stripes Speed", Range(0, 1)) = 0
        _StripesAmount("Stripes Amount", Range(0, 1)) = 0.25
        _StripesTiling("Stripes Tiling", Vector) = (0.5, 0.5, 0, 0)
        _StripesDistance("Stripes Distance", Range(0, 1)) = 0.025
        _NoiseTiling("Noise Tiling", Range(0, 1)) = 1
        _NewSeedSpeed("New Seed Speed", Range(0, 1)) = 0.5

        [Toggle]_Debug("Debug", float) = 0
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _StripesSpeed;
            float _StripesAmount;
            float _StripesDistance;
            float _NoiseTiling;
            float _NewSeedSpeed;

            float2 _StripesTiling;
            float _Debug;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float fract(float val)
            {
                return val - floor(val);
            }

            float random(float2 st) 
            {
                return fract(sin(dot(st.xy, float2(12.9898, 78.233))) * 43758.5453123);
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uvs = i.uv.xy;

                //Calulate noise values
                //float stripeSize = 100 * (1 - _StripesSize);
                float stripeSizeX = 100 * (1 - _StripesTiling.x);
                float stripeSizeY = 100 * (1 - _StripesTiling.y);

                float2 flooredUvs = float2(floor(uvs.x * stripeSizeX), floor(uvs.y * stripeSizeY));
                float newRandomSeedValue = floor(_Time.w * (_NewSeedSpeed * 10));
                newRandomSeedValue = random(float2(newRandomSeedValue, newRandomSeedValue));
                float rand = random(flooredUvs + newRandomSeedValue);

                //Time values
                float2 noiseUvs = uvs * _NoiseTiling;
                float time = _Time.w + random(noiseUvs) + rand;
                time *= step(rand, _StripesAmount); //Check if moving
                time *= _StripesSpeed * 2;
               
                //Tiling
                time = fract(time) * _StripesDistance;
                float newX = uvs.x + time;
                if (newX > 1)
                    newX -= 1;

                uvs.x = newX;

                fixed4 col = tex2D(_MainTex, uvs);

                float debugVal = rand * step(rand, _StripesAmount);
                if (_Debug > 0.5)
                    if (debugVal > 0.01)
                        col = debugVal;

                return col;
            }
            ENDCG
        }
    }
}
