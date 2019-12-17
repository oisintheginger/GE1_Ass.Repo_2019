Shader "Unlit/TestShader"
{
	//I have little to no experience with shaderLab, so the hologram shader used is not of my own design
	//This script is from a tutorial found here: https://www.youtube.com/watch?v=vlYGmVC_Qzg
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Color("Primary Colour", Color) = (1,0,1,1)
		_Float("Offset", Float) = 1
		_Bias("Bias", Float) = 0
		_ScanningFrequency("Scanning Frequency", Float) = 100
		_ScanningSpeed("Scanning Speed", Float) = 100


    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType" = "Transparent" }
        LOD 100
		ZWrite Off
		Blend SrcAlpha One
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
				float4 objVertex :TEXTCOORD1;
            };


            sampler2D _MainTex;
			fixed4 _Color;
			float _Float;
            float4 _MainTex_ST;
			float _Bias;
			float _ScanningFrequency;
			float _ScanningSpeed;


            v2f vert (appdata v)
            {
                v2f o;
				o.objVertex = mul(unity_ObjectToWorld, v.vertex);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                UNITY_APPLY_FOG(i.fogCoord, col);
				col = _Color * max(0, sin(i.objVertex.y * _ScanningFrequency + _Time.x * _ScanningSpeed) + _Bias);
                return col;
            }
            ENDCG
        }
    }
}
