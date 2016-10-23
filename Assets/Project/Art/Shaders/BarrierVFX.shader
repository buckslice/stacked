Shader "Custom/BarrierVFX"
{
	Properties
	{
		_Color("_Color", Color) = (0.0,1.0,0.0,1.0)
		_HighlightColor("Highlight Color", Color) = (1, 1, 1, .5) //Color when intersecting
		_Inside("_Inside", Range(0.0,1.0) ) = 0.0
		_Rim("_Rim", Range(0.0,1.0) ) = 1.2
		_MainTex("_Texture", 2D) = "white" {}
		_Speed("_Speed", Range(0.5,5.0) ) = 0.5
		_Tile("_Tile", Range(1.0,10.0) ) = 5.0
		_Strength("_Strength", Range(0.0,5.0) ) = 1.5

		_HighlightThresholdMax("Highlight Threshold Max (note: for best results, scale with shield size)", Float) = 1 //Max difference for intersections
		
	}
	SubShader
	{
		Tags
		{
			"Queue"="Transparent"
			"IgnoreProjector"="True"
			"RenderType"="Transparent"

		}
		Cull Off
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

			uniform float4 _Color;
			uniform float4 _HighlightColor;
			uniform fixed _Inside;
			uniform fixed _Rim;
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform fixed _Speed;
			uniform fixed _Tile;
			uniform fixed _Strength;

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
				float dNorm = 1.0 - dot(normalize(i.viewDir), normalize(i.normal) );

				float2 UV_Pan= _Tile.x * float2(i.uv.x, i.uv.y + _Time.x * _Speed.x);
				float4 Tex2D=tex2D(_MainTex,UV_Pan);


				float Step=step(dNorm,1);
				float4 Clamp=saturate(Step);
				float4 Pow=pow(dNorm,_Rim);

				float4 result = lerp(_Color, _HighlightColor, Clamp * Pow * _Strength);

				result.a *= Clamp * Pow * _Strength * Tex2D.a;

				return result;
			}
			ENDCG
		}

		//Overlay
		Pass
		{
			Blend SrcAlpha One
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
				float4 projPos : TEXCOORD3; //Screen position of pos
			};

			uniform float4 _Color;
			uniform float4 _HighlightColor;
            uniform float _HighlightThresholdMax;
			uniform float _Inside;
			uniform sampler2D _CameraDepthTexture; //Depth Texture

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.projPos = ComputeScreenPos(o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float4 result = _Inside * _Color;

				//Get the distance to the camera from the depth buffer for this point
                float sceneZ = LinearEyeDepth (tex2Dproj(_CameraDepthTexture,
                                                         UNITY_PROJ_COORD(i.projPos)).r);
 
                //Actual distance to the camera
                float partZ = i.projPos.z;
 
                //If the two are similar, then there is an object intersecting with our object
                float diff = (abs(sceneZ - partZ)) /
                    _HighlightThresholdMax;
 
                if(diff <= 1)
                {
                    //result += (1 - diff) * _HighlightColor;

					diff = pow(1 - diff, 3);
					result = lerp(result, _HighlightColor, diff);
                }

				return result;
			}
			ENDCG
		}
	}
}
