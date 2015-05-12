// Upgrade NOTE: commented out 'float4 unity_DynamicLightmapST', a built-in variable
// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable

// Shader created with Shader Forge v1.04 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.04;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,dith:2,ufog:True,aust:False,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:38244,y:32712,varname:node_1,prsc:2|emission-2392-OUT,alpha-9260-OUT;n:type:ShaderForge.SFN_DepthBlend,id:5,x:34082,y:33555,varname:node_5,prsc:2|DIST-1022-OUT;n:type:ShaderForge.SFN_OneMinus,id:884,x:34248,y:33555,varname:node_884,prsc:2|IN-5-OUT;n:type:ShaderForge.SFN_Multiply,id:890,x:37675,y:32726,varname:node_890,prsc:2|A-1876-RGB,B-1665-OUT;n:type:ShaderForge.SFN_Add,id:892,x:35081,y:33270,varname:node_892,prsc:2|A-1805-OUT,B-1771-OUT;n:type:ShaderForge.SFN_Multiply,id:895,x:36216,y:32786,varname:node_895,prsc:2|A-1316-R,B-892-OUT;n:type:ShaderForge.SFN_Tex2d,id:896,x:34773,y:32158,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_9824,prsc:2,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-2540-OUT;n:type:ShaderForge.SFN_Add,id:974,x:36651,y:32835,varname:node_974,prsc:2|A-1898-OUT,B-1771-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1022,x:33917,y:33555,ptovrint:False,ptlb:Edge_Detection_Distance,ptin:_Edge_Detection_Distance,varname:node_8971,prsc:2,glob:False,v1:3;n:type:ShaderForge.SFN_Tex2d,id:1316,x:35985,y:32674,ptovrint:False,ptlb:Gradient_Texture_Decay,ptin:_Gradient_Texture_Decay,varname:node_7488,prsc:2,ntxv:0,isnm:False|UVIN-1319-OUT;n:type:ShaderForge.SFN_Append,id:1319,x:35792,y:32674,varname:node_1319,prsc:2|A-1948-OUT,B-1359-OUT;n:type:ShaderForge.SFN_TexCoord,id:1336,x:34412,y:32439,varname:node_1336,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:1340,x:34591,y:32525,varname:node_1340,prsc:2|A-1336-V,B-1342-OUT;n:type:ShaderForge.SFN_Vector1,id:1342,x:34412,y:32598,varname:node_1342,prsc:2,v1:0;n:type:ShaderForge.SFN_Add,id:1359,x:34811,y:32645,varname:node_1359,prsc:2|A-1340-OUT,B-2056-OUT;n:type:ShaderForge.SFN_Add,id:1497,x:35114,y:32563,varname:node_1497,prsc:2|A-896-R,B-1805-OUT;n:type:ShaderForge.SFN_Append,id:1590,x:34430,y:33586,varname:node_1590,prsc:2|A-884-OUT,B-1618-V;n:type:ShaderForge.SFN_TexCoord,id:1618,x:34248,y:33675,varname:node_1618,prsc:2,uv:0;n:type:ShaderForge.SFN_Tex2d,id:1620,x:34607,y:33586,ptovrint:False,ptlb:Gradient_Edge_Detection,ptin:_Gradient_Edge_Detection,varname:node_6284,prsc:2,ntxv:0,isnm:False|UVIN-1590-OUT;n:type:ShaderForge.SFN_TexCoord,id:1652,x:37319,y:32948,varname:node_1652,prsc:2,uv:0;n:type:ShaderForge.SFN_Append,id:1654,x:37515,y:32888,varname:node_1654,prsc:2|A-1665-OUT,B-1652-V;n:type:ShaderForge.SFN_Tex2d,id:1656,x:37681,y:32888,ptovrint:False,ptlb:Gradient_Color,ptin:_Gradient_Color,varname:node_7645,prsc:2,ntxv:0,isnm:False|UVIN-1654-OUT;n:type:ShaderForge.SFN_Clamp,id:1665,x:37305,y:32782,varname:node_1665,prsc:2|IN-2108-OUT,MIN-1667-OUT,MAX-1666-OUT;n:type:ShaderForge.SFN_Vector1,id:1666,x:37112,y:32864,varname:node_1666,prsc:2,v1:0.95;n:type:ShaderForge.SFN_Vector1,id:1667,x:37112,y:32815,varname:node_1667,prsc:2,v1:0.05;n:type:ShaderForge.SFN_SwitchProperty,id:1771,x:34784,y:33586,ptovrint:False,ptlb:Edge_Detection,ptin:_Edge_Detection,varname:node_1092,prsc:2,on:True|A-1772-OUT,B-1620-R;n:type:ShaderForge.SFN_Vector1,id:1772,x:34591,y:33350,varname:node_1772,prsc:2,v1:0;n:type:ShaderForge.SFN_SwitchProperty,id:1805,x:34803,y:33034,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:node_1892,prsc:2,on:True|A-1772-OUT,B-2081-R;n:type:ShaderForge.SFN_SwitchProperty,id:1858,x:37849,y:32811,ptovrint:False,ptlb:Gradient_Or_Solid_Color,ptin:_Gradient_Or_Solid_Color,varname:node_7531,prsc:2,on:True|A-890-OUT,B-1656-RGB;n:type:ShaderForge.SFN_Color,id:1876,x:37305,y:32631,ptovrint:False,ptlb:Solid_Color,ptin:_Solid_Color,varname:node_8121,prsc:2,glob:False,c1:0.1764706,c2:0.5229208,c3:1,c4:1;n:type:ShaderForge.SFN_SwitchProperty,id:1898,x:36457,y:32735,ptovrint:False,ptlb:Make_Same_As_Mask,ptin:_Make_Same_As_Mask,varname:node_1224,prsc:2,on:True|A-1316-R,B-895-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:1948,x:35570,y:32647,ptovrint:False,ptlb:Soft_Texture,ptin:_Soft_Texture,varname:node_6263,prsc:2,on:False|A-2339-OUT,B-896-R;n:type:ShaderForge.SFN_Slider,id:2056,x:34426,y:32807,ptovrint:False,ptlb:Decay,ptin:_Decay,varname:node_7759,prsc:2,min:0.05,cur:0.3,max:0.95;n:type:ShaderForge.SFN_Tex2d,id:2081,x:34605,y:33034,ptovrint:False,ptlb:Mask_Texture,ptin:_Mask_Texture,varname:node_8109,prsc:2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:2108,x:36878,y:32780,varname:node_2108,prsc:2|A-974-OUT,B-2109-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2109,x:36651,y:32769,ptovrint:False,ptlb:Intensity,ptin:_Intensity,varname:node_1197,prsc:2,glob:False,v1:1;n:type:ShaderForge.SFN_Subtract,id:2339,x:35346,y:32815,varname:node_2339,prsc:2|A-2056-OUT,B-1497-OUT;n:type:ShaderForge.SFN_Multiply,id:2392,x:38050,y:32811,varname:node_2392,prsc:2|A-1858-OUT,B-2393-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2393,x:37860,y:32960,ptovrint:False,ptlb:Brightness,ptin:_Brightness,varname:node_3635,prsc:2,glob:False,v1:1;n:type:ShaderForge.SFN_Time,id:2530,x:33947,y:32263,varname:node_2530,prsc:2;n:type:ShaderForge.SFN_Multiply,id:2532,x:34167,y:32231,varname:node_2532,prsc:2|A-2530-T,B-2596-OUT;n:type:ShaderForge.SFN_Time,id:2534,x:33958,y:32040,varname:node_2534,prsc:2;n:type:ShaderForge.SFN_Multiply,id:2536,x:34167,y:32112,varname:node_2536,prsc:2|A-2534-T,B-2598-OUT;n:type:ShaderForge.SFN_TexCoord,id:2538,x:34167,y:31961,varname:node_2538,prsc:2,uv:0;n:type:ShaderForge.SFN_Append,id:2540,x:34583,y:32158,varname:node_2540,prsc:2|A-2583-OUT,B-2593-OUT;n:type:ShaderForge.SFN_Add,id:2583,x:34375,y:32100,varname:node_2583,prsc:2|A-2538-U,B-2536-OUT;n:type:ShaderForge.SFN_Add,id:2593,x:34375,y:32231,varname:node_2593,prsc:2|A-2538-V,B-2532-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2596,x:33947,y:32410,ptovrint:False,ptlb:Pan_SpeedY,ptin:_Pan_SpeedY,varname:node_9268,prsc:2,glob:False,v1:0.1;n:type:ShaderForge.SFN_ValueProperty,id:2598,x:33958,y:32192,ptovrint:False,ptlb:Pan_SpeedX,ptin:_Pan_SpeedX,varname:node_1903,prsc:2,glob:False,v1:0;n:type:ShaderForge.SFN_Multiply,id:9260,x:38035,y:33048,varname:node_9260,prsc:2|A-1665-OUT,B-8618-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8618,x:37843,y:33134,ptovrint:False,ptlb:Transparent,ptin:_Transparent,varname:node_8618,prsc:2,glob:False,v1:1;proporder:2393-2109-1858-1656-1876-896-1316-2056-1805-1898-2081-1771-1022-1620-1948-2596-2598-8618;pass:END;sub:END;*/

Shader "ZFS Shaders/ZFS_Flat_Pro_Fog" {
    Properties {
        _Brightness ("Brightness", Float ) = 1
        _Intensity ("Intensity", Float ) = 1
        [MaterialToggle] _Gradient_Or_Solid_Color ("Gradient_Or_Solid_Color", Float ) = 1
        _Gradient_Color ("Gradient_Color", 2D) = "white" {}
        _Solid_Color ("Solid_Color", Color) = (0.1764706,0.5229208,1,1)
        _Texture ("Texture", 2D) = "white" {}
        _Gradient_Texture_Decay ("Gradient_Texture_Decay", 2D) = "white" {}
        _Decay ("Decay", Range(0.05, 0.95)) = 0.3
        [MaterialToggle] _Mask ("Mask", Float ) = 1
        [MaterialToggle] _Make_Same_As_Mask ("Make_Same_As_Mask", Float ) = 0
        _Mask_Texture ("Mask_Texture", 2D) = "white" {}
        [MaterialToggle] _Edge_Detection ("Edge_Detection", Float ) = 1
        _Edge_Detection_Distance ("Edge_Detection_Distance", Float ) = 3
        _Gradient_Edge_Detection ("Gradient_Edge_Detection", 2D) = "white" {}
        [MaterialToggle] _Soft_Texture ("Soft_Texture", Float ) = -1.398039
        _Pan_SpeedY ("Pan_SpeedY", Float ) = 0.1
        _Pan_SpeedX ("Pan_SpeedX", Float ) = 0
        _Transparent ("Transparent", Float ) = 1
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
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform sampler2D _CameraDepthTexture;
            uniform float4 _TimeEditor;
            // float4 unity_LightmapST;
            #ifdef DYNAMICLIGHTMAP_ON
                // float4 unity_DynamicLightmapST;
            #endif
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float _Edge_Detection_Distance;
            uniform sampler2D _Gradient_Texture_Decay; uniform float4 _Gradient_Texture_Decay_ST;
            uniform sampler2D _Gradient_Edge_Detection; uniform float4 _Gradient_Edge_Detection_ST;
            uniform sampler2D _Gradient_Color; uniform float4 _Gradient_Color_ST;
            uniform fixed _Edge_Detection;
            uniform fixed _Mask;
            uniform fixed _Gradient_Or_Solid_Color;
            uniform float4 _Solid_Color;
            uniform fixed _Make_Same_As_Mask;
            uniform fixed _Soft_Texture;
            uniform float _Decay;
            uniform sampler2D _Mask_Texture; uniform float4 _Mask_Texture_ST;
            uniform float _Intensity;
            uniform float _Brightness;
            uniform float _Pan_SpeedY;
            uniform float _Pan_SpeedX;
            uniform float _Transparent;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 projPos : TEXCOORD1;
                UNITY_FOG_COORDS(2)
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD3;
                #else
                    float3 shLight : TEXCOORD3;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
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
                float4 node_2534 = _Time + _TimeEditor;
                float4 node_2530 = _Time + _TimeEditor;
                float2 node_2540 = float2((i.uv0.r+(node_2534.g*_Pan_SpeedX)),(i.uv0.g+(node_2530.g*_Pan_SpeedY)));
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(node_2540, _Texture));
                float node_1772 = 0.0;
                float4 _Mask_Texture_var = tex2D(_Mask_Texture,TRANSFORM_TEX(i.uv0, _Mask_Texture));
                float _Mask_var = lerp( node_1772, _Mask_Texture_var.r, _Mask );
                float2 node_1319 = float2(lerp( (_Decay-(_Texture_var.r+_Mask_var)), _Texture_var.r, _Soft_Texture ),((i.uv0.g*0.0)+_Decay));
                float4 _Gradient_Texture_Decay_var = tex2D(_Gradient_Texture_Decay,TRANSFORM_TEX(node_1319, _Gradient_Texture_Decay));
                float2 node_1590 = float2((1.0 - saturate((sceneZ-partZ)/_Edge_Detection_Distance)),i.uv0.g);
                float4 _Gradient_Edge_Detection_var = tex2D(_Gradient_Edge_Detection,TRANSFORM_TEX(node_1590, _Gradient_Edge_Detection));
                float _Edge_Detection_var = lerp( node_1772, _Gradient_Edge_Detection_var.r, _Edge_Detection );
                float node_1665 = clamp(((lerp( _Gradient_Texture_Decay_var.r, (_Gradient_Texture_Decay_var.r*(_Mask_var+_Edge_Detection_var)), _Make_Same_As_Mask )+_Edge_Detection_var)*_Intensity),0.05,0.95);
                float2 node_1654 = float2(node_1665,i.uv0.g);
                float4 _Gradient_Color_var = tex2D(_Gradient_Color,TRANSFORM_TEX(node_1654, _Gradient_Color));
                float3 emissive = (lerp( (_Solid_Color.rgb*node_1665), _Gradient_Color_var.rgb, _Gradient_Or_Solid_Color )*_Brightness);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,(node_1665*_Transparent));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
