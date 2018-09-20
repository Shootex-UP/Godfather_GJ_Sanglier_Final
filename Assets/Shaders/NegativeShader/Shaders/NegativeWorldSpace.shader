// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/NegativeWorldSpace" 
{
    Properties 
    {
        _MainTex ("Tint Color (RGB)", 2D) = "white" {}
    }
 
    Category 
    {
 
        // We must be transparent, so other objects are drawn before this one.
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Opaque" }
 
 
        SubShader 
        {
     
            // Distortion
            GrabPass 
            {                        
                Tags { "LightMode" = "Always" }
            }
            Pass 
            {

            	Blend SrcAlpha OneMinusSrcAlpha

                Tags { "LightMode" = "Always" }
             
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma fragmentoption ARB_precision_hint_fastest
                #include "UnityCG.cginc"
             
                struct appdata_t 
                {
                    float4 vertex : POSITION;
                    float2 texcoord: TEXCOORD0;
                };
             
                struct v2f 
                {
                    float4 vertex : POSITION;
                    float4 uvgrab : TEXCOORD0;
                    float2 uvmain : TEXCOORD2;
                };
             
                float4 _MainTex_ST;
             
                v2f vert (appdata_t v) 
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    #if UNITY_UV_STARTS_AT_TOP
                    float scale = -1.0;
                    #else
                    float scale = 1.0;
                    #endif
                    o.uvgrab.xy = (float2(o.vertex.x, o.vertex.y*scale) + o.vertex.w) * 0.5;
                    o.uvgrab.zw = o.vertex.zw;
                    o.uvmain = TRANSFORM_TEX( v.texcoord, _MainTex );
                    return o;
                }
             
                sampler2D _GrabTexture;
                sampler2D _MainTex;

             
                half4 frag( v2f i ) : COLOR 
                {

                	fixed4 m = tex2D(_MainTex, i.uvmain);

                    fixed4 orgCol = tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(i.uvgrab));

                    fixed4 newCol = orgCol;

					newCol.r = 1.0 - orgCol.r;
					newCol.g = 1.0 - orgCol.g;
					newCol.b = 1.0 - orgCol.b;

					newCol.a = m.a;

                 
                    return newCol ;
                }
                ENDCG
            }
        }
    }
}