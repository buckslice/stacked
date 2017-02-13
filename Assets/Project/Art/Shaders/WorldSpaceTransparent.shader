Shader "Custom/WorldSpaceTransparent" {
    Properties{
        _Color("Main Color", Color) = (1,1,1,1)
        _MainTex("Base (RGB)", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
    }

    SubShader{
    Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }

    ZWrite Off
    Blend OneMinusDstColor One // Soft Additive

    CGPROGRAM
    #pragma surface surf Standard fullforwardshadows
    #pragma target 3.0

    sampler2D _MainTex;
    float4 _MainTex_ST;
    fixed4 _Color;

    half _Glossiness;
    half _Metallic;

    struct Input {
        float3 worldPos;
        float3 worldNormal;
    };

    void surf(Input IN, inout SurfaceOutputStandard o) {
        float2 uv;
        fixed4 color;

        // RIGHT NOW USES SAME TEXTURE FOR ALL SIDES
        if (abs(IN.worldNormal.x) > 0.5) {
            uv = IN.worldPos.yz; // left right
        } else if (abs(IN.worldNormal.z) > 0.5) {
            uv = IN.worldPos.xy; // front back
        } else {
            uv = IN.worldPos.xz; // top bottom
        }
        uv = uv * _MainTex_ST.xy + _MainTex_ST.zw;
        color = tex2D(_MainTex, uv);

        o.Albedo = color.rgb * _Color;

        // Metallic and smoothness come from slider variables
        o.Metallic = _Metallic;
        o.Smoothness = _Glossiness;
    }
    ENDCG
    }
    Fallback "Diffuse"
}