Shader "Unlit/VertexMovement"
{
    Properties
    {
        //_VerticalOffset("Vertical Offset", Float) = 0
        _SpeedOfVerticalMovement("Speed of Vertical Movement", Float) = 1
        _SpeedOfBlend("Speed of Blend", Float) = 1
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Testure2D", 2D) = "white" {}
        _SecTex("Secondary Texture2D", 2D) = "white" {}
        //_Blend("Blend", Range(0,1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        
        Blend SrcAlpha OneMinusSrcAlpha
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"
            
            //float _VerticalOffset;
            
            half4 _Color;
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            sampler2D _SecTex;
            float4 _SecTex_ST;
            
            //float _Blend;

            float _SpeedOfVerticalMovement;
            float _SpeedOfBlend;

            
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
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.vertex.y += sin(_Time.y * _SpeedOfVerticalMovement);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = lerp(tex2D(_MainTex, i.uv), tex2D(_SecTex, i.uv), abs(sin(_Time.y * _SpeedOfBlend)));
                col *= _Color;
                return col;
            }
            ENDCG
        }
    }
}
