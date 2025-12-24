Shader "EmpireWars/UI/CircularMask"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _BorderColor ("Border Color", Color) = (0.6, 0.5, 0.3, 1)
        _BorderWidth ("Border Width", Range(0, 0.1)) = 0.03
        _Softness ("Edge Softness", Range(0, 0.1)) = 0.02
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

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
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            fixed4 _BorderColor;
            float _BorderWidth;
            float _Softness;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color * _Color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Merkeze uzaklik hesapla (0-0.5 arasinda)
                float2 center = float2(0.5, 0.5);
                float dist = distance(i.uv, center);

                // Daire yarıcapi (0.5 = kenar)
                float radius = 0.5;
                float innerRadius = radius - _BorderWidth;

                // Texture ornegi
                fixed4 col = tex2D(_MainTex, i.uv) * i.color;

                // Dairesel mask - kenar yumusatma ile
                float circle = 1.0 - smoothstep(innerRadius - _Softness, innerRadius, dist);

                // Border
                float border = smoothstep(innerRadius - _Softness, innerRadius, dist)
                             - smoothstep(radius - _Softness, radius, dist);

                // Sonuc - border ile birlikte
                fixed4 result = col * circle + _BorderColor * border;
                result.a = (circle + border) * col.a;

                // Daire disini tamamen sil
                if (dist > radius) discard;

                return result;
            }
            ENDCG
        }
    }
}
