﻿Shader "Unlit/PortalCard"
{
	Properties
	{
		_NoiseTexture("Noise Texture", 2D) = "white" {}
		_NoiseDirection("Noise Direction", Vector) = (1,0,0,0)
		_FogColor("Fog Color", Color) = (1,1,1,1)
		_FogColor2("Fog Color", Color) = (1,1,1,1)
		_Shift("Shift", Range(0, 1)) = 0
		_FogLength("Fog Density", float) = 0.01
		_MaxDensity("Max Density", float) = 0.8
	}
		SubShader
	{
		Tags{"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
		LOD 200
		Blend SrcAlpha OneMinusSrcAlpha
		Lighting Off ZWrite Off

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				float4 screenPos : TEXCOORD2;
				float3 wpos : TEXCOORD3;
			};

			sampler2D _CameraDepthTexture;
			sampler2D _NoiseTexture;
			float4 _NoiseTexture_ST;
			float4 _CameraDepthTexture_ST;
			fixed4 _FogColor;
			fixed4 _FogColor2;
			float _Shift;
			float _FogLength;
			float4 _NoiseDirection;
			float _MaxDensity;

			v2f vert(appdata v)
			{
				v2f o;

				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _CameraDepthTexture);
				o.screenPos = ComputeScreenPos(o.vertex);
				o.wpos =  mul(unity_ObjectToWorld, v.vertex).xyz;

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				float depth = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, i.screenPos)) * 0.1 * _FogLength;

				float dist = i.screenPos.w;
				float noiseVal = tex2D(_NoiseTexture, (i.wpos.xz * _NoiseTexture_ST) + _NoiseDirection.xy * _Time.x);
				float noiseVal2 = tex2D(_NoiseTexture, (i.wpos.xz * _NoiseTexture_ST) + float2(-_NoiseDirection.y, _NoiseDirection.x) * _Time.x);
				float noise = (noiseVal + noiseVal2) * 0.5;

				depth -= noise;
				float newDepth = saturate(depth - (dist * 0.1 * _FogLength));

				float3 fog = lerp(_FogColor.xyz, _FogColor2, _Shift);
				return float4(fog, min(min(newDepth, 1), _MaxDensity));
			}
			ENDCG
		}
	}
}