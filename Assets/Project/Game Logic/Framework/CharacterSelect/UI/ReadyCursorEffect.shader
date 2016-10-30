Shader "Unlit/ReadyCursorEffect"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
        _NoiseTex("Noise", 2D) = "white" {}
        _Color("Main Color", COLOR) = (1,1,1,1)
	}
	SubShader
	{
        Tags{ "RenderType" = "Transparent" "IgnoreProjector" = "True" "Queue" = "Transparent"  "PreviewType" = "Plane" }
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
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
            sampler2D _NoiseTex;
            float4 _Color;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
                float d = distance(i.uv,float2(0.5, 0.5));
                col *= _Color;
                i.uv.x += _Time.x;
                fixed4 off = tex2D(_NoiseTex, i.uv*1);
                col.a *= saturate((sin(_Time.x*200+d*d*60-off*5)+1.)/2.);
				return col;
			}
			ENDCG
		}
	}
}
