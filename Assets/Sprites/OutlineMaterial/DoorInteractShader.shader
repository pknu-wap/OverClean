Shader "Sprites/DoorOutline" {
    Properties {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Main texture Tint", Color) = (1,1,1,1)

        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineSize ("Outline Width", Range(0.0, 0.05)) = 0.02

        [Header(General Settings)]
        [MaterialToggle] _OutlineEnabled ("Outline Enabled", Float) = 1
        [MaterialToggle] _ConnectedAlpha ("Connected Alpha", Float) = 0
        [HideInInspector] _AlphaThreshold ("Alpha clean", Range (0, 1)) = 0
        _Thickness ("Width (Max recommended 100)", float) = 3
        [KeywordEnum(Solid, Gradient, Image)] _OutlineMode("Outline mode", Float) = 0
        [KeywordEnum(Contour, Frame)] _OutlineShape("Outline shape", Float) = 0
        [KeywordEnum(Inside under sprite, Inside over sprite, Outside)] _OutlinePosition("Outline Position (Frame Only)", Float) = 0
    }

    SubShader {
        Tags {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ PIXELSNAP_ON
            #pragma exclude_renderers d3d11_9x

            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
                fixed4 color : COLOR;
            };

            fixed4 _Color;
            fixed _Thickness;
            fixed _OutlineEnabled;
            fixed _ConnectedAlpha;
            fixed _OutlineShape;
            fixed _OutlinePosition;
            fixed _OutlineMode;
            fixed4 _OutlineColor;
            fixed _OutlineSize;

            sampler2D _MainTex;
            uniform float4 _MainTex_TexelSize;

            v2f vert(appdata_t IN) {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color * _Color;
                #ifdef PIXELSNAP_ON
                OUT.vertex = UnityPixelSnap(OUT.vertex);
                #endif
                return OUT;
            }

            // UV 좌표를 기반으로 테두리 그리기 로직
            fixed4 frag(v2f IN) : SV_Target {
                float2 uv = IN.texcoord;
                float alpha = 0;

                // 기본 이미지 색상 샘플링
                fixed4 original = tex2D(_MainTex, uv);

                // 테두리 사이즈만큼 이미지 경계 밖으로 UV를 확장하여 테두리 처리
                for (float x = -1.0; x <= 1.0; x += 0.5) {
                    for (float y = -1.0; y <= 1.0; y += 0.5) {
                        float2 offsetUV = uv + float2(x * _OutlineSize, y * _OutlineSize);

                        // 테두리 처리
                        fixed4 sample = tex2D(_MainTex, offsetUV);
                        alpha = max(alpha, sample.a);
                    }
                }

                // 테두리가 필요한 경우 테두리 색상 적용
                if (alpha > 0.1 && original.a < 0.1) {
                    return _OutlineColor;
                }

                // 원래 이미지를 출력
                return original;
            }
            ENDCG
        }
    }
}
