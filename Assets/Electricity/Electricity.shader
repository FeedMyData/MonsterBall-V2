// Upgrade NOTE: commented out 'float4 unity_DynamicLightmapST', a built-in variable
// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable

// Shader created with Shader Forge v1.02 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.02;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:6610,x:33136,y:32711,varname:node_6610,prsc:2|diff-5913-RGB,emission-8628-OUT,clip-5371-OUT;n:type:ShaderForge.SFN_Color,id:5913,x:32534,y:32607,ptovrint:False,ptlb:texture,ptin:_texture,varname:node_5913,prsc:2,glob:False,c1:0.625,c2:0.5606618,c3:0.5606618,c4:1;n:type:ShaderForge.SFN_Multiply,id:8628,x:33141,y:33283,varname:node_8628,prsc:2|A-2780-OUT,B-5964-OUT;n:type:ShaderForge.SFN_Slider,id:5964,x:32792,y:33479,ptovrint:False,ptlb:glow_strenght,ptin:_glow_strenght,varname:node_5964,prsc:2,min:1,cur:2.918069,max:5;n:type:ShaderForge.SFN_Multiply,id:2780,x:32883,y:33288,varname:node_2780,prsc:2|A-1640-OUT,B-1443-RGB;n:type:ShaderForge.SFN_Multiply,id:1640,x:32664,y:33210,varname:node_1640,prsc:2|A-575-RGB,B-2711-RGB;n:type:ShaderForge.SFN_Color,id:1443,x:32507,y:33422,ptovrint:False,ptlb:glow_collor,ptin:_glow_collor,varname:node_1443,prsc:2,glob:False,c1:0.2286981,c2:0.6146285,c3:0.6911765,c4:1;n:type:ShaderForge.SFN_Tex2d,id:2711,x:32382,y:33167,varname:node_2711,prsc:2,tex:133e4048dd582d44fb93ca7901743499,ntxv:0,isnm:False|UVIN-8155-OUT,TEX-71-TEX;n:type:ShaderForge.SFN_Tex2d,id:575,x:32369,y:32931,ptovrint:False,ptlb:edgefade,ptin:_edgefade,varname:node_575,prsc:2,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:3,isnm:False;n:type:ShaderForge.SFN_Append,id:8155,x:32126,y:32931,varname:node_8155,prsc:2|A-8827-V,B-1311-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:71,x:32079,y:33213,ptovrint:False,ptlb:line_glow,ptin:_line_glow,varname:node_71,tex:133e4048dd582d44fb93ca7901743499,ntxv:0,isnm:False;n:type:ShaderForge.SFN_TexCoord,id:8827,x:31574,y:32725,varname:node_8827,prsc:2,uv:0;n:type:ShaderForge.SFN_Lerp,id:1311,x:31861,y:32950,varname:node_1311,prsc:2|A-8827-U,B-6510-OUT,T-6256-OUT;n:type:ShaderForge.SFN_Add,id:6510,x:31574,y:32962,varname:node_6510,prsc:2|A-8827-U,B-6684-R;n:type:ShaderForge.SFN_Slider,id:6256,x:31471,y:33169,ptovrint:False,ptlb:noise_strength,ptin:_noise_strength,varname:node_6256,prsc:2,min:0,cur:0.25,max:1;n:type:ShaderForge.SFN_Tex2d,id:6684,x:31368,y:32962,ptovrint:False,ptlb:noise,ptin:_noise,varname:node_6684,prsc:2,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:3,isnm:False|UVIN-7528-UVOUT;n:type:ShaderForge.SFN_Panner,id:7528,x:31089,y:32960,varname:node_7528,prsc:2,spu:0.1,spv:2|UVIN-631-UVOUT,DIST-5164-OUT;n:type:ShaderForge.SFN_TexCoord,id:3199,x:30672,y:32741,varname:node_3199,prsc:2,uv:1;n:type:ShaderForge.SFN_Multiply,id:5164,x:30890,y:33038,varname:node_5164,prsc:2|A-2271-OUT,B-6203-TDB;n:type:ShaderForge.SFN_Time,id:6203,x:30583,y:33090,varname:node_6203,prsc:2;n:type:ShaderForge.SFN_Slider,id:2271,x:30357,y:32897,ptovrint:False,ptlb:panner,ptin:_panner,varname:node_2271,prsc:2,min:0,cur:0.25,max:1;n:type:ShaderForge.SFN_Panner,id:631,x:30890,y:32824,varname:node_631,prsc:2,spu:1,spv:1|UVIN-3199-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:6034,x:32296,y:32262,varname:node_6034,prsc:2,uv:0;n:type:ShaderForge.SFN_Tex2d,id:6818,x:32642,y:32852,ptovrint:False,ptlb:node_6818,ptin:_node_6818,varname:node_6818,prsc:2,tex:005c01c5d422dee409c9dbd834a32985,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Add,id:5371,x:32806,y:33008,varname:node_5371,prsc:2|A-6818-A,B-2711-RGB,C-4153-OUT;n:type:ShaderForge.SFN_Slider,id:4153,x:32427,y:33097,ptovrint:False,ptlb:node_4153,ptin:_node_4153,varname:node_4153,prsc:2,min:0,cur:0.3799062,max:1;proporder:5913-5964-1443-575-71-6256-6684-2271-6818-4153;pass:END;sub:END;*/

Shader "Shader Forge/Electricity" {
    Properties {
        _texture ("texture", Color) = (0.625,0.5606618,0.5606618,1)
        _glow_strenght ("glow_strenght", Range(1, 5)) = 2.918069
        _glow_collor ("glow_collor", Color) = (0.2286981,0.6146285,0.6911765,1)
        _edgefade ("edgefade", 2D) = "bump" {}
        _line_glow ("line_glow", 2D) = "white" {}
        _noise_strength ("noise_strength", Range(0, 1)) = 0.25
        _noise ("noise", 2D) = "bump" {}
        _panner ("panner", Range(0, 1)) = 0.25
        _node_6818 ("node_6818", 2D) = "white" {}
        _node_4153 ("node_4153", Range(0, 1)) = 0.3799062
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
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
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            // float4 unity_LightmapST;
            #ifdef DYNAMICLIGHTMAP_ON
                // float4 unity_DynamicLightmapST;
            #endif
            uniform float4 _texture;
            uniform float _glow_strenght;
            uniform float4 _glow_collor;
            uniform sampler2D _edgefade; uniform float4 _edgefade_ST;
            uniform sampler2D _line_glow; uniform float4 _line_glow_ST;
            uniform float _noise_strength;
            uniform sampler2D _noise; uniform float4 _noise_ST;
            uniform float _panner;
            uniform sampler2D _node_6818; uniform float4 _node_6818_ST;
            uniform float _node_4153;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float3 normalDir : TEXCOORD3;
                LIGHTING_COORDS(4,5)
                UNITY_FOG_COORDS(6)
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD7;
                #else
                    float3 shLight : TEXCOORD7;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 normalDirection = i.normalDir;
                float4 _node_6818_var = tex2D(_node_6818,TRANSFORM_TEX(i.uv0, _node_6818));
                float4 node_6203 = _Time + _TimeEditor;
                float4 node_1554 = _Time + _TimeEditor;
                float2 node_7528 = ((i.uv1+node_1554.g*float2(1,1))+(_panner*node_6203.b)*float2(0.1,2));
                float4 _noise_var = tex2D(_noise,TRANSFORM_TEX(node_7528, _noise));
                float2 node_8155 = float2(i.uv0.g,lerp(i.uv0.r,(i.uv0.r+_noise_var.r),_noise_strength));
                float4 node_2711 = tex2D(_line_glow,TRANSFORM_TEX(node_8155, _line_glow));
                clip((_node_6818_var.a+node_2711.rgb+_node_4153) - 0.5);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 indirectDiffuse = float3(0,0,0);
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float3 diffuse = (directDiffuse + indirectDiffuse) * _texture.rgb;
////// Emissive:
                float4 _edgefade_var = tex2D(_edgefade,TRANSFORM_TEX(i.uv0, _edgefade));
                float3 emissive = (((_edgefade_var.rgb*node_2711.rgb)*_glow_collor.rgb)*_glow_strenght);
/// Final Color:
                float3 finalColor = diffuse + emissive;
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
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            // float4 unity_LightmapST;
            #ifdef DYNAMICLIGHTMAP_ON
                // float4 unity_DynamicLightmapST;
            #endif
            uniform float4 _texture;
            uniform float _glow_strenght;
            uniform float4 _glow_collor;
            uniform sampler2D _edgefade; uniform float4 _edgefade_ST;
            uniform sampler2D _line_glow; uniform float4 _line_glow_ST;
            uniform float _noise_strength;
            uniform sampler2D _noise; uniform float4 _noise_ST;
            uniform float _panner;
            uniform sampler2D _node_6818; uniform float4 _node_6818_ST;
            uniform float _node_4153;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float3 normalDir : TEXCOORD3;
                LIGHTING_COORDS(4,5)
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD6;
                #else
                    float3 shLight : TEXCOORD6;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 normalDirection = i.normalDir;
                float4 _node_6818_var = tex2D(_node_6818,TRANSFORM_TEX(i.uv0, _node_6818));
                float4 node_6203 = _Time + _TimeEditor;
                float4 node_77 = _Time + _TimeEditor;
                float2 node_7528 = ((i.uv1+node_77.g*float2(1,1))+(_panner*node_6203.b)*float2(0.1,2));
                float4 _noise_var = tex2D(_noise,TRANSFORM_TEX(node_7528, _noise));
                float2 node_8155 = float2(i.uv0.g,lerp(i.uv0.r,(i.uv0.r+_noise_var.r),_noise_strength));
                float4 node_2711 = tex2D(_line_glow,TRANSFORM_TEX(node_8155, _line_glow));
                clip((_node_6818_var.a+node_2711.rgb+_node_4153) - 0.5);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 diffuse = directDiffuse * _texture.rgb;
/// Final Color:
                float3 finalColor = diffuse;
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
            uniform float4 _TimeEditor;
            // float4 unity_LightmapST;
            #ifdef DYNAMICLIGHTMAP_ON
                // float4 unity_DynamicLightmapST;
            #endif
            uniform sampler2D _line_glow; uniform float4 _line_glow_ST;
            uniform float _noise_strength;
            uniform sampler2D _noise; uniform float4 _noise_ST;
            uniform float _panner;
            uniform sampler2D _node_6818; uniform float4 _node_6818_ST;
            uniform float _node_4153;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
            };
            struct VertexOutput {
                V2F_SHADOW_COLLECTOR;
                float2 uv0 : TEXCOORD5;
                float2 uv1 : TEXCOORD6;
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD7;
                #else
                    float3 shLight : TEXCOORD7;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_SHADOW_COLLECTOR(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
/////// Vectors:
                float4 _node_6818_var = tex2D(_node_6818,TRANSFORM_TEX(i.uv0, _node_6818));
                float4 node_6203 = _Time + _TimeEditor;
                float4 node_7797 = _Time + _TimeEditor;
                float2 node_7528 = ((i.uv1+node_7797.g*float2(1,1))+(_panner*node_6203.b)*float2(0.1,2));
                float4 _noise_var = tex2D(_noise,TRANSFORM_TEX(node_7528, _noise));
                float2 node_8155 = float2(i.uv0.g,lerp(i.uv0.r,(i.uv0.r+_noise_var.r),_noise_strength));
                float4 node_2711 = tex2D(_line_glow,TRANSFORM_TEX(node_8155, _line_glow));
                clip((_node_6818_var.a+node_2711.rgb+_node_4153) - 0.5);
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
            uniform float4 _TimeEditor;
            // float4 unity_LightmapST;
            #ifdef DYNAMICLIGHTMAP_ON
                // float4 unity_DynamicLightmapST;
            #endif
            uniform sampler2D _line_glow; uniform float4 _line_glow_ST;
            uniform float _noise_strength;
            uniform sampler2D _noise; uniform float4 _noise_ST;
            uniform float _panner;
            uniform sampler2D _node_6818; uniform float4 _node_6818_ST;
            uniform float _node_4153;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float2 uv1 : TEXCOORD2;
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD3;
                #else
                    float3 shLight : TEXCOORD3;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
/////// Vectors:
                float4 _node_6818_var = tex2D(_node_6818,TRANSFORM_TEX(i.uv0, _node_6818));
                float4 node_6203 = _Time + _TimeEditor;
                float4 node_3000 = _Time + _TimeEditor;
                float2 node_7528 = ((i.uv1+node_3000.g*float2(1,1))+(_panner*node_6203.b)*float2(0.1,2));
                float4 _noise_var = tex2D(_noise,TRANSFORM_TEX(node_7528, _noise));
                float2 node_8155 = float2(i.uv0.g,lerp(i.uv0.r,(i.uv0.r+_noise_var.r),_noise_strength));
                float4 node_2711 = tex2D(_line_glow,TRANSFORM_TEX(node_8155, _line_glow));
                clip((_node_6818_var.a+node_2711.rgb+_node_4153) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
