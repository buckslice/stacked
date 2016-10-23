Shader "Unlit/FlowMap"
{
	Properties
	{
		_MainTex("MainTexture", 2D) = "white" {}
		_FlowMapTex("Flow Map", 2D) = "bump" {}
		_DistortionStrength("Distortion Strength", Float) = 0.1
		_TimeScale("Time Scale", Float) = 0.1
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "IgnoreProjector"="True" "Queue"="Transparent"  "PreviewType"="Plane"}
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off
		Lighting Off

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
				float2 uv : TEXCOORD0;
				float2 uv_main : TEXCOORD1;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _FlowMapTex;

			half _DistortionStrength;
			half _TimeScale;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				o.uv_main = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float2 displacement = tex2D(_FlowMapTex, i.uv).xy;

				//map from [0, 1] to [-1, 1] space
				displacement -= 0.5;
				displacement *= 2 * _DistortionStrength;

				half timeFrac1 = frac(_TimeScale * _Time.y);
				half timeFrac2 = frac(_TimeScale * _Time.y + 0.5);

				half lerpValue = 2 * min(timeFrac1, 1 - timeFrac1);
				
				fixed4 value1 = tex2D(_MainTex, i.uv_main - timeFrac1 * displacement);
				fixed4 value2 = tex2D(_MainTex, i.uv_main - timeFrac2 * displacement);
				return lerp(value2, value1, lerpValue);
			}
			ENDCG
		}
	}
}