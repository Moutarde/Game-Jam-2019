// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "JustDance/WipeTransition"
{
	Properties
	{
        _FillSampler ("Fill Texture", 2D) = "white" {}
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
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex: POSITION;
				float2 texcoord: TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex: SV_POSITION;
				half2 uv: TEXCOORD0;
                half2 progressUV: TEXCOORD1;
                float progress: TEXCOORD2;
			};
			
			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.progress = abs(IN.texcoord.x) * 2.0 - 0.5;
				
                OUT.uv = OUT.vertex;
                OUT.uv.x *= _ScreenParams.x / _ScreenParams.y;
                OUT.uv = OUT.uv * 0.5 + 0.5;

                OUT.progressUV = sign(IN.texcoord.x) * OUT.vertex * 0.5 + 0.5;
				OUT.progressUV.x *= _ScreenParams.x / _ScreenParams.y;

#ifdef UNITY_HALF_TEXEL_OFFSET
				OUT.vertex.xy += (_ScreenParams.zw-1.0)*float2(-1,1);
#endif
				return OUT;
			}

            sampler2D _FillSampler;

			fixed4 frag(v2f IN) : SV_Target
			{
                half3 fillColor = tex2D(_FillSampler, IN.uv);

                float gradient = -dot(IN.progressUV, half2(0.5, 0.5)) + IN.progress;
				float gradientAlpha = saturate(gradient * 2.5 + 0.5);
				
				return half4(fillColor, gradientAlpha);
			}
		ENDCG
		}
	}
}
