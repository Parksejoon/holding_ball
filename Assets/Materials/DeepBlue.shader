Shader "Temp/DeepBlue"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Sampler2D", 2D) = "white" {}
	}
	
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Standard

		sampler2D _MainTex;
		fixed4 _Color;

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;

			o.Emission = c.rgb;
		}

		ENDCG
	}

	FallBack "Diffuse"
}
