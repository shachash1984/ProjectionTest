﻿Shader "Custom/ProjectionShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Xpos("X", float) = 0.0
		_Ypos("Y", float) = 0.0
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

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

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			float _Xpos;
			float _Ypos;

			fixed4 frag (v2f i) : SV_Target
			{
				i.uv.x += _Xpos;
				i.uv.y += _Ypos;
				fixed4 col = tex2D(_MainTex, i.uv);
				// just invert the colors
				//col.rgb = 1 - col.rgb;
				return col;
			}
			ENDCG
		}
	}
}
