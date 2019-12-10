// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "UI/SpinningGradient"
{
	Properties
	{
		_MainTex ("RGB Texture", 2D) = "white" {}
        
		_Tint("Tint", Color) = (1,1,1,1)
		_BackgroundColor ("Background Color", Color) = (1,1,1,1)
		_FlashColor ("Flash Color", Color) = (1,1,1,0)
		_Speed ("Speed", Float) = 1
		_Spread ("Spread", Float) = 1
		
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
                half2 target    : TEXCOORD1;
			};
			
			sampler2D _MainTex;
			uniform float4 _MainTex_ST;
            
			fixed4 _Tint;
			fixed4 _BackgroundColor;
			fixed4 _FlashColor;
			float _Speed;
			float _Spread;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = TRANSFORM_TEX(IN.texcoord, _MainTex);
#ifdef UNITY_HALF_TEXEL_OFFSET
				OUT.vertex.xy += (_ScreenParams.zw-1.0)*float2(-1,1);
#endif
				OUT.color = _Tint * IN.color;
                
                float targetAngle = _Time.y * _Speed;
                OUT.target = float2(cos(targetAngle), sin(targetAngle));
                
				return OUT;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				half4 textureColor = tex2D(_MainTex, IN.texcoord);
                clip(textureColor.a - 0.001);
                
                float intensity = pow(saturate(dot(IN.target, normalize(IN.texcoord * 2.0 - 1.0))), _Spread);
				fixed4 color = textureColor * IN.color * lerp(_BackgroundColor, _FlashColor, intensity);

				return color;
			}
		ENDCG
		}
	}
}
