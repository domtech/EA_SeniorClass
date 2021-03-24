﻿Shader "Custom/LightShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _LightColor ("Light Color", Color) = (1,1,1,1)
        _Smoothness ("Smoothness", Range(0, 1)) = 0.5
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
                float3 normal: NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float3 normal: TEXCOORD1;
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _LightColor;
            float _Smoothness;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                //o.normal = mul(unity_ObjectToWorld, float4(v.normal, 0));
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                //-1,1 -> (0, 1)
                i.normal = normalize(i.normal);

                float3 lightDir = _WorldSpaceLightPos0.xyz;
                
               

                 //reflect dir
                float3 reflectDir = reflect (-lightDir, i.normal);

                //return float4(reflectDir * 0.5 + 0.5, 1);

                // view dir

                float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);

                return  pow(saturate(dot(viewDir, reflectDir)), _Smoothness * 100);

                
                 float3 albedo = tex2D(_MainTex, i.uv).rgb;

                 return float4(_LightColor * dot(lightDir, i.normal) * albedo, 1);

            }
            ENDCG
        }
    }
}
