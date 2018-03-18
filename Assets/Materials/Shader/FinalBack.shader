Shader "Custom/FinalBack"
{
	Properties
	{
		_MainTex("MainTexture", 2D) = "white" {}
		_TopColor("TopColor", Color) = (0, 0, 0, 1)
		_BotColor("BotColor", Color) = (1, 1, 1, 1)
	}

	SubShader
	{
		Tags { "RenderType" = "Opaque" }

		CGPROGRAM
		#pragma surface surf Nolight noambient

		sampler2D _MainTex;
		float4 _TopColor;
		float4 _BotColor;

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutput o)
		{
			half uv_y = ((int)(IN.uv_MainTex.y * 100 + 0.5f));
			
			uv_y /= 100;

			o.Emission = (uv_y * _BotColor) + ((1 - uv_y) * _TopColor);
		}

		half4 LightingNolight(SurfaceOutput s, half3 lightDir, half atten)
		{
			return half4(0, 0, 0, 1);
		}

		ENDCG
	}

	FallBack "Diffuse"
}
