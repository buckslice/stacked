	Shader "Custom/Shockwave"
	{
		Properties
	{
		_Annulus ("Annulus Radius", Float) = 1
		_MaxRange ("Outer Radius", Float) = 1
		_DistortionStrength ("DistortionStrength", Float) = 1
	}
	SubShader
	{
		Tags { "Queue"="Transparent" "PreviewType"="Plane"}
		LOD 100

		GrabPass { "_GrabTexture"}

		Pass
		{
			ZTest Off
			ZWrite Off
			Blend One Zero
			Lighting Off
			Fog { Mode Off }
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
				float4 uv : TEXCOORD0;
				float4 uv_center : TEXCOORD1;
				float4 vertex : SV_POSITION;
				float4 obj_vertex : TEXCOORD2;
			};

			sampler2D _GrabTexture;
			half _Annulus;
			half _MaxRange;
			half _DistortionStrength;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = ComputeGrabScreenPos(o.vertex);
				o.obj_vertex = v.vertex;
				o.uv_center = ComputeGrabScreenPos(mul(UNITY_MATRIX_MVP, float4(0, 0, 0, 1)));
				return o;
			}
			
			fixed4 frag (v2f i) : COLOR
			{
				half dist = length(i.obj_vertex.xy);
				dist = saturate((dist - _MaxRange + _Annulus) / (_Annulus));

				if(dist > 0 && dist < 1)
				{
					dist = dist * dist;
					float4 modifiedUVs = i.uv;
					modifiedUVs.xy += dist * _DistortionStrength * normalize(i.uv.xy - i.uv_center.xy);
					return tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(modifiedUVs));
				}
				else
				{
					return tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(i.uv));
				}
			}
			ENDCG
		}
	}
	}
