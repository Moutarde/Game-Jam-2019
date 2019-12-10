Shader "UI/ShinyAvatar"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Speed("Speed", Float) = 0.2
		_Period("Period", Float) = 10
		_MainWidth("Main Width", Float) = 0.2
		_SecondaryWidth("Secondary Width", Float) = 0.15
		_SecondaryOffset("Secondary Offset", Float) = 0.25
		_Angle("Angle", Float) = 0.79 // ~  pi / 4
		_SkewFactor("Skew Factor", Float) = 1.0

		_ShineColor("Shine Color", Color) = (1, 1, 1, 0.063)

		_StencilComp("Stencil Comparison", Float) = 8
		_Stencil("Stencil ID", Float) = 0
		_StencilOp("Stencil Operation", Float) = 0
		_StencilWriteMask("Stencil Write Mask", Float) = 255
		_StencilReadMask("Stencil Read Mask", Float) = 255

		_ColorMask("Color Mask", Float) = 15
	}
    SubShader
    {
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}

		Stencil
		{
			Ref[_Stencil]
			Comp[_StencilComp]
			Pass[_StencilOp]
			ReadMask[_StencilReadMask]
			WriteMask[_StencilWriteMask]
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

            struct appdata
            {
                float4 position : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 position : SV_POSITION;
				float2 uv : TEXCOORD0;
				float2 direction : TEXCOORD1;
				float timeOffset : TEXCOORD2;
			};

            sampler2D _MainTex;
            float4 _MainTex_ST;

			float _Speed;
			float _Period;
			float _MainWidth;
			float _SecondaryWidth;
			float _SecondaryOffset;
			float _Angle;
			float _SkewFactor;

			fixed4 _ShineColor;

            v2f vert (appdata v)
            {
				float2 tangent = float2(-sin(_Angle), cos(_Angle));
				float skew = dot(tangent, v.uv);
				float skewedAngle = _Angle + skew * _SkewFactor;

				v2f o;
                o.position = UnityObjectToClipPos(v.position);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.direction = float2(cos(skewedAngle), sin(skewedAngle));
				o.timeOffset = (_Time.y % _Period) * _Speed;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
				fixed4 color = tex2D(_MainTex, i.uv);

				float distance = dot(normalize(i.direction), i.uv);

				float mainStripe = saturate(1.0 - abs((distance - i.timeOffset)) / _MainWidth);
				float secondaryStripe = saturate(1.0 - abs((distance - i.timeOffset - _SecondaryOffset)) / _SecondaryWidth);

				mainStripe = pow(mainStripe, 2.2);
				secondaryStripe = pow(secondaryStripe, 2.2);

				color.rgb += (mainStripe + secondaryStripe) * _ShineColor.rgb * _ShineColor.a;

                return saturate(color);
            }
            ENDCG
        }
    }
}
