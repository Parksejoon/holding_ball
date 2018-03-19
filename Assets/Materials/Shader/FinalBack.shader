Shader "Custom/FinalBack"
{
	Properties
	{
		_MainTex("MainTexture", 2D) = "white" {}
		_Partition("Partition", Int) = 30
		_TopColor("TopColor", Color) = (0, 0, 0, 1)
		_BotColor("BotColor", Color) = (1, 1, 1, 1)
	}

	SubShader
	{
		Tags { "RenderType" = "Opaque" }

		CGPROGRAM
		#pragma surface surf Nolight noambient

		sampler2D	_MainTex;				// 가짜 텍스쳐
		float4		_TopColor;				// 위쪽 색
		float4		_BotColor;				// 아래쪽 색
		int			_Partition;				// 몇분할 할지

		// 입력 구조체
		struct Input
		{
			float2 uv_MainTex;
		};

		// 표면
		void surf(Input IN, inout SurfaceOutput o)
		{
			half uv_y = (int)(IN.uv_MainTex.y * _Partition + (0.5f * (_Partition / 10)));
			
			uv_y /= _Partition;

			o.Emission = (uv_y * _BotColor) + ((1 - uv_y) * _TopColor);
		}

		// Nolight
		half4 LightingNolight(SurfaceOutput s, half3 lightDir, half atten)
		{
			return half4(0, 0, 0, 1);
		}

		ENDCG
	}

	FallBack "Diffuse"
}
