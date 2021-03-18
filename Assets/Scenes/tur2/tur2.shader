Shader "Unlit/tur2"
{
    Properties
    {
        _SplatMap ("Splat Tex", 2D) = "white" {}
        _MainTex ("Texture", 2D) = "white" {}
        _DetailTex ("Detail Tex", 2D) = "white" {}
        _BrightCtrl ("Bright Ctrl", Range(0.5, 2.5)) = 2
        
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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
                float2 detailuv : TEXCOORD1;
                float2 spltuv : TEXCOORD2;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _DetailTex;
            float4 _DetailTex_ST;

            sampler2D _SplatMap;
            float4 _SplatMap_ST;




            float _BrightCtrl;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.detailuv = TRANSFORM_TEX(v.uv, _DetailTex);
                o.spltuv = TRANSFORM_TEX(v.uv, _SplatMap);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                // sample the texture
                // float4 col = tex2D(_MainTex, i.uv);

                // col *= tex2D(_DetailTex, i.detailuv) * _BrightCtrl;

                float splat = tex2D(_SplatMap, i.spltuv);

                float4 c1 = tex2D(_MainTex, i.uv);

                float c2 = tex2D(_DetailTex, i.detailuv);
                
                float4 ret = c1 * splat.r + c2 * (1 - splat.r);

                return ret;
            }
            ENDCG
        }
    }
}
