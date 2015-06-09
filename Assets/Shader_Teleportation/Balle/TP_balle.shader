// Upgrade NOTE: commented out 'float4 unity_DynamicLightmapST', a built-in variable
// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable

// Shader created with Shader Forge v1.02 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.02;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:4656,x:33939,y:32600,varname:node_4656,prsc:2|diff-1393-OUT,emission-5349-OUT,custl-6358-OUT,clip-465-OUT,olwid-7057-OUT,voffset-753-OUT;n:type:ShaderForge.SFN_Slider,id:6744,x:31346,y:32682,ptovrint:False,ptlb:alpha_slider,ptin:_alpha_slider,varname:node_6744,prsc:2,min:0,cur:80,max:80;n:type:ShaderForge.SFN_Add,id:465,x:33012,y:33295,varname:node_465,prsc:2|A-962-OUT,B-220-OUT;n:type:ShaderForge.SFN_Multiply,id:220,x:32724,y:33385,varname:node_220,prsc:2|A-6146-OUT,B-7621-A;n:type:ShaderForge.SFN_Tex2d,id:7703,x:32401,y:33050,ptovrint:False,ptlb:alpha_neg,ptin:_alpha_neg,varname:node_7703,prsc:2,tex:f2f02f2108349994f83035690c78de1f,ntxv:3,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:7621,x:32401,y:33514,ptovrint:False,ptlb:alpha_base,ptin:_alpha_base,varname:node_7621,prsc:2,tex:09f4ed74d1b928d43b257e1ed71f3da9,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:962,x:32724,y:33249,varname:node_962,prsc:2|A-7703-A,B-2282-OUT;n:type:ShaderForge.SFN_Add,id:2282,x:32401,y:33215,varname:node_2282,prsc:2|A-16-OUT,B-8199-OUT;n:type:ShaderForge.SFN_Add,id:6146,x:32401,y:33348,varname:node_6146,prsc:2|A-8199-OUT,B-8721-OUT;n:type:ShaderForge.SFN_ValueProperty,id:16,x:32122,y:33152,ptovrint:False,ptlb:Neg,ptin:_Neg,varname:node_16,prsc:2,glob:False,v1:0.1;n:type:ShaderForge.SFN_ValueProperty,id:8721,x:32122,y:33458,ptovrint:False,ptlb:Base,ptin:_Base,varname:node_16,prsc:2,glob:False,v1:0.2;n:type:ShaderForge.SFN_Multiply,id:2311,x:31835,y:33322,varname:node_2311,prsc:2|A-6744-OUT,B-1104-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1104,x:31584,y:33343,ptovrint:False,ptlb:coeff,ptin:_coeff,varname:node_1104,prsc:2,glob:False,v1:0.001;n:type:ShaderForge.SFN_ValueProperty,id:8622,x:32122,y:33233,ptovrint:False,ptlb:Coeff_2,ptin:_Coeff_2,varname:node_8622,prsc:2,glob:False,v1:0.3;n:type:ShaderForge.SFN_Add,id:8199,x:32122,y:33300,varname:node_8199,prsc:2|A-8622-OUT,B-2311-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4445,x:31929,y:32676,ptovrint:False,ptlb:divide_color,ptin:_divide_color,varname:node_16,prsc:2,glob:False,v1:10;n:type:ShaderForge.SFN_Divide,id:7617,x:31929,y:32761,varname:node_7617,prsc:2|A-4445-OUT,B-6744-OUT;n:type:ShaderForge.SFN_Fresnel,id:5495,x:32391,y:32901,varname:node_5495,prsc:2|NRM-2646-RGB,EXP-4873-OUT;n:type:ShaderForge.SFN_Multiply,id:5349,x:32881,y:32698,varname:node_5349,prsc:2|A-1393-OUT,B-5495-OUT;n:type:ShaderForge.SFN_Tex2d,id:2646,x:32173,y:32788,ptovrint:False,ptlb:normale,ptin:_normale,varname:node_2646,prsc:2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Add,id:4873,x:31929,y:32966,varname:node_4873,prsc:2|A-7617-OUT,B-1632-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1632,x:31936,y:33152,ptovrint:False,ptlb:node_1632,ptin:_node_1632,varname:node_1632,prsc:2,glob:False,v1:-0.125;n:type:ShaderForge.SFN_Multiply,id:753,x:32881,y:32987,varname:node_753,prsc:2|A-9644-Y,B-5336-OUT,C-7703-RGB,D-7082-OUT;n:type:ShaderForge.SFN_Multiply,id:5336,x:32264,y:33001,varname:node_5336,prsc:2|A-4873-OUT,B-9249-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9249,x:32122,y:33067,ptovrint:False,ptlb:Neg_copy,ptin:_Neg_copy,varname:node_16,prsc:2,glob:False,v1:10;n:type:ShaderForge.SFN_ObjectScale,id:9644,x:32881,y:32836,varname:node_9644,prsc:2,rcp:True;n:type:ShaderForge.SFN_Vector3,id:7082,x:32702,y:33105,varname:node_7082,prsc:2,v1:0,v2:0,v3:1;n:type:ShaderForge.SFN_Multiply,id:8551,x:31997,y:32042,varname:node_8551,prsc:2|A-6744-OUT,B-682-OUT;n:type:ShaderForge.SFN_Vector3,id:682,x:31997,y:32203,varname:node_682,prsc:2,v1:0.0125,v2:0.0125,v3:0.0125;n:type:ShaderForge.SFN_Multiply,id:4277,x:32166,y:32023,varname:node_4277,prsc:2|A-900-RGB,B-8551-OUT;n:type:ShaderForge.SFN_Color,id:900,x:32166,y:31872,ptovrint:False,ptlb:Color_base,ptin:_Color_base,varname:node_2737,prsc:2,glob:False,c1:0.1172414,c2:0,c3:1,c4:1;n:type:ShaderForge.SFN_Add,id:1393,x:32896,y:32227,varname:node_1393,prsc:2|A-4277-OUT,B-249-OUT;n:type:ShaderForge.SFN_Multiply,id:3320,x:32415,y:32414,varname:node_3320,prsc:2|A-4848-RGB,B-5598-OUT;n:type:ShaderForge.SFN_Multiply,id:5598,x:32229,y:32435,varname:node_5598,prsc:2|A-431-OUT,B-4826-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3810,x:31904,y:32388,ptovrint:False,ptlb:Test2,ptin:_Test2,varname:node_7122,prsc:2,glob:False,v1:80;n:type:ShaderForge.SFN_Divide,id:431,x:31904,y:32463,varname:node_431,prsc:2|A-3810-OUT,B-6744-OUT;n:type:ShaderForge.SFN_Color,id:4848,x:32415,y:32264,ptovrint:False,ptlb:Color_tp,ptin:_Color_tp,varname:node_4848,prsc:2,glob:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Vector3,id:4826,x:32229,y:32583,varname:node_4826,prsc:2,v1:0.0125,v2:0.0125,v3:0.0125;n:type:ShaderForge.SFN_Multiply,id:249,x:32745,y:32422,varname:node_249,prsc:2|A-3320-OUT,B-3663-OUT;n:type:ShaderForge.SFN_Vector1,id:3663,x:32558,y:32507,varname:node_3663,prsc:2,v1:5;n:type:ShaderForge.SFN_Multiply,id:6358,x:33563,y:31938,varname:node_6358,prsc:2|A-5199-RGB,B-7643-RGB;n:type:ShaderForge.SFN_Tex2d,id:5199,x:33315,y:31938,ptovrint:False,ptlb:Ramp,ptin:_Ramp,varname:node_5199,prsc:2,ntxv:2,isnm:False|UVIN-2896-OUT;n:type:ShaderForge.SFN_LightColor,id:7643,x:33315,y:32109,varname:node_7643,prsc:2;n:type:ShaderForge.SFN_Append,id:2896,x:33088,y:31938,varname:node_2896,prsc:2|A-2833-OUT,B-2833-OUT;n:type:ShaderForge.SFN_Multiply,id:2833,x:32896,y:31938,varname:node_2833,prsc:2|A-7832-OUT,B-2202-OUT;n:type:ShaderForge.SFN_Dot,id:7832,x:32687,y:31841,varname:node_7832,prsc:2,dt:4|A-394-OUT,B-8991-OUT;n:type:ShaderForge.SFN_LightAttenuation,id:2202,x:32687,y:31993,varname:node_2202,prsc:2;n:type:ShaderForge.SFN_LightVector,id:8991,x:32494,y:31899,varname:node_8991,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:394,x:32494,y:31729,prsc:2,pt:False;n:type:ShaderForge.SFN_ValueProperty,id:7057,x:33574,y:32910,ptovrint:False,ptlb:Out,ptin:_Out,varname:node_7057,prsc:2,glob:False,v1:0.05;proporder:6744-7703-7621-16-8721-1104-8622-4445-2646-1632-9249-900-3810-4848-5199-7057;pass:END;sub:END;*/

Shader "Shader Forge/TP" {
    Properties {
        _alpha_slider ("alpha_slider", Range(0, 80)) = 80
        _alpha_neg ("alpha_neg", 2D) = "bump" {}
        _alpha_base ("alpha_base", 2D) = "white" {}
        _Neg ("Neg", Float ) = -0.1
        _Base ("Base", Float ) = 0.2
        _coeff ("coeff", Float ) = 0.01
        _Coeff_2 ("Coeff_2", Float ) = 0.3
        _divide_color ("divide_color", Float ) = 10
        _normale ("normale", 2D) = "white" {}
        _node_1632 ("node_1632", Float ) = -0.125
        _Neg_copy ("Neg_copy", Float ) = 10
        _Color_base ("Color_base", Color) = (0.1172414,0,1,1)
        _Test2 ("Test2", Float ) = 100
        _Color_tp ("Color_tp", Color) = (1,0,0,1)
        _Ramp ("Ramp", 2D) = "black" {}
        _Out ("Out", Float ) = 0.05
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "Outline"
            Tags {
            }
            Cull Front
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            #pragma glsl
            // float4 unity_LightmapST;
            #ifdef DYNAMICLIGHTMAP_ON
                // float4 unity_DynamicLightmapST;
            #endif
            uniform float _alpha_slider;
            uniform sampler2D _alpha_neg; uniform float4 _alpha_neg_ST;
            uniform sampler2D _alpha_base; uniform float4 _alpha_base_ST;
            uniform float _Neg;
            uniform float _Base;
            uniform float _coeff;
            uniform float _Coeff_2;
            uniform float _divide_color;
            uniform float _node_1632;
            uniform float _Neg_copy;
            uniform float _Out;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD2;
                #else
                    float3 shLight : TEXCOORD2;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                float3 recipObjScale = float3( length(_World2Object[0].xyz), length(_World2Object[1].xyz), length(_World2Object[2].xyz) );
                float node_4873 = ((_divide_color/_alpha_slider)+_node_1632);
                float4 _alpha_neg_var = tex2Dlod(_alpha_neg,float4(TRANSFORM_TEX(o.uv0, _alpha_neg),0.0,0));
                v.vertex.xyz += (recipObjScale.g*(node_4873*_Neg_copy)*_alpha_neg_var.rgb*float3(-1,0,0));
                o.pos = mul(UNITY_MATRIX_MVP, float4(v.vertex.xyz + v.normal*_Out,1));
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                float3 recipObjScale = float3( length(_World2Object[0].xyz), length(_World2Object[1].xyz), length(_World2Object[2].xyz) );
/////// Vectors:
                float4 _alpha_neg_var = tex2D(_alpha_neg,TRANSFORM_TEX(i.uv0, _alpha_neg));
                float node_8199 = (_Coeff_2+(_alpha_slider*_coeff));
                float4 _alpha_base_var = tex2D(_alpha_base,TRANSFORM_TEX(i.uv0, _alpha_base));
                clip(((_alpha_neg_var.a*(_Neg+node_8199))+((node_8199+_Base)*_alpha_base_var.a)) - 0.5);
                return fixed4(float3(0,0,0),0);
            }
            ENDCG
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            #pragma glsl
            // float4 unity_LightmapST;
            #ifdef DYNAMICLIGHTMAP_ON
                // float4 unity_DynamicLightmapST;
            #endif
            uniform float _alpha_slider;
            uniform sampler2D _alpha_neg; uniform float4 _alpha_neg_ST;
            uniform sampler2D _alpha_base; uniform float4 _alpha_base_ST;
            uniform float _Neg;
            uniform float _Base;
            uniform float _coeff;
            uniform float _Coeff_2;
            uniform float _divide_color;
            uniform sampler2D _normale; uniform float4 _normale_ST;
            uniform float _node_1632;
            uniform float _Neg_copy;
            uniform float4 _Color_base;
            uniform float _Test2;
            uniform float4 _Color_tp;
            uniform sampler2D _Ramp; uniform float4 _Ramp_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD6;
                #else
                    float3 shLight : TEXCOORD6;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                float3 recipObjScale = float3( length(_World2Object[0].xyz), length(_World2Object[1].xyz), length(_World2Object[2].xyz) );
                float node_4873 = ((_divide_color/_alpha_slider)+_node_1632);
                float4 _alpha_neg_var = tex2Dlod(_alpha_neg,float4(TRANSFORM_TEX(o.uv0, _alpha_neg),0.0,0));
                v.vertex.xyz += (recipObjScale.g*(node_4873*_Neg_copy)*_alpha_neg_var.rgb*float3(0,0,1));
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                float3 recipObjScale = float3( length(_World2Object[0].xyz), length(_World2Object[1].xyz), length(_World2Object[2].xyz) );
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float4 _alpha_neg_var = tex2D(_alpha_neg,TRANSFORM_TEX(i.uv0, _alpha_neg));
                float node_8199 = (_Coeff_2+(_alpha_slider*_coeff));
                float4 _alpha_base_var = tex2D(_alpha_base,TRANSFORM_TEX(i.uv0, _alpha_base));
                clip(((_alpha_neg_var.a*(_Neg+node_8199))+((node_8199+_Base)*_alpha_base_var.a)) - 0.5);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
////// Emissive:
                float3 node_1393 = ((_Color_base.rgb*(_alpha_slider*float3(0.0125,0.0125,0.0125)))+((_Color_tp.rgb*((_Test2/_alpha_slider)*float3(0.0125,0.0125,0.0125)))*5.0));
                float4 _normale_var = tex2D(_normale,TRANSFORM_TEX(i.uv0, _normale));
                float node_4873 = ((_divide_color/_alpha_slider)+_node_1632);
                float3 emissive = (node_1393*pow(1.0-max(0,dot(_normale_var.rgb, viewDirection)),node_4873));
                float node_2833 = (0.5*dot(i.normalDir,lightDirection)+0.5*attenuation);
                float2 node_2896 = float2(node_2833,node_2833);
                float4 _Ramp_var = tex2D(_Ramp,TRANSFORM_TEX(node_2896, _Ramp));
                float3 finalColor = emissive + (_Ramp_var.rgb*_LightColor0.rgb);
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            #pragma glsl
            // float4 unity_LightmapST;
            #ifdef DYNAMICLIGHTMAP_ON
                // float4 unity_DynamicLightmapST;
            #endif
            uniform float _alpha_slider;
            uniform sampler2D _alpha_neg; uniform float4 _alpha_neg_ST;
            uniform sampler2D _alpha_base; uniform float4 _alpha_base_ST;
            uniform float _Neg;
            uniform float _Base;
            uniform float _coeff;
            uniform float _Coeff_2;
            uniform float _divide_color;
            uniform sampler2D _normale; uniform float4 _normale_ST;
            uniform float _node_1632;
            uniform float _Neg_copy;
            uniform float4 _Color_base;
            uniform float _Test2;
            uniform float4 _Color_tp;
            uniform sampler2D _Ramp; uniform float4 _Ramp_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD5;
                #else
                    float3 shLight : TEXCOORD5;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                float3 recipObjScale = float3( length(_World2Object[0].xyz), length(_World2Object[1].xyz), length(_World2Object[2].xyz) );
                float node_4873 = ((_divide_color/_alpha_slider)+_node_1632);
                float4 _alpha_neg_var = tex2Dlod(_alpha_neg,float4(TRANSFORM_TEX(o.uv0, _alpha_neg),0.0,0));
                v.vertex.xyz += (recipObjScale.g*(node_4873*_Neg_copy)*_alpha_neg_var.rgb*float3(0,0,1));
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                float3 recipObjScale = float3( length(_World2Object[0].xyz), length(_World2Object[1].xyz), length(_World2Object[2].xyz) );
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float4 _alpha_neg_var = tex2D(_alpha_neg,TRANSFORM_TEX(i.uv0, _alpha_neg));
                float node_8199 = (_Coeff_2+(_alpha_slider*_coeff));
                float4 _alpha_base_var = tex2D(_alpha_base,TRANSFORM_TEX(i.uv0, _alpha_base));
                clip(((_alpha_neg_var.a*(_Neg+node_8199))+((node_8199+_Base)*_alpha_base_var.a)) - 0.5);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float node_2833 = (0.5*dot(i.normalDir,lightDirection)+0.5*attenuation);
                float2 node_2896 = float2(node_2833,node_2833);
                float4 _Ramp_var = tex2D(_Ramp,TRANSFORM_TEX(node_2896, _Ramp));
                float3 finalColor = (_Ramp_var.rgb*_LightColor0.rgb);
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCollector"
            Tags {
                "LightMode"="ShadowCollector"
            }
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCOLLECTOR
            #define SHADOW_COLLECTOR_PASS
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcollector
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            #pragma glsl
            // float4 unity_LightmapST;
            #ifdef DYNAMICLIGHTMAP_ON
                // float4 unity_DynamicLightmapST;
            #endif
            uniform float _alpha_slider;
            uniform sampler2D _alpha_neg; uniform float4 _alpha_neg_ST;
            uniform sampler2D _alpha_base; uniform float4 _alpha_base_ST;
            uniform float _Neg;
            uniform float _Base;
            uniform float _coeff;
            uniform float _Coeff_2;
            uniform float _divide_color;
            uniform float _node_1632;
            uniform float _Neg_copy;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_COLLECTOR;
                float2 uv0 : TEXCOORD5;
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD6;
                #else
                    float3 shLight : TEXCOORD6;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                float3 recipObjScale = float3( length(_World2Object[0].xyz), length(_World2Object[1].xyz), length(_World2Object[2].xyz) );
                float node_4873 = ((_divide_color/_alpha_slider)+_node_1632);
                float4 _alpha_neg_var = tex2Dlod(_alpha_neg,float4(TRANSFORM_TEX(o.uv0, _alpha_neg),0.0,0));
                v.vertex.xyz += (recipObjScale.g*(node_4873*_Neg_copy)*_alpha_neg_var.rgb*float3(0,0,1));
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_SHADOW_COLLECTOR(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                float3 recipObjScale = float3( length(_World2Object[0].xyz), length(_World2Object[1].xyz), length(_World2Object[2].xyz) );
/////// Vectors:
                float4 _alpha_neg_var = tex2D(_alpha_neg,TRANSFORM_TEX(i.uv0, _alpha_neg));
                float node_8199 = (_Coeff_2+(_alpha_slider*_coeff));
                float4 _alpha_base_var = tex2D(_alpha_base,TRANSFORM_TEX(i.uv0, _alpha_base));
                clip(((_alpha_neg_var.a*(_Neg+node_8199))+((node_8199+_Base)*_alpha_base_var.a)) - 0.5);
                SHADOW_COLLECTOR_FRAGMENT(i)
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Cull Off
            Offset 1, 1
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            #pragma glsl
            // float4 unity_LightmapST;
            #ifdef DYNAMICLIGHTMAP_ON
                // float4 unity_DynamicLightmapST;
            #endif
            uniform float _alpha_slider;
            uniform sampler2D _alpha_neg; uniform float4 _alpha_neg_ST;
            uniform sampler2D _alpha_base; uniform float4 _alpha_base_ST;
            uniform float _Neg;
            uniform float _Base;
            uniform float _coeff;
            uniform float _Coeff_2;
            uniform float _divide_color;
            uniform float _node_1632;
            uniform float _Neg_copy;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD2;
                #else
                    float3 shLight : TEXCOORD2;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                float3 recipObjScale = float3( length(_World2Object[0].xyz), length(_World2Object[1].xyz), length(_World2Object[2].xyz) );
                float node_4873 = ((_divide_color/_alpha_slider)+_node_1632);
                float4 _alpha_neg_var = tex2Dlod(_alpha_neg,float4(TRANSFORM_TEX(o.uv0, _alpha_neg),0.0,0));
                v.vertex.xyz += (recipObjScale.g*(node_4873*_Neg_copy)*_alpha_neg_var.rgb*float3(0,0,1));
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                float3 recipObjScale = float3( length(_World2Object[0].xyz), length(_World2Object[1].xyz), length(_World2Object[2].xyz) );
/////// Vectors:
                float4 _alpha_neg_var = tex2D(_alpha_neg,TRANSFORM_TEX(i.uv0, _alpha_neg));
                float node_8199 = (_Coeff_2+(_alpha_slider*_coeff));
                float4 _alpha_base_var = tex2D(_alpha_base,TRANSFORM_TEX(i.uv0, _alpha_base));
                clip(((_alpha_neg_var.a*(_Neg+node_8199))+((node_8199+_Base)*_alpha_base_var.a)) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
