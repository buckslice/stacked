﻿Shader "UI/Custom/UIFlowMap"
{
	Properties
	{	
		_FlowingTex("Flowing Texture", 2D) = "white" {}
		_FlowMapTex("Flow Map", 2D) = "bump" {}
		_DistortionStrength("Distortion Strength", Float) = 0.1
		_TimeScale("Time Scale", Float) = 0.1
		
		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255

		_ColorMask ("Color Mask", Float) = 15
		
		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
	}
	
	SubShader
	{
		LOD 100

		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType"="Plane"
		}

		Stencil
		{
			Ref [_Stencil]
			Comp [_StencilComp]
			Pass [_StencilOp] 
			ReadMask [_StencilReadMask]
			WriteMask [_StencilWriteMask]
		}
		
		Cull Off
		Lighting Off
		ZWrite Off
		ZTest [unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask [_ColorMask]

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
		
			#include "UnityCG.cginc"
			#include "UnityUI.cginc"

			#pragma multi_compile __ UNITY_UI_ALPHACLIP

			struct appdata
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				fixed4 color : COLOR;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 texcoord : TEXCOORD0;
				float4 worldPosition : TEXCOORD1;
				fixed4 color : COLOR;
			};

			sampler2D _FlowingTex;
			sampler2D _FlowMapTex;

			float _DistortionStrength;
			float _TimeScale;

			fixed _Strength;
			
			fixed4 _TextureSampleAdd;

			bool _UseClipRect;
			float4 _ClipRect;
				
			bool _UseAlphaClip;

			v2f vert (appdata v)
			{
				v2f o;
				o.worldPosition = v.vertex;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = v.texcoord;
				o.color = v.color;

				#ifdef UNITY_HALF_TEXEL_OFFSET
				o.vertex.xy += (_ScreenParams.zw-1.0)*float2(-1,1);
				#endif

				return o;
			}

			fixed4 frag (v2f i) : COLOR
			{
				float2 displacement = tex2D(_FlowMapTex, i.texcoord).xy;

				//map from [0, 1] to [-1, 1] space
				displacement -= 0.5;
				displacement *= 2 * _DistortionStrength;

				half timeFrac1 = frac(_TimeScale * _Time.y);
				half timeFrac2 = frac(_TimeScale * _Time.y + 0.5);

				half lerpValue = 2 * min(timeFrac1, 1 - timeFrac1);
				
				fixed4 value1 = tex2D(_FlowingTex, i.texcoord - timeFrac1 * displacement);
				fixed4 value2 = tex2D(_FlowingTex, i.texcoord - timeFrac2 * displacement);
				fixed4 color = lerp(value2, value1, lerpValue);
				
				color.a *= UnityGet2DClipping(i.worldPosition.xy, _ClipRect);
				
				#ifdef UNITY_UI_ALPHACLIP
				clip (color.a - 0.001);
				#endif
				
				return color;
			}
			ENDCG

		}
	}
}