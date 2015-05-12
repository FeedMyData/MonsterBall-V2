// Shader created with Shader Forge Beta 0.36 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.36;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:2,bsrc:0,bdst:0,culm:2,dpts:2,wrdp:False,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32719,y:32712|emission-4-OUT,alpha-2-A;n:type:ShaderForge.SFN_Tex2d,id:2,x:33348,y:32557,ptlb:Front_Line,ptin:_Front_Line,tex:bed3f56cccbbca647bdc16e75350086b,ntxv:0,isnm:False|UVIN-21-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:3,x:33348,y:32917,ptlb:Back_Line,ptin:_Back_Line,tex:bf3a06e242dff6a4ba33cf6aacb4c02a,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Add,id:4,x:32961,y:32668|A-35-OUT,B-9-OUT,C-10-OUT;n:type:ShaderForge.SFN_Multiply,id:9,x:33161,y:32472|A-11-RGB,B-2-RGB;n:type:ShaderForge.SFN_Multiply,id:10,x:33165,y:32858|A-12-RGB,B-3-RGB;n:type:ShaderForge.SFN_Color,id:11,x:33348,y:32390,ptlb:Front_Color,ptin:_Front_Color,glob:False,c1:0.7750865,c2:0.9343038,c3:0.9411765,c4:1;n:type:ShaderForge.SFN_Color,id:12,x:33348,y:32751,ptlb:Back_Color,ptin:_Back_Color,glob:False,c1:0.316609,c2:0.5367796,c3:0.8970588,c4:1;n:type:ShaderForge.SFN_Time,id:18,x:33981,y:32540;n:type:ShaderForge.SFN_Slider,id:19,x:33915,y:32425,ptlb:_ScrollSpeed,ptin:__ScrollSpeed,min:0,cur:0.1406198,max:1;n:type:ShaderForge.SFN_Multiply,id:20,x:33697,y:32495|A-18-T,B-19-OUT;n:type:ShaderForge.SFN_Panner,id:21,x:33507,y:32555,spu:1,spv:0|DIST-20-OUT;n:type:ShaderForge.SFN_Tex2d,id:33,x:33346,y:32197,ptlb:Front_Line2,ptin:_Front_Line2,tex:bed3f56cccbbca647bdc16e75350086b,ntxv:0,isnm:False|UVIN-45-UVOUT;n:type:ShaderForge.SFN_Multiply,id:35,x:33159,y:32112|A-37-RGB,B-33-RGB;n:type:ShaderForge.SFN_Color,id:37,x:33346,y:32030,ptlb:Front_Color2,ptin:_Front_Color2,glob:False,c1:0.7750865,c2:0.9343038,c3:0.9411765,c4:1;n:type:ShaderForge.SFN_Time,id:39,x:33979,y:32180;n:type:ShaderForge.SFN_Multiply,id:43,x:33695,y:32135|A-39-TDB,B-19-OUT;n:type:ShaderForge.SFN_Panner,id:45,x:33505,y:32195,spu:1,spv:0|DIST-43-OUT;proporder:11-2-37-33-3-12-19;pass:END;sub:END;*/

Shader "Shader Forge/FX_EnergyLine" {
    Properties {
        _Front_Color ("Front_Color", Color) = (0.7750865,0.9343038,0.9411765,1)
        _Front_Line ("Front_Line", 2D) = "white" {}
        _Front_Color2 ("Front_Color2", Color) = (0.7750865,0.9343038,0.9411765,1)
        _Front_Line2 ("Front_Line2", 2D) = "white" {}
        _Back_Line ("Back_Line", 2D) = "white" {}
        _Back_Color ("Back_Color", Color) = (0.316609,0.5367796,0.8970588,1)
        __ScrollSpeed ("_ScrollSpeed", Range(0, 1)) = 0.1406198
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
            uniform float4 _TimeEditor;
            uniform sampler2D _Front_Line; uniform float4 _Front_Line_ST;
            uniform sampler2D _Back_Line; uniform float4 _Back_Line_ST;
            uniform float4 _Front_Color;
            uniform float4 _Back_Color;
            uniform float __ScrollSpeed;
            uniform sampler2D _Front_Line2; uniform float4 _Front_Line2_ST;
            uniform float4 _Front_Color2;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_39 = _Time + _TimeEditor;
                float2 node_60 = i.uv0;
                float2 node_45 = (node_60.rg+(node_39.b*__ScrollSpeed)*float2(1,0));
                float4 node_18 = _Time + _TimeEditor;
                float2 node_21 = (node_60.rg+(node_18.g*__ScrollSpeed)*float2(1,0));
                float4 node_2 = tex2D(_Front_Line,TRANSFORM_TEX(node_21, _Front_Line));
                float3 emissive = ((_Front_Color2.rgb*tex2D(_Front_Line2,TRANSFORM_TEX(node_45, _Front_Line2)).rgb)+(_Front_Color.rgb*node_2.rgb)+(_Back_Color.rgb*tex2D(_Back_Line,TRANSFORM_TEX(node_60.rg, _Back_Line)).rgb));
                float3 finalColor = emissive;
/// Final Color:
                return fixed4(finalColor,node_2.a);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
