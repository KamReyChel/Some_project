Shader "Unlit/Water"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DisplacementTexture("Displacement", 2D) = "bump"{}
        _DisplacementX("Displacement X", Range(0,1)) = 0
        _DisplacementY("Displacement Y", Range(0,1)) = 0
        _ColorTint("Color Tint", Color) = (1,1,1,1)
        _SpeedX("Speed along X", Range(-0.25, 0.25)) = 0
        _SpeedY("Speed along Y", Range(-0.25, 0.25)) = 0
        
        _Amplitude("Amplitude", Float) = 1
        _Frequency("Frequency", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

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
                float2 uv2 :TEXCOORD1;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
                float4 vertex : POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            sampler2D _DisplacementTexture;
            float4 _DisplacementTexture_ST;

            float _DisplacementX;
            float _DisplacementY;

            fixed4 _ColorTint;

            float _SpeedX;
            float _SpeedY;

            float _Amplitude;
            float _Frequency;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                
                o.vertex.y += sin(o.vertex.x * UNITY_PI * (1/_Frequency) + _Time.y) * _Amplitude;

                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv2 = TRANSFORM_TEX(v.uv2, _DisplacementTexture);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                i.uv2.x += _Time.y * _SpeedX;
                i.uv2.y += _Time.y * _SpeedY;
                
                half disVal = tex2D(_DisplacementTexture, i.uv2).g;
                i.uv.x += disVal * _DisplacementX - _DisplacementX * 0.5;
                i.uv.y += disVal * _DisplacementY - _DisplacementY * 0.5;

                i.uv.y = 1.0 - i.uv.y;
                
                fixed4 col = tex2D(_MainTex, i.uv) * _ColorTint;
                return col;
            }
            ENDCG
        }
    }
}
