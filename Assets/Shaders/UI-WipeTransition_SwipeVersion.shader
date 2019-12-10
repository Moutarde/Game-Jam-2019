// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "JustDance/WipeTransition"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_FillSampler("Fill Texture", 2D) = "white" {}
		_ColorMask("Color Mask", Float) = 15

		_StencilComp("Stencil Comparison", Float) = 8
		_Stencil("Stencil ID", Float) = 0
		_StencilOp("Stencil Operation", Float) = 0
		_StencilWriteMask("Stencil Write Mask", Float) = 255
		_StencilReadMask("Stencil Read Mask", Float) = 255
	}

		SubShader
		{
			Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest[unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha

		ColorMask[_ColorMask]

		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

	struct appdata_t
	{
		float4 vertex: POSITION;
		float4 color    : COLOR;
		float2 texcoord: TEXCOORD0;
	};

	struct v2f
	{
		float4 vertex: SV_POSITION;
		half2 texcoord: TEXCOORD0;
		fixed4 color : COLOR;
		half2 progressUV: TEXCOORD1;
		float progress : TEXCOORD2;
	};

	v2f vert(appdata_t IN)
	{
		v2f OUT;
		OUT.vertex = UnityObjectToClipPos(IN.vertex);
		OUT.progress = abs(IN.texcoord.x) * 2.0 - 0.5;

		OUT.texcoord = IN.texcoord;
		OUT.texcoord.x *= _ScreenParams.x / _ScreenParams.y;
		OUT.texcoord = OUT.texcoord * 0.5 + 0.5;

		OUT.progressUV = step(0, IN.texcoord.x) * OUT.vertex * 0.5 + 0.5;
		OUT.progressUV.x *= _ScreenParams.x / _ScreenParams.y;

#ifdef UNITY_HALF_TEXEL_OFFSET
		OUT.vertex.xy += (_ScreenParams.zw - 1.0)*float2(-1,1);
#endif
		OUT.color = IN.color;
		return OUT;
	}

	sampler2D _FillSampler;

	fixed4 frag(v2f IN) : SV_Target
	{
		half3 fillColor = tex2D(_FillSampler, IN.texcoord) * IN.color.rgb;

		float gradient = -dot(IN.progressUV, half2(0.5, 0.5)) + IN.progress;
		float gradientAlpha = saturate(gradient * 2.5 + 0.5) * IN.color.a;

		return half4(fillColor, gradientAlpha);
	}
		ENDCG
	}
		}
}
