Shader "Custom/MeshPulse"
{
	Properties
	{
		_Color1("_Color1", Color) = (0.0,1.0,0.0,1.0)
		_Color2("_Color2", Color) = (1.0,0.0,1.0,1.0)
		_NumBands("NumBands", Float) = 100
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
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 uv : TEXCOORD0;
			};

			fixed4 _Color1;
			fixed4 _Color2;
			float _NumBands;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv  = o.vertex;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float screenBand = (_NumBands * (2 + ((i.uv.x - i.uv.y) / i.uv.w))) % 1;
				if(screenBand > 0.5) {
					return _Color1;
				} else {
					return _Color2;
				}
			}

			ENDCG
		}
	}
}