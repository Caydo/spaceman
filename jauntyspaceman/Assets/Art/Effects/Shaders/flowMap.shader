// Shader created with Shader Forge Beta 0.36 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.36;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:False,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:False,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:2,dpts:2,wrdp:False,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:True;n:type:ShaderForge.SFN_Final,id:1,x:32365,y:32706|diff-6-OUT,emission-908-OUT,alpha-455-OUT;n:type:ShaderForge.SFN_Color,id:2,x:33069,y:32385,ptlb:tint,ptin:_tint,glob:False,c1:0.8897059,c2:0.8897059,c3:0.8897059,c4:1;n:type:ShaderForge.SFN_Multiply,id:3,x:32819,y:32385|A-2-RGB,B-4-RGB;n:type:ShaderForge.SFN_VertexColor,id:4,x:33113,y:32543;n:type:ShaderForge.SFN_Multiply,id:6,x:32856,y:32684|A-3-OUT,B-7-OUT;n:type:ShaderForge.SFN_Lerp,id:7,x:33069,y:32748|A-1003-OUT,B-1001-OUT,T-252-OUT;n:type:ShaderForge.SFN_Tex2d,id:8,x:33506,y:32715,tex:a32cff31bddf3bb4e926d003041c088b,ntxv:0,isnm:False|UVIN-233-OUT,TEX-180-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:180,x:33882,y:32696,ptlb:Main Texture,ptin:_MainTexture,glob:False,tex:a32cff31bddf3bb4e926d003041c088b;n:type:ShaderForge.SFN_Tex2d,id:182,x:33506,y:32588,tex:a32cff31bddf3bb4e926d003041c088b,ntxv:0,isnm:False|UVIN-231-OUT,TEX-180-TEX;n:type:ShaderForge.SFN_Time,id:204,x:35389,y:33461;n:type:ShaderForge.SFN_ValueProperty,id:205,x:35033,y:33513,ptlb:Flow Speed,ptin:_FlowSpeed,glob:False,v1:1;n:type:ShaderForge.SFN_Vector1,id:206,x:35033,y:33674,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:208,x:34825,y:33469|A-382-TSL,B-205-OUT;n:type:ShaderForge.SFN_Add,id:209,x:34813,y:33656|A-208-OUT,B-206-OUT;n:type:ShaderForge.SFN_Frac,id:210,x:34591,y:33469|IN-208-OUT;n:type:ShaderForge.SFN_Frac,id:212,x:34591,y:33630|IN-209-OUT;n:type:ShaderForge.SFN_Multiply,id:214,x:34237,y:33308|A-226-OUT,B-212-OUT;n:type:ShaderForge.SFN_Tex2d,id:216,x:35288,y:33086,ptlb:Flow Map,ptin:_FlowMap,tex:80efbe9d834398d4bb8193aecd313d2a,ntxv:0,isnm:False;n:type:ShaderForge.SFN_ComponentMask,id:217,x:35017,y:33084,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-216-RGB;n:type:ShaderForge.SFN_ValueProperty,id:219,x:35033,y:33265,ptlb:Flow Power,ptin:_FlowPower,glob:False,v1:0.5;n:type:ShaderForge.SFN_Vector1,id:221,x:35033,y:33324,v1:-1;n:type:ShaderForge.SFN_Multiply,id:223,x:34825,y:33265|A-219-OUT,B-221-OUT;n:type:ShaderForge.SFN_RemapRange,id:224,x:34825,y:33084,frmn:0,frmx:1,tomn:-0.5,tomx:0.5|IN-217-OUT;n:type:ShaderForge.SFN_Multiply,id:226,x:34549,y:33134|A-224-OUT,B-223-OUT;n:type:ShaderForge.SFN_Multiply,id:228,x:34237,y:33133|A-226-OUT,B-210-OUT;n:type:ShaderForge.SFN_TexCoord,id:229,x:35350,y:32602,uv:0;n:type:ShaderForge.SFN_Add,id:231,x:33760,y:32504|A-2402-OUT,B-228-OUT;n:type:ShaderForge.SFN_Add,id:233,x:33760,y:32877|A-2402-OUT,B-214-OUT;n:type:ShaderForge.SFN_Subtract,id:248,x:34237,y:33449|A-250-OUT,B-210-OUT;n:type:ShaderForge.SFN_Vector1,id:250,x:34237,y:33621,v1:0.5;n:type:ShaderForge.SFN_Divide,id:251,x:33760,y:33038|A-248-OUT,B-250-OUT;n:type:ShaderForge.SFN_Abs,id:252,x:33566,y:33038|IN-251-OUT;n:type:ShaderForge.SFN_Lerp,id:259,x:33175,y:33036|A-182-A,B-8-A,T-252-OUT;n:type:ShaderForge.SFN_Multiply,id:364,x:32856,y:32945|A-448-OUT,B-259-OUT;n:type:ShaderForge.SFN_Panner,id:380,x:34825,y:32840,spu:0,spv:1|UVIN-2221-OUT,DIST-386-OUT;n:type:ShaderForge.SFN_Time,id:382,x:35497,y:32950;n:type:ShaderForge.SFN_ValueProperty,id:384,x:35239,y:32958,ptlb:Pan Speed - Vertical,ptin:_PanSpeedVertical,glob:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:386,x:35030,y:32840|A-382-TSL,B-384-OUT;n:type:ShaderForge.SFN_Multiply,id:448,x:32856,y:32807|A-2-A,B-4-A;n:type:ShaderForge.SFN_Tex2d,id:449,x:33521,y:33244,ptlb:mask,ptin:_mask,ntxv:0,isnm:False|UVIN-1069-UVOUT;n:type:ShaderForge.SFN_Multiply,id:455,x:32796,y:33342|A-364-OUT,B-457-OUT,C-449-A;n:type:ShaderForge.SFN_Desaturate,id:457,x:33175,y:33240|COL-449-RGB;n:type:ShaderForge.SFN_ValueProperty,id:866,x:32682,y:32979,ptlb:Glow Strength,ptin:_GlowStrength,glob:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:908,x:32612,y:32807|A-866-OUT,B-6-OUT;n:type:ShaderForge.SFN_ToggleProperty,id:1000,x:33294,y:32385,ptlb:Desaturate,ptin:_Desaturate,on:False;n:type:ShaderForge.SFN_Desaturate,id:1001,x:33294,y:32633|COL-8-RGB,DES-1000-OUT;n:type:ShaderForge.SFN_Desaturate,id:1003,x:33294,y:32453|COL-182-RGB,DES-1000-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1059,x:34781,y:34185,ptlb:mask pan speed,ptin:_maskpanspeed,glob:False,v1:1;n:type:ShaderForge.SFN_TexCoord,id:1067,x:34454,y:33793,uv:0;n:type:ShaderForge.SFN_Panner,id:1069,x:34244,y:33859,spu:0,spv:1|UVIN-1067-UVOUT,DIST-1073-OUT;n:type:ShaderForge.SFN_Time,id:1071,x:34781,y:33951;n:type:ShaderForge.SFN_Multiply,id:1073,x:34454,y:33955|A-382-TSL,B-1059-OUT;n:type:ShaderForge.SFN_TexCoord,id:1172,x:35028,y:32234,uv:0;n:type:ShaderForge.SFN_Panner,id:1174,x:34822,y:32383,spu:1,spv:0|UVIN-2223-OUT,DIST-1180-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1178,x:35243,y:32383,ptlb:Pan Speed - Horizontal,ptin:_PanSpeedHorizontal,glob:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:1180,x:35028,y:32383|A-1178-OUT,B-382-TSL;n:type:ShaderForge.SFN_Add,id:1857,x:33826,y:32026|A-1174-UVOUT,B-228-OUT;n:type:ShaderForge.SFN_Add,id:1859,x:33826,y:32174|A-1174-UVOUT,B-214-OUT;n:type:ShaderForge.SFN_Vector2,id:1880,x:35350,y:32753,v1:0,v2:1;n:type:ShaderForge.SFN_Vector2,id:1888,x:35350,y:32505,v1:1,v2:0;n:type:ShaderForge.SFN_Multiply,id:2221,x:35094,y:32688|A-229-V,B-1880-OUT;n:type:ShaderForge.SFN_Multiply,id:2223,x:35094,y:32551|A-229-U,B-1888-OUT;n:type:ShaderForge.SFN_Add,id:2402,x:34822,y:32602|A-1174-UVOUT,B-380-UVOUT;proporder:2-180-1000-216-219-205-1178-384-1059-449-866;pass:END;sub:END;*/

Shader "Custom/flowMap" {
    Properties {
        _tint ("tint", Color) = (0.8897059,0.8897059,0.8897059,1)
        _MainTexture ("Main Texture", 2D) = "white" {}
        [MaterialToggle] _Desaturate ("Desaturate", Float ) = 0
        _FlowMap ("Flow Map", 2D) = "white" {}
        _FlowPower ("Flow Power", Float ) = 0.5
        _FlowSpeed ("Flow Speed", Float ) = 1
        _PanSpeedHorizontal ("Pan Speed - Horizontal", Float ) = 1
        _PanSpeedVertical ("Pan Speed - Vertical", Float ) = 1
        _maskpanspeed ("mask pan speed", Float ) = 1
        _mask ("mask", 2D) = "white" {}
        _GlowStrength ("Glow Strength", Float ) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 2.0
            uniform float4 _TimeEditor;
            uniform float4 _tint;
            uniform sampler2D _MainTexture; uniform float4 _MainTexture_ST;
            uniform float _FlowSpeed;
            uniform sampler2D _FlowMap; uniform float4 _FlowMap_ST;
            uniform float _FlowPower;
            uniform float _PanSpeedVertical;
            uniform sampler2D _mask; uniform float4 _mask_ST;
            uniform float _GlowStrength;
            uniform fixed _Desaturate;
            uniform float _maskpanspeed;
            uniform float _PanSpeedHorizontal;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_4 = i.vertexColor;
                float4 node_382 = _Time + _TimeEditor;
                float2 node_229 = i.uv0;
                float2 node_2223 = (node_229.r*float2(1,0));
                float2 node_1174 = (node_2223+(_PanSpeedHorizontal*node_382.r)*float2(1,0));
                float2 node_2221 = (node_229.g*float2(0,1));
                float2 node_380 = (node_2221+(node_382.r*_PanSpeedVertical)*float2(0,1));
                float2 node_2402 = (node_1174+node_380);
                float2 node_2483 = i.uv0;
                float2 node_226 = ((tex2D(_FlowMap,TRANSFORM_TEX(node_2483.rg, _FlowMap)).rgb.rg*1.0+-0.5)*(_FlowPower*(-1.0)));
                float node_208 = (node_382.r*_FlowSpeed);
                float node_210 = frac(node_208);
                float2 node_228 = (node_226*node_210);
                float2 node_231 = (node_2402+node_228);
                float4 node_182 = tex2D(_MainTexture,TRANSFORM_TEX(node_231, _MainTexture));
                float2 node_214 = (node_226*frac((node_208+0.5)));
                float2 node_233 = (node_2402+node_214);
                float4 node_8 = tex2D(_MainTexture,TRANSFORM_TEX(node_233, _MainTexture));
                float node_250 = 0.5;
                float node_252 = abs(((node_250-node_210)/node_250));
                float3 node_6 = ((_tint.rgb*node_4.rgb)*lerp(lerp(node_182.rgb,dot(node_182.rgb,float3(0.3,0.59,0.11)),_Desaturate),lerp(node_8.rgb,dot(node_8.rgb,float3(0.3,0.59,0.11)),_Desaturate),node_252));
                float3 emissive = (_GlowStrength*node_6);
                float3 finalColor = emissive;
                float2 node_1069 = (i.uv0.rg+(node_382.r*_maskpanspeed)*float2(0,1));
                float4 node_449 = tex2D(_mask,TRANSFORM_TEX(node_1069, _mask));
/// Final Color:
                return fixed4(finalColor,(((_tint.a*node_4.a)*lerp(node_182.a,node_8.a,node_252))*dot(node_449.rgb,float3(0.3,0.59,0.11))*node_449.a));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
