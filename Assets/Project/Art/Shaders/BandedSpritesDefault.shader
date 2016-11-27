Shader "Custom/BandedSpritesDefault"
{
	Properties
	{
		[NoScaleOffset] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color1("_Color1", Color) = (0.0,1.0,0.0,1.0)
		_Color2("_Color2", Color) = (1.0,0.0,1.0,1.0)
		_NumBands("NumBands", Float) = 100
	}
		
	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent" 
			"PreviewType" = "Plane"
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
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				float4 bandPos : TEXCOORD1;
			};

			fixed4 _Color1;
			fixed4 _Color2;
			float _NumBands;

			sampler2D _MainTex;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.bandPos = o.vertex;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				half4 mainColor = tex2D(_MainTex, i.uv);
				float screenBand = (_NumBands * (2 + ((i.bandPos.x - i.bandPos.y) / i.bandPos.w))) % 1;
				if (screenBand > 0.5) {
					return mainColor * _Color1;
				}
				else {
					return mainColor * _Color2;
				}
			}

			ENDCG
		}
	}
}