Shader "Custom/UVPan"  {
Properties {
	_TintColor("Tint Color", Color) = (0.5,0.5,0.5,0.5)
    _MainTex("_MainTex", 2D) = "white" {}
    _panSpeed1("_panSpeed1", Float) = 6
    _panSpeed2("_panSpeed2", Float) = 8
    _textureOffset("_textureOffset", Vector) = (0.8,0.1,0.3,0.3)

    _GlowMask("_GlowMask", 2D) = "black" {}
    _glowMaskAdjust("_glowMaskAdjust", Range(0,1) ) = 1
    _GlowStrength("_GlowStrength", Float) = 5
}
	
Category {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }

    Blend SrcAlpha One
    AlphaTest Greater .01    
    Cull Off
    Lighting Off
    ZWrite Off
//    ZTest LEqual
    Fog { Color (0,0,0,0) }

	SubShader {
    	Pass {
//    	Blend SrcAlpha One
//    	AlphaTest Greater .01
    
      	CGPROGRAM
      	#pragma vertex vert
      	#pragma fragment frag
      	#pragma multi_compile_particles
      
      	#include "UnityCG.cginc"
    

      	#pragma target 3.0


      	sampler2D _MainTex;
      	float4 _TintColor;
      	float4 _MainTex_ST;
      	float _panSpeed1;
      	float _panSpeed2;
      	float4 _textureOffset;
      	sampler2D _GlowMask;
      	float4 _GlowMask_ST;
      	float _glowMaskAdjust;
      	float _GlowStrength;


      	struct appdata {
        	float4 vertex : POSITION;
        	float2 texcoord : TEXCOORD0;
        	float4 color : COLOR;
      	};

      	struct v2f{
        	float4 pos : SV_POSITION;
        	float2 uv1 : TEXCOORD0;
          float2 uv2 : TEXCOORD1;
        	float4 color : COLOR;
      	};

      	v2f vert (appdata v)
      	{
        	v2f o;
        	o.pos = mul( UNITY_MATRIX_MVP, v.vertex );
        	o.uv1 = TRANSFORM_TEX( v.texcoord, _MainTex );
          o.uv2 = TRANSFORM_TEX( v.texcoord, _GlowMask );
        	o.color = v.color;
        	return o;
      	}

      		half4 frag( v2f i ) : COLOR
      	{
        	half4 c = tex2D(_MainTex, i.uv1);

        	half2 offsetUV=_textureOffset.zw - (i.uv1.xy);
        	half ATan22=atan2(offsetUV.x, offsetUV.y);

        	half2 Assemble1=half2(sin(ATan22), cos(ATan22)) * offsetUV;


        	half2 Add5=(_textureOffset.zw + Assemble1);
        	float2 UV_Pan3=Add5 + (_Time.x * _panSpeed2);

        	half4 Tex2D4=tex2D(_MainTex,frac(UV_Pan3.xy));


        	half2 offsetUV2=_textureOffset.xy - i.uv1;
        	half ATan21=atan2(offsetUV2.x, offsetUV2.y);
        	half2 Multiply7=float2(sin(ATan21), cos(ATan21)) * offsetUV2;
        	half2 Add3=(_textureOffset.xy + Multiply7);
        	float Multiply8=(_Time.x * _panSpeed1);
        	float2 UV_Pan2=Add3 + Multiply8;

        	half4 Tex2D0=tex2D(_MainTex, frac(UV_Pan2));

        	half4 Add6=Tex2D4 + Tex2D0;
        	half4 Multiply13=_TintColor * _GlowStrength;
        	half4 Multiply14=Add6 * Multiply13;
        	half4 Tex2D1=tex2D(_GlowMask, i.uv2.xy);
        	half4 Multiply1=Tex2D1 * _glowMaskAdjust;
        	half4 Multiply11=Multiply14 * Multiply1;
          
        	return Multiply11 * i.color;
      	}
      ENDCG
	  }
	}
}
}
