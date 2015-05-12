// Upgrade NOTE: commented out 'float4 unity_DynamicLightmapST', a built-in variable
// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable

// Shader created with Shader Forge v1.04 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.04;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,dith:2,ufog:True,aust:False,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:37792,y:32712,varname:node_1,prsc:2|emission-2476-OUT,alpha-1703-OUT;n:type:ShaderForge.SFN_DepthBlend,id:5,x:34012,y:33074,varname:node_5,prsc:2|DIST-1022-OUT;n:type:ShaderForge.SFN_OneMinus,id:884,x:34178,y:33074,varname:node_884,prsc:2|IN-5-OUT;n:type:ShaderForge.SFN_Multiply,id:890,x:37160,y:32726,varname:node_890,prsc:2|A-1876-RGB,B-1665-OUT;n:type:ShaderForge.SFN_Add,id:892,x:35020,y:32719,varname:node_892,prsc:2|A-1805-OUT,B-1771-OUT;n:type:ShaderForge.SFN_Fresnel,id:893,x:34546,y:32771,varname:node_893,prsc:2|EXP-1173-OUT;n:type:ShaderForge.SFN_Multiply,id:895,x:36003,y:32481,varname:node_895,prsc:2|A-1316-R,B-892-OUT;n:type:ShaderForge.SFN_Tex2d,id:896,x:34709,y:31836,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_4861,prsc:2,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-2538-OUT;n:type:ShaderForge.SFN_Add,id:974,x:36328,y:32670,varname:node_974,prsc:2|A-1898-OUT,B-1771-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1022,x:33847,y:33074,ptovrint:False,ptlb:Edge_Detection_Distance,ptin:_Edge_Detection_Distance,varname:node_1834,prsc:2,glob:False,v1:3;n:type:ShaderForge.SFN_ValueProperty,id:1173,x:34381,y:32791,ptovrint:False,ptlb:Fresnel_Exponent,ptin:_Fresnel_Exponent,varname:node_5580,prsc:2,glob:False,v1:1;n:type:ShaderForge.SFN_Tex2d,id:1316,x:35794,y:32326,ptovrint:False,ptlb:Gradient_Texture_Decay,ptin:_Gradient_Texture_Decay,varname:node_2627,prsc:2,ntxv:0,isnm:False|UVIN-1319-OUT;n:type:ShaderForge.SFN_Append,id:1319,x:35610,y:32326,varname:node_1319,prsc:2|A-1948-OUT,B-1359-OUT;n:type:ShaderForge.SFN_TexCoord,id:1336,x:34254,y:32198,varname:node_1336,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:1340,x:34555,y:32290,varname:node_1340,prsc:2|A-1336-V,B-1342-OUT;n:type:ShaderForge.SFN_Vector1,id:1342,x:34254,y:32379,varname:node_1342,prsc:2,v1:0;n:type:ShaderForge.SFN_Add,id:1359,x:34726,y:32347,varname:node_1359,prsc:2|A-1340-OUT,B-2056-OUT;n:type:ShaderForge.SFN_Add,id:1497,x:35035,y:32315,varname:node_1497,prsc:2|A-896-R,B-1805-OUT;n:type:ShaderForge.SFN_Append,id:1590,x:34359,y:33133,varname:node_1590,prsc:2|A-884-OUT,B-1618-V;n:type:ShaderForge.SFN_TexCoord,id:1618,x:34178,y:33194,varname:node_1618,prsc:2,uv:0;n:type:ShaderForge.SFN_Tex2d,id:1620,x:34536,y:33133,ptovrint:False,ptlb:Gradient_Edge_Detection,ptin:_Gradient_Edge_Detection,varname:node_2649,prsc:2,ntxv:0,isnm:False|UVIN-1590-OUT;n:type:ShaderForge.SFN_TexCoord,id:1652,x:36804,y:32948,varname:node_1652,prsc:2,uv:0;n:type:ShaderForge.SFN_Append,id:1654,x:36985,y:32888,varname:node_1654,prsc:2|A-1665-OUT,B-1652-V;n:type:ShaderForge.SFN_Tex2d,id:1656,x:37166,y:32888,ptovrint:False,ptlb:Gradient_Color,ptin:_Gradient_Color,varname:node_58,prsc:2,ntxv:0,isnm:False|UVIN-1654-OUT;n:type:ShaderForge.SFN_Clamp,id:1665,x:36790,y:32784,varname:node_1665,prsc:2|IN-2450-OUT,MIN-1667-OUT,MAX-1666-OUT;n:type:ShaderForge.SFN_Vector1,id:1666,x:36555,y:32899,varname:node_1666,prsc:2,v1:0.95;n:type:ShaderForge.SFN_Vector1,id:1667,x:36555,y:32845,varname:node_1667,prsc:2,v1:0.05;n:type:ShaderForge.SFN_SwitchProperty,id:1771,x:34721,y:33133,ptovrint:False,ptlb:Edge_Detection,ptin:_Edge_Detection,varname:node_323,prsc:2,on:True|A-1772-OUT,B-1620-R;n:type:ShaderForge.SFN_Vector1,id:1772,x:34519,y:32986,varname:node_1772,prsc:2,v1:0;n:type:ShaderForge.SFN_SwitchProperty,id:1805,x:34728,y:32751,ptovrint:False,ptlb:Fresnel,ptin:_Fresnel,varname:node_7366,prsc:2,on:True|A-1772-OUT,B-893-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:1858,x:37346,y:32811,ptovrint:False,ptlb:Gradient_Or_Solid_Color,ptin:_Gradient_Or_Solid_Color,varname:node_9583,prsc:2,on:True|A-890-OUT,B-1656-RGB;n:type:ShaderForge.SFN_Color,id:1876,x:36953,y:32660,ptovrint:False,ptlb:Solid_Color,ptin:_Solid_Color,varname:node_8786,prsc:2,glob:False,c1:0.1764706,c2:0.5229208,c3:1,c4:1;n:type:ShaderForge.SFN_SwitchProperty,id:1898,x:36079,y:32670,ptovrint:False,ptlb:Make_Same_As_Fresnel,ptin:_Make_Same_As_Fresnel,varname:node_6326,prsc:2,on:True|A-1316-R,B-895-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:1948,x:35406,y:32394,ptovrint:False,ptlb:Soft_Texture,ptin:_Soft_Texture,varname:node_3628,prsc:2,on:False|A-1497-OUT,B-896-R;n:type:ShaderForge.SFN_Slider,id:2056,x:34398,y:32464,ptovrint:False,ptlb:Decay,ptin:_Decay,varname:node_9396,prsc:2,min:0.05,cur:0.3,max:0.95;n:type:ShaderForge.SFN_Multiply,id:2450,x:36555,y:32716,varname:node_2450,prsc:2|A-974-OUT,B-2452-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2452,x:36328,y:32833,ptovrint:False,ptlb:Intensity,ptin:_Intensity,varname:node_1420,prsc:2,glob:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:2476,x:37532,y:32811,varname:node_2476,prsc:2|A-1858-OUT,B-2477-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2477,x:37346,y:32955,ptovrint:False,ptlb:Brightness,ptin:_Brightness,varname:node_7216,prsc:2,glob:False,v1:1;n:type:ShaderForge.SFN_Time,id:2528,x:33887,y:31940,varname:node_2528,prsc:2;n:type:ShaderForge.SFN_Multiply,id:2530,x:34107,y:31908,varname:node_2530,prsc:2|A-2528-T,B-2594-OUT;n:type:ShaderForge.SFN_Time,id:2532,x:33898,y:31717,varname:node_2532,prsc:2;n:type:ShaderForge.SFN_Multiply,id:2534,x:34107,y:31789,varname:node_2534,prsc:2|A-2532-T,B-2596-OUT;n:type:ShaderForge.SFN_TexCoord,id:2536,x:34107,y:31638,varname:node_2536,prsc:2,uv:0;n:type:ShaderForge.SFN_Append,id:2538,x:34523,y:31835,varname:node_2538,prsc:2|A-2581-OUT,B-2591-OUT;n:type:ShaderForge.SFN_Add,id:2581,x:34315,y:31777,varname:node_2581,prsc:2|A-2536-U,B-2534-OUT;n:type:ShaderForge.SFN_Add,id:2591,x:34315,y:31908,varname:node_2591,prsc:2|A-2536-V,B-2530-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2594,x:33887,y:32087,ptovrint:False,ptlb:Pan_SpeedY,ptin:_Pan_SpeedY,varname:node_6229,prsc:2,glob:False,v1:0.1;n:type:ShaderForge.SFN_ValueProperty,id:2596,x:33898,y:31869,ptovrint:False,ptlb:Pan_SpeedX,ptin:_Pan_SpeedX,varname:node_5359,prsc:2,glob:False,v1:0;n:type:ShaderForge.SFN_Multiply,id:1703,x:37532,y:33021,varname:node_1703,prsc:2|A-974-OUT,B-1429-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1429,x:37346,y:33091,ptovrint:False,ptlb:Transparency,ptin:_Transparency,varname:node_1429,prsc:2,glob:False,v1:1;proporder:2477-2452-1858-1656-1876-896-1316-2056-1805-1898-1173-1771-1022-1620-1948-2594-2596-1429;pass:END;sub:END;*/

Shader "ZFS Shaders/ZFS_3D_Pro_Fog" {
    Properties {
        _Brightness ("Brightness", Float ) = 1
        _Intensity ("Intensity", Float ) = 1
        [MaterialToggle] _Gradient_Or_Solid_Color ("Gradient_Or_Solid_Color", Float ) = 1
        _Gradient_Color ("Gradient_Color", 2D) = "white" {}
        _Solid_Color ("Solid_Color", Color) = (0.1764706,0.5229208,1,1)
        _Texture ("Texture", 2D) = "white" {}
        _Gradient_Texture_Decay ("Gradient_Texture_Decay", 2D) = "white" {}
        _Decay ("Decay", Range(0.05, 0.95)) = 0.3
        [MaterialToggle] _Fresnel ("Fresnel", Float ) = 1
        [MaterialToggle] _Make_Same_As_Fresnel ("Make_Same_As_Fresnel", Float ) = 0
        _Fresnel_Exponent ("Fresnel_Exponent", Float ) = 1
        [MaterialToggle] _Edge_Detection ("Edge_Detection", Float ) = 1
        _Edge_Detection_Distance ("Edge_Detection_Distance", Float ) = 3
        _Gradient_Edge_Detection ("Gradient_Edge_Detection", 2D) = "white" {}
        [MaterialToggle] _Soft_Texture ("Soft_Texture", Float ) = 1.698039
        _Pan_SpeedY ("Pan_SpeedY", Float ) = 0.1
        _Pan_SpeedX ("Pan_SpeedX", Float ) = 0
        _Transparency ("Transparency", Float ) = 1
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
            uniform float _Fresnel_Exponent;
            uniform sampler2D _Gradient_Texture_Decay; uniform float4 _Gradient_Texture_Decay_ST;
            uniform sampler2D _Gradient_Edge_Detection; uniform float4 _Gradient_Edge_Detection_ST;
            uniform sampler2D _Gradient_Color; uniform float4 _Gradient_Color_ST;
            uniform fixed _Edge_Detection;
            uniform fixed _Fresnel;
            uniform fixed _Gradient_Or_Solid_Color;
            uniform float4 _Solid_Color;
            uniform fixed _Make_Same_As_Fresnel;
            uniform fixed _Soft_Texture;
            uniform float _Decay;
            uniform float _Intensity;
            uniform float _Brightness;
            uniform float _Pan_SpeedY;
            uniform float _Pan_SpeedX;
            uniform float _Transparency;
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
                float4 projPos : TEXCOORD3;
                UNITY_FOG_COORDS(4)
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD5;
                #else
                    float3 shLight : TEXCOORD5;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 node_2532 = _Time + _TimeEditor;
                float4 node_2528 = _Time + _TimeEditor;
                float2 node_2538 = float2((i.uv0.r+(node_2532.g*_Pan_SpeedX)),(i.uv0.g+(node_2528.g*_Pan_SpeedY)));
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(node_2538, _Texture));
                float node_1772 = 0.0;
                float _Fresnel_var = lerp( node_1772, pow(1.0-max(0,dot(normalDirection, viewDirection)),_Fresnel_Exponent), _Fresnel );
                float2 node_1319 = float2(lerp( (_Texture_var.r+_Fresnel_var), _Texture_var.r, _Soft_Texture ),((i.uv0.g*0.0)+_Decay));
                float4 _Gradient_Texture_Decay_var = tex2D(_Gradient_Texture_Decay,TRANSFORM_TEX(node_1319, _Gradient_Texture_Decay));
                float2 node_1590 = float2((1.0 - saturate((sceneZ-partZ)/_Edge_Detection_Distance)),i.uv0.g);
                float4 _Gradient_Edge_Detection_var = tex2D(_Gradient_Edge_Detection,TRANSFORM_TEX(node_1590, _Gradient_Edge_Detection));
                float _Edge_Detection_var = lerp( node_1772, _Gradient_Edge_Detection_var.r, _Edge_Detection );
                float node_974 = (lerp( _Gradient_Texture_Decay_var.r, (_Gradient_Texture_Decay_var.r*(_Fresnel_var+_Edge_Detection_var)), _Make_Same_As_Fresnel )+_Edge_Detection_var);
                float node_1665 = clamp((node_974*_Intensity),0.05,0.95);
                float2 node_1654 = float2(node_1665,i.uv0.g);
                float4 _Gradient_Color_var = tex2D(_Gradient_Color,TRANSFORM_TEX(node_1654, _Gradient_Color));
                float3 emissive = (lerp( (_Solid_Color.rgb*node_1665), _Gradient_Color_var.rgb, _Gradient_Or_Solid_Color )*_Brightness);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,(node_974*_Transparency));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
