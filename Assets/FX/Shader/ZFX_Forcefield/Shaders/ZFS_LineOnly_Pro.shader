// Upgrade NOTE: commented out 'float4 unity_DynamicLightmapST', a built-in variable
// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable

// Shader created with Shader Forge v1.04 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.04;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:2,bsrc:0,bdst:0,culm:2,dpts:2,wrdp:False,dith:2,ufog:False,aust:False,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:35325,y:32712,varname:node_1,prsc:2|emission-2476-OUT;n:type:ShaderForge.SFN_DepthBlend,id:5,x:32949,y:32709,varname:node_5,prsc:2|DIST-1022-OUT;n:type:ShaderForge.SFN_OneMinus,id:884,x:33115,y:32709,varname:node_884,prsc:2|IN-5-OUT;n:type:ShaderForge.SFN_Multiply,id:890,x:34693,y:32726,varname:node_890,prsc:2|A-1876-RGB,B-1665-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1022,x:32784,y:32709,ptovrint:False,ptlb:Edge_Detection_Distance,ptin:_Edge_Detection_Distance,varname:node_8380,prsc:2,glob:False,v1:3;n:type:ShaderForge.SFN_Append,id:1590,x:33296,y:32768,varname:node_1590,prsc:2|A-884-OUT,B-1618-V;n:type:ShaderForge.SFN_TexCoord,id:1618,x:33115,y:32829,varname:node_1618,prsc:2,uv:0;n:type:ShaderForge.SFN_Tex2d,id:1620,x:33473,y:32768,ptovrint:False,ptlb:Gradient_Edge_Detection,ptin:_Gradient_Edge_Detection,varname:node_924,prsc:2,ntxv:0,isnm:False|UVIN-1590-OUT;n:type:ShaderForge.SFN_TexCoord,id:1652,x:34337,y:32948,varname:node_1652,prsc:2,uv:0;n:type:ShaderForge.SFN_Append,id:1654,x:34518,y:32888,varname:node_1654,prsc:2|A-1665-OUT,B-1652-V;n:type:ShaderForge.SFN_Tex2d,id:1656,x:34699,y:32888,ptovrint:False,ptlb:Gradient_Color,ptin:_Gradient_Color,varname:node_1031,prsc:2,ntxv:0,isnm:False|UVIN-1654-OUT;n:type:ShaderForge.SFN_Clamp,id:1665,x:34323,y:32784,varname:node_1665,prsc:2|IN-2450-OUT,MIN-2647-OUT,MAX-2648-OUT;n:type:ShaderForge.SFN_Vector1,id:1666,x:34088,y:32914,varname:node_1666,prsc:2,v1:0.9;n:type:ShaderForge.SFN_Vector1,id:1667,x:34088,y:32860,varname:node_1667,prsc:2,v1:0;n:type:ShaderForge.SFN_SwitchProperty,id:1771,x:33658,y:32768,ptovrint:False,ptlb:Edge_Detection,ptin:_Edge_Detection,varname:node_8377,prsc:2,on:True|A-1772-OUT,B-1620-R;n:type:ShaderForge.SFN_Vector1,id:1772,x:33463,y:32689,varname:node_1772,prsc:2,v1:0;n:type:ShaderForge.SFN_SwitchProperty,id:1858,x:34879,y:32811,ptovrint:False,ptlb:Gradient_Or_Solid_Color,ptin:_Gradient_Or_Solid_Color,varname:node_256,prsc:2,on:True|A-890-OUT,B-1656-RGB;n:type:ShaderForge.SFN_Color,id:1876,x:34486,y:32660,ptovrint:False,ptlb:Solid_Color,ptin:_Solid_Color,varname:node_7186,prsc:2,glob:False,c1:0.1764706,c2:0.5229208,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:2450,x:34088,y:32716,varname:node_2450,prsc:2|A-1771-OUT,B-2452-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2452,x:33861,y:32833,ptovrint:False,ptlb:Intensity,ptin:_Intensity,varname:node_8382,prsc:2,glob:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:2476,x:35065,y:32811,varname:node_2476,prsc:2|A-1858-OUT,B-2477-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2477,x:34879,y:32955,ptovrint:False,ptlb:Brightness,ptin:_Brightness,varname:node_5820,prsc:2,glob:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:2647,x:34067,y:33034,ptovrint:False,ptlb:Value_Clamp_Minimum,ptin:_Value_Clamp_Minimum,varname:node_6324,prsc:2,glob:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:2648,x:34067,y:33110,ptovrint:False,ptlb:Value_Clamp_Maximum,ptin:_Value_Clamp_Maximum,varname:node_1686,prsc:2,glob:False,v1:0.95;proporder:2477-2452-1858-1656-1876-1771-1022-1620-2647-2648;pass:END;sub:END;*/

Shader "ZFS Shaders/ZFS_LineOnly_Pro" {
    Properties {
        _Brightness ("Brightness", Float ) = 1
        _Intensity ("Intensity", Float ) = 1
        [MaterialToggle] _Gradient_Or_Solid_Color ("Gradient_Or_Solid_Color", Float ) = 1
        _Gradient_Color ("Gradient_Color", 2D) = "white" {}
        _Solid_Color ("Solid_Color", Color) = (0.1764706,0.5229208,1,1)
        [MaterialToggle] _Edge_Detection ("Edge_Detection", Float ) = 1
        _Edge_Detection_Distance ("Edge_Detection_Distance", Float ) = 3
        _Gradient_Edge_Detection ("Gradient_Edge_Detection", 2D) = "white" {}
        _Value_Clamp_Minimum ("Value_Clamp_Minimum", Float ) = 0
        _Value_Clamp_Maximum ("Value_Clamp_Maximum", Float ) = 0.95
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
            Blend One One
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform sampler2D _CameraDepthTexture;
            // float4 unity_LightmapST;
            #ifdef DYNAMICLIGHTMAP_ON
                // float4 unity_DynamicLightmapST;
            #endif
            uniform float _Edge_Detection_Distance;
            uniform sampler2D _Gradient_Edge_Detection; uniform float4 _Gradient_Edge_Detection_ST;
            uniform sampler2D _Gradient_Color; uniform float4 _Gradient_Color_ST;
            uniform fixed _Edge_Detection;
            uniform fixed _Gradient_Or_Solid_Color;
            uniform float4 _Solid_Color;
            uniform float _Intensity;
            uniform float _Brightness;
            uniform float _Value_Clamp_Minimum;
            uniform float _Value_Clamp_Maximum;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 projPos : TEXCOORD1;
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD2;
                #else
                    float3 shLight : TEXCOORD2;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
/////// Vectors:
////// Lighting:
////// Emissive:
                float2 node_1590 = float2((1.0 - saturate((sceneZ-partZ)/_Edge_Detection_Distance)),i.uv0.g);
                float4 _Gradient_Edge_Detection_var = tex2D(_Gradient_Edge_Detection,TRANSFORM_TEX(node_1590, _Gradient_Edge_Detection));
                float node_1665 = clamp((lerp( 0.0, _Gradient_Edge_Detection_var.r, _Edge_Detection )*_Intensity),_Value_Clamp_Minimum,_Value_Clamp_Maximum);
                float2 node_1654 = float2(node_1665,i.uv0.g);
                float4 _Gradient_Color_var = tex2D(_Gradient_Color,TRANSFORM_TEX(node_1654, _Gradient_Color));
                float3 emissive = (lerp( (_Solid_Color.rgb*node_1665), _Gradient_Color_var.rgb, _Gradient_Or_Solid_Color )*_Brightness);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
