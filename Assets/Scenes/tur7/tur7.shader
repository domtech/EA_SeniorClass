Shader "Unlit/tur7"
{
    Properties
    {
        _ColorA ("Color A", Color) = (1,1,1,1)
        _ColorB ("Color B", Color) = (1,1,1,1)
        
    }
    SubShader
    {
        Tags { 
            "RenderType"="Transparent"
            "Queue" = "Transparent"
         }
        Blend One One
        Zwrite Off
        Cull Off

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

            float4 _ColorA;
            float4 _ColorB;
            



            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {

                float4 col = lerp(_ColorA, _ColorB, i.uv.y);
                return col;
            }
            ENDCG
        }
    }
}
