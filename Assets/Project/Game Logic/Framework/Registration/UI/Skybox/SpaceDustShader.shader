// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/SpaceDustShader"
{
	//Derek Edrich, 2016

	//the alpha channel of the input color controls the size of the actual dust particle relative to the out of focus circle. It also serves as a multiplier to the circle's alpha.
	Properties
	{
		_MainTex("Main (Halo) Texture (Greyscale)", 2D) = "gray" {}
		_HaloDist("Distance range of focus", Float) = 3
		_DustDist("Distance to show a hard dust texture", Float) = 2
		_HaloAlpha("HaloAlphaMultiplier", Range(0, 1)) = 0.25
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "IgnoreProjector"="True" "Queue"="Transparent"  "PreviewType"="Plane"}
		Blend One One
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
				float4 color : COLOR;
			};

			struct v2f
			{
				half4 color : COLOR;
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				float focus : TEXCOORD1;
			};

			uniform sampler2D _MainTex;
			uniform float _HaloDist;
			uniform float _DustDist;

			inline float inverseLerp(float min, float max, float value)
			{
				//inverse of lerp
				return (value - min) / (max - min);
			}

			v2f vert (appdata v)
			{
				v2f o;

				//fade out mechanism for dust particles
				//the dust texture shrinks to simulate being in focus

				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);

				//get distance from camera
				float dist = o.vertex.z;

				o.focus = abs(dist - _DustDist) / _HaloDist; //distance from the point of being perfectly in focus
				o.focus = saturate(o.focus); //clamp [0 .. 1]



				o.uv = v.uv;
				o.color = v.color;


				o.color.a *= 1 - o.focus*o.focus;

				//focus = 1 - (focus-1)^2
				float2 focusFunc = o.focus - 1;
				focusFunc *= focusFunc;
				o.focus = 1 - focusFunc;

				

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float2 centeredUV = (i.uv - 0.5);

				centeredUV /= max(i.focus, 0.01);
				centeredUV = clamp(centeredUV, -1, 1); //this can only be done with the interpolated fragment UV. doing this on a vertex UV screws up interpolation.
				i.uv = centeredUV + 0.5;

				float4 dustColor = tex2D(_MainTex, i.uv);
				dustColor *= i.color;
				dustColor.rgb *= dustColor.a;

				return dustColor;
			}
			ENDCG
		}
	}
}