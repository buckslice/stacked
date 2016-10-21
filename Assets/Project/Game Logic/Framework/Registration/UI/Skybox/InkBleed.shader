﻿Shader "Hidden/InkBleed"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_PrevFrame("Previous Frame", 2D) = "white" {}
		_InkBleedMap("Ink Bleed Map", 2D) = "white" {}
		_InkBleedMag("Ink Bleed Magnitude", float) = 0
		_InkFadeMag("Ink Fade Magnitude", float) = 0
		_OffsetMag("Offset Magnitude", float) = 0
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

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float2 uv0 : TEXCOORD0;
					float2 uv1 : TEXCOORD1;
					float2 inkuv : INKUV;
					float4 vertex : SV_POSITION;
				};

				sampler2D _MainTex;
				float2 _MainTex_TexelSize;
				sampler2D _PrevFrame;
				sampler2D _InkBleedMap;
				float4 _InkBleedMap_ST;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
					o.uv0 = v.uv;
					o.uv1 = v.uv;

					o.inkuv = TRANSFORM_TEX(v.uv, _InkBleedMap);

					#if UNITY_UV_STARTS_AT_TOP
					if (_MainTex_TexelSize.y < 0)
						o.uv1.y = 1 - o.uv1.y;
					#endif

					return o;
				}

				float _InkBleedMag;
				float _InkFadeMag;
				float _OffsetMag;

				fixed4 frag(v2f i) : SV_Target
				{
					float2 offsetOffset = _OffsetMag * float2(_Time.y * 0.25, _CosTime.y);
					float2 bleedOffset1 = (tex2D(_InkBleedMap, i.inkuv + offsetOffset) - 0.25) * 2;
					float2 bleedOffset2 = (tex2D(_InkBleedMap, i.inkuv - offsetOffset) - 0.25) * 2;

					//i.uv1 += 0.01 * float2(_CosTime.y, _CosTime.y);

					fixed4 col = (tex2D(_PrevFrame, i.uv1 + bleedOffset1 * _InkBleedMag) + tex2D(_PrevFrame, i.uv1 + bleedOffset2 * _InkBleedMag)) + _InkFadeMag;
					col *= tex2D(_MainTex, i.uv0);

					return col;
				}
				ENDCG
			}
		}
}