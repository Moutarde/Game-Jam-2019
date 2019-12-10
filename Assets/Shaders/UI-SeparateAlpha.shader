// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "UI/SeparateAlpha"
{
	Properties
	{
		_MainTex ("RGB Texture", 2D) = "white" {}
		_AlphaTex ("Alpha", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		
		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255

		_ColorMask ("Color Mask", Float) = 15
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}
		
		Stencil
		{
			Ref [_Stencil]
			Comp [_StencilComp]
			Pass [_StencilOp] 
			ReadMask [_StencilReadMask]
			WriteMask [_StencilWriteMask]
		}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest [unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask [_ColorMask]

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				half2 texcoord  : TEXCOORD0;
				half2 texcoordAlpha : TEXCOORD1;
			};
			
			fixed4 _Color;

			sampler2D _MainTex;
			uniform float4 _MainTex_ST;

            sampler2D _AlphaTex;
			uniform float4 _AlphaTex_ST;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = TRANSFORM_TEX(IN.texcoord, _MainTex);
				OUT.texcoordAlpha = TRANSFORM_TEX(IN.texcoord, _AlphaTex);
#ifdef UNITY_HALF_TEXEL_OFFSET
				OUT.vertex.xy += (_ScreenParams.zw-1.0)*float2(-1,1);
#endif
				OUT.color = IN.color * _Color;
				return OUT;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				// discard fragments with UVs outside [0-1]
				clip(IN.texcoord.x);
				clip(IN.texcoord.y);
				clip(1.0 - IN.texcoord.x);
				clip(1.0 - IN.texcoord.y);
				
				half4 col = tex2D(_MainTex, IN.texcoord) * IN.color;
                half4 col2 = tex2D(_AlphaTex, IN.texcoordAlpha);
                
				half4 finalcol = fixed4(col.r, col.g, col.b, col2.g * col.a);
				
				clip (finalcol.a - 0.01);
				return finalcol;
			}
		ENDCG
		}
	}
}
