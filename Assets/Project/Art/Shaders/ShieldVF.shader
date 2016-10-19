Shader "Custom/ShieldVF"
{
	Properties
	{
		_Color("_Color", Color) = (0.0,1.0,0.0,1.0)
		_Inside("_Inside", Range(0.0,2.0) ) = 0.0
		_Rim("_Rim", Range(0.0,1.0) ) = 1.2
		_MainTex("_Texture", 2D) = "white" {}
		_Speed("_Speed", Range(0.5,5.0) ) = 0.5
		_Tile("_Tile", Range(1.0,10.0) ) = 5.0
		_Strength("_Strength", Range(0.0,5.0) ) = 1.5
	}
	SubShader
	{
		Tags
		{
			"Queue"="Transparent"
			"IgnoreProjector"="True"
			"RenderType"="Transparent"

		}
		Cull Back
		ZTest LEqual

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
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float3 normal : TEXCOORD1;
				float3 viewDir : TEXCOORD2;
			};

			fixed4 _Color;
			fixed _Inside;
			fixed _Rim;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed _Speed;
			fixed _Tile;
			fixed _Strength;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.normal = v.normal;
				o.viewDir = ObjSpaceViewDir(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float4 Saturate0=fixed4(0.3,0.3,0.3,1.0);
				float dNorm = 1.0 - dot(normalize(i.viewDir), normalize(i.normal) );

				/*
				float Fresnel0 = float4(dNorm,dNorm,dNorm,dNorm);
				float4 Step0=step(Fresnel0,float4( 1.0, 1.0, 1.0, 1.0 ));
				float4 Clamp0=clamp(Step0,_Inside.xxxx,float4( 1.0, 1.0, 1.0, 1.0 ));
				float4 Pow0=pow(Fresnel0,(_Rim).xxxx);
				float2 UV_Pan0= _Tile.x * float2(i.uv.x, i.uv.y + _Time.x * _Speed.x);
				float4 Tex2D0=tex2D(_MainTex,UV_Pan0);
				float4 Multiply2= Tex2D0 * _Strength.x;
				float4 Multiply0=Pow0 * Multiply2;
				float4 Multiply3=Clamp0 * Multiply0;
				float4 Multiply4=Saturate0 * Multiply3;
				float4 result;

				result.xyz = Multiply3.rgb * _Color.rgb;
				result.a = Multiply3.a * _Color.a;

				return result;
				*/

				float2 UV_Pan= _Tile.x * float2(i.uv.x, i.uv.y + _Time.x * _Speed.x);
				float4 Tex2D=tex2D(_MainTex,UV_Pan);

				float4 result;
				result.rgb = _Color.rgb;

				float Step=step(dNorm,1);
				float4 Clamp=clamp(Step,_Inside,1);
				float4 Pow=pow(dNorm,_Rim);

				result.a = Clamp * Pow * _Strength * Tex2D.a * _Color.a;

				return result;
			}
			ENDCG
		}
	}
}