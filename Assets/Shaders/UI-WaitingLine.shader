// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "JustDance/WaitingLine"
{
	Properties
	{
	}

	SubShader
	{
		Tags
		{
			"Queue"="Transparent"
			"IgnoreProjector"="True"
			"RenderType"="Transparent"
			"PreviewType"="Plane"
		}
		
		Cull Off
		Lighting Off
		ZWrite Off
		ZTest [unity_GUIZTestMode]
		Blend SrcAlpha One
		
		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex: POSITION;
				float4 color: COLOR;
				float2 texcoord: TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex: SV_POSITION;
				fixed4 color: COLOR;
				half3 texcoord: TEXCOORD0;
			};
			
			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.color = IN.color;

                float x = IN.texcoord.x;

                // equivalent to tan(time * 0.5) * 0.2
                float offset = _SinTime.z / _CosTime.z * 0.2;

                // make time loop for precision issues, the period matches the above function to hide
                // discontinuities when nothing is visible
                static const float PI = 3.14159265;
                float time = (_Time.y + PI) % (2.0 * PI);

                OUT.texcoord.x = x * 5.0 - time - offset;
                OUT.texcoord.y = x * 7.0 - time * 1.7 - offset;
                OUT.texcoord.z = x - offset;
				
#ifdef UNITY_HALF_TEXEL_OFFSET
				OUT.vertex.xy += (_ScreenParams.zw-1.0)*float2(-1,1);
#endif
				return OUT;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
                float texcoord2 = IN.texcoord.x + cos(IN.texcoord.x * 2.0);

                float alpha = step(0.3, frac(texcoord2));
                alpha = saturate(alpha + step(0.8, frac(IN.texcoord.y)));

                alpha *= step(0.0, IN.texcoord.z) * step(-1.0, -IN.texcoord.z);

				half4 color = half4(1.0, 1.0, 1.0, alpha) * IN.color;
				return color;
			}
		ENDCG
		}
	}
}
