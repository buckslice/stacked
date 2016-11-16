Shader "Custom/SpriteMasking"
{
	Properties
	{
		_MainTex ("Sprite Texture", 2D) = "white" {}
		_MaskTex("Animation Mask", 2D) = "white" {}
		_Color("Color", Color) = (1, 1, 1, 1)
		_MaskScalar("Mask Scalar", Float) = 1
		_Cutoff("Cutoff", Range(0, 1)) = 0.5
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend SrcAlpha One

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				float2 texcoord  : TEXCOORD0;
			};
			
			fixed4 _Color;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;

				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _MaskTex;
			float _MaskScalar;
			float _Cutoff;

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 c = tex2D(_MainTex, IN.texcoord) * _Color;
				float animationValue = _MaskScalar * tex2D(_MaskTex, IN.texcoord).r;
				c.a *= step(animationValue, _Cutoff);
				return c;
			}
		ENDCG
		}
	}
}