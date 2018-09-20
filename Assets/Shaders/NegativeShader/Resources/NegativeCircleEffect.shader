// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/NegativeCircleEffect"
{
	Properties {
		_MainTex ("", 2D) = "white" {}
		_CenterX ("CenterX", Range(-1,2)) = 0.5
		_CenterY ("CenterY", Range(-1,2)) = 0.5
		_Radius1 ("Radius1", Range(-0.20,1)) = -0.2
		_Radius2 ("Radius2", Range(-0.20,1)) = -0.2
		_WaveSize ("WaveSize", Range(0.0,1)) = 0.0
		_ScreenRatio ("ScreenRatio", Float) = 1 //Width/Height

		_UseWaveSize ("UseWaveSize", Float) = 0
	}
 
	SubShader 
	{
	 
		ZTest Always Cull Off ZWrite Off Fog { Mode Off } //Rendering settings
	 
	 	Pass{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			float _CenterX;
			float _CenterY;
			float _Radius1;
			float _Radius2;
			float _ScreenRatio;
			float _WaveSize;
			float _UseWaveSize;

			struct v2f {
				float4 pos : POSITION;
				half2 uv : TEXCOORD0;
			};
	   
			//Our Vertex Shader
			v2f vert (appdata_img v){
				v2f o;
				o.pos = UnityObjectToClipPos (v.vertex);
				o.uv = MultiplyUV (UNITY_MATRIX_TEXTURE0, v.texcoord.xy);
				return o;
			}

			sampler2D _MainTex; //Reference in Pass is necessary to let us use this variable in shaders

			//Our Fragment Shader
			fixed4 frag (v2f i) : COLOR
			{

//				float2 diff=float2(i.uv.x-_CenterX,i.uv.y-_CenterY);
				float2 diff=float2(i.uv.x-_CenterX, (i.uv.y-_CenterY) * _ScreenRatio); 

				float dist=sqrt(diff.x*diff.x+diff.y*diff.y);
				float2 uv_displaced = float2(i.uv.x,i.uv.y);

				fixed4 orgCol = tex2D(_MainTex,uv_displaced);

				if (_UseWaveSize == 0)
				{
					if (dist < _Radius1
					) 
					{
						if (dist > _Radius2)
						{
							orgCol.r = 1.0 - orgCol.r;
							orgCol.g = 1.0 - orgCol.g;
							orgCol.b = 1.0 - orgCol.b;

							orgCol.a = orgCol.a;
						}
					}
				}
				else
				{
					if (dist < _Radius1
					) 
					{
						if (dist > _Radius1 - _WaveSize)
						{
							orgCol.r = 1.0 - orgCol.r;
							orgCol.g = 1.0 - orgCol.g;
							orgCol.b = 1.0 - orgCol.b;

							orgCol.a = orgCol.a;
						}
					}
				}




				return orgCol;
			}
			ENDCG
		}
	}
	
	FallBack "Diffuse"
}