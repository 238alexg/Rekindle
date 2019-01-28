Shader "Rekindle/ScreenSpaceDarkness"
{
	Properties
	{
		_DarknessTexture ("Darkness Texture", 2D) = "black" {}
	}
	SubShader
	{
		Tags { "Queue"="Transparent" "RenderType"="Trasnsparent" }
		LOD 100

		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

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

			sampler2D _DarknessTexture;
			float4 _LightPositions[10];
			float _LightRadii[10];
			float4 _LightColors[10];
			float2 _TextureSize;
			float2 _UVTiling;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			fixed4 frag (v2f vertexData) : SV_Target
			{
				float4 color = tex2D(_DarknessTexture, vertexData.uv);
				color *= 0.1;
				color.a = 1;

				float4 lightColor = float4(0,0,0,0);
				int lightOverlaps = 0;

				for (int i = 0; i < 10; i++)
				{
					float radius = _LightRadii[i];
					float2 lightCenter = _LightPositions[i];

					if (radius == 0) continue;

					float xDistanceSquared = pow(vertexData.uv.x - lightCenter.x, 2);
					float yDistanceSquared = pow(vertexData.uv.y - lightCenter.y, 2);

					float totalDistanceSquared = xDistanceSquared + yDistanceSquared;
					float radiusSquared = pow(radius, 2);

					if (totalDistanceSquared < radiusSquared)
					{
						lightColor = (lightColor * lightOverlaps) + _LightColors[i];
						lightOverlaps += 1;
						lightColor /= lightOverlaps;
						lightColor.a -= _LightColors[i].a;
					}
					else
					{
						float modifier = radiusSquared / (totalDistanceSquared);
						float4 blendedDarkness = color * (1 - modifier) + _LightColors[i] * pow(modifier, 2);
						lightColor = (lightColor * lightOverlaps) + blendedDarkness;
						lightOverlaps += 1;
						lightColor /= lightOverlaps;
					}
				}

				color = lightColor;
				return color;
			}
			ENDCG
		}
	}
}
