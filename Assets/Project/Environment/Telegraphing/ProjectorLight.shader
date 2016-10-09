
Shader "Projector/Light" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_ShadowTex ("Cookie", 2D) = "" {}
        _PulseRate ("Pulse Rate", Range(0.1, 5.0)) = 1.0
        _MaxPulse("Max Pulse", Range(0.0, 1.0)) = 1.0
        _MinPulse("Min Pulse", Range(0.0, 1.0)) = 0.0
	}
	
	Subshader {
		Tags {"Queue"="Transparent"}
		Pass {
			ZWrite Off
			ColorMask RGB
            Blend One One
			Offset -1, -1
	
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			#include "UnityCG.cginc"
			
			struct v2f {
				float4 uvShadow : TEXCOORD0;
				UNITY_FOG_COORDS(2)
				float4 pos : SV_POSITION;
                float2 t : TEXCOORD1;
			};
			
			float4x4 unity_Projector;
			float4x4 unity_ProjectorClip;

            fixed4 _Color;
            sampler2D _ShadowTex;
            float _PulseRate;
            float _MaxPulse;
            float _MinPulse;
			
			v2f vert (float4 vertex : POSITION)
			{
				v2f o;
				o.pos = mul (UNITY_MATRIX_MVP, vertex);
				o.uvShadow = mul (unity_Projector, vertex);
                // the 3.11 is a magic number to get the pulseRate to approximately match seconds
                // couldnt figure out how to get it working otherwise
                o.t.x = lerp(_MinPulse, _MaxPulse, (sin(_Time.z * _PulseRate * 3.11) + 1) / 2.0);
				UNITY_TRANSFER_FOG(o,o.pos);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 texS = tex2Dproj (_ShadowTex, UNITY_PROJ_COORD(i.uvShadow));
                texS.rgb *= _Color.rgb;
				texS.a = 1.0-texS.a;
                texS *= i.t.x;
	
				UNITY_APPLY_FOG_COLOR(i.fogCoord, texS, fixed4(0,0,0,0));
                return texS;
			}
			ENDCG
		}
	}
}
