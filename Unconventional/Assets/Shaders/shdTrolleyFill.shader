Shader "Custom/shdTrolleyFill"
{
	Properties
	{
		_Color ("Colour", Color) = (1.0, 1.0, 1.0, 1.0)
		_SpecColor ("Specular Colour", Color) = (1.0, 1.0, 1.0, 1.0)
		_Shininess ("Shininess", Float) = 1
		_Rim ("Rim", Float) = 1
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_BumpMap ("Bump Map", 2D) = "bump" {}
		_Frequency ("Frequency", Float) = 0
		_Amplitude ("Amplitude", Float) = 0
		_Fill ("Fill of Resources", Float) = 0
		//_Stagger ("Stagger", Float) = 100.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Staggered vertex:vert
		
		//half _Stagger;
		half4 _Color;
		sampler2D _MainTex;
		sampler2D _BumpMap;
		half _Shininess;
		half _Rim;
		half _Frequency;
		half _Amplitude;
		
		half4 LightingStaggered (SurfaceOutput s, half3 lightDir, half atten)
		{
			half NdotL = dot (s.Normal, lightDir);
            half4 c;
            c.rgb = s.Albedo * _LightColor0.rgb * dot (s.Normal, lightDir) * atten * 2 * 0.2f;
            
            c.a = s.Alpha;
            return c;
		}

		struct Input
		{
			float2 uv_MainTex;
			float2 uv_BumpMap;
			float3 viewDir;
		};
		
		void vert(inout appdata_full v)
		{
			if (v.vertex.y > 0.0)
				v.vertex.y += _Amplitude * sin(_Time.y * _Frequency + (v.vertex.z + v.vertex.x) * 2) - _Amplitude;
		}

		void surf (Input IN, inout SurfaceOutput o)
		{		
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb * _Color;
			o.Normal = UnpackNormal (tex2D(_BumpMap, IN.uv_BumpMap));
			
			// Pixel specular.
			o.Emission = max(0.001, pow(dot(normalize(IN.viewDir), o.Normal), _Shininess)) * _SpecColor;
			
			// Rim Shade
			o.Emission += (1 - dot(normalize(IN.viewDir), o.Normal)) * 0.2 * _Rim;
			
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
