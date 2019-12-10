// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/SpriteGradient" {
 Properties {
     [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
     _Tint ("Tint", Color) = (1,1,1,1)
     _Color ("Color Default", Color) = (1,1,1,1)
     _Color2 ("Color Secondary", Color) = (1,1,1,1)
	 _Vertical ("isVertical", int) = 0
	 _Horizontal ("isHorizontal", int) = 1
     
 // these six unused properties are required when a shader
 // is used in the UI system, or you get a warning.
 // look to UI-Default.shader to see these.
 _StencilComp ("Stencil Comparison", Float) = 8
 _Stencil ("Stencil ID", Float) = 0
 _StencilOp ("Stencil Operation", Float) = 0
 _StencilWriteMask ("Stencil Write Mask", Float) = 255
 _StencilReadMask ("Stencil Read Mask", Float) = 255
 _ColorMask ("Color Mask", Float) = 15
 
 }
  
 SubShader {
     Tags {
         "Queue"="Transparent"
         "IgnoreProjector"="True"
         "RenderType" = "Transparent" 
     }
     LOD 100
  

     Stencil
     {
         Ref [_Stencil]
         Comp [_StencilComp]
         Pass [_StencilOp]
         ReadMask [_StencilReadMask]
         WriteMask [_StencilWriteMask]
     }
     
     Cull Off
     ZWrite Off
     Blend SrcAlpha OneMinusSrcAlpha
  
     Pass {
         CGPROGRAM
         #pragma vertex vert  
         #pragma fragment frag
         #include "UnityCG.cginc"
  
         fixed4 _Tint;
         fixed4 _Color;
         fixed4 _Color2;
		 fixed _Vertical;
		 fixed _Horizontal;
  
         struct appdata_t
         {
             float4 position : POSITION;
             float4 color    : COLOR;
             float2 texcoord : TEXCOORD0;
         };

         struct v2f {
             float4 pos : SV_POSITION;
             fixed4 col : COLOR;
         };
  
         v2f vert (appdata_t vertex)
         {
             v2f o;
             o.pos = UnityObjectToClipPos (vertex.position);
             o.col = _Tint * vertex.color * lerp(_Color,_Color2, vertex.texcoord.y * _Vertical + vertex.texcoord.x * _Horizontal);
             return o;
         }
        
  
         float4 frag (v2f i) : COLOR {
             return i.col;
         }
             ENDCG
         }
     }
 }
