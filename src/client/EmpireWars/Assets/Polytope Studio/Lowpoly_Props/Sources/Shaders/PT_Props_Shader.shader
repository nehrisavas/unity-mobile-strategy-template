// Made with Amplify Shader Editor v1.9.8
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Polytope Studio/ PT_Medieval Props Shader"
{
	Properties
	{
		[HideInInspector] _EmissionColor("Emission Color", Color) = (1,1,1,1)
		[HideInInspector] _AlphaCutoff("Alpha Cutoff ", Range(0, 1)) = 0.5
		[HDR][Header(WALLS )]_Leather1colour("Leather 1 colour", Color) = (0.5283019,0.192425,0.1420434,1)
		[HDR]_leather2color("leather 2 color", Color) = (0.6320754,0.3730699,0.2415005,1)
		[HDR]_Leather3color("Leather 3 color", Color) = (0.8867924,0.6561894,0.23843,1)
		[HDR][Header(WOOD)]_Wood1color("Wood 1 color", Color) = (0.4056604,0.2683544,0.135858,1)
		[HDR]_Wood2color("Wood 2 color", Color) = (0.1981132,0.103908,0.06634924,1)
		[HDR]_Wood3color("Wood 3 color", Color) = (0.1886792,0.0907685,0.05606977,1)
		[HDR][Header(FABRICS)]_Fabric1color("Fabric 1 color", Color) = (1,1,1,0)
		[HDR]_Fabric2color("Fabric 2 color", Color) = (1,0,0.01978827,0)
		[HDR]_Fabric3color("Fabric 3 color", Color) = (0,0.005197836,0.9245283,0)
		[HDR][Header(ROCK )]_Rock1color("Rock 1 color", Color) = (0.5088814,0.5149178,0.5088814,1)
		[HDR]_Rock2color("Rock 2 color", Color) = (0.5188679,0.4540594,0.4283108,1)
		[HDR][Header(CERAMIC TILES)]_Ceramictiles1color("Ceramic tiles 1 color", Color) = (0.3207547,0.04869195,0.01059096,1)
		_Ceramic1smoothness("Ceramic 1 smoothness", Range( 0 , 1)) = 0.3
		[HDR]_Ceramictiles2color("Ceramic tiles 2 color", Color) = (0.7924528,0.3776169,0.1682093,1)
		_Ceramic2smoothness("Ceramic 2 smoothness", Range( 0 , 1)) = 0.3
		[HDR]_Ceramictiles3color("Ceramic tiles 3 color ", Color) = (0.4677838,0.3813261,0.2501584,1)
		_Ceramic3smoothness("Ceramic 3 smoothness", Range( 0 , 1)) = 0.3
		[HDR][Header(METAL)]_Metal1color("Metal 1 color", Color) = (0.385947,0.5532268,0.5566038,0)
		_Metal1metallic("Metal 1 metallic", Range( 0 , 1)) = 0.65
		_Metal1smootness("Metal 1 smootness", Range( 0 , 1)) = 0.7
		[HDR]_Metal2color("Metal 2 color", Color) = (1.309888,1.123492,1.044203,0)
		_Metal2metallic("Metal 2 metallic", Range( 0 , 1)) = 0.65
		_Metal2smootness("Metal 2 smootness", Range( 0 , 1)) = 0.7
		[HDR]_Metal3color("Metal 3 color", Color) = (0.4578082,0.638907,0.6787086,1)
		_Metal3metallic("Metal 3 metallic", Range( 0 , 1)) = 0.65
		_Metal3smootness("Metal 3 smootness", Range( 0 , 1)) = 0.7
		[HDR][Header(OTHER COLORS)]_Ropecolor("Rope color", Color) = (0.6037736,0.5810711,0.3389106,1)
		[HDR]_Haycolor("Hay color", Color) = (0.764151,0.517899,0.1622019,1)
		[HDR]_Mortarcolor("Mortar color", Color) = (0.6415094,0.5745595,0.4629761,0)
		[HDR]_Coatofarmscolor("Coat of arms color", Color) = (1,0,0,0)
		[NoScaleOffset]_Coarofarmstexture("Coar of arms texture", 2D) = "black" {}
		[Toggle]_MetallicOn("Metallic On", Float) = 1
		[Toggle]_SmoothnessOn("Smoothness On", Float) = 1
		[HideInInspector]_TextureSample2("Texture Sample 2", 2D) = "white" {}
		[HideInInspector][NoScaleOffset]_TextureSample9("Texture Sample 9", 2D) = "white" {}
		[HideInInspector] _texcoord2( "", 2D ) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}


		//_TransmissionShadow( "Transmission Shadow", Range( 0, 1 ) ) = 0.5
		//_TransStrength( "Trans Strength", Range( 0, 50 ) ) = 1
		//_TransNormal( "Trans Normal Distortion", Range( 0, 1 ) ) = 0.5
		//_TransScattering( "Trans Scattering", Range( 1, 50 ) ) = 2
		//_TransDirect( "Trans Direct", Range( 0, 1 ) ) = 0.9
		//_TransAmbient( "Trans Ambient", Range( 0, 1 ) ) = 0.1
		//_TransShadow( "Trans Shadow", Range( 0, 1 ) ) = 0.5
		//_TessPhongStrength( "Tess Phong Strength", Range( 0, 1 ) ) = 0.5
		//_TessValue( "Tess Max Tessellation", Range( 1, 32 ) ) = 16
		//_TessMin( "Tess Min Distance", Float ) = 10
		//_TessMax( "Tess Max Distance", Float ) = 25
		//_TessEdgeLength ( "Tess Edge length", Range( 2, 50 ) ) = 16
		//_TessMaxDisp( "Tess Max Displacement", Float ) = 25

		[HideInInspector][ToggleOff] _SpecularHighlights("Specular Highlights", Float) = 1
		[HideInInspector][ToggleOff] _EnvironmentReflections("Environment Reflections", Float) = 1
		[HideInInspector][ToggleOff] _ReceiveShadows("Receive Shadows", Float) = 1.0

		[HideInInspector] _QueueOffset("_QueueOffset", Float) = 0
        [HideInInspector] _QueueControl("_QueueControl", Float) = -1

        [HideInInspector][NoScaleOffset] unity_Lightmaps("unity_Lightmaps", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset] unity_LightmapsInd("unity_LightmapsInd", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset] unity_ShadowMasks("unity_ShadowMasks", 2DArray) = "" {}

		//[HideInInspector][ToggleUI] _AddPrecomputedVelocity("Add Precomputed Velocity", Float) = 1
	}

	SubShader
	{
		LOD 0

		

		Tags { "RenderPipeline"="UniversalPipeline" "RenderType"="Opaque" "Queue"="Geometry" "UniversalMaterialType"="Lit" }

		Cull Back
		ZWrite On
		ZTest LEqual
		Offset 0 , 0
		AlphaToMask Off

		

		HLSLINCLUDE
		#pragma target 4.5
		#pragma prefer_hlslcc gles
		// ensure rendering platforms toggle list is visible

		#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
		#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Filtering.hlsl"

		#ifndef ASE_TESS_FUNCS
		#define ASE_TESS_FUNCS
		float4 FixedTess( float tessValue )
		{
			return tessValue;
		}

		float CalcDistanceTessFactor (float4 vertex, float minDist, float maxDist, float tess, float4x4 o2w, float3 cameraPos )
		{
			float3 wpos = mul(o2w,vertex).xyz;
			float dist = distance (wpos, cameraPos);
			float f = clamp(1.0 - (dist - minDist) / (maxDist - minDist), 0.01, 1.0) * tess;
			return f;
		}

		float4 CalcTriEdgeTessFactors (float3 triVertexFactors)
		{
			float4 tess;
			tess.x = 0.5 * (triVertexFactors.y + triVertexFactors.z);
			tess.y = 0.5 * (triVertexFactors.x + triVertexFactors.z);
			tess.z = 0.5 * (triVertexFactors.x + triVertexFactors.y);
			tess.w = (triVertexFactors.x + triVertexFactors.y + triVertexFactors.z) / 3.0f;
			return tess;
		}

		float CalcEdgeTessFactor (float3 wpos0, float3 wpos1, float edgeLen, float3 cameraPos, float4 scParams )
		{
			float dist = distance (0.5 * (wpos0+wpos1), cameraPos);
			float len = distance(wpos0, wpos1);
			float f = max(len * scParams.y / (edgeLen * dist), 1.0);
			return f;
		}

		float DistanceFromPlane (float3 pos, float4 plane)
		{
			float d = dot (float4(pos,1.0f), plane);
			return d;
		}

		bool WorldViewFrustumCull (float3 wpos0, float3 wpos1, float3 wpos2, float cullEps, float4 planes[6] )
		{
			float4 planeTest;
			planeTest.x = (( DistanceFromPlane(wpos0, planes[0]) > -cullEps) ? 1.0f : 0.0f ) +
							(( DistanceFromPlane(wpos1, planes[0]) > -cullEps) ? 1.0f : 0.0f ) +
							(( DistanceFromPlane(wpos2, planes[0]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.y = (( DistanceFromPlane(wpos0, planes[1]) > -cullEps) ? 1.0f : 0.0f ) +
							(( DistanceFromPlane(wpos1, planes[1]) > -cullEps) ? 1.0f : 0.0f ) +
							(( DistanceFromPlane(wpos2, planes[1]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.z = (( DistanceFromPlane(wpos0, planes[2]) > -cullEps) ? 1.0f : 0.0f ) +
							(( DistanceFromPlane(wpos1, planes[2]) > -cullEps) ? 1.0f : 0.0f ) +
							(( DistanceFromPlane(wpos2, planes[2]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.w = (( DistanceFromPlane(wpos0, planes[3]) > -cullEps) ? 1.0f : 0.0f ) +
							(( DistanceFromPlane(wpos1, planes[3]) > -cullEps) ? 1.0f : 0.0f ) +
							(( DistanceFromPlane(wpos2, planes[3]) > -cullEps) ? 1.0f : 0.0f );
			return !all (planeTest);
		}

		float4 DistanceBasedTess( float4 v0, float4 v1, float4 v2, float tess, float minDist, float maxDist, float4x4 o2w, float3 cameraPos )
		{
			float3 f;
			f.x = CalcDistanceTessFactor (v0,minDist,maxDist,tess,o2w,cameraPos);
			f.y = CalcDistanceTessFactor (v1,minDist,maxDist,tess,o2w,cameraPos);
			f.z = CalcDistanceTessFactor (v2,minDist,maxDist,tess,o2w,cameraPos);

			return CalcTriEdgeTessFactors (f);
		}

		float4 EdgeLengthBasedTess( float4 v0, float4 v1, float4 v2, float edgeLength, float4x4 o2w, float3 cameraPos, float4 scParams )
		{
			float3 pos0 = mul(o2w,v0).xyz;
			float3 pos1 = mul(o2w,v1).xyz;
			float3 pos2 = mul(o2w,v2).xyz;
			float4 tess;
			tess.x = CalcEdgeTessFactor (pos1, pos2, edgeLength, cameraPos, scParams);
			tess.y = CalcEdgeTessFactor (pos2, pos0, edgeLength, cameraPos, scParams);
			tess.z = CalcEdgeTessFactor (pos0, pos1, edgeLength, cameraPos, scParams);
			tess.w = (tess.x + tess.y + tess.z) / 3.0f;
			return tess;
		}

		float4 EdgeLengthBasedTessCull( float4 v0, float4 v1, float4 v2, float edgeLength, float maxDisplacement, float4x4 o2w, float3 cameraPos, float4 scParams, float4 planes[6] )
		{
			float3 pos0 = mul(o2w,v0).xyz;
			float3 pos1 = mul(o2w,v1).xyz;
			float3 pos2 = mul(o2w,v2).xyz;
			float4 tess;

			if (WorldViewFrustumCull(pos0, pos1, pos2, maxDisplacement, planes))
			{
				tess = 0.0f;
			}
			else
			{
				tess.x = CalcEdgeTessFactor (pos1, pos2, edgeLength, cameraPos, scParams);
				tess.y = CalcEdgeTessFactor (pos2, pos0, edgeLength, cameraPos, scParams);
				tess.z = CalcEdgeTessFactor (pos0, pos1, edgeLength, cameraPos, scParams);
				tess.w = (tess.x + tess.y + tess.z) / 3.0f;
			}
			return tess;
		}
		#endif //ASE_TESS_FUNCS
		ENDHLSL

		
		Pass
		{
			
			Name "Forward"
			Tags { "LightMode"="UniversalForward" }

			Blend One Zero, One Zero
			ZWrite On
			ZTest LEqual
			Offset 0 , 0
			ColorMask RGBA

			

			HLSLPROGRAM

			#pragma multi_compile_fragment _ALPHATEST_ON
			#define _NORMAL_DROPOFF_TS 1
			#pragma shader_feature_local _RECEIVE_SHADOWS_OFF
			#pragma multi_compile_fragment _ _SCREEN_SPACE_OCCLUSION
			#pragma multi_compile_instancing
			#pragma instancing_options renderinglayer
			#pragma multi_compile _ LOD_FADE_CROSSFADE
			#pragma multi_compile_fog
			#define ASE_FOG 1
			#define ASE_VERSION 19800
			#define ASE_SRP_VERSION 170003


			#pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
			#pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
            #pragma multi_compile _ EVALUATE_SH_MIXED EVALUATE_SH_VERTEX
			#pragma multi_compile_fragment _ _ADDITIONAL_LIGHT_SHADOWS
			#pragma multi_compile_fragment _ _REFLECTION_PROBE_BLENDING
			#pragma multi_compile_fragment _ _REFLECTION_PROBE_BOX_PROJECTION
			#pragma multi_compile_fragment _ _SHADOWS_SOFT _SHADOWS_SOFT_LOW _SHADOWS_SOFT_MEDIUM _SHADOWS_SOFT_HIGH
			#pragma multi_compile_fragment _ _DBUFFER_MRT1 _DBUFFER_MRT2 _DBUFFER_MRT3
			#pragma multi_compile _ _LIGHT_LAYERS
			#pragma multi_compile_fragment _ _LIGHT_COOKIES
			#pragma multi_compile _ _FORWARD_PLUS

			#pragma multi_compile _ LIGHTMAP_SHADOW_MIXING
			#pragma multi_compile _ SHADOWS_SHADOWMASK
			#pragma multi_compile _ DIRLIGHTMAP_COMBINED
			#pragma multi_compile _ LIGHTMAP_ON
			#pragma multi_compile _ DYNAMICLIGHTMAP_ON
			#pragma multi_compile _ USE_LEGACY_LIGHTMAPS

			#pragma vertex vert
			#pragma fragment frag

			#if defined(_SPECULAR_SETUP) && defined(_ASE_LIGHTING_SIMPLE)
				#define _SPECULAR_COLOR 1
			#endif

			#define SHADERPASS SHADERPASS_FORWARD

			#include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"
			#include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/RenderingLayers.hlsl"
			#include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ProbeVolumeVariants.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
            #include_with_pragmas "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRenderingKeywords.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRendering.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/DebugMipmapStreamingMacros.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DBuffer.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

			#if defined(LOD_FADE_CROSSFADE)
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/LODCrossFade.hlsl"
            #endif

			#if defined(UNITY_INSTANCING_ENABLED) && defined(_TERRAIN_INSTANCED_PERPIXEL_NORMAL)
				#define ENABLE_TERRAIN_PERPIXEL_NORMAL
			#endif

			

			#if defined(ASE_EARLY_Z_DEPTH_OPTIMIZE) && (SHADER_TARGET >= 45)
				#define ASE_SV_DEPTH SV_DepthLessEqual
				#define ASE_SV_POSITION_QUALIFIERS linear noperspective centroid
			#else
				#define ASE_SV_DEPTH SV_Depth
				#define ASE_SV_POSITION_QUALIFIERS
			#endif

			struct Attributes
			{
				float4 positionOS : POSITION;
				float3 normalOS : NORMAL;
				float4 tangentOS : TANGENT;
				float4 texcoord : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct PackedVaryings
			{
				ASE_SV_POSITION_QUALIFIERS float4 positionCS : SV_POSITION;
				float4 clipPosV : TEXCOORD0;
				float4 lightmapUVOrVertexSH : TEXCOORD1;
				#if defined(ASE_FOG) || defined(_ADDITIONAL_LIGHTS_VERTEX)
					half4 fogFactorAndVertexLight : TEXCOORD2;
				#endif
				float4 tSpace0 : TEXCOORD3;
				float4 tSpace1 : TEXCOORD4;
				float4 tSpace2 : TEXCOORD5;
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
					float4 shadowCoord : TEXCOORD6;
				#endif
				#if defined(DYNAMICLIGHTMAP_ON)
					float2 dynamicLightmapUV : TEXCOORD7;
				#endif	
				#if defined(USE_APV_PROBE_OCCLUSION)
					float4 probeOcclusion : TEXCOORD8;
				#endif
				float4 ase_texcoord9 : TEXCOORD9;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _TextureSample2_ST;
			float4 _Coatofarmscolor;
			float4 _Metal3color;
			float4 _Metal2color;
			float4 _Metal1color;
			float4 _Haycolor;
			float4 _Ceramictiles3color;
			float4 _Ceramictiles2color;
			float4 _Ceramictiles1color;
			float4 _Wood3color;
			float4 _Wood2color;
			float4 _Ropecolor;
			float4 _Leather3color;
			float4 _leather2color;
			float4 _Leather1colour;
			float4 _Fabric3color;
			float4 _Fabric2color;
			float4 _Fabric1color;
			float4 _Rock2color;
			float4 _Rock1color;
			float4 _Mortarcolor;
			float4 _Wood1color;
			float _Metal1smootness;
			float _Ceramic3smoothness;
			float _Ceramic2smoothness;
			float _Ceramic1smoothness;
			float _MetallicOn;
			float _Metal3metallic;
			float _Metal2metallic;
			float _Metal1metallic;
			float _Metal2smootness;
			float _SmoothnessOn;
			float _Metal3smootness;
			#ifdef ASE_TRANSMISSION
				float _TransmissionShadow;
			#endif
			#ifdef ASE_TRANSLUCENCY
				float _TransStrength;
				float _TransNormal;
				float _TransScattering;
				float _TransDirect;
				float _TransAmbient;
				float _TransShadow;
			#endif
			#ifdef ASE_TESSELLATION
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END

			#ifdef SCENEPICKINGPASS
				float4 _SelectionID;
			#endif

			#ifdef SCENESELECTIONPASS
				int _ObjectId;
				int _PassValue;
			#endif

			sampler2D _TextureSample2;
			sampler2D _TextureSample9;
			sampler2D _Coarofarmstexture;


			
			PackedVaryings VertexFunction( Attributes input  )
			{
				PackedVaryings output = (PackedVaryings)0;
				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_TRANSFER_INSTANCE_ID(input, output);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

				output.ase_texcoord9.xy = input.texcoord.xy;
				output.ase_texcoord9.zw = input.texcoord1.xy;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = input.positionOS.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif

				float3 vertexValue = defaultVertexValue;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					input.positionOS.xyz = vertexValue;
				#else
					input.positionOS.xyz += vertexValue;
				#endif
				input.normalOS = input.normalOS;
				input.tangentOS = input.tangentOS;

				VertexPositionInputs vertexInput = GetVertexPositionInputs( input.positionOS.xyz );
				VertexNormalInputs normalInput = GetVertexNormalInputs( input.normalOS, input.tangentOS );

				output.tSpace0 = float4( normalInput.normalWS, vertexInput.positionWS.x );
				output.tSpace1 = float4( normalInput.tangentWS, vertexInput.positionWS.y );
				output.tSpace2 = float4( normalInput.bitangentWS, vertexInput.positionWS.z );

				#if defined(LIGHTMAP_ON)
					OUTPUT_LIGHTMAP_UV( input.texcoord1, unity_LightmapST, output.lightmapUVOrVertexSH.xy );
				#endif

				#if defined(DYNAMICLIGHTMAP_ON)
					output.dynamicLightmapUV.xy = input.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
				#endif

				OUTPUT_SH4( vertexInput.positionWS, normalInput.normalWS.xyz, GetWorldSpaceNormalizeViewDir( vertexInput.positionWS ), output.lightmapUVOrVertexSH.xyz, output.probeOcclusion );

				#if defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
					output.lightmapUVOrVertexSH.zw = input.texcoord.xy;
					output.lightmapUVOrVertexSH.xy = input.texcoord.xy * unity_LightmapST.xy + unity_LightmapST.zw;
				#endif

				#if defined(ASE_FOG) || defined(_ADDITIONAL_LIGHTS_VERTEX)
					output.fogFactorAndVertexLight = 0;
					#if defined(ASE_FOG) && !defined(_FOG_FRAGMENT)
						output.fogFactorAndVertexLight.x = ComputeFogFactor(vertexInput.positionCS.z);
					#endif
					#ifdef _ADDITIONAL_LIGHTS_VERTEX
						half3 vertexLight = VertexLighting( vertexInput.positionWS, normalInput.normalWS );
						output.fogFactorAndVertexLight.yzw = vertexLight;
					#endif
				#endif

				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
					output.shadowCoord = GetShadowCoord( vertexInput );
				#endif

				output.positionCS = vertexInput.positionCS;
				output.clipPosV = vertexInput.positionCS;
				return output;
			}

			#if defined(ASE_TESSELLATION)
			struct VertexControl
			{
				float4 positionOS : INTERNALTESSPOS;
				float3 normalOS : NORMAL;
				float4 tangentOS : TANGENT;
				float4 texcoord : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( Attributes input )
			{
				VertexControl output;
				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_TRANSFER_INSTANCE_ID(input, output);
				output.positionOS = input.positionOS;
				output.normalOS = input.normalOS;
				output.tangentOS = input.tangentOS;
				output.texcoord = input.texcoord;
				output.texcoord1 = input.texcoord1;
				output.texcoord2 = input.texcoord2;
				
				return output;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> input)
			{
				TessellationFactors output;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(input[0].positionOS, input[1].positionOS, input[2].positionOS, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(input[0].positionOS, input[1].positionOS, input[2].positionOS, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(input[0].positionOS, input[1].positionOS, input[2].positionOS, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				output.edge[0] = tf.x; output.edge[1] = tf.y; output.edge[2] = tf.z; output.inside = tf.w;
				return output;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
				return patch[id];
			}

			[domain("tri")]
			PackedVaryings DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				Attributes output = (Attributes) 0;
				output.positionOS = patch[0].positionOS * bary.x + patch[1].positionOS * bary.y + patch[2].positionOS * bary.z;
				output.normalOS = patch[0].normalOS * bary.x + patch[1].normalOS * bary.y + patch[2].normalOS * bary.z;
				output.tangentOS = patch[0].tangentOS * bary.x + patch[1].tangentOS * bary.y + patch[2].tangentOS * bary.z;
				output.texcoord = patch[0].texcoord * bary.x + patch[1].texcoord * bary.y + patch[2].texcoord * bary.z;
				output.texcoord1 = patch[0].texcoord1 * bary.x + patch[1].texcoord1 * bary.y + patch[2].texcoord1 * bary.z;
				output.texcoord2 = patch[0].texcoord2 * bary.x + patch[1].texcoord2 * bary.y + patch[2].texcoord2 * bary.z;
				
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = output.positionOS.xyz - patch[i].normalOS * (dot(output.positionOS.xyz, patch[i].normalOS) - dot(patch[i].positionOS.xyz, patch[i].normalOS));
				float phongStrength = _TessPhongStrength;
				output.positionOS.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * output.positionOS.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], output);
				return VertexFunction(output);
			}
			#else
			PackedVaryings vert ( Attributes input )
			{
				return VertexFunction( input );
			}
			#endif

			half4 frag ( PackedVaryings input
						#ifdef ASE_DEPTH_WRITE_ON
						,out float outputDepth : ASE_SV_DEPTH
						#endif
						#ifdef _WRITE_RENDERING_LAYERS
						, out float4 outRenderingLayers : SV_Target1
						#endif
						 ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

				#if defined(LOD_FADE_CROSSFADE)
					LODFadeCrossFade( input.positionCS );
				#endif

				#if defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
					float2 sampleCoords = (input.lightmapUVOrVertexSH.zw / _TerrainHeightmapRecipSize.zw + 0.5f) * _TerrainHeightmapRecipSize.xy;
					float3 WorldNormal = TransformObjectToWorldNormal(normalize(SAMPLE_TEXTURE2D(_TerrainNormalmapTexture, sampler_TerrainNormalmapTexture, sampleCoords).rgb * 2 - 1));
					float3 WorldTangent = -cross(GetObjectToWorldMatrix()._13_23_33, WorldNormal);
					float3 WorldBiTangent = cross(WorldNormal, -WorldTangent);
				#else
					float3 WorldNormal = normalize( input.tSpace0.xyz );
					float3 WorldTangent = input.tSpace1.xyz;
					float3 WorldBiTangent = input.tSpace2.xyz;
				#endif

				float3 WorldPosition = float3(input.tSpace0.w,input.tSpace1.w,input.tSpace2.w);
				float3 WorldViewDirection = _WorldSpaceCameraPos.xyz  - WorldPosition;
				float4 ShadowCoords = float4( 0, 0, 0, 0 );

				float4 ClipPos = input.clipPosV;
				float4 ScreenPos = ComputeScreenPos( input.clipPosV );

				float2 NormalizedScreenSpaceUV = GetNormalizedScreenSpaceUV(input.positionCS);

				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
					ShadowCoords = input.shadowCoord;
				#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
					ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
				#endif

				WorldViewDirection = SafeNormalize( WorldViewDirection );

				float2 uv_TextureSample2 = input.ase_texcoord9.xy * _TextureSample2_ST.xy + _TextureSample2_ST.zw;
				float4 BASETEXTURE243 = tex2D( _TextureSample2, uv_TextureSample2 );
				float4 color307 = IsGammaSpace() ? float4(1,1,1,0) : float4(1,1,1,0);
				float4 color315 = IsGammaSpace() ? float4(0.1294118,0.9803922,0.7921569,1) : float4(0.01520852,0.9559735,0.5906189,1);
				float2 uv_TextureSample9120 = input.ase_texcoord9.xy;
				float4 MASKTEXTURE251 = tex2D( _TextureSample9, uv_TextureSample9120 );
				float4 lerpResult309 = lerp( float4( 0,0,0,0 ) , ( BASETEXTURE243 * color307 ) , saturate( ( 1.0 - ( ( distance( color315.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color314 = IsGammaSpace() ? float4(0.9921569,0.9647059,0.0627451,1) : float4(0.9822509,0.921582,0.005181517,1);
				float4 lerpResult313 = lerp( lerpResult309 , ( BASETEXTURE243 * _Mortarcolor ) , saturate( ( 1.0 - ( ( distance( color314.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color130 = IsGammaSpace() ? float4(0.7529413,0.7529413,0.7529413,1) : float4(0.5271155,0.5271155,0.5271155,1);
				float4 lerpResult132 = lerp( lerpResult313 , ( BASETEXTURE243 * _Rock1color ) , saturate( ( 1.0 - ( ( distance( color130.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color134 = IsGammaSpace() ? float4(0.2901961,0.2862745,0.2901961,1) : float4(0.06847818,0.06662594,0.06847818,1);
				float4 lerpResult133 = lerp( lerpResult132 , ( BASETEXTURE243 * _Rock2color ) , saturate( ( 1.0 - ( ( distance( color134.rgb , MASKTEXTURE251.rgb ) - 0.04 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color121 = IsGammaSpace() ? float4(0.1058824,0.2352941,0.6980392,1) : float4(0.0109601,0.0451862,0.4452012,1);
				float4 lerpResult124 = lerp( lerpResult133 , ( BASETEXTURE243 * _Fabric1color ) , saturate( ( 1.0 - ( ( distance( color121.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color138 = IsGammaSpace() ? float4(0.1294118,0.2705882,0.8392158,1) : float4(0.01520852,0.05951123,0.6724434,1);
				float4 lerpResult126 = lerp( lerpResult124 , ( BASETEXTURE243 * _Fabric2color ) , saturate( ( 1.0 - ( ( distance( color138.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color136 = IsGammaSpace() ? float4(0.1294118,0.3176471,0.9921569,1) : float4(0.01520852,0.08228273,0.9822509,1);
				float4 lerpResult9 = lerp( lerpResult126 , ( BASETEXTURE243 * _Fabric3color ) , saturate( ( 1.0 - ( ( distance( color136.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color107 = IsGammaSpace() ? float4(0.04705883,0.6196079,0.2392157,1) : float4(0.003676507,0.3419145,0.04666509,1);
				float4 lerpResult10 = lerp( lerpResult9 , ( BASETEXTURE243 * _Leather1colour ) , saturate( ( 1.0 - ( ( distance( color107.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color106 = IsGammaSpace() ? float4(0.0627451,0.8274511,0.3098039,1) : float4(0.005181517,0.6514059,0.07818741,1);
				float4 lerpResult15 = lerp( lerpResult10 , ( BASETEXTURE243 * _leather2color ) , saturate( ( 1.0 - ( ( distance( color106.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color105 = IsGammaSpace() ? float4(0.0509804,0.9803922,0.3529412,1) : float4(0.004024717,0.9559735,0.1022417,1);
				float4 lerpResult19 = lerp( lerpResult15 , ( BASETEXTURE243 * _Leather3color ) , saturate( ( 1.0 - ( ( distance( color105.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color103 = IsGammaSpace() ? float4(0.3882353,0.254902,0.0627451,1) : float4(0.1247718,0.05286067,0.005181517,1);
				float4 lerpResult22 = lerp( lerpResult19 , ( BASETEXTURE243 * _Wood1color ) , saturate( ( 1.0 - ( ( distance( color103.rgb , MASKTEXTURE251.rgb ) - 0.05 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color104 = IsGammaSpace() ? float4(0.5333334,0.4431373,0.3215686,1) : float4(0.2462015,0.1651322,0.0843762,1);
				float4 lerpResult27 = lerp( lerpResult22 , ( BASETEXTURE243 * _Wood2color ) , saturate( ( 1.0 - ( ( distance( color104.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color102 = IsGammaSpace() ? float4(0.6470588,0.5333334,0.3882353,1) : float4(0.3762621,0.2462015,0.1247718,1);
				float4 lerpResult32 = lerp( lerpResult27 , ( BASETEXTURE243 * _Wood3color ) , saturate( ( 1.0 - ( ( distance( color102.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color17 = IsGammaSpace() ? float4(0.6039216,0.0627451,0.6470588,1) : float4(0.3231432,0.005181517,0.3762621,1);
				float temp_output_92_0 = saturate( ( 1.0 - ( ( distance( color17.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult91 = lerp( lerpResult32 , ( BASETEXTURE243 * _Ceramictiles1color ) , temp_output_92_0);
				float4 color24 = IsGammaSpace() ? float4(0.7529413,0.0627451,0.8196079,1) : float4(0.5271155,0.005181517,0.637597,1);
				float temp_output_95_0 = saturate( ( 1.0 - ( ( distance( color24.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult93 = lerp( lerpResult91 , ( BASETEXTURE243 * _Ceramictiles2color ) , temp_output_95_0);
				float4 color94 = IsGammaSpace() ? float4(0.9058824,0.0509804,0.9803922,1) : float4(0.799103,0.004024717,0.9559735,1);
				float temp_output_96_0 = saturate( ( 1.0 - ( ( distance( color94.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult39 = lerp( lerpResult93 , ( BASETEXTURE243 * _Ceramictiles3color ) , temp_output_96_0);
				float4 color84 = IsGammaSpace() ? float4(0.8627452,0.8470589,0.7215686,1) : float4(0.7156939,0.6866854,0.4793201,1);
				float4 lerpResult71 = lerp( lerpResult39 , ( BASETEXTURE243 * _Ropecolor ) , saturate( ( 1.0 - ( ( distance( color84.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color85 = IsGammaSpace() ? float4(0.8078432,0.7254902,0.5490196,1) : float4(0.6172068,0.48515,0.2622507,1);
				float4 lerpResult72 = lerp( lerpResult71 , ( BASETEXTURE243 * _Haycolor ) , saturate( ( 1.0 - ( ( distance( color85.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color38 = IsGammaSpace() ? float4(0.2784314,0.4470589,0.5490196,1) : float4(0.06301003,0.1682695,0.2622507,1);
				float temp_output_41_0 = saturate( ( 1.0 - ( ( distance( color38.rgb , MASKTEXTURE251.rgb ) - 0.05 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult47 = lerp( lerpResult72 , ( BASETEXTURE243 * _Metal1color ) , temp_output_41_0);
				float4 color117 = IsGammaSpace() ? float4(0.2470588,0.3803922,0.4627451,1) : float4(0.04970655,0.1195384,0.1811642,1);
				float temp_output_116_0 = saturate( ( 1.0 - ( ( distance( color117.rgb , MASKTEXTURE251.rgb ) - 0.05 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult150 = lerp( lerpResult47 , ( BASETEXTURE243 * _Metal2color ) , temp_output_116_0);
				float4 color118 = IsGammaSpace() ? float4(0.1921569,0.2941177,0.3529412,1) : float4(0.03071345,0.07036012,0.1022417,1);
				float temp_output_149_0 = saturate( ( 1.0 - ( ( distance( color118.rgb , MASKTEXTURE251.rgb ) - 0.05 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult151 = lerp( lerpResult150 , ( BASETEXTURE243 * _Metal3color ) , temp_output_149_0);
				float2 uv1_Coarofarmstexture157 = input.ase_texcoord9.zw;
				float temp_output_49_0 = ( 1.0 - tex2D( _Coarofarmstexture, uv1_Coarofarmstexture157 ).a );
				float4 temp_cast_42 = (temp_output_49_0).xxxx;
				float4 temp_output_1_0_g171 = temp_cast_42;
				float4 color54 = IsGammaSpace() ? float4(0,0,0,0) : float4(0,0,0,0);
				float4 temp_output_2_0_g171 = color54;
				float temp_output_11_0_g171 = distance( temp_output_1_0_g171 , temp_output_2_0_g171 );
				float2 _Vector0 = float2(1.6,1);
				float4 lerpResult21_g171 = lerp( _Coatofarmscolor , temp_output_1_0_g171 , saturate( ( ( temp_output_11_0_g171 - _Vector0.x ) / max( _Vector0.y , 1E-05 ) ) ));
				float temp_output_156_0 = ( 1.0 - temp_output_49_0 );
				float4 lerpResult165 = lerp( lerpResult151 , lerpResult21_g171 , temp_output_156_0);
				
				float lerpResult110 = lerp( 0.0 , _Metal1metallic , temp_output_41_0);
				float lerpResult112 = lerp( lerpResult110 , _Metal2metallic , temp_output_116_0);
				float lerpResult113 = lerp( lerpResult112 , _Metal3metallic , temp_output_149_0);
				float lerpResult55 = lerp( lerpResult113 , 0.0 , temp_output_156_0);
				
				float lerpResult26 = lerp( 0.0 , _Ceramic1smoothness , temp_output_92_0);
				float lerpResult31 = lerp( lerpResult26 , _Ceramic2smoothness , temp_output_95_0);
				float lerpResult34 = lerp( lerpResult31 , _Ceramic3smoothness , temp_output_96_0);
				float lerpResult42 = lerp( lerpResult34 , _Metal1smootness , temp_output_41_0);
				float lerpResult43 = lerp( lerpResult42 , _Metal2smootness , temp_output_116_0);
				float lerpResult46 = lerp( lerpResult43 , _Metal3smootness , temp_output_149_0);
				

				float3 BaseColor = lerpResult165.rgb;
				float3 Normal = float3(0, 0, 1);
				float3 Emission = 0;
				float3 Specular = 0.5;
				float Metallic = (( _MetallicOn )?( lerpResult55 ):( 0.0 ));
				float Smoothness = (( _SmoothnessOn )?( lerpResult46 ):( 0.0 ));
				float Occlusion = 1;
				float Alpha = 1;
				float AlphaClipThreshold = 0.5;
				float AlphaClipThresholdShadow = 0.5;
				float3 BakedGI = 0;
				float3 RefractionColor = 1;
				float RefractionIndex = 1;
				float3 Transmission = 1;
				float3 Translucency = 1;

				#ifdef ASE_DEPTH_WRITE_ON
					float DepthValue = input.positionCS.z;
				#endif

				#ifdef _CLEARCOAT
					float CoatMask = 0;
					float CoatSmoothness = 0;
				#endif

				#ifdef _ALPHATEST_ON
					clip(Alpha - AlphaClipThreshold);
				#endif

				InputData inputData = (InputData)0;
				inputData.positionWS = WorldPosition;
				inputData.positionCS = input.positionCS;
				inputData.viewDirectionWS = WorldViewDirection;

				#ifdef _NORMALMAP
						#if _NORMAL_DROPOFF_TS
							inputData.normalWS = TransformTangentToWorld(Normal, half3x3(WorldTangent, WorldBiTangent, WorldNormal));
						#elif _NORMAL_DROPOFF_OS
							inputData.normalWS = TransformObjectToWorldNormal(Normal);
						#elif _NORMAL_DROPOFF_WS
							inputData.normalWS = Normal;
						#endif
					inputData.normalWS = NormalizeNormalPerPixel(inputData.normalWS);
				#else
					inputData.normalWS = WorldNormal;
				#endif

				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
					inputData.shadowCoord = ShadowCoords;
				#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
					inputData.shadowCoord = TransformWorldToShadowCoord(inputData.positionWS);
				#else
					inputData.shadowCoord = float4(0, 0, 0, 0);
				#endif

				#ifdef ASE_FOG
					inputData.fogCoord = InitializeInputDataFog(float4(inputData.positionWS, 1.0), input.fogFactorAndVertexLight.x);
				#endif
				#ifdef _ADDITIONAL_LIGHTS_VERTEX
					inputData.vertexLighting = input.fogFactorAndVertexLight.yzw;
				#endif

				#if defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
					float3 SH = SampleSH(inputData.normalWS.xyz);
				#else
					float3 SH = input.lightmapUVOrVertexSH.xyz;
				#endif

				#if defined(DYNAMICLIGHTMAP_ON)
					inputData.bakedGI = SAMPLE_GI(input.lightmapUVOrVertexSH.xy, input.dynamicLightmapUV.xy, SH, inputData.normalWS);
					inputData.shadowMask = SAMPLE_SHADOWMASK(input.lightmapUVOrVertexSH.xy);
				#elif !defined(LIGHTMAP_ON) && (defined(PROBE_VOLUMES_L1) || defined(PROBE_VOLUMES_L2))
					inputData.bakedGI = SAMPLE_GI( SH, GetAbsolutePositionWS(inputData.positionWS),
						inputData.normalWS,
						inputData.viewDirectionWS,
						input.positionCS.xy,
						input.probeOcclusion,
						inputData.shadowMask );
				#else
					inputData.bakedGI = SAMPLE_GI(input.lightmapUVOrVertexSH.xy, SH, inputData.normalWS);
					inputData.shadowMask = SAMPLE_SHADOWMASK(input.lightmapUVOrVertexSH.xy);
				#endif

				#ifdef ASE_BAKEDGI
					inputData.bakedGI = BakedGI;
				#endif

				inputData.normalizedScreenSpaceUV = NormalizedScreenSpaceUV;

				#if defined(DEBUG_DISPLAY)
					#if defined(DYNAMICLIGHTMAP_ON)
						inputData.dynamicLightmapUV = input.dynamicLightmapUV.xy;
					#endif
					#if defined(LIGHTMAP_ON)
						inputData.staticLightmapUV = input.lightmapUVOrVertexSH.xy;
					#else
						inputData.vertexSH = SH;
					#endif
					#if defined(USE_APV_PROBE_OCCLUSION)
						inputData.probeOcclusion = input.probeOcclusion;
					#endif
				#endif

				SurfaceData surfaceData;
				surfaceData.albedo              = BaseColor;
				surfaceData.metallic            = saturate(Metallic);
				surfaceData.specular            = Specular;
				surfaceData.smoothness          = saturate(Smoothness),
				surfaceData.occlusion           = Occlusion,
				surfaceData.emission            = Emission,
				surfaceData.alpha               = saturate(Alpha);
				surfaceData.normalTS            = Normal;
				surfaceData.clearCoatMask       = 0;
				surfaceData.clearCoatSmoothness = 1;

				#ifdef _CLEARCOAT
					surfaceData.clearCoatMask       = saturate(CoatMask);
					surfaceData.clearCoatSmoothness = saturate(CoatSmoothness);
				#endif

				#ifdef _DBUFFER
					ApplyDecalToSurfaceData(input.positionCS, surfaceData, inputData);
				#endif

				#ifdef _ASE_LIGHTING_SIMPLE
					half4 color = UniversalFragmentBlinnPhong( inputData, surfaceData);
				#else
					half4 color = UniversalFragmentPBR( inputData, surfaceData);
				#endif

				#ifdef ASE_TRANSMISSION
				{
					float shadow = _TransmissionShadow;

					#define SUM_LIGHT_TRANSMISSION(Light)\
						float3 atten = Light.color * Light.distanceAttenuation;\
						atten = lerp( atten, atten * Light.shadowAttenuation, shadow );\
						half3 transmission = max( 0, -dot( inputData.normalWS, Light.direction ) ) * atten * Transmission;\
						color.rgb += BaseColor * transmission;

					SUM_LIGHT_TRANSMISSION( GetMainLight( inputData.shadowCoord ) );

					#if defined(_ADDITIONAL_LIGHTS)
						uint meshRenderingLayers = GetMeshRenderingLayer();
						uint pixelLightCount = GetAdditionalLightsCount();
						#if USE_FORWARD_PLUS
							[loop] for (uint lightIndex = 0; lightIndex < min(URP_FP_DIRECTIONAL_LIGHTS_COUNT, MAX_VISIBLE_LIGHTS); lightIndex++)
							{
								FORWARD_PLUS_SUBTRACTIVE_LIGHT_CHECK

								Light light = GetAdditionalLight(lightIndex, inputData.positionWS, inputData.shadowMask);
								#ifdef _LIGHT_LAYERS
								if (IsMatchingLightLayer(light.layerMask, meshRenderingLayers))
								#endif
								{
									SUM_LIGHT_TRANSMISSION( light );
								}
							}
						#endif
						LIGHT_LOOP_BEGIN( pixelLightCount )
							Light light = GetAdditionalLight(lightIndex, inputData.positionWS, inputData.shadowMask);
							#ifdef _LIGHT_LAYERS
							if (IsMatchingLightLayer(light.layerMask, meshRenderingLayers))
							#endif
							{
								SUM_LIGHT_TRANSMISSION( light );
							}
						LIGHT_LOOP_END
					#endif
				}
				#endif

				#ifdef ASE_TRANSLUCENCY
				{
					float shadow = _TransShadow;
					float normal = _TransNormal;
					float scattering = _TransScattering;
					float direct = _TransDirect;
					float ambient = _TransAmbient;
					float strength = _TransStrength;

					#define SUM_LIGHT_TRANSLUCENCY(Light)\
						float3 atten = Light.color * Light.distanceAttenuation;\
						atten = lerp( atten, atten * Light.shadowAttenuation, shadow );\
						half3 lightDir = Light.direction + inputData.normalWS * normal;\
						half VdotL = pow( saturate( dot( inputData.viewDirectionWS, -lightDir ) ), scattering );\
						half3 translucency = atten * ( VdotL * direct + inputData.bakedGI * ambient ) * Translucency;\
						color.rgb += BaseColor * translucency * strength;

					SUM_LIGHT_TRANSLUCENCY( GetMainLight( inputData.shadowCoord ) );

					#if defined(_ADDITIONAL_LIGHTS)
						uint meshRenderingLayers = GetMeshRenderingLayer();
						uint pixelLightCount = GetAdditionalLightsCount();
						#if USE_FORWARD_PLUS
							[loop] for (uint lightIndex = 0; lightIndex < min(URP_FP_DIRECTIONAL_LIGHTS_COUNT, MAX_VISIBLE_LIGHTS); lightIndex++)
							{
								FORWARD_PLUS_SUBTRACTIVE_LIGHT_CHECK

								Light light = GetAdditionalLight(lightIndex, inputData.positionWS, inputData.shadowMask);
								#ifdef _LIGHT_LAYERS
								if (IsMatchingLightLayer(light.layerMask, meshRenderingLayers))
								#endif
								{
									SUM_LIGHT_TRANSLUCENCY( light );
								}
							}
						#endif
						LIGHT_LOOP_BEGIN( pixelLightCount )
							Light light = GetAdditionalLight(lightIndex, inputData.positionWS, inputData.shadowMask);
							#ifdef _LIGHT_LAYERS
							if (IsMatchingLightLayer(light.layerMask, meshRenderingLayers))
							#endif
							{
								SUM_LIGHT_TRANSLUCENCY( light );
							}
						LIGHT_LOOP_END
					#endif
				}
				#endif

				#ifdef ASE_REFRACTION
					float4 projScreenPos = ScreenPos / ScreenPos.w;
					float3 refractionOffset = ( RefractionIndex - 1.0 ) * mul( UNITY_MATRIX_V, float4( WorldNormal,0 ) ).xyz * ( 1.0 - dot( WorldNormal, WorldViewDirection ) );
					projScreenPos.xy += refractionOffset.xy;
					float3 refraction = SHADERGRAPH_SAMPLE_SCENE_COLOR( projScreenPos.xy ) * RefractionColor;
					color.rgb = lerp( refraction, color.rgb, color.a );
					color.a = 1;
				#endif

				#ifdef ASE_FINAL_COLOR_ALPHA_MULTIPLY
					color.rgb *= color.a;
				#endif

				#ifdef ASE_FOG
					#ifdef TERRAIN_SPLAT_ADDPASS
						color.rgb = MixFogColor(color.rgb, half3(0,0,0), inputData.fogCoord);
					#else
						color.rgb = MixFog(color.rgb, inputData.fogCoord);
					#endif
				#endif

				#ifdef ASE_DEPTH_WRITE_ON
					outputDepth = DepthValue;
				#endif

				#ifdef _WRITE_RENDERING_LAYERS
					uint renderingLayers = GetMeshRenderingLayer();
					outRenderingLayers = float4( EncodeMeshRenderingLayer( renderingLayers ), 0, 0, 0 );
				#endif

				return color;
			}
			ENDHLSL
		}

		
		Pass
		{
			
			Name "ShadowCaster"
			Tags { "LightMode"="ShadowCaster" }

			ZWrite On
			ZTest LEqual
			AlphaToMask Off
			ColorMask 0

			HLSLPROGRAM

			#pragma multi_compile _ALPHATEST_ON
			#define _NORMAL_DROPOFF_TS 1
			#pragma multi_compile_instancing
			#pragma multi_compile _ LOD_FADE_CROSSFADE
			#define ASE_FOG 1
			#define ASE_VERSION 19800
			#define ASE_SRP_VERSION 170003


			#pragma multi_compile_vertex _ _CASTING_PUNCTUAL_LIGHT_SHADOW

			#pragma vertex vert
			#pragma fragment frag

			#if defined(_SPECULAR_SETUP) && defined(_ASE_LIGHTING_SIMPLE)
				#define _SPECULAR_COLOR 1
			#endif

			#define SHADERPASS SHADERPASS_SHADOWCASTER

			#include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
            #include_with_pragmas "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRenderingKeywords.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRendering.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/DebugMipmapStreamingMacros.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

			#if defined(LOD_FADE_CROSSFADE)
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/LODCrossFade.hlsl"
            #endif

			

			#if defined(ASE_EARLY_Z_DEPTH_OPTIMIZE) && (SHADER_TARGET >= 45)
				#define ASE_SV_DEPTH SV_DepthLessEqual
				#define ASE_SV_POSITION_QUALIFIERS linear noperspective centroid
			#else
				#define ASE_SV_DEPTH SV_Depth
				#define ASE_SV_POSITION_QUALIFIERS
			#endif

			struct Attributes
			{
				float4 positionOS : POSITION;
				float3 normalOS : NORMAL;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct PackedVaryings
			{
				ASE_SV_POSITION_QUALIFIERS float4 positionCS : SV_POSITION;
				float4 clipPosV : TEXCOORD0;
				float3 positionWS : TEXCOORD1;
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					float4 shadowCoord : TEXCOORD2;
				#endif
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _TextureSample2_ST;
			float4 _Coatofarmscolor;
			float4 _Metal3color;
			float4 _Metal2color;
			float4 _Metal1color;
			float4 _Haycolor;
			float4 _Ceramictiles3color;
			float4 _Ceramictiles2color;
			float4 _Ceramictiles1color;
			float4 _Wood3color;
			float4 _Wood2color;
			float4 _Ropecolor;
			float4 _Leather3color;
			float4 _leather2color;
			float4 _Leather1colour;
			float4 _Fabric3color;
			float4 _Fabric2color;
			float4 _Fabric1color;
			float4 _Rock2color;
			float4 _Rock1color;
			float4 _Mortarcolor;
			float4 _Wood1color;
			float _Metal1smootness;
			float _Ceramic3smoothness;
			float _Ceramic2smoothness;
			float _Ceramic1smoothness;
			float _MetallicOn;
			float _Metal3metallic;
			float _Metal2metallic;
			float _Metal1metallic;
			float _Metal2smootness;
			float _SmoothnessOn;
			float _Metal3smootness;
			#ifdef ASE_TRANSMISSION
				float _TransmissionShadow;
			#endif
			#ifdef ASE_TRANSLUCENCY
				float _TransStrength;
				float _TransNormal;
				float _TransScattering;
				float _TransDirect;
				float _TransAmbient;
				float _TransShadow;
			#endif
			#ifdef ASE_TESSELLATION
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END

			#ifdef SCENEPICKINGPASS
				float4 _SelectionID;
			#endif

			#ifdef SCENESELECTIONPASS
				int _ObjectId;
				int _PassValue;
			#endif

			

			
			float3 _LightDirection;
			float3 _LightPosition;

			PackedVaryings VertexFunction( Attributes input )
			{
				PackedVaryings output;
				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_TRANSFER_INSTANCE_ID(input, output);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( output );

				

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = input.positionOS.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif

				float3 vertexValue = defaultVertexValue;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					input.positionOS.xyz = vertexValue;
				#else
					input.positionOS.xyz += vertexValue;
				#endif

				input.normalOS = input.normalOS;

				float3 positionWS = TransformObjectToWorld( input.positionOS.xyz );
				float3 normalWS = TransformObjectToWorldDir(input.normalOS);

				#if _CASTING_PUNCTUAL_LIGHT_SHADOW
					float3 lightDirectionWS = normalize(_LightPosition - positionWS);
				#else
					float3 lightDirectionWS = _LightDirection;
				#endif

				float4 positionCS = TransformWorldToHClip(ApplyShadowBias(positionWS, normalWS, lightDirectionWS));

				//code for UNITY_REVERSED_Z is moved into Shadows.hlsl from 6000.0.22 and or higher
				positionCS = ApplyShadowClamping(positionCS);

				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					VertexPositionInputs vertexInput = (VertexPositionInputs)0;
					vertexInput.positionWS = positionWS;
					vertexInput.positionCS = positionCS;
					output.shadowCoord = GetShadowCoord( vertexInput );
				#endif

				output.positionCS = positionCS;
				output.clipPosV = positionCS;
				output.positionWS = positionWS;
				return output;
			}

			#if defined(ASE_TESSELLATION)
			struct VertexControl
			{
				float4 positionOS : INTERNALTESSPOS;
				float3 normalOS : NORMAL;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( Attributes input )
			{
				VertexControl output;
				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_TRANSFER_INSTANCE_ID(input, output);
				output.positionOS = input.positionOS;
				output.normalOS = input.normalOS;
				
				return output;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> input)
			{
				TessellationFactors output;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(input[0].positionOS, input[1].positionOS, input[2].positionOS, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(input[0].positionOS, input[1].positionOS, input[2].positionOS, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(input[0].positionOS, input[1].positionOS, input[2].positionOS, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				output.edge[0] = tf.x; output.edge[1] = tf.y; output.edge[2] = tf.z; output.inside = tf.w;
				return output;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
				return patch[id];
			}

			[domain("tri")]
			PackedVaryings DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				Attributes output = (Attributes) 0;
				output.positionOS = patch[0].positionOS * bary.x + patch[1].positionOS * bary.y + patch[2].positionOS * bary.z;
				output.normalOS = patch[0].normalOS * bary.x + patch[1].normalOS * bary.y + patch[2].normalOS * bary.z;
				
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = output.positionOS.xyz - patch[i].normalOS * (dot(output.positionOS.xyz, patch[i].normalOS) - dot(patch[i].positionOS.xyz, patch[i].normalOS));
				float phongStrength = _TessPhongStrength;
				output.positionOS.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * output.positionOS.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], output);
				return VertexFunction(output);
			}
			#else
			PackedVaryings vert ( Attributes input )
			{
				return VertexFunction( input );
			}
			#endif

			half4 frag(	PackedVaryings input
						#ifdef ASE_DEPTH_WRITE_ON
						,out float outputDepth : ASE_SV_DEPTH
						#endif
						 ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( input );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( input );

				float3 WorldPosition = input.positionWS;
				float4 ShadowCoords = float4( 0, 0, 0, 0 );
				float4 ClipPos = input.clipPosV;
				float4 ScreenPos = ComputeScreenPos( input.clipPosV );

				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
						ShadowCoords = input.shadowCoord;
					#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
						ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
					#endif
				#endif

				

				float Alpha = 1;
				float AlphaClipThreshold = 0.5;
				float AlphaClipThresholdShadow = 0.5;

				#ifdef ASE_DEPTH_WRITE_ON
					float DepthValue = input.positionCS.z;
				#endif

				#ifdef _ALPHATEST_ON
					#ifdef _ALPHATEST_SHADOW_ON
						clip(Alpha - AlphaClipThresholdShadow);
					#else
						clip(Alpha - AlphaClipThreshold);
					#endif
				#endif

				#if defined(LOD_FADE_CROSSFADE)
					LODFadeCrossFade( input.positionCS );
				#endif

				#ifdef ASE_DEPTH_WRITE_ON
					outputDepth = DepthValue;
				#endif

				return 0;
			}
			ENDHLSL
		}

		
		Pass
		{
			
			Name "DepthOnly"
			Tags { "LightMode"="DepthOnly" }

			ZWrite On
			ColorMask R
			AlphaToMask Off

			HLSLPROGRAM

			#pragma multi_compile _ALPHATEST_ON
			#define _NORMAL_DROPOFF_TS 1
			#pragma multi_compile_instancing
			#pragma multi_compile _ LOD_FADE_CROSSFADE
			#define ASE_FOG 1
			#define ASE_VERSION 19800
			#define ASE_SRP_VERSION 170003


			#pragma vertex vert
			#pragma fragment frag

			#if defined(_SPECULAR_SETUP) && defined(_ASE_LIGHTING_SIMPLE)
				#define _SPECULAR_COLOR 1
			#endif

			#define SHADERPASS SHADERPASS_DEPTHONLY

			#include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
            #include_with_pragmas "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRenderingKeywords.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRendering.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

			#if defined(LOD_FADE_CROSSFADE)
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/LODCrossFade.hlsl"
            #endif

			

			#if defined(ASE_EARLY_Z_DEPTH_OPTIMIZE) && (SHADER_TARGET >= 45)
				#define ASE_SV_DEPTH SV_DepthLessEqual
				#define ASE_SV_POSITION_QUALIFIERS linear noperspective centroid
			#else
				#define ASE_SV_DEPTH SV_Depth
				#define ASE_SV_POSITION_QUALIFIERS
			#endif

			struct Attributes
			{
				float4 positionOS : POSITION;
				float3 normalOS : NORMAL;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct PackedVaryings
			{
				ASE_SV_POSITION_QUALIFIERS float4 positionCS : SV_POSITION;
				float4 clipPosV : TEXCOORD0;
				float3 positionWS : TEXCOORD1;
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					float4 shadowCoord : TEXCOORD2;
				#endif
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _TextureSample2_ST;
			float4 _Coatofarmscolor;
			float4 _Metal3color;
			float4 _Metal2color;
			float4 _Metal1color;
			float4 _Haycolor;
			float4 _Ceramictiles3color;
			float4 _Ceramictiles2color;
			float4 _Ceramictiles1color;
			float4 _Wood3color;
			float4 _Wood2color;
			float4 _Ropecolor;
			float4 _Leather3color;
			float4 _leather2color;
			float4 _Leather1colour;
			float4 _Fabric3color;
			float4 _Fabric2color;
			float4 _Fabric1color;
			float4 _Rock2color;
			float4 _Rock1color;
			float4 _Mortarcolor;
			float4 _Wood1color;
			float _Metal1smootness;
			float _Ceramic3smoothness;
			float _Ceramic2smoothness;
			float _Ceramic1smoothness;
			float _MetallicOn;
			float _Metal3metallic;
			float _Metal2metallic;
			float _Metal1metallic;
			float _Metal2smootness;
			float _SmoothnessOn;
			float _Metal3smootness;
			#ifdef ASE_TRANSMISSION
				float _TransmissionShadow;
			#endif
			#ifdef ASE_TRANSLUCENCY
				float _TransStrength;
				float _TransNormal;
				float _TransScattering;
				float _TransDirect;
				float _TransAmbient;
				float _TransShadow;
			#endif
			#ifdef ASE_TESSELLATION
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END

			#ifdef SCENEPICKINGPASS
				float4 _SelectionID;
			#endif

			#ifdef SCENESELECTIONPASS
				int _ObjectId;
				int _PassValue;
			#endif

			

			
			PackedVaryings VertexFunction( Attributes input  )
			{
				PackedVaryings output = (PackedVaryings)0;
				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_TRANSFER_INSTANCE_ID(input, output);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

				

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = input.positionOS.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif

				float3 vertexValue = defaultVertexValue;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					input.positionOS.xyz = vertexValue;
				#else
					input.positionOS.xyz += vertexValue;
				#endif

				input.normalOS = input.normalOS;

				VertexPositionInputs vertexInput = GetVertexPositionInputs( input.positionOS.xyz );

				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					output.shadowCoord = GetShadowCoord( vertexInput );
				#endif

				output.positionCS = vertexInput.positionCS;
				output.clipPosV = vertexInput.positionCS;
				output.positionWS = vertexInput.positionWS;
				return output;
			}

			#if defined(ASE_TESSELLATION)
			struct VertexControl
			{
				float4 positionOS : INTERNALTESSPOS;
				float3 normalOS : NORMAL;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( Attributes input )
			{
				VertexControl output;
				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_TRANSFER_INSTANCE_ID(input, output);
				output.positionOS = input.positionOS;
				output.normalOS = input.normalOS;
				
				return output;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> input)
			{
				TessellationFactors output;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(input[0].positionOS, input[1].positionOS, input[2].positionOS, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(input[0].positionOS, input[1].positionOS, input[2].positionOS, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(input[0].positionOS, input[1].positionOS, input[2].positionOS, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				output.edge[0] = tf.x; output.edge[1] = tf.y; output.edge[2] = tf.z; output.inside = tf.w;
				return output;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
				return patch[id];
			}

			[domain("tri")]
			PackedVaryings DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				Attributes output = (Attributes) 0;
				output.positionOS = patch[0].positionOS * bary.x + patch[1].positionOS * bary.y + patch[2].positionOS * bary.z;
				output.normalOS = patch[0].normalOS * bary.x + patch[1].normalOS * bary.y + patch[2].normalOS * bary.z;
				
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = output.positionOS.xyz - patch[i].normalOS * (dot(output.positionOS.xyz, patch[i].normalOS) - dot(patch[i].positionOS.xyz, patch[i].normalOS));
				float phongStrength = _TessPhongStrength;
				output.positionOS.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * output.positionOS.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], output);
				return VertexFunction(output);
			}
			#else
			PackedVaryings vert ( Attributes input )
			{
				return VertexFunction( input );
			}
			#endif

			half4 frag(	PackedVaryings input
						#ifdef ASE_DEPTH_WRITE_ON
						,out float outputDepth : ASE_SV_DEPTH
						#endif
						 ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( input );

				float3 WorldPosition = input.positionWS;
				float4 ShadowCoords = float4( 0, 0, 0, 0 );
				float4 ClipPos = input.clipPosV;
				float4 ScreenPos = ComputeScreenPos( input.clipPosV );

				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
						ShadowCoords = input.shadowCoord;
					#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
						ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
					#endif
				#endif

				

				float Alpha = 1;
				float AlphaClipThreshold = 0.5;

				#ifdef ASE_DEPTH_WRITE_ON
					float DepthValue = input.positionCS.z;
				#endif

				#ifdef _ALPHATEST_ON
					clip(Alpha - AlphaClipThreshold);
				#endif

				#if defined(LOD_FADE_CROSSFADE)
					LODFadeCrossFade( input.positionCS );
				#endif

				#ifdef ASE_DEPTH_WRITE_ON
					outputDepth = DepthValue;
				#endif

				return 0;
			}
			ENDHLSL
		}

		
		Pass
		{
			
			Name "Meta"
			Tags { "LightMode"="Meta" }

			Cull Off

			HLSLPROGRAM
			#pragma multi_compile_fragment _ALPHATEST_ON
			#define _NORMAL_DROPOFF_TS 1
			#define ASE_FOG 1
			#define ASE_VERSION 19800
			#define ASE_SRP_VERSION 170003

			#pragma shader_feature EDITOR_VISUALIZATION

			#pragma vertex vert
			#pragma fragment frag

			#if defined(_SPECULAR_SETUP) && defined(_ASE_LIGHTING_SIMPLE)
				#define _SPECULAR_COLOR 1
			#endif

			#define SHADERPASS SHADERPASS_META

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
            #include_with_pragmas "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRenderingKeywords.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRendering.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/DebugMipmapStreamingMacros.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/MetaInput.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

			

			struct Attributes
			{
				float4 positionOS : POSITION;
				float3 normalOS : NORMAL;
				float4 texcoord0 : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct PackedVaryings
			{
				float4 positionCS : SV_POSITION;
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
					float3 positionWS : TEXCOORD0;
				#endif
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					float4 shadowCoord : TEXCOORD1;
				#endif
				#ifdef EDITOR_VISUALIZATION
					float4 VizUV : TEXCOORD2;
					float4 LightCoord : TEXCOORD3;
				#endif
				float4 ase_texcoord4 : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _TextureSample2_ST;
			float4 _Coatofarmscolor;
			float4 _Metal3color;
			float4 _Metal2color;
			float4 _Metal1color;
			float4 _Haycolor;
			float4 _Ceramictiles3color;
			float4 _Ceramictiles2color;
			float4 _Ceramictiles1color;
			float4 _Wood3color;
			float4 _Wood2color;
			float4 _Ropecolor;
			float4 _Leather3color;
			float4 _leather2color;
			float4 _Leather1colour;
			float4 _Fabric3color;
			float4 _Fabric2color;
			float4 _Fabric1color;
			float4 _Rock2color;
			float4 _Rock1color;
			float4 _Mortarcolor;
			float4 _Wood1color;
			float _Metal1smootness;
			float _Ceramic3smoothness;
			float _Ceramic2smoothness;
			float _Ceramic1smoothness;
			float _MetallicOn;
			float _Metal3metallic;
			float _Metal2metallic;
			float _Metal1metallic;
			float _Metal2smootness;
			float _SmoothnessOn;
			float _Metal3smootness;
			#ifdef ASE_TRANSMISSION
				float _TransmissionShadow;
			#endif
			#ifdef ASE_TRANSLUCENCY
				float _TransStrength;
				float _TransNormal;
				float _TransScattering;
				float _TransDirect;
				float _TransAmbient;
				float _TransShadow;
			#endif
			#ifdef ASE_TESSELLATION
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END

			#ifdef SCENEPICKINGPASS
				float4 _SelectionID;
			#endif

			#ifdef SCENESELECTIONPASS
				int _ObjectId;
				int _PassValue;
			#endif

			sampler2D _TextureSample2;
			sampler2D _TextureSample9;
			sampler2D _Coarofarmstexture;


			
			PackedVaryings VertexFunction( Attributes input  )
			{
				PackedVaryings output = (PackedVaryings)0;
				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_TRANSFER_INSTANCE_ID(input, output);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

				output.ase_texcoord4.xy = input.texcoord0.xy;
				output.ase_texcoord4.zw = input.texcoord1.xy;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = input.positionOS.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif

				float3 vertexValue = defaultVertexValue;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					input.positionOS.xyz = vertexValue;
				#else
					input.positionOS.xyz += vertexValue;
				#endif

				input.normalOS = input.normalOS;

				float3 positionWS = TransformObjectToWorld( input.positionOS.xyz );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
					output.positionWS = positionWS;
				#endif

				output.positionCS = MetaVertexPosition( input.positionOS, input.texcoord1.xy, input.texcoord1.xy, unity_LightmapST, unity_DynamicLightmapST );

				#ifdef EDITOR_VISUALIZATION
					float2 VizUV = 0;
					float4 LightCoord = 0;
					UnityEditorVizData(input.positionOS.xyz, input.texcoord0.xy, input.texcoord1.xy, input.texcoord2.xy, VizUV, LightCoord);
					output.VizUV = float4(VizUV, 0, 0);
					output.LightCoord = LightCoord;
				#endif

				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					VertexPositionInputs vertexInput = (VertexPositionInputs)0;
					vertexInput.positionWS = positionWS;
					vertexInput.positionCS = output.positionCS;
					output.shadowCoord = GetShadowCoord( vertexInput );
				#endif

				return output;
			}

			#if defined(ASE_TESSELLATION)
			struct VertexControl
			{
				float4 positionOS : INTERNALTESSPOS;
				float3 normalOS : NORMAL;
				float4 texcoord0 : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( Attributes input )
			{
				VertexControl output;
				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_TRANSFER_INSTANCE_ID(input, output);
				output.positionOS = input.positionOS;
				output.normalOS = input.normalOS;
				output.texcoord0 = input.texcoord0;
				output.texcoord1 = input.texcoord1;
				output.texcoord2 = input.texcoord2;
				
				return output;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> input)
			{
				TessellationFactors output;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(input[0].positionOS, input[1].positionOS, input[2].positionOS, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(input[0].positionOS, input[1].positionOS, input[2].positionOS, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(input[0].positionOS, input[1].positionOS, input[2].positionOS, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				output.edge[0] = tf.x; output.edge[1] = tf.y; output.edge[2] = tf.z; output.inside = tf.w;
				return output;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
				return patch[id];
			}

			[domain("tri")]
			PackedVaryings DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				Attributes output = (Attributes) 0;
				output.positionOS = patch[0].positionOS * bary.x + patch[1].positionOS * bary.y + patch[2].positionOS * bary.z;
				output.normalOS = patch[0].normalOS * bary.x + patch[1].normalOS * bary.y + patch[2].normalOS * bary.z;
				output.texcoord0 = patch[0].texcoord0 * bary.x + patch[1].texcoord0 * bary.y + patch[2].texcoord0 * bary.z;
				output.texcoord1 = patch[0].texcoord1 * bary.x + patch[1].texcoord1 * bary.y + patch[2].texcoord1 * bary.z;
				output.texcoord2 = patch[0].texcoord2 * bary.x + patch[1].texcoord2 * bary.y + patch[2].texcoord2 * bary.z;
				
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = output.positionOS.xyz - patch[i].normalOS * (dot(output.positionOS.xyz, patch[i].normalOS) - dot(patch[i].positionOS.xyz, patch[i].normalOS));
				float phongStrength = _TessPhongStrength;
				output.positionOS.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * output.positionOS.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], output);
				return VertexFunction(output);
			}
			#else
			PackedVaryings vert ( Attributes input )
			{
				return VertexFunction( input );
			}
			#endif

			half4 frag(PackedVaryings input  ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( input );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
					float3 WorldPosition = input.positionWS;
				#endif

				float4 ShadowCoords = float4( 0, 0, 0, 0 );

				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
						ShadowCoords = input.shadowCoord;
					#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
						ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
					#endif
				#endif

				float2 uv_TextureSample2 = input.ase_texcoord4.xy * _TextureSample2_ST.xy + _TextureSample2_ST.zw;
				float4 BASETEXTURE243 = tex2D( _TextureSample2, uv_TextureSample2 );
				float4 color307 = IsGammaSpace() ? float4(1,1,1,0) : float4(1,1,1,0);
				float4 color315 = IsGammaSpace() ? float4(0.1294118,0.9803922,0.7921569,1) : float4(0.01520852,0.9559735,0.5906189,1);
				float2 uv_TextureSample9120 = input.ase_texcoord4.xy;
				float4 MASKTEXTURE251 = tex2D( _TextureSample9, uv_TextureSample9120 );
				float4 lerpResult309 = lerp( float4( 0,0,0,0 ) , ( BASETEXTURE243 * color307 ) , saturate( ( 1.0 - ( ( distance( color315.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color314 = IsGammaSpace() ? float4(0.9921569,0.9647059,0.0627451,1) : float4(0.9822509,0.921582,0.005181517,1);
				float4 lerpResult313 = lerp( lerpResult309 , ( BASETEXTURE243 * _Mortarcolor ) , saturate( ( 1.0 - ( ( distance( color314.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color130 = IsGammaSpace() ? float4(0.7529413,0.7529413,0.7529413,1) : float4(0.5271155,0.5271155,0.5271155,1);
				float4 lerpResult132 = lerp( lerpResult313 , ( BASETEXTURE243 * _Rock1color ) , saturate( ( 1.0 - ( ( distance( color130.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color134 = IsGammaSpace() ? float4(0.2901961,0.2862745,0.2901961,1) : float4(0.06847818,0.06662594,0.06847818,1);
				float4 lerpResult133 = lerp( lerpResult132 , ( BASETEXTURE243 * _Rock2color ) , saturate( ( 1.0 - ( ( distance( color134.rgb , MASKTEXTURE251.rgb ) - 0.04 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color121 = IsGammaSpace() ? float4(0.1058824,0.2352941,0.6980392,1) : float4(0.0109601,0.0451862,0.4452012,1);
				float4 lerpResult124 = lerp( lerpResult133 , ( BASETEXTURE243 * _Fabric1color ) , saturate( ( 1.0 - ( ( distance( color121.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color138 = IsGammaSpace() ? float4(0.1294118,0.2705882,0.8392158,1) : float4(0.01520852,0.05951123,0.6724434,1);
				float4 lerpResult126 = lerp( lerpResult124 , ( BASETEXTURE243 * _Fabric2color ) , saturate( ( 1.0 - ( ( distance( color138.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color136 = IsGammaSpace() ? float4(0.1294118,0.3176471,0.9921569,1) : float4(0.01520852,0.08228273,0.9822509,1);
				float4 lerpResult9 = lerp( lerpResult126 , ( BASETEXTURE243 * _Fabric3color ) , saturate( ( 1.0 - ( ( distance( color136.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color107 = IsGammaSpace() ? float4(0.04705883,0.6196079,0.2392157,1) : float4(0.003676507,0.3419145,0.04666509,1);
				float4 lerpResult10 = lerp( lerpResult9 , ( BASETEXTURE243 * _Leather1colour ) , saturate( ( 1.0 - ( ( distance( color107.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color106 = IsGammaSpace() ? float4(0.0627451,0.8274511,0.3098039,1) : float4(0.005181517,0.6514059,0.07818741,1);
				float4 lerpResult15 = lerp( lerpResult10 , ( BASETEXTURE243 * _leather2color ) , saturate( ( 1.0 - ( ( distance( color106.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color105 = IsGammaSpace() ? float4(0.0509804,0.9803922,0.3529412,1) : float4(0.004024717,0.9559735,0.1022417,1);
				float4 lerpResult19 = lerp( lerpResult15 , ( BASETEXTURE243 * _Leather3color ) , saturate( ( 1.0 - ( ( distance( color105.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color103 = IsGammaSpace() ? float4(0.3882353,0.254902,0.0627451,1) : float4(0.1247718,0.05286067,0.005181517,1);
				float4 lerpResult22 = lerp( lerpResult19 , ( BASETEXTURE243 * _Wood1color ) , saturate( ( 1.0 - ( ( distance( color103.rgb , MASKTEXTURE251.rgb ) - 0.05 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color104 = IsGammaSpace() ? float4(0.5333334,0.4431373,0.3215686,1) : float4(0.2462015,0.1651322,0.0843762,1);
				float4 lerpResult27 = lerp( lerpResult22 , ( BASETEXTURE243 * _Wood2color ) , saturate( ( 1.0 - ( ( distance( color104.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color102 = IsGammaSpace() ? float4(0.6470588,0.5333334,0.3882353,1) : float4(0.3762621,0.2462015,0.1247718,1);
				float4 lerpResult32 = lerp( lerpResult27 , ( BASETEXTURE243 * _Wood3color ) , saturate( ( 1.0 - ( ( distance( color102.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color17 = IsGammaSpace() ? float4(0.6039216,0.0627451,0.6470588,1) : float4(0.3231432,0.005181517,0.3762621,1);
				float temp_output_92_0 = saturate( ( 1.0 - ( ( distance( color17.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult91 = lerp( lerpResult32 , ( BASETEXTURE243 * _Ceramictiles1color ) , temp_output_92_0);
				float4 color24 = IsGammaSpace() ? float4(0.7529413,0.0627451,0.8196079,1) : float4(0.5271155,0.005181517,0.637597,1);
				float temp_output_95_0 = saturate( ( 1.0 - ( ( distance( color24.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult93 = lerp( lerpResult91 , ( BASETEXTURE243 * _Ceramictiles2color ) , temp_output_95_0);
				float4 color94 = IsGammaSpace() ? float4(0.9058824,0.0509804,0.9803922,1) : float4(0.799103,0.004024717,0.9559735,1);
				float temp_output_96_0 = saturate( ( 1.0 - ( ( distance( color94.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult39 = lerp( lerpResult93 , ( BASETEXTURE243 * _Ceramictiles3color ) , temp_output_96_0);
				float4 color84 = IsGammaSpace() ? float4(0.8627452,0.8470589,0.7215686,1) : float4(0.7156939,0.6866854,0.4793201,1);
				float4 lerpResult71 = lerp( lerpResult39 , ( BASETEXTURE243 * _Ropecolor ) , saturate( ( 1.0 - ( ( distance( color84.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color85 = IsGammaSpace() ? float4(0.8078432,0.7254902,0.5490196,1) : float4(0.6172068,0.48515,0.2622507,1);
				float4 lerpResult72 = lerp( lerpResult71 , ( BASETEXTURE243 * _Haycolor ) , saturate( ( 1.0 - ( ( distance( color85.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color38 = IsGammaSpace() ? float4(0.2784314,0.4470589,0.5490196,1) : float4(0.06301003,0.1682695,0.2622507,1);
				float temp_output_41_0 = saturate( ( 1.0 - ( ( distance( color38.rgb , MASKTEXTURE251.rgb ) - 0.05 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult47 = lerp( lerpResult72 , ( BASETEXTURE243 * _Metal1color ) , temp_output_41_0);
				float4 color117 = IsGammaSpace() ? float4(0.2470588,0.3803922,0.4627451,1) : float4(0.04970655,0.1195384,0.1811642,1);
				float temp_output_116_0 = saturate( ( 1.0 - ( ( distance( color117.rgb , MASKTEXTURE251.rgb ) - 0.05 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult150 = lerp( lerpResult47 , ( BASETEXTURE243 * _Metal2color ) , temp_output_116_0);
				float4 color118 = IsGammaSpace() ? float4(0.1921569,0.2941177,0.3529412,1) : float4(0.03071345,0.07036012,0.1022417,1);
				float temp_output_149_0 = saturate( ( 1.0 - ( ( distance( color118.rgb , MASKTEXTURE251.rgb ) - 0.05 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult151 = lerp( lerpResult150 , ( BASETEXTURE243 * _Metal3color ) , temp_output_149_0);
				float2 uv1_Coarofarmstexture157 = input.ase_texcoord4.zw;
				float temp_output_49_0 = ( 1.0 - tex2D( _Coarofarmstexture, uv1_Coarofarmstexture157 ).a );
				float4 temp_cast_42 = (temp_output_49_0).xxxx;
				float4 temp_output_1_0_g171 = temp_cast_42;
				float4 color54 = IsGammaSpace() ? float4(0,0,0,0) : float4(0,0,0,0);
				float4 temp_output_2_0_g171 = color54;
				float temp_output_11_0_g171 = distance( temp_output_1_0_g171 , temp_output_2_0_g171 );
				float2 _Vector0 = float2(1.6,1);
				float4 lerpResult21_g171 = lerp( _Coatofarmscolor , temp_output_1_0_g171 , saturate( ( ( temp_output_11_0_g171 - _Vector0.x ) / max( _Vector0.y , 1E-05 ) ) ));
				float temp_output_156_0 = ( 1.0 - temp_output_49_0 );
				float4 lerpResult165 = lerp( lerpResult151 , lerpResult21_g171 , temp_output_156_0);
				

				float3 BaseColor = lerpResult165.rgb;
				float3 Emission = 0;
				float Alpha = 1;
				float AlphaClipThreshold = 0.5;

				#ifdef _ALPHATEST_ON
					clip(Alpha - AlphaClipThreshold);
				#endif

				MetaInput metaInput = (MetaInput)0;
				metaInput.Albedo = BaseColor;
				metaInput.Emission = Emission;
				#ifdef EDITOR_VISUALIZATION
					metaInput.VizUV = input.VizUV.xy;
					metaInput.LightCoord = input.LightCoord;
				#endif

				return UnityMetaFragment(metaInput);
			}
			ENDHLSL
		}

		
		Pass
		{
			
			Name "Universal2D"
			Tags { "LightMode"="Universal2D" }

			Blend One Zero, One Zero
			ZWrite On
			ZTest LEqual
			Offset 0 , 0
			ColorMask RGBA

			HLSLPROGRAM

			#pragma multi_compile_fragment _ALPHATEST_ON
			#define _NORMAL_DROPOFF_TS 1
			#define ASE_FOG 1
			#define ASE_VERSION 19800
			#define ASE_SRP_VERSION 170003


			#pragma vertex vert
			#pragma fragment frag

			#if defined(_SPECULAR_SETUP) && defined(_ASE_LIGHTING_SIMPLE)
				#define _SPECULAR_COLOR 1
			#endif

			#define SHADERPASS SHADERPASS_2D

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
            #include_with_pragmas "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRenderingKeywords.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRendering.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/DebugMipmapStreamingMacros.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

			

			struct Attributes
			{
				float4 positionOS : POSITION;
				float3 normalOS : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct PackedVaryings
			{
				float4 positionCS : SV_POSITION;
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
					float3 positionWS : TEXCOORD0;
				#endif
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					float4 shadowCoord : TEXCOORD1;
				#endif
				float4 ase_texcoord2 : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _TextureSample2_ST;
			float4 _Coatofarmscolor;
			float4 _Metal3color;
			float4 _Metal2color;
			float4 _Metal1color;
			float4 _Haycolor;
			float4 _Ceramictiles3color;
			float4 _Ceramictiles2color;
			float4 _Ceramictiles1color;
			float4 _Wood3color;
			float4 _Wood2color;
			float4 _Ropecolor;
			float4 _Leather3color;
			float4 _leather2color;
			float4 _Leather1colour;
			float4 _Fabric3color;
			float4 _Fabric2color;
			float4 _Fabric1color;
			float4 _Rock2color;
			float4 _Rock1color;
			float4 _Mortarcolor;
			float4 _Wood1color;
			float _Metal1smootness;
			float _Ceramic3smoothness;
			float _Ceramic2smoothness;
			float _Ceramic1smoothness;
			float _MetallicOn;
			float _Metal3metallic;
			float _Metal2metallic;
			float _Metal1metallic;
			float _Metal2smootness;
			float _SmoothnessOn;
			float _Metal3smootness;
			#ifdef ASE_TRANSMISSION
				float _TransmissionShadow;
			#endif
			#ifdef ASE_TRANSLUCENCY
				float _TransStrength;
				float _TransNormal;
				float _TransScattering;
				float _TransDirect;
				float _TransAmbient;
				float _TransShadow;
			#endif
			#ifdef ASE_TESSELLATION
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END

			#ifdef SCENEPICKINGPASS
				float4 _SelectionID;
			#endif

			#ifdef SCENESELECTIONPASS
				int _ObjectId;
				int _PassValue;
			#endif

			sampler2D _TextureSample2;
			sampler2D _TextureSample9;
			sampler2D _Coarofarmstexture;


			
			PackedVaryings VertexFunction( Attributes input  )
			{
				PackedVaryings output = (PackedVaryings)0;
				UNITY_SETUP_INSTANCE_ID( input );
				UNITY_TRANSFER_INSTANCE_ID( input, output );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( output );

				output.ase_texcoord2.xy = input.ase_texcoord.xy;
				output.ase_texcoord2.zw = input.ase_texcoord1.xy;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = input.positionOS.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif

				float3 vertexValue = defaultVertexValue;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					input.positionOS.xyz = vertexValue;
				#else
					input.positionOS.xyz += vertexValue;
				#endif

				input.normalOS = input.normalOS;

				VertexPositionInputs vertexInput = GetVertexPositionInputs( input.positionOS.xyz );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
					output.positionWS = vertexInput.positionWS;
				#endif

				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					output.shadowCoord = GetShadowCoord( vertexInput );
				#endif

				output.positionCS = vertexInput.positionCS;
				return output;
			}

			#if defined(ASE_TESSELLATION)
			struct VertexControl
			{
				float4 positionOS : INTERNALTESSPOS;
				float3 normalOS : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( Attributes input )
			{
				VertexControl output;
				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_TRANSFER_INSTANCE_ID(input, output);
				output.positionOS = input.positionOS;
				output.normalOS = input.normalOS;
				output.ase_texcoord = input.ase_texcoord;
				output.ase_texcoord1 = input.ase_texcoord1;
				return output;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> input)
			{
				TessellationFactors output;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(input[0].positionOS, input[1].positionOS, input[2].positionOS, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(input[0].positionOS, input[1].positionOS, input[2].positionOS, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(input[0].positionOS, input[1].positionOS, input[2].positionOS, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				output.edge[0] = tf.x; output.edge[1] = tf.y; output.edge[2] = tf.z; output.inside = tf.w;
				return output;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
				return patch[id];
			}

			[domain("tri")]
			PackedVaryings DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				Attributes output = (Attributes) 0;
				output.positionOS = patch[0].positionOS * bary.x + patch[1].positionOS * bary.y + patch[2].positionOS * bary.z;
				output.normalOS = patch[0].normalOS * bary.x + patch[1].normalOS * bary.y + patch[2].normalOS * bary.z;
				output.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				output.ase_texcoord1 = patch[0].ase_texcoord1 * bary.x + patch[1].ase_texcoord1 * bary.y + patch[2].ase_texcoord1 * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = output.positionOS.xyz - patch[i].normalOS * (dot(output.positionOS.xyz, patch[i].normalOS) - dot(patch[i].positionOS.xyz, patch[i].normalOS));
				float phongStrength = _TessPhongStrength;
				output.positionOS.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * output.positionOS.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], output);
				return VertexFunction(output);
			}
			#else
			PackedVaryings vert ( Attributes input )
			{
				return VertexFunction( input );
			}
			#endif

			half4 frag(PackedVaryings input  ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( input );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( input );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
					float3 WorldPosition = input.positionWS;
				#endif

				float4 ShadowCoords = float4( 0, 0, 0, 0 );

				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
						ShadowCoords = input.shadowCoord;
					#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
						ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
					#endif
				#endif

				float2 uv_TextureSample2 = input.ase_texcoord2.xy * _TextureSample2_ST.xy + _TextureSample2_ST.zw;
				float4 BASETEXTURE243 = tex2D( _TextureSample2, uv_TextureSample2 );
				float4 color307 = IsGammaSpace() ? float4(1,1,1,0) : float4(1,1,1,0);
				float4 color315 = IsGammaSpace() ? float4(0.1294118,0.9803922,0.7921569,1) : float4(0.01520852,0.9559735,0.5906189,1);
				float2 uv_TextureSample9120 = input.ase_texcoord2.xy;
				float4 MASKTEXTURE251 = tex2D( _TextureSample9, uv_TextureSample9120 );
				float4 lerpResult309 = lerp( float4( 0,0,0,0 ) , ( BASETEXTURE243 * color307 ) , saturate( ( 1.0 - ( ( distance( color315.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color314 = IsGammaSpace() ? float4(0.9921569,0.9647059,0.0627451,1) : float4(0.9822509,0.921582,0.005181517,1);
				float4 lerpResult313 = lerp( lerpResult309 , ( BASETEXTURE243 * _Mortarcolor ) , saturate( ( 1.0 - ( ( distance( color314.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color130 = IsGammaSpace() ? float4(0.7529413,0.7529413,0.7529413,1) : float4(0.5271155,0.5271155,0.5271155,1);
				float4 lerpResult132 = lerp( lerpResult313 , ( BASETEXTURE243 * _Rock1color ) , saturate( ( 1.0 - ( ( distance( color130.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color134 = IsGammaSpace() ? float4(0.2901961,0.2862745,0.2901961,1) : float4(0.06847818,0.06662594,0.06847818,1);
				float4 lerpResult133 = lerp( lerpResult132 , ( BASETEXTURE243 * _Rock2color ) , saturate( ( 1.0 - ( ( distance( color134.rgb , MASKTEXTURE251.rgb ) - 0.04 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color121 = IsGammaSpace() ? float4(0.1058824,0.2352941,0.6980392,1) : float4(0.0109601,0.0451862,0.4452012,1);
				float4 lerpResult124 = lerp( lerpResult133 , ( BASETEXTURE243 * _Fabric1color ) , saturate( ( 1.0 - ( ( distance( color121.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color138 = IsGammaSpace() ? float4(0.1294118,0.2705882,0.8392158,1) : float4(0.01520852,0.05951123,0.6724434,1);
				float4 lerpResult126 = lerp( lerpResult124 , ( BASETEXTURE243 * _Fabric2color ) , saturate( ( 1.0 - ( ( distance( color138.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color136 = IsGammaSpace() ? float4(0.1294118,0.3176471,0.9921569,1) : float4(0.01520852,0.08228273,0.9822509,1);
				float4 lerpResult9 = lerp( lerpResult126 , ( BASETEXTURE243 * _Fabric3color ) , saturate( ( 1.0 - ( ( distance( color136.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color107 = IsGammaSpace() ? float4(0.04705883,0.6196079,0.2392157,1) : float4(0.003676507,0.3419145,0.04666509,1);
				float4 lerpResult10 = lerp( lerpResult9 , ( BASETEXTURE243 * _Leather1colour ) , saturate( ( 1.0 - ( ( distance( color107.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color106 = IsGammaSpace() ? float4(0.0627451,0.8274511,0.3098039,1) : float4(0.005181517,0.6514059,0.07818741,1);
				float4 lerpResult15 = lerp( lerpResult10 , ( BASETEXTURE243 * _leather2color ) , saturate( ( 1.0 - ( ( distance( color106.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color105 = IsGammaSpace() ? float4(0.0509804,0.9803922,0.3529412,1) : float4(0.004024717,0.9559735,0.1022417,1);
				float4 lerpResult19 = lerp( lerpResult15 , ( BASETEXTURE243 * _Leather3color ) , saturate( ( 1.0 - ( ( distance( color105.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color103 = IsGammaSpace() ? float4(0.3882353,0.254902,0.0627451,1) : float4(0.1247718,0.05286067,0.005181517,1);
				float4 lerpResult22 = lerp( lerpResult19 , ( BASETEXTURE243 * _Wood1color ) , saturate( ( 1.0 - ( ( distance( color103.rgb , MASKTEXTURE251.rgb ) - 0.05 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color104 = IsGammaSpace() ? float4(0.5333334,0.4431373,0.3215686,1) : float4(0.2462015,0.1651322,0.0843762,1);
				float4 lerpResult27 = lerp( lerpResult22 , ( BASETEXTURE243 * _Wood2color ) , saturate( ( 1.0 - ( ( distance( color104.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color102 = IsGammaSpace() ? float4(0.6470588,0.5333334,0.3882353,1) : float4(0.3762621,0.2462015,0.1247718,1);
				float4 lerpResult32 = lerp( lerpResult27 , ( BASETEXTURE243 * _Wood3color ) , saturate( ( 1.0 - ( ( distance( color102.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color17 = IsGammaSpace() ? float4(0.6039216,0.0627451,0.6470588,1) : float4(0.3231432,0.005181517,0.3762621,1);
				float temp_output_92_0 = saturate( ( 1.0 - ( ( distance( color17.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult91 = lerp( lerpResult32 , ( BASETEXTURE243 * _Ceramictiles1color ) , temp_output_92_0);
				float4 color24 = IsGammaSpace() ? float4(0.7529413,0.0627451,0.8196079,1) : float4(0.5271155,0.005181517,0.637597,1);
				float temp_output_95_0 = saturate( ( 1.0 - ( ( distance( color24.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult93 = lerp( lerpResult91 , ( BASETEXTURE243 * _Ceramictiles2color ) , temp_output_95_0);
				float4 color94 = IsGammaSpace() ? float4(0.9058824,0.0509804,0.9803922,1) : float4(0.799103,0.004024717,0.9559735,1);
				float temp_output_96_0 = saturate( ( 1.0 - ( ( distance( color94.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult39 = lerp( lerpResult93 , ( BASETEXTURE243 * _Ceramictiles3color ) , temp_output_96_0);
				float4 color84 = IsGammaSpace() ? float4(0.8627452,0.8470589,0.7215686,1) : float4(0.7156939,0.6866854,0.4793201,1);
				float4 lerpResult71 = lerp( lerpResult39 , ( BASETEXTURE243 * _Ropecolor ) , saturate( ( 1.0 - ( ( distance( color84.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color85 = IsGammaSpace() ? float4(0.8078432,0.7254902,0.5490196,1) : float4(0.6172068,0.48515,0.2622507,1);
				float4 lerpResult72 = lerp( lerpResult71 , ( BASETEXTURE243 * _Haycolor ) , saturate( ( 1.0 - ( ( distance( color85.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color38 = IsGammaSpace() ? float4(0.2784314,0.4470589,0.5490196,1) : float4(0.06301003,0.1682695,0.2622507,1);
				float temp_output_41_0 = saturate( ( 1.0 - ( ( distance( color38.rgb , MASKTEXTURE251.rgb ) - 0.05 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult47 = lerp( lerpResult72 , ( BASETEXTURE243 * _Metal1color ) , temp_output_41_0);
				float4 color117 = IsGammaSpace() ? float4(0.2470588,0.3803922,0.4627451,1) : float4(0.04970655,0.1195384,0.1811642,1);
				float temp_output_116_0 = saturate( ( 1.0 - ( ( distance( color117.rgb , MASKTEXTURE251.rgb ) - 0.05 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult150 = lerp( lerpResult47 , ( BASETEXTURE243 * _Metal2color ) , temp_output_116_0);
				float4 color118 = IsGammaSpace() ? float4(0.1921569,0.2941177,0.3529412,1) : float4(0.03071345,0.07036012,0.1022417,1);
				float temp_output_149_0 = saturate( ( 1.0 - ( ( distance( color118.rgb , MASKTEXTURE251.rgb ) - 0.05 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult151 = lerp( lerpResult150 , ( BASETEXTURE243 * _Metal3color ) , temp_output_149_0);
				float2 uv1_Coarofarmstexture157 = input.ase_texcoord2.zw;
				float temp_output_49_0 = ( 1.0 - tex2D( _Coarofarmstexture, uv1_Coarofarmstexture157 ).a );
				float4 temp_cast_42 = (temp_output_49_0).xxxx;
				float4 temp_output_1_0_g171 = temp_cast_42;
				float4 color54 = IsGammaSpace() ? float4(0,0,0,0) : float4(0,0,0,0);
				float4 temp_output_2_0_g171 = color54;
				float temp_output_11_0_g171 = distance( temp_output_1_0_g171 , temp_output_2_0_g171 );
				float2 _Vector0 = float2(1.6,1);
				float4 lerpResult21_g171 = lerp( _Coatofarmscolor , temp_output_1_0_g171 , saturate( ( ( temp_output_11_0_g171 - _Vector0.x ) / max( _Vector0.y , 1E-05 ) ) ));
				float temp_output_156_0 = ( 1.0 - temp_output_49_0 );
				float4 lerpResult165 = lerp( lerpResult151 , lerpResult21_g171 , temp_output_156_0);
				

				float3 BaseColor = lerpResult165.rgb;
				float Alpha = 1;
				float AlphaClipThreshold = 0.5;

				half4 color = half4(BaseColor, Alpha );

				#ifdef _ALPHATEST_ON
					clip(Alpha - AlphaClipThreshold);
				#endif

				return color;
			}
			ENDHLSL
		}

		
		Pass
		{
			
			Name "DepthNormals"
			Tags { "LightMode"="DepthNormals" }

			ZWrite On
			Blend One Zero
			ZTest LEqual
			ZWrite On

			HLSLPROGRAM

			#pragma multi_compile _ALPHATEST_ON
			#define _NORMAL_DROPOFF_TS 1
			#pragma multi_compile_instancing
			#pragma multi_compile _ LOD_FADE_CROSSFADE
			#define ASE_FOG 1
			#define ASE_VERSION 19800
			#define ASE_SRP_VERSION 170003


			#pragma vertex vert
			#pragma fragment frag

			#if defined(_SPECULAR_SETUP) && defined(_ASE_LIGHTING_SIMPLE)
				#define _SPECULAR_COLOR 1
			#endif

			#define SHADERPASS SHADERPASS_DEPTHNORMALSONLY
			//#define SHADERPASS SHADERPASS_DEPTHNORMALS

			#include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"
			#include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/RenderingLayers.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
            #include_with_pragmas "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRenderingKeywords.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRendering.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/DebugMipmapStreamingMacros.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

			#if defined(LOD_FADE_CROSSFADE)
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/LODCrossFade.hlsl"
            #endif

			

			#if defined(ASE_EARLY_Z_DEPTH_OPTIMIZE) && (SHADER_TARGET >= 45)
				#define ASE_SV_DEPTH SV_DepthLessEqual
				#define ASE_SV_POSITION_QUALIFIERS linear noperspective centroid
			#else
				#define ASE_SV_DEPTH SV_Depth
				#define ASE_SV_POSITION_QUALIFIERS
			#endif

			struct Attributes
			{
				float4 positionOS : POSITION;
				float3 normalOS : NORMAL;
				float4 tangentOS : TANGENT;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct PackedVaryings
			{
				ASE_SV_POSITION_QUALIFIERS float4 positionCS : SV_POSITION;
				float4 clipPosV : TEXCOORD0;
				float3 positionWS : TEXCOORD1;
				float3 normalWS : TEXCOORD2;
				float4 tangentWS : TEXCOORD3;
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					float4 shadowCoord : TEXCOORD4;
				#endif
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _TextureSample2_ST;
			float4 _Coatofarmscolor;
			float4 _Metal3color;
			float4 _Metal2color;
			float4 _Metal1color;
			float4 _Haycolor;
			float4 _Ceramictiles3color;
			float4 _Ceramictiles2color;
			float4 _Ceramictiles1color;
			float4 _Wood3color;
			float4 _Wood2color;
			float4 _Ropecolor;
			float4 _Leather3color;
			float4 _leather2color;
			float4 _Leather1colour;
			float4 _Fabric3color;
			float4 _Fabric2color;
			float4 _Fabric1color;
			float4 _Rock2color;
			float4 _Rock1color;
			float4 _Mortarcolor;
			float4 _Wood1color;
			float _Metal1smootness;
			float _Ceramic3smoothness;
			float _Ceramic2smoothness;
			float _Ceramic1smoothness;
			float _MetallicOn;
			float _Metal3metallic;
			float _Metal2metallic;
			float _Metal1metallic;
			float _Metal2smootness;
			float _SmoothnessOn;
			float _Metal3smootness;
			#ifdef ASE_TRANSMISSION
				float _TransmissionShadow;
			#endif
			#ifdef ASE_TRANSLUCENCY
				float _TransStrength;
				float _TransNormal;
				float _TransScattering;
				float _TransDirect;
				float _TransAmbient;
				float _TransShadow;
			#endif
			#ifdef ASE_TESSELLATION
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END

			#ifdef SCENEPICKINGPASS
				float4 _SelectionID;
			#endif

			#ifdef SCENESELECTIONPASS
				int _ObjectId;
				int _PassValue;
			#endif

			

			
			PackedVaryings VertexFunction( Attributes input  )
			{
				PackedVaryings output = (PackedVaryings)0;
				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_TRANSFER_INSTANCE_ID(input, output);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

				
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = input.positionOS.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif

				float3 vertexValue = defaultVertexValue;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					input.positionOS.xyz = vertexValue;
				#else
					input.positionOS.xyz += vertexValue;
				#endif

				input.normalOS = input.normalOS;
				input.tangentOS = input.tangentOS;

				VertexPositionInputs vertexInput = GetVertexPositionInputs( input.positionOS.xyz );

				float3 normalWS = TransformObjectToWorldNormal( input.normalOS );
				float4 tangentWS = float4( TransformObjectToWorldDir( input.tangentOS.xyz ), input.tangentOS.w );

				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					output.shadowCoord = GetShadowCoord( vertexInput );
				#endif

				output.positionCS = vertexInput.positionCS;
				output.clipPosV = vertexInput.positionCS;
				output.positionWS = vertexInput.positionWS;
				output.normalWS = normalWS;
				output.tangentWS = tangentWS;
				return output;
			}

			#if defined(ASE_TESSELLATION)
			struct VertexControl
			{
				float4 positionOS : INTERNALTESSPOS;
				float3 normalOS : NORMAL;
				float4 tangentOS : TANGENT;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( Attributes input )
			{
				VertexControl output;
				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_TRANSFER_INSTANCE_ID(input, output);
				output.positionOS = input.positionOS;
				output.normalOS = input.normalOS;
				output.tangentOS = input.tangentOS;
				
				return output;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> input)
			{
				TessellationFactors output;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(input[0].positionOS, input[1].positionOS, input[2].positionOS, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(input[0].positionOS, input[1].positionOS, input[2].positionOS, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(input[0].positionOS, input[1].positionOS, input[2].positionOS, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				output.edge[0] = tf.x; output.edge[1] = tf.y; output.edge[2] = tf.z; output.inside = tf.w;
				return output;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
				return patch[id];
			}

			[domain("tri")]
			PackedVaryings DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				Attributes output = (Attributes) 0;
				output.positionOS = patch[0].positionOS * bary.x + patch[1].positionOS * bary.y + patch[2].positionOS * bary.z;
				output.normalOS = patch[0].normalOS * bary.x + patch[1].normalOS * bary.y + patch[2].normalOS * bary.z;
				output.tangentOS = patch[0].tangentOS * bary.x + patch[1].tangentOS * bary.y + patch[2].tangentOS * bary.z;
				
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = output.positionOS.xyz - patch[i].normalOS * (dot(output.positionOS.xyz, patch[i].normalOS) - dot(patch[i].positionOS.xyz, patch[i].normalOS));
				float phongStrength = _TessPhongStrength;
				output.positionOS.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * output.positionOS.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], output);
				return VertexFunction(output);
			}
			#else
			PackedVaryings vert ( Attributes input )
			{
				return VertexFunction( input );
			}
			#endif

			void frag(	PackedVaryings input
						, out half4 outNormalWS : SV_Target0
						#ifdef ASE_DEPTH_WRITE_ON
						,out float outputDepth : ASE_SV_DEPTH
						#endif
						#ifdef _WRITE_RENDERING_LAYERS
						, out float4 outRenderingLayers : SV_Target1
						#endif
						 )
			{
				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( input );

				float4 ShadowCoords = float4( 0, 0, 0, 0 );
				float3 WorldNormal = input.normalWS;
				float4 WorldTangent = input.tangentWS;
				float3 WorldPosition = input.positionWS;
				float4 ClipPos = input.clipPosV;
				float4 ScreenPos = ComputeScreenPos( input.clipPosV );

				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
						ShadowCoords = input.shadowCoord;
					#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
						ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
					#endif
				#endif

				

				float3 Normal = float3(0, 0, 1);
				float Alpha = 1;
				float AlphaClipThreshold = 0.5;

				#ifdef ASE_DEPTH_WRITE_ON
					float DepthValue = input.positionCS.z;
				#endif

				#ifdef _ALPHATEST_ON
					clip(Alpha - AlphaClipThreshold);
				#endif

				#if defined(LOD_FADE_CROSSFADE)
					LODFadeCrossFade( input.positionCS );
				#endif

				#ifdef ASE_DEPTH_WRITE_ON
					outputDepth = DepthValue;
				#endif

				#if defined(_GBUFFER_NORMALS_OCT)
					float2 octNormalWS = PackNormalOctQuadEncode(WorldNormal);
					float2 remappedOctNormalWS = saturate(octNormalWS * 0.5 + 0.5);
					half3 packedNormalWS = PackFloat2To888(remappedOctNormalWS);
					outNormalWS = half4(packedNormalWS, 0.0);
				#else
					#if defined(_NORMALMAP)
						#if _NORMAL_DROPOFF_TS
							float crossSign = (WorldTangent.w > 0.0 ? 1.0 : -1.0) * GetOddNegativeScale();
							float3 bitangent = crossSign * cross(WorldNormal.xyz, WorldTangent.xyz);
							float3 normalWS = TransformTangentToWorld(Normal, half3x3(WorldTangent.xyz, bitangent, WorldNormal.xyz));
						#elif _NORMAL_DROPOFF_OS
							float3 normalWS = TransformObjectToWorldNormal(Normal);
						#elif _NORMAL_DROPOFF_WS
							float3 normalWS = Normal;
						#endif
					#else
						float3 normalWS = WorldNormal;
					#endif
					outNormalWS = half4(NormalizeNormalPerPixel(normalWS), 0.0);
				#endif

				#ifdef _WRITE_RENDERING_LAYERS
					uint renderingLayers = GetMeshRenderingLayer();
					outRenderingLayers = float4(EncodeMeshRenderingLayer(renderingLayers), 0, 0, 0);
				#endif
			}
			ENDHLSL
		}

		
		Pass
		{
			
			Name "GBuffer"
			Tags { "LightMode"="UniversalGBuffer" }

			Blend One Zero, One Zero
			ZWrite On
			ZTest LEqual
			Offset 0 , 0
			ColorMask RGBA
			

			HLSLPROGRAM

			#pragma multi_compile_fragment _ALPHATEST_ON
			#define _NORMAL_DROPOFF_TS 1
			#pragma shader_feature_local _RECEIVE_SHADOWS_OFF
			#pragma multi_compile_instancing
			#pragma instancing_options renderinglayer
			#pragma multi_compile _ LOD_FADE_CROSSFADE
			#pragma multi_compile_fog
			#define ASE_FOG 1
			#define ASE_VERSION 19800
			#define ASE_SRP_VERSION 170003


			#pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
			#pragma multi_compile_fragment _ _REFLECTION_PROBE_BLENDING
			#pragma multi_compile_fragment _ _REFLECTION_PROBE_BOX_PROJECTION
			#pragma multi_compile_fragment _ _SHADOWS_SOFT _SHADOWS_SOFT_LOW _SHADOWS_SOFT_MEDIUM _SHADOWS_SOFT_HIGH
			#pragma multi_compile_fragment _ _DBUFFER_MRT1 _DBUFFER_MRT2 _DBUFFER_MRT3
			#pragma multi_compile_fragment _ _GBUFFER_NORMALS_OCT
			#pragma multi_compile_fragment _ _RENDER_PASS_ENABLED

			#pragma multi_compile _ LIGHTMAP_SHADOW_MIXING
			#pragma multi_compile _ _MIXED_LIGHTING_SUBTRACTIVE
			#pragma multi_compile _ SHADOWS_SHADOWMASK
			#pragma multi_compile _ DIRLIGHTMAP_COMBINED
			#pragma multi_compile _ USE_LEGACY_LIGHTMAPS
			#pragma multi_compile _ LIGHTMAP_ON
			#pragma multi_compile _ DYNAMICLIGHTMAP_ON

			#pragma vertex vert
			#pragma fragment frag

			#if defined(_SPECULAR_SETUP) && defined(_ASE_LIGHTING_SIMPLE)
				#define _SPECULAR_COLOR 1
			#endif

			#define SHADERPASS SHADERPASS_GBUFFER

			#include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"
			#include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/RenderingLayers.hlsl"
			#include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ProbeVolumeVariants.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
            #include_with_pragmas "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRenderingKeywords.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRendering.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/DebugMipmapStreamingMacros.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DBuffer.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

			#if defined(LOD_FADE_CROSSFADE)
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/LODCrossFade.hlsl"
            #endif
			
			#if defined(UNITY_INSTANCING_ENABLED) && defined(_TERRAIN_INSTANCED_PERPIXEL_NORMAL)
				#define ENABLE_TERRAIN_PERPIXEL_NORMAL
			#endif

			

			#if defined(ASE_EARLY_Z_DEPTH_OPTIMIZE) && (SHADER_TARGET >= 45)
				#define ASE_SV_DEPTH SV_DepthLessEqual
				#define ASE_SV_POSITION_QUALIFIERS linear noperspective centroid
			#else
				#define ASE_SV_DEPTH SV_Depth
				#define ASE_SV_POSITION_QUALIFIERS
			#endif

			struct Attributes
			{
				float4 positionOS : POSITION;
				float3 normalOS : NORMAL;
				float4 tangentOS : TANGENT;
				float4 texcoord : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct PackedVaryings
			{
				ASE_SV_POSITION_QUALIFIERS float4 positionCS : SV_POSITION;
				float4 clipPosV : TEXCOORD0;
				float4 lightmapUVOrVertexSH : TEXCOORD1;
				#if defined(ASE_FOG) || defined(_ADDITIONAL_LIGHTS_VERTEX)
					half4 fogFactorAndVertexLight : TEXCOORD2;
				#endif
				float4 tSpace0 : TEXCOORD3;
				float4 tSpace1 : TEXCOORD4;
				float4 tSpace2 : TEXCOORD5;
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
				float4 shadowCoord : TEXCOORD6;
				#endif
				#if defined(DYNAMICLIGHTMAP_ON)
				float2 dynamicLightmapUV : TEXCOORD7;
				#endif
				#if defined(USE_APV_PROBE_OCCLUSION)
					float4 probeOcclusion : TEXCOORD8;
				#endif
				float4 ase_texcoord9 : TEXCOORD9;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _TextureSample2_ST;
			float4 _Coatofarmscolor;
			float4 _Metal3color;
			float4 _Metal2color;
			float4 _Metal1color;
			float4 _Haycolor;
			float4 _Ceramictiles3color;
			float4 _Ceramictiles2color;
			float4 _Ceramictiles1color;
			float4 _Wood3color;
			float4 _Wood2color;
			float4 _Ropecolor;
			float4 _Leather3color;
			float4 _leather2color;
			float4 _Leather1colour;
			float4 _Fabric3color;
			float4 _Fabric2color;
			float4 _Fabric1color;
			float4 _Rock2color;
			float4 _Rock1color;
			float4 _Mortarcolor;
			float4 _Wood1color;
			float _Metal1smootness;
			float _Ceramic3smoothness;
			float _Ceramic2smoothness;
			float _Ceramic1smoothness;
			float _MetallicOn;
			float _Metal3metallic;
			float _Metal2metallic;
			float _Metal1metallic;
			float _Metal2smootness;
			float _SmoothnessOn;
			float _Metal3smootness;
			#ifdef ASE_TRANSMISSION
				float _TransmissionShadow;
			#endif
			#ifdef ASE_TRANSLUCENCY
				float _TransStrength;
				float _TransNormal;
				float _TransScattering;
				float _TransDirect;
				float _TransAmbient;
				float _TransShadow;
			#endif
			#ifdef ASE_TESSELLATION
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END

			#ifdef SCENEPICKINGPASS
				float4 _SelectionID;
			#endif

			#ifdef SCENESELECTIONPASS
				int _ObjectId;
				int _PassValue;
			#endif

			sampler2D _TextureSample2;
			sampler2D _TextureSample9;
			sampler2D _Coarofarmstexture;


			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/UnityGBuffer.hlsl"

			
			PackedVaryings VertexFunction( Attributes input  )
			{
				PackedVaryings output = (PackedVaryings)0;
				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_TRANSFER_INSTANCE_ID(input, output);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

				output.ase_texcoord9.xy = input.texcoord.xy;
				output.ase_texcoord9.zw = input.texcoord1.xy;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = input.positionOS.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif

				float3 vertexValue = defaultVertexValue;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					input.positionOS.xyz = vertexValue;
				#else
					input.positionOS.xyz += vertexValue;
				#endif

				input.normalOS = input.normalOS;
				input.tangentOS = input.tangentOS;

				VertexPositionInputs vertexInput = GetVertexPositionInputs( input.positionOS.xyz );
				VertexNormalInputs normalInput = GetVertexNormalInputs( input.normalOS, input.tangentOS );

				output.tSpace0 = float4( normalInput.normalWS, vertexInput.positionWS.x);
				output.tSpace1 = float4( normalInput.tangentWS, vertexInput.positionWS.y);
				output.tSpace2 = float4( normalInput.bitangentWS, vertexInput.positionWS.z);

				#if defined(LIGHTMAP_ON)
					OUTPUT_LIGHTMAP_UV(input.texcoord1, unity_LightmapST, output.lightmapUVOrVertexSH.xy);
				#endif

				#if defined(DYNAMICLIGHTMAP_ON)
					output.dynamicLightmapUV.xy = input.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
				#endif

				OUTPUT_SH4( vertexInput.positionWS, normalInput.normalWS.xyz, GetWorldSpaceNormalizeViewDir( vertexInput.positionWS ), output.lightmapUVOrVertexSH.xyz, output.probeOcclusion );

				#if defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
					output.lightmapUVOrVertexSH.zw = input.texcoord.xy;
					output.lightmapUVOrVertexSH.xy = input.texcoord.xy * unity_LightmapST.xy + unity_LightmapST.zw;
				#endif

				#if defined(ASE_FOG) || defined(_ADDITIONAL_LIGHTS_VERTEX)
					output.fogFactorAndVertexLight = 0;
					#if defined(ASE_FOG) && !defined(_FOG_FRAGMENT)
						// @diogo: no fog applied in GBuffer
					#endif
					#ifdef _ADDITIONAL_LIGHTS_VERTEX
						half3 vertexLight = VertexLighting( vertexInput.positionWS, normalInput.normalWS );
						output.fogFactorAndVertexLight.yzw = vertexLight;
					#endif
				#endif

				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
					output.shadowCoord = GetShadowCoord( vertexInput );
				#endif

				output.positionCS = vertexInput.positionCS;
				output.clipPosV = vertexInput.positionCS;
				return output;
			}

			#if defined(ASE_TESSELLATION)
			struct VertexControl
			{
				float4 positionOS : INTERNALTESSPOS;
				float3 normalOS : NORMAL;
				float4 tangentOS : TANGENT;
				float4 texcoord : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( Attributes input )
			{
				VertexControl output;
				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_TRANSFER_INSTANCE_ID(input, output);
				output.positionOS = input.positionOS;
				output.normalOS = input.normalOS;
				output.tangentOS = input.tangentOS;
				output.texcoord = input.texcoord;
				output.texcoord1 = input.texcoord1;
				output.texcoord2 = input.texcoord2;
				
				return output;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> input)
			{
				TessellationFactors output;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(input[0].positionOS, input[1].positionOS, input[2].positionOS, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(input[0].positionOS, input[1].positionOS, input[2].positionOS, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(input[0].positionOS, input[1].positionOS, input[2].positionOS, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				output.edge[0] = tf.x; output.edge[1] = tf.y; output.edge[2] = tf.z; output.inside = tf.w;
				return output;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
				return patch[id];
			}

			[domain("tri")]
			PackedVaryings DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				Attributes output = (Attributes) 0;
				output.positionOS = patch[0].positionOS * bary.x + patch[1].positionOS * bary.y + patch[2].positionOS * bary.z;
				output.normalOS = patch[0].normalOS * bary.x + patch[1].normalOS * bary.y + patch[2].normalOS * bary.z;
				output.tangentOS = patch[0].tangentOS * bary.x + patch[1].tangentOS * bary.y + patch[2].tangentOS * bary.z;
				output.texcoord = patch[0].texcoord * bary.x + patch[1].texcoord * bary.y + patch[2].texcoord * bary.z;
				output.texcoord1 = patch[0].texcoord1 * bary.x + patch[1].texcoord1 * bary.y + patch[2].texcoord1 * bary.z;
				output.texcoord2 = patch[0].texcoord2 * bary.x + patch[1].texcoord2 * bary.y + patch[2].texcoord2 * bary.z;
				
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = output.positionOS.xyz - patch[i].normalOS * (dot(output.positionOS.xyz, patch[i].normalOS) - dot(patch[i].positionOS.xyz, patch[i].normalOS));
				float phongStrength = _TessPhongStrength;
				output.positionOS.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * output.positionOS.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], output);
				return VertexFunction(output);
			}
			#else
			PackedVaryings vert ( Attributes input )
			{
				return VertexFunction( input );
			}
			#endif

			FragmentOutput frag ( PackedVaryings input
								#ifdef ASE_DEPTH_WRITE_ON
								,out float outputDepth : ASE_SV_DEPTH
								#endif
								 )
			{
				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

				#if defined(LOD_FADE_CROSSFADE)
					LODFadeCrossFade( input.positionCS );
				#endif

				#if defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
					float2 sampleCoords = (input.lightmapUVOrVertexSH.zw / _TerrainHeightmapRecipSize.zw + 0.5f) * _TerrainHeightmapRecipSize.xy;
					float3 WorldNormal = TransformObjectToWorldNormal(normalize(SAMPLE_TEXTURE2D(_TerrainNormalmapTexture, sampler_TerrainNormalmapTexture, sampleCoords).rgb * 2 - 1));
					float3 WorldTangent = -cross(GetObjectToWorldMatrix()._13_23_33, WorldNormal);
					float3 WorldBiTangent = cross(WorldNormal, -WorldTangent);
				#else
					float3 WorldNormal = normalize( input.tSpace0.xyz );
					float3 WorldTangent = input.tSpace1.xyz;
					float3 WorldBiTangent = input.tSpace2.xyz;
				#endif

				float3 WorldPosition = float3(input.tSpace0.w,input.tSpace1.w,input.tSpace2.w);
				float3 WorldViewDirection = _WorldSpaceCameraPos.xyz  - WorldPosition;
				float4 ShadowCoords = float4( 0, 0, 0, 0 );
				float4 ClipPos = input.clipPosV;
				float4 ScreenPos = ComputeScreenPos( input.clipPosV );

				float2 NormalizedScreenSpaceUV = GetNormalizedScreenSpaceUV(input.positionCS);

				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
					ShadowCoords = input.shadowCoord;
				#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
					ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
				#else
					ShadowCoords = float4(0, 0, 0, 0);
				#endif

				WorldViewDirection = SafeNormalize( WorldViewDirection );

				float2 uv_TextureSample2 = input.ase_texcoord9.xy * _TextureSample2_ST.xy + _TextureSample2_ST.zw;
				float4 BASETEXTURE243 = tex2D( _TextureSample2, uv_TextureSample2 );
				float4 color307 = IsGammaSpace() ? float4(1,1,1,0) : float4(1,1,1,0);
				float4 color315 = IsGammaSpace() ? float4(0.1294118,0.9803922,0.7921569,1) : float4(0.01520852,0.9559735,0.5906189,1);
				float2 uv_TextureSample9120 = input.ase_texcoord9.xy;
				float4 MASKTEXTURE251 = tex2D( _TextureSample9, uv_TextureSample9120 );
				float4 lerpResult309 = lerp( float4( 0,0,0,0 ) , ( BASETEXTURE243 * color307 ) , saturate( ( 1.0 - ( ( distance( color315.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color314 = IsGammaSpace() ? float4(0.9921569,0.9647059,0.0627451,1) : float4(0.9822509,0.921582,0.005181517,1);
				float4 lerpResult313 = lerp( lerpResult309 , ( BASETEXTURE243 * _Mortarcolor ) , saturate( ( 1.0 - ( ( distance( color314.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color130 = IsGammaSpace() ? float4(0.7529413,0.7529413,0.7529413,1) : float4(0.5271155,0.5271155,0.5271155,1);
				float4 lerpResult132 = lerp( lerpResult313 , ( BASETEXTURE243 * _Rock1color ) , saturate( ( 1.0 - ( ( distance( color130.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color134 = IsGammaSpace() ? float4(0.2901961,0.2862745,0.2901961,1) : float4(0.06847818,0.06662594,0.06847818,1);
				float4 lerpResult133 = lerp( lerpResult132 , ( BASETEXTURE243 * _Rock2color ) , saturate( ( 1.0 - ( ( distance( color134.rgb , MASKTEXTURE251.rgb ) - 0.04 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color121 = IsGammaSpace() ? float4(0.1058824,0.2352941,0.6980392,1) : float4(0.0109601,0.0451862,0.4452012,1);
				float4 lerpResult124 = lerp( lerpResult133 , ( BASETEXTURE243 * _Fabric1color ) , saturate( ( 1.0 - ( ( distance( color121.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color138 = IsGammaSpace() ? float4(0.1294118,0.2705882,0.8392158,1) : float4(0.01520852,0.05951123,0.6724434,1);
				float4 lerpResult126 = lerp( lerpResult124 , ( BASETEXTURE243 * _Fabric2color ) , saturate( ( 1.0 - ( ( distance( color138.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color136 = IsGammaSpace() ? float4(0.1294118,0.3176471,0.9921569,1) : float4(0.01520852,0.08228273,0.9822509,1);
				float4 lerpResult9 = lerp( lerpResult126 , ( BASETEXTURE243 * _Fabric3color ) , saturate( ( 1.0 - ( ( distance( color136.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color107 = IsGammaSpace() ? float4(0.04705883,0.6196079,0.2392157,1) : float4(0.003676507,0.3419145,0.04666509,1);
				float4 lerpResult10 = lerp( lerpResult9 , ( BASETEXTURE243 * _Leather1colour ) , saturate( ( 1.0 - ( ( distance( color107.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color106 = IsGammaSpace() ? float4(0.0627451,0.8274511,0.3098039,1) : float4(0.005181517,0.6514059,0.07818741,1);
				float4 lerpResult15 = lerp( lerpResult10 , ( BASETEXTURE243 * _leather2color ) , saturate( ( 1.0 - ( ( distance( color106.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color105 = IsGammaSpace() ? float4(0.0509804,0.9803922,0.3529412,1) : float4(0.004024717,0.9559735,0.1022417,1);
				float4 lerpResult19 = lerp( lerpResult15 , ( BASETEXTURE243 * _Leather3color ) , saturate( ( 1.0 - ( ( distance( color105.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color103 = IsGammaSpace() ? float4(0.3882353,0.254902,0.0627451,1) : float4(0.1247718,0.05286067,0.005181517,1);
				float4 lerpResult22 = lerp( lerpResult19 , ( BASETEXTURE243 * _Wood1color ) , saturate( ( 1.0 - ( ( distance( color103.rgb , MASKTEXTURE251.rgb ) - 0.05 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color104 = IsGammaSpace() ? float4(0.5333334,0.4431373,0.3215686,1) : float4(0.2462015,0.1651322,0.0843762,1);
				float4 lerpResult27 = lerp( lerpResult22 , ( BASETEXTURE243 * _Wood2color ) , saturate( ( 1.0 - ( ( distance( color104.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color102 = IsGammaSpace() ? float4(0.6470588,0.5333334,0.3882353,1) : float4(0.3762621,0.2462015,0.1247718,1);
				float4 lerpResult32 = lerp( lerpResult27 , ( BASETEXTURE243 * _Wood3color ) , saturate( ( 1.0 - ( ( distance( color102.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color17 = IsGammaSpace() ? float4(0.6039216,0.0627451,0.6470588,1) : float4(0.3231432,0.005181517,0.3762621,1);
				float temp_output_92_0 = saturate( ( 1.0 - ( ( distance( color17.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult91 = lerp( lerpResult32 , ( BASETEXTURE243 * _Ceramictiles1color ) , temp_output_92_0);
				float4 color24 = IsGammaSpace() ? float4(0.7529413,0.0627451,0.8196079,1) : float4(0.5271155,0.005181517,0.637597,1);
				float temp_output_95_0 = saturate( ( 1.0 - ( ( distance( color24.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult93 = lerp( lerpResult91 , ( BASETEXTURE243 * _Ceramictiles2color ) , temp_output_95_0);
				float4 color94 = IsGammaSpace() ? float4(0.9058824,0.0509804,0.9803922,1) : float4(0.799103,0.004024717,0.9559735,1);
				float temp_output_96_0 = saturate( ( 1.0 - ( ( distance( color94.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult39 = lerp( lerpResult93 , ( BASETEXTURE243 * _Ceramictiles3color ) , temp_output_96_0);
				float4 color84 = IsGammaSpace() ? float4(0.8627452,0.8470589,0.7215686,1) : float4(0.7156939,0.6866854,0.4793201,1);
				float4 lerpResult71 = lerp( lerpResult39 , ( BASETEXTURE243 * _Ropecolor ) , saturate( ( 1.0 - ( ( distance( color84.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color85 = IsGammaSpace() ? float4(0.8078432,0.7254902,0.5490196,1) : float4(0.6172068,0.48515,0.2622507,1);
				float4 lerpResult72 = lerp( lerpResult71 , ( BASETEXTURE243 * _Haycolor ) , saturate( ( 1.0 - ( ( distance( color85.rgb , MASKTEXTURE251.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color38 = IsGammaSpace() ? float4(0.2784314,0.4470589,0.5490196,1) : float4(0.06301003,0.1682695,0.2622507,1);
				float temp_output_41_0 = saturate( ( 1.0 - ( ( distance( color38.rgb , MASKTEXTURE251.rgb ) - 0.05 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult47 = lerp( lerpResult72 , ( BASETEXTURE243 * _Metal1color ) , temp_output_41_0);
				float4 color117 = IsGammaSpace() ? float4(0.2470588,0.3803922,0.4627451,1) : float4(0.04970655,0.1195384,0.1811642,1);
				float temp_output_116_0 = saturate( ( 1.0 - ( ( distance( color117.rgb , MASKTEXTURE251.rgb ) - 0.05 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult150 = lerp( lerpResult47 , ( BASETEXTURE243 * _Metal2color ) , temp_output_116_0);
				float4 color118 = IsGammaSpace() ? float4(0.1921569,0.2941177,0.3529412,1) : float4(0.03071345,0.07036012,0.1022417,1);
				float temp_output_149_0 = saturate( ( 1.0 - ( ( distance( color118.rgb , MASKTEXTURE251.rgb ) - 0.05 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult151 = lerp( lerpResult150 , ( BASETEXTURE243 * _Metal3color ) , temp_output_149_0);
				float2 uv1_Coarofarmstexture157 = input.ase_texcoord9.zw;
				float temp_output_49_0 = ( 1.0 - tex2D( _Coarofarmstexture, uv1_Coarofarmstexture157 ).a );
				float4 temp_cast_42 = (temp_output_49_0).xxxx;
				float4 temp_output_1_0_g171 = temp_cast_42;
				float4 color54 = IsGammaSpace() ? float4(0,0,0,0) : float4(0,0,0,0);
				float4 temp_output_2_0_g171 = color54;
				float temp_output_11_0_g171 = distance( temp_output_1_0_g171 , temp_output_2_0_g171 );
				float2 _Vector0 = float2(1.6,1);
				float4 lerpResult21_g171 = lerp( _Coatofarmscolor , temp_output_1_0_g171 , saturate( ( ( temp_output_11_0_g171 - _Vector0.x ) / max( _Vector0.y , 1E-05 ) ) ));
				float temp_output_156_0 = ( 1.0 - temp_output_49_0 );
				float4 lerpResult165 = lerp( lerpResult151 , lerpResult21_g171 , temp_output_156_0);
				
				float lerpResult110 = lerp( 0.0 , _Metal1metallic , temp_output_41_0);
				float lerpResult112 = lerp( lerpResult110 , _Metal2metallic , temp_output_116_0);
				float lerpResult113 = lerp( lerpResult112 , _Metal3metallic , temp_output_149_0);
				float lerpResult55 = lerp( lerpResult113 , 0.0 , temp_output_156_0);
				
				float lerpResult26 = lerp( 0.0 , _Ceramic1smoothness , temp_output_92_0);
				float lerpResult31 = lerp( lerpResult26 , _Ceramic2smoothness , temp_output_95_0);
				float lerpResult34 = lerp( lerpResult31 , _Ceramic3smoothness , temp_output_96_0);
				float lerpResult42 = lerp( lerpResult34 , _Metal1smootness , temp_output_41_0);
				float lerpResult43 = lerp( lerpResult42 , _Metal2smootness , temp_output_116_0);
				float lerpResult46 = lerp( lerpResult43 , _Metal3smootness , temp_output_149_0);
				

				float3 BaseColor = lerpResult165.rgb;
				float3 Normal = float3(0, 0, 1);
				float3 Emission = 0;
				float3 Specular = 0.5;
				float Metallic = (( _MetallicOn )?( lerpResult55 ):( 0.0 ));
				float Smoothness = (( _SmoothnessOn )?( lerpResult46 ):( 0.0 ));
				float Occlusion = 1;
				float Alpha = 1;
				float AlphaClipThreshold = 0.5;
				float AlphaClipThresholdShadow = 0.5;
				float3 BakedGI = 0;
				float3 RefractionColor = 1;
				float RefractionIndex = 1;
				float3 Transmission = 1;
				float3 Translucency = 1;

				#ifdef ASE_DEPTH_WRITE_ON
					float DepthValue = input.positionCS.z;
				#endif

				#ifdef _ALPHATEST_ON
					clip(Alpha - AlphaClipThreshold);
				#endif

				InputData inputData = (InputData)0;
				inputData.positionWS = WorldPosition;
				inputData.positionCS = input.positionCS;
				inputData.shadowCoord = ShadowCoords;

				#ifdef _NORMALMAP
					#if _NORMAL_DROPOFF_TS
						inputData.normalWS = TransformTangentToWorld(Normal, half3x3( WorldTangent, WorldBiTangent, WorldNormal ));
					#elif _NORMAL_DROPOFF_OS
						inputData.normalWS = TransformObjectToWorldNormal(Normal);
					#elif _NORMAL_DROPOFF_WS
						inputData.normalWS = Normal;
					#endif
				#else
					inputData.normalWS = WorldNormal;
				#endif

				inputData.normalWS = NormalizeNormalPerPixel(inputData.normalWS);
				inputData.viewDirectionWS = SafeNormalize( WorldViewDirection );

				#ifdef ASE_FOG
					// @diogo: no fog applied in GBuffer
				#endif
				#ifdef _ADDITIONAL_LIGHTS_VERTEX
					inputData.vertexLighting = input.fogFactorAndVertexLight.yzw;
				#endif

				#if defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
					float3 SH = SampleSH(inputData.normalWS.xyz);
				#else
					float3 SH = input.lightmapUVOrVertexSH.xyz;
				#endif

				#if defined(DYNAMICLIGHTMAP_ON)
					inputData.bakedGI = SAMPLE_GI(input.lightmapUVOrVertexSH.xy, input.dynamicLightmapUV.xy, SH, inputData.normalWS);
					inputData.shadowMask = SAMPLE_SHADOWMASK(input.lightmapUVOrVertexSH.xy);
				#elif !defined(LIGHTMAP_ON) && (defined(PROBE_VOLUMES_L1) || defined(PROBE_VOLUMES_L2))
					inputData.bakedGI = SAMPLE_GI( SH, GetAbsolutePositionWS(inputData.positionWS),
						inputData.normalWS,
						inputData.viewDirectionWS,
						input.positionCS.xy,
						input.probeOcclusion,
						inputData.shadowMask );
				#else
					inputData.bakedGI = SAMPLE_GI(input.lightmapUVOrVertexSH.xy, SH, inputData.normalWS);
					inputData.shadowMask = SAMPLE_SHADOWMASK(input.lightmapUVOrVertexSH.xy);
				#endif

				#ifdef ASE_BAKEDGI
					inputData.bakedGI = BakedGI;
				#endif

				inputData.normalizedScreenSpaceUV = NormalizedScreenSpaceUV;

				#if defined(DEBUG_DISPLAY)
					#if defined(DYNAMICLIGHTMAP_ON)
						inputData.dynamicLightmapUV = input.dynamicLightmapUV.xy;
						#endif
					#if defined(LIGHTMAP_ON)
						inputData.staticLightmapUV = input.lightmapUVOrVertexSH.xy;
					#else
						inputData.vertexSH = SH;
					#endif
					#if defined(USE_APV_PROBE_OCCLUSION)
						inputData.probeOcclusion = input.probeOcclusion;
					#endif
				#endif

				#ifdef _DBUFFER
					ApplyDecal(input.positionCS,
						BaseColor,
						Specular,
						inputData.normalWS,
						Metallic,
						Occlusion,
						Smoothness);
				#endif

				BRDFData brdfData;
				InitializeBRDFData
				(BaseColor, Metallic, Specular, Smoothness, Alpha, brdfData);

				Light mainLight = GetMainLight(inputData.shadowCoord, inputData.positionWS, inputData.shadowMask);
				half4 color;
				MixRealtimeAndBakedGI(mainLight, inputData.normalWS, inputData.bakedGI, inputData.shadowMask);
				color.rgb = GlobalIllumination(brdfData, inputData.bakedGI, Occlusion, inputData.positionWS, inputData.normalWS, inputData.viewDirectionWS);
				color.a = Alpha;

				#ifdef ASE_FINAL_COLOR_ALPHA_MULTIPLY
					color.rgb *= color.a;
				#endif

				#ifdef ASE_DEPTH_WRITE_ON
					outputDepth = DepthValue;
				#endif

				return BRDFDataToGbuffer(brdfData, inputData, Smoothness, Emission + color.rgb, Occlusion);
			}

			ENDHLSL
		}

		
		Pass
		{
			
			Name "SceneSelectionPass"
			Tags { "LightMode"="SceneSelectionPass" }

			Cull Off
			AlphaToMask Off

			HLSLPROGRAM

			#define _NORMAL_DROPOFF_TS 1
			#define ASE_FOG 1
			#define ASE_VERSION 19800
			#define ASE_SRP_VERSION 170003


			#pragma vertex vert
			#pragma fragment frag

			#if defined(_SPECULAR_SETUP) && defined(_ASE_LIGHTING_SIMPLE)
				#define _SPECULAR_COLOR 1
			#endif

			#define SCENESELECTIONPASS 1

			#define ATTRIBUTES_NEED_NORMAL
			#define ATTRIBUTES_NEED_TANGENT
			#define SHADERPASS SHADERPASS_DEPTHONLY

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
            #include_with_pragmas "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRenderingKeywords.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRendering.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/DebugMipmapStreamingMacros.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

			

			struct Attributes
			{
				float4 positionOS : POSITION;
				float3 normalOS : NORMAL;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct PackedVaryings
			{
				float4 positionCS : SV_POSITION;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _TextureSample2_ST;
			float4 _Coatofarmscolor;
			float4 _Metal3color;
			float4 _Metal2color;
			float4 _Metal1color;
			float4 _Haycolor;
			float4 _Ceramictiles3color;
			float4 _Ceramictiles2color;
			float4 _Ceramictiles1color;
			float4 _Wood3color;
			float4 _Wood2color;
			float4 _Ropecolor;
			float4 _Leather3color;
			float4 _leather2color;
			float4 _Leather1colour;
			float4 _Fabric3color;
			float4 _Fabric2color;
			float4 _Fabric1color;
			float4 _Rock2color;
			float4 _Rock1color;
			float4 _Mortarcolor;
			float4 _Wood1color;
			float _Metal1smootness;
			float _Ceramic3smoothness;
			float _Ceramic2smoothness;
			float _Ceramic1smoothness;
			float _MetallicOn;
			float _Metal3metallic;
			float _Metal2metallic;
			float _Metal1metallic;
			float _Metal2smootness;
			float _SmoothnessOn;
			float _Metal3smootness;
			#ifdef ASE_TRANSMISSION
				float _TransmissionShadow;
			#endif
			#ifdef ASE_TRANSLUCENCY
				float _TransStrength;
				float _TransNormal;
				float _TransScattering;
				float _TransDirect;
				float _TransAmbient;
				float _TransShadow;
			#endif
			#ifdef ASE_TESSELLATION
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END

			#ifdef SCENEPICKINGPASS
				float4 _SelectionID;
			#endif

			#ifdef SCENESELECTIONPASS
				int _ObjectId;
				int _PassValue;
			#endif

			

			
			struct SurfaceDescription
			{
				float Alpha;
				float AlphaClipThreshold;
			};

			PackedVaryings VertexFunction(Attributes input  )
			{
				PackedVaryings output;
				ZERO_INITIALIZE(PackedVaryings, output);

				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_TRANSFER_INSTANCE_ID(input, output);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

				

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = input.positionOS.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif

				float3 vertexValue = defaultVertexValue;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					input.positionOS.xyz = vertexValue;
				#else
					input.positionOS.xyz += vertexValue;
				#endif

				input.normalOS = input.normalOS;

				float3 positionWS = TransformObjectToWorld( input.positionOS.xyz );

				output.positionCS = TransformWorldToHClip(positionWS);

				return output;
			}

			#if defined(ASE_TESSELLATION)
			struct VertexControl
			{
				float4 positionOS : INTERNALTESSPOS;
				float3 normalOS : NORMAL;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( Attributes input )
			{
				VertexControl output;
				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_TRANSFER_INSTANCE_ID(input, output);
				output.positionOS = input.positionOS;
				output.normalOS = input.normalOS;
				
				return output;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> input)
			{
				TessellationFactors output;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(input[0].positionOS, input[1].positionOS, input[2].positionOS, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(input[0].positionOS, input[1].positionOS, input[2].positionOS, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(input[0].positionOS, input[1].positionOS, input[2].positionOS, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				output.edge[0] = tf.x; output.edge[1] = tf.y; output.edge[2] = tf.z; output.inside = tf.w;
				return output;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
				return patch[id];
			}

			[domain("tri")]
			PackedVaryings DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				Attributes output = (Attributes) 0;
				output.positionOS = patch[0].positionOS * bary.x + patch[1].positionOS * bary.y + patch[2].positionOS * bary.z;
				output.normalOS = patch[0].normalOS * bary.x + patch[1].normalOS * bary.y + patch[2].normalOS * bary.z;
				
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = output.positionOS.xyz - patch[i].normalOS * (dot(output.positionOS.xyz, patch[i].normalOS) - dot(patch[i].positionOS.xyz, patch[i].normalOS));
				float phongStrength = _TessPhongStrength;
				output.positionOS.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * output.positionOS.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], output);
				return VertexFunction(output);
			}
			#else
			PackedVaryings vert ( Attributes input )
			{
				return VertexFunction( input );
			}
			#endif

			half4 frag(PackedVaryings input ) : SV_Target
			{
				SurfaceDescription surfaceDescription = (SurfaceDescription)0;

				

				surfaceDescription.Alpha = 1;
				surfaceDescription.AlphaClipThreshold = 0.5;

				#if _ALPHATEST_ON
					float alphaClipThreshold = 0.01f;
					#if ALPHA_CLIP_THRESHOLD
						alphaClipThreshold = surfaceDescription.AlphaClipThreshold;
					#endif
					clip(surfaceDescription.Alpha - alphaClipThreshold);
				#endif

				half4 outColor = 0;

				#ifdef SCENESELECTIONPASS
					outColor = half4(_ObjectId, _PassValue, 1.0, 1.0);
				#elif defined(SCENEPICKINGPASS)
					outColor = _SelectionID;
				#endif

				return outColor;
			}

			ENDHLSL
		}

		
		Pass
		{
			
			Name "ScenePickingPass"
			Tags { "LightMode"="Picking" }

			AlphaToMask Off

			HLSLPROGRAM

			#define _NORMAL_DROPOFF_TS 1
			#define ASE_FOG 1
			#define ASE_VERSION 19800
			#define ASE_SRP_VERSION 170003


			#pragma vertex vert
			#pragma fragment frag

			#if defined(_SPECULAR_SETUP) && defined(_ASE_LIGHTING_SIMPLE)
				#define _SPECULAR_COLOR 1
			#endif

		    #define SCENEPICKINGPASS 1

			#define ATTRIBUTES_NEED_NORMAL
			#define ATTRIBUTES_NEED_TANGENT
			#define SHADERPASS SHADERPASS_DEPTHONLY

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
            #include_with_pragmas "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRenderingKeywords.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRendering.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/DebugMipmapStreamingMacros.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

			

			struct Attributes
			{
				float4 positionOS : POSITION;
				float3 normalOS : NORMAL;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct PackedVaryings
			{
				float4 positionCS : SV_POSITION;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _TextureSample2_ST;
			float4 _Coatofarmscolor;
			float4 _Metal3color;
			float4 _Metal2color;
			float4 _Metal1color;
			float4 _Haycolor;
			float4 _Ceramictiles3color;
			float4 _Ceramictiles2color;
			float4 _Ceramictiles1color;
			float4 _Wood3color;
			float4 _Wood2color;
			float4 _Ropecolor;
			float4 _Leather3color;
			float4 _leather2color;
			float4 _Leather1colour;
			float4 _Fabric3color;
			float4 _Fabric2color;
			float4 _Fabric1color;
			float4 _Rock2color;
			float4 _Rock1color;
			float4 _Mortarcolor;
			float4 _Wood1color;
			float _Metal1smootness;
			float _Ceramic3smoothness;
			float _Ceramic2smoothness;
			float _Ceramic1smoothness;
			float _MetallicOn;
			float _Metal3metallic;
			float _Metal2metallic;
			float _Metal1metallic;
			float _Metal2smootness;
			float _SmoothnessOn;
			float _Metal3smootness;
			#ifdef ASE_TRANSMISSION
				float _TransmissionShadow;
			#endif
			#ifdef ASE_TRANSLUCENCY
				float _TransStrength;
				float _TransNormal;
				float _TransScattering;
				float _TransDirect;
				float _TransAmbient;
				float _TransShadow;
			#endif
			#ifdef ASE_TESSELLATION
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END

			#ifdef SCENEPICKINGPASS
				float4 _SelectionID;
			#endif

			#ifdef SCENESELECTIONPASS
				int _ObjectId;
				int _PassValue;
			#endif

			

			
			struct SurfaceDescription
			{
				float Alpha;
				float AlphaClipThreshold;
			};

			PackedVaryings VertexFunction(Attributes input  )
			{
				PackedVaryings output;
				ZERO_INITIALIZE(PackedVaryings, output);

				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_TRANSFER_INSTANCE_ID(input, output);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

				

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = input.positionOS.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif

				float3 vertexValue = defaultVertexValue;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					input.positionOS.xyz = vertexValue;
				#else
					input.positionOS.xyz += vertexValue;
				#endif

				input.normalOS = input.normalOS;

				float3 positionWS = TransformObjectToWorld( input.positionOS.xyz );
				output.positionCS = TransformWorldToHClip(positionWS);

				return output;
			}

			#if defined(ASE_TESSELLATION)
			struct VertexControl
			{
				float4 positionOS : INTERNALTESSPOS;
				float3 normalOS : NORMAL;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( Attributes input )
			{
				VertexControl output;
				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_TRANSFER_INSTANCE_ID(input, output);
				output.positionOS = input.positionOS;
				output.normalOS = input.normalOS;
				
				return output;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> input)
			{
				TessellationFactors output;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(input[0].positionOS, input[1].positionOS, input[2].positionOS, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(input[0].positionOS, input[1].positionOS, input[2].positionOS, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(input[0].positionOS, input[1].positionOS, input[2].positionOS, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				output.edge[0] = tf.x; output.edge[1] = tf.y; output.edge[2] = tf.z; output.inside = tf.w;
				return output;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
				return patch[id];
			}

			[domain("tri")]
			PackedVaryings DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				Attributes output = (Attributes) 0;
				output.positionOS = patch[0].positionOS * bary.x + patch[1].positionOS * bary.y + patch[2].positionOS * bary.z;
				output.normalOS = patch[0].normalOS * bary.x + patch[1].normalOS * bary.y + patch[2].normalOS * bary.z;
				
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = output.positionOS.xyz - patch[i].normalOS * (dot(output.positionOS.xyz, patch[i].normalOS) - dot(patch[i].positionOS.xyz, patch[i].normalOS));
				float phongStrength = _TessPhongStrength;
				output.positionOS.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * output.positionOS.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], output);
				return VertexFunction(output);
			}
			#else
			PackedVaryings vert ( Attributes input )
			{
				return VertexFunction( input );
			}
			#endif

			half4 frag(PackedVaryings input ) : SV_Target
			{
				SurfaceDescription surfaceDescription = (SurfaceDescription)0;

				

				surfaceDescription.Alpha = 1;
				surfaceDescription.AlphaClipThreshold = 0.5;

				#if _ALPHATEST_ON
					float alphaClipThreshold = 0.01f;
					#if ALPHA_CLIP_THRESHOLD
						alphaClipThreshold = surfaceDescription.AlphaClipThreshold;
					#endif
						clip(surfaceDescription.Alpha - alphaClipThreshold);
				#endif

				half4 outColor = 0;

				#ifdef SCENESELECTIONPASS
					outColor = half4(_ObjectId, _PassValue, 1.0, 1.0);
				#elif defined(SCENEPICKINGPASS)
					outColor = _SelectionID;
				#endif

				return outColor;
			}

			ENDHLSL
		}

		
		Pass
		{
			
			Name "MotionVectors"
			Tags { "LightMode"="MotionVectors" }

			ColorMask RG

			HLSLPROGRAM

			#pragma multi_compile _ALPHATEST_ON
			#define _NORMAL_DROPOFF_TS 1
			#pragma multi_compile_instancing
			#pragma multi_compile _ LOD_FADE_CROSSFADE
			#define ASE_FOG 1
			#define ASE_VERSION 19800
			#define ASE_SRP_VERSION 170003


			#pragma vertex vert
			#pragma fragment frag

			#if defined(_SPECULAR_SETUP) && defined(_ASE_LIGHTING_SIMPLE)
				#define _SPECULAR_COLOR 1
			#endif
	
            #define SHADERPASS SHADERPASS_MOTION_VECTORS

            #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"
			#include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/RenderingLayers.hlsl"
		    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
		    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
		    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
		    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
		    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
		    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
            #include_with_pragmas "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRenderingKeywords.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRendering.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/DebugMipmapStreamingMacros.hlsl"
		    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
		    #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

			#if defined(LOD_FADE_CROSSFADE)
				#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/LODCrossFade.hlsl"
			#endif

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/MotionVectorsCommon.hlsl"

			

			struct Attributes
			{
				float4 positionOS : POSITION;
				float3 positionOld : TEXCOORD4;
				#if _ADD_PRECOMPUTED_VELOCITY
					float3 alembicMotionVector : TEXCOORD5;
				#endif
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct PackedVaryings
			{
				float4 positionCS : SV_POSITION;
				float4 positionCSNoJitter : TEXCOORD0;
				float4 previousPositionCSNoJitter : TEXCOORD1;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _TextureSample2_ST;
			float4 _Coatofarmscolor;
			float4 _Metal3color;
			float4 _Metal2color;
			float4 _Metal1color;
			float4 _Haycolor;
			float4 _Ceramictiles3color;
			float4 _Ceramictiles2color;
			float4 _Ceramictiles1color;
			float4 _Wood3color;
			float4 _Wood2color;
			float4 _Ropecolor;
			float4 _Leather3color;
			float4 _leather2color;
			float4 _Leather1colour;
			float4 _Fabric3color;
			float4 _Fabric2color;
			float4 _Fabric1color;
			float4 _Rock2color;
			float4 _Rock1color;
			float4 _Mortarcolor;
			float4 _Wood1color;
			float _Metal1smootness;
			float _Ceramic3smoothness;
			float _Ceramic2smoothness;
			float _Ceramic1smoothness;
			float _MetallicOn;
			float _Metal3metallic;
			float _Metal2metallic;
			float _Metal1metallic;
			float _Metal2smootness;
			float _SmoothnessOn;
			float _Metal3smootness;
			#ifdef ASE_TRANSMISSION
				float _TransmissionShadow;
			#endif
			#ifdef ASE_TRANSLUCENCY
				float _TransStrength;
				float _TransNormal;
				float _TransScattering;
				float _TransDirect;
				float _TransAmbient;
				float _TransShadow;
			#endif
			#ifdef ASE_TESSELLATION
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END

			#ifdef SCENEPICKINGPASS
				float4 _SelectionID;
			#endif

			#ifdef SCENESELECTIONPASS
				int _ObjectId;
				int _PassValue;
			#endif

			

			
			PackedVaryings VertexFunction( Attributes input  )
			{
				PackedVaryings output = (PackedVaryings)0;
				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_TRANSFER_INSTANCE_ID(input, output);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

				

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = input.positionOS.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif

				float3 vertexValue = defaultVertexValue;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					input.positionOS.xyz = vertexValue;
				#else
					input.positionOS.xyz += vertexValue;
				#endif

				VertexPositionInputs vertexInput = GetVertexPositionInputs( input.positionOS.xyz );

				#if defined(APLICATION_SPACE_WARP_MOTION)
					// We do not need jittered position in ASW
					output.positionCSNoJitter = mul(_NonJitteredViewProjMatrix, mul(UNITY_MATRIX_M, input.positionOS));;
					output.positionCS = output.positionCSNoJitter;
				#else
					// Jittered. Match the frame.
					output.positionCS = vertexInput.positionCS;
					output.positionCSNoJitter = mul( _NonJitteredViewProjMatrix, mul( UNITY_MATRIX_M, input.positionOS));
				#endif

				float4 prevPos = ( unity_MotionVectorsParams.x == 1 ) ? float4( input.positionOld, 1 ) : input.positionOS;

				#if _ADD_PRECOMPUTED_VELOCITY
					prevPos = prevPos - float4(input.alembicMotionVector, 0);
				#endif

				output.previousPositionCSNoJitter = mul( _PrevViewProjMatrix, mul( UNITY_PREV_MATRIX_M, prevPos ) );
				// removed in ObjectMotionVectors.hlsl found in unity 6000.0.23 and higher
				//ApplyMotionVectorZBias( output.positionCS );
				return output;
			}

			PackedVaryings vert ( Attributes input )
			{
				return VertexFunction( input );
			}

			half4 frag(	PackedVaryings input  ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( input );

				

				float Alpha = 1;
				float AlphaClipThreshold = 0.5;

				#ifdef _ALPHATEST_ON
					clip(Alpha - AlphaClipThreshold);
				#endif

				#if defined(LOD_FADE_CROSSFADE)
					LODFadeCrossFade( input.positionCS );
				#endif

				#if defined(APLICATION_SPACE_WARP_MOTION)
					return float4( CalcAswNdcMotionVectorFromCsPositions( input.positionCSNoJitter, input.previousPositionCSNoJitter ), 1 );
				#else
					return float4( CalcNdcMotionVectorFromCsPositions( input.positionCSNoJitter, input.previousPositionCSNoJitter ), 0, 0 );
				#endif
			}		
			ENDHLSL
		}
		
	}
	
	CustomEditor "UnityEditor.ShaderGraphLitGUI"
	FallBack "Hidden/Shader Graph/FallbackError"
	
	Fallback Off
}
/*ASEBEGIN
Version=19800
Node;AmplifyShaderEditor.CommentaryNode;2;-11727.8,601.6224;Inherit;False;2305.307;694.8573;Comment;15;138;137;136;135;128;127;126;125;124;123;122;121;66;9;6;FABRIC COLORS;0.05562881,0.9716981,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;3;-5798.301,-1438.134;Inherit;False;427.9199;359.978;Comment;1;120;MASK TEXTURE;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;4;-5653.832,-1956.559;Inherit;False;341.4902;248.4146;Comment;1;119;BASE TEXTURE;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;5;-9023.379,608.0663;Inherit;False;4637.659;753.5428;Comment;30;107;106;105;104;103;102;101;100;99;98;97;68;67;32;29;27;25;23;22;20;19;18;16;15;13;12;11;10;8;7;WALL&WOOD COLORS;0.735849,0.7152051,0.3158597,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;14;-4153.02,596.6636;Inherit;False;2359.399;695.7338;Comment;15;96;95;94;93;92;91;90;89;88;39;35;33;30;24;17;CERAMIC COLORS;0.4690726,0.7830189,0.47128,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;21;-3597.649,2159.435;Inherit;False;1768.502;211.4459;Comment;6;77;76;75;34;31;26;CERAMIC  SMOOTHNESS;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;28;-1645.72,576.5326;Inherit;False;2342.301;700.673;Comment;10;154;87;86;85;84;73;72;71;70;69;ROPE HAY  COLORS;0.7735849,0.5371538,0.1788003,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;36;908.123,575.767;Inherit;False;2552.517;745.6387;Comment;15;153;152;151;150;149;148;147;118;117;116;111;74;47;41;38;METAL COLORS;0.259434,0.8569208,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;37;1257.24,2110.681;Inherit;False;2210.534;259.0801;Comment;6;81;80;79;46;43;42;METAL SMOOTHNESS;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;40;1246.527,1532.62;Inherit;False;2183.689;260.3257;Comment;6;115;114;113;112;110;78;METAL METALLIC;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;44;3391.176,-994.6529;Inherit;False;1262.249;589.4722;;7;157;156;56;54;53;51;49;COAT OF ARMS;1,0,0.7651567,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;129;-13256.22,655.2789;Inherit;False;1320.625;643.1454;Comment;10;141;134;133;140;143;130;131;132;139;142;ROCK COLORS;0.05562881,0.9716981,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;306;-15001.28,660.3426;Inherit;False;1320.625;643.1454;Comment;12;316;315;314;313;312;311;310;309;308;307;317;318;INTERIOR&MORTAR;0.05562881,0.9716981,0,1;0;0
Node;AmplifyShaderEditor.LerpOp;9;-9606.48,866.7899;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;10;-8102.76,876.0576;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;15;-7478.76,860.0576;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;17;-3840.87,1096.28;Inherit;False;Constant;_Color10;Color 10;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.6039216,0.0627451,0.6470588,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.LerpOp;22;-6208.12,859.874;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;24;-3178.109,1065.361;Inherit;False;Constant;_Color5;Color 5;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.7529413,0.0627451,0.8196079,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.FunctionNode;25;-5872.45,1039.417;Inherit;True;Color Mask;-1;;139;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;26;-3248.52,2213.889;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;27;-5580.63,861.3362;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;31;-2622.24,2214.881;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;32;-5008.12,842.1277;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;33;-2737.8,649.0425;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;34;-2013.149,2209.435;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;39;-1977.62,861.8309;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;42;1865.915,2207.542;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;43;2537.24,2190.681;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;46;3177.24,2190.681;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;47;1952.685,840.8496;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;69;-865.71,631.7888;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;70;-247.5977,628.9116;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;71;-684.6436,846.9166;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;72;-64.19238,844.6108;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;85;-603.5586,1092.531;Inherit;False;Constant;_Color15;Color 15;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.8078432,0.7254902,0.5490196,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.FunctionNode;86;-341.6494,1028.773;Inherit;True;Color Mask;-1;;155;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;87;-941.1699,1032.642;Inherit;True;Color Mask;-1;;156;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;91;-3174.84,867.0479;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;92;-3540.89,1032.595;Inherit;True;Color Mask;-1;;157;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;93;-2554.39,852.516;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;94;-2579.38,1083.599;Inherit;False;Constant;_Color11;Color 11;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.9058824,0.0509804,0.9803922,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.FunctionNode;95;-2907.2,1036.439;Inherit;True;Color Mask;-1;;158;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;96;-2309.47,1036.677;Inherit;True;Color Mask;-1;;159;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;110;1862.643,1623.919;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;112;2582.651,1573.047;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;113;3165.043,1573.725;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;117;2001.787,1117.835;Inherit;False;Constant;_Color7;Color 7;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.2470588,0.3803922,0.4627451,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;118;2676.376,1137.907;Inherit;False;Constant;_Color8;Color 7;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.1921569,0.2941177,0.3529412,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;121;-11434.19,1151.006;Inherit;False;Constant;_Color23;Color 23;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.1058824,0.2352941,0.6980392,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.FunctionNode;122;-11173.81,1057.373;Inherit;True;Color Mask;-1;;163;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;124;-10803.72,872.0068;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;126;-10183.26,857.4746;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;135;-9927.891,1039.456;Inherit;True;Color Mask;-1;;165;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;136;-10207.81,1113.681;Inherit;False;Constant;_Color25;Color 25;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.1294118,0.3176471,0.9921569,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.FunctionNode;137;-10546.03,1020.571;Inherit;True;Color Mask;-1;;166;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;138;-10797.31,1094.721;Inherit;False;Constant;_Color24;Color 24;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.1294118,0.2705882,0.8392158,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;148;2382.021,705.0095;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;150;2617.332,846.6812;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;151;3405.815,868.7943;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;103;-6723.689,1145.791;Inherit;False;Constant;_Color34;Color 34;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.3882353,0.254902,0.0627451,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;104;-6118.91,1151.033;Inherit;False;Constant;_Color33;Color 33;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.5333334,0.4431373,0.3215686,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.LerpOp;55;4321.82,1564.807;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;123;-10928.77,702.3899;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;125;-10307.44,708.9993;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-9785.64,681.6268;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;244;-10625.49,459.1433;Inherit;False;243;BASETEXTURE;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-8159.558,716.8089;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-7647.213,709.4507;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-7100.646,696.2062;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;-6349.994,716.7143;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-5722.873,706.413;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;246;-5924.16,459.5471;Inherit;False;243;BASETEXTURE;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;247;-2714.35,391.4492;Inherit;False;243;BASETEXTURE;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;-3308.171,735.1832;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;-2211.73,654.1045;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;248;-666.146,233.4418;Inherit;False;243;BASETEXTURE;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;147;1814.686,663.9406;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;153;3166.919,727.6151;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;253;-10471.74,1493.962;Inherit;False;251;MASKTEXTURE;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;302;-6121.233,1509.821;Inherit;False;251;MASKTEXTURE;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;303;-3062.826,1511.723;Inherit;False;251;MASKTEXTURE;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;304;-710.8005,1590.452;Inherit;False;251;MASKTEXTURE;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;305;2304.995,1430.737;Inherit;False;251;MASKTEXTURE;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;119;-5623.402,-1895.385;Inherit;True;Property;_TextureSample2;Texture Sample 2;34;1;[HideInInspector];Create;True;0;0;0;False;0;False;-1;e4553ab0125662f42b497b41a40da8e9;e4553ab0125662f42b497b41a40da8e9;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.RegisterLocalVarNode;243;-5237.781,-1904.64;Inherit;False;BASETEXTURE;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;251;-5227.111,-1537.049;Inherit;False;MASKTEXTURE;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;249;2192.794,383.184;Inherit;False;243;BASETEXTURE;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;139;-12802.18,739.1353;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;132;-12636.8,893.1993;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;131;-12912,1013.638;Inherit;True;Color Mask;-1;;170;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;140;-12182.57,703.0303;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;133;-12146.87,899.1673;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;250;-12717.54,538.4932;Inherit;False;243;BASETEXTURE;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;308;-14547.24,744.1994;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;309;-14381.86,898.2629;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;312;-13927.63,708.0933;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;313;-13891.93,904.2319;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;143;-12424.21,700.1229;Inherit;False;Property;_Rock2color;Rock 2 color;10;1;[HDR];Create;True;0;0;0;False;0;False;0.5188679,0.4540594,0.4283108,1;0.2023368,0,0.4339623,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;98;-6027.79,655.4275;Inherit;False;Property;_Wood2color;Wood 2 color;4;1;[HDR];Create;True;0;0;0;False;0;False;0.1981132,0.103908,0.06634924,1;0.6792453,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;89;-3013.319,661.918;Inherit;False;Property;_Ceramictiles2color;Ceramic tiles 2 color;13;1;[HDR];Create;True;0;0;0;False;0;False;0.7924528,0.3776169,0.1682093,1;1,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;90;-2432.66,671.0714;Inherit;False;Property;_Ceramictiles3color;Ceramic tiles 3 color ;15;1;[HDR];Create;True;0;0;0;False;0;False;0.4677838,0.3813261,0.2501584,1;0,0.1142961,0.1698113,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;111;2121.972,708.9114;Inherit;False;Property;_Metal2color;Metal 2 color;20;1;[HDR];Create;True;0;0;0;False;0;False;1.309888,1.123492,1.044203,0;0.9528302,0.9528302,0.9528302,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;152;2826.721,692.4199;Inherit;False;Property;_Metal3color;Metal 3 color;23;1;[HDR];Create;True;0;0;0;False;0;False;0.4578082,0.638907,0.6787086,1;0.3301887,0.3301887,0.3301887,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;38;1330.412,1127.181;Inherit;False;Constant;_Color17;Color 17;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.2784314,0.4470589,0.5490196,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.RangedFloatNode;78;1558.641,1655.919;Inherit;False;Property;_Metal1metallic;Metal 1 metallic;18;0;Create;True;0;0;0;False;0;False;0.65;0.903;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;114;2229.969,1639.058;Inherit;False;Property;_Metal2metallic;Metal 2 metallic;21;0;Create;True;0;0;0;False;0;False;0.65;0.903;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;115;2864.037,1633.553;Inherit;False;Property;_Metal3metallic;Metal 3 metallic;24;0;Create;True;0;0;0;False;0;False;0.65;0.903;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;79;1535.77,2263.888;Inherit;False;Property;_Metal1smootness;Metal 1 smootness;19;0;Create;True;0;0;0;False;0;False;0.7;0.721;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;80;2247.969,2207.952;Inherit;False;Property;_Metal2smootness;Metal 2 smootness;22;0;Create;True;0;0;0;False;0;False;0.7;0.721;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;81;2873.24,2206.681;Inherit;False;Property;_Metal3smootness;Metal 3 smootness;25;0;Create;True;0;0;0;False;0;False;0.7;0.7;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;75;-3547.649,2236.753;Inherit;False;Property;_Ceramic1smoothness;Ceramic 1 smoothness;12;0;Create;True;0;0;0;False;0;False;0.3;0.3;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;76;-2915.24,2236.934;Inherit;False;Property;_Ceramic2smoothness;Ceramic 2 smoothness;14;0;Create;True;0;0;0;False;0;False;0.3;0.3;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;77;-2332.96,2231.924;Inherit;False;Property;_Ceramic3smoothness;Ceramic 3 smoothness;16;0;Create;True;0;0;0;False;0;False;0.3;0.3;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;73;-540.9141,659.7532;Inherit;False;Property;_Haycolor;Hay color;27;2;[HDR];[Header];Create;True;0;0;0;False;0;False;0.764151,0.517899,0.1622019,1;0.4245283,0.190437,0.09011215,1;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;311;-14166.27,704.1863;Inherit;False;Property;_Mortarcolor;Mortar color;28;1;[HDR];Create;True;0;0;0;False;0;False;0.6415094,0.5745595,0.4629761,0;0.2023368,0,0.4339623,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.RangedFloatNode;171;7357.75,2121.079;Float;False;Property;_Transparency;Transparency;33;2;[HideInInspector];[Gamma];Create;True;0;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;64;4840.567,823.2047;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;65;4887.951,785.4938;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;165;5056.114,785.393;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ToggleSwitchNode;166;7032.947,1537.321;Inherit;True;Property;_MetallicOn;Metallic On;31;0;Create;True;0;0;0;False;0;False;1;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;173;7046.772,2121.252;Inherit;True;Property;_SmoothnessOn;Smoothness On;32;0;Create;True;0;0;0;False;0;False;1;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;50;4022.734,1640.753;Inherit;False;Constant;_Float1;Float 0;50;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;88;-3641.46,664.2064;Inherit;False;Property;_Ceramictiles1color;Ceramic tiles 1 color;11;2;[HDR];[Header];Create;True;1;CERAMIC TILES;0;0;False;0;False;0.3207547,0.04869195,0.01059096,1;0.9056604,0.6815338,0.4229263,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;142;-13099.11,707.9922;Inherit;False;Property;_Rock1color;Rock 1 color;9;2;[HDR];[Header];Create;True;1;ROCK ;0;0;False;0;False;0.5088814,0.5149178,0.5088814,1;0,0.1132075,0.01206957,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;97;-6631.768,668.1229;Inherit;False;Property;_Wood1color;Wood 1 color;3;2;[HDR];[Header];Create;True;1;WOOD;0;0;False;0;False;0.4056604,0.2683544,0.135858,1;0,0.1793142,0.7264151,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;154;-1158.091,706.2426;Inherit;False;Property;_Ropecolor;Rope color;26;2;[HDR];[Header];Create;True;1;OTHER COLORS;0;0;False;0;False;0.6037736,0.5810711,0.3389106,1;0.1698113,0.04637412,0.02963688,1;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;74;1444.886,720.598;Inherit;False;Property;_Metal1color;Metal 1 color;17;2;[HDR];[Header];Create;True;1;METAL;0;0;False;0;False;0.385947,0.5532268,0.5566038,0;0.9528302,0.9528302,0.9528302,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;130;-13129.93,1069.544;Inherit;False;Constant;_Color26;Color 23;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.7529413,0.7529413,0.7529413,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.GetLocalVarNode;252;-12949.74,1344.962;Inherit;False;251;MASKTEXTURE;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;49;3768.177,-815.4178;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;53;4160.477,-596.9551;Inherit;False;Constant;_Vector0;Vector 0;1;0;Create;True;0;0;0;False;0;False;1.6,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.ColorNode;54;4136.386,-959.0538;Inherit;False;Constant;_Color4;Color 4;1;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.FunctionNode;56;4389.274,-821.8439;Inherit;False;Replace Color;-1;;171;896dccb3016c847439def376a728b869;1,12,0;5;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;59;4875.945,-795.9808;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;60;4817.969,-492.5753;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;62;4846.844,-440.6251;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;63;4907.966,-761.865;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;156;3960.033,-541.5626;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;51;4140.196,-779.3428;Inherit;False;Property;_Coatofarmscolor;Coat of arms color;29;1;[HDR];Create;True;0;0;0;False;0;False;1,0,0,0;1,0.0990566,0.0990566,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SamplerNode;157;3415.809,-925.424;Inherit;True;Property;_Coarofarmstexture;Coar of arms texture;30;1;[NoScaleOffset];Create;True;0;0;0;False;0;False;-1;None;None;True;1;False;black;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;127;-11196.36,713.4846;Inherit;False;Property;_Fabric1color;Fabric 1 color;6;2;[HDR];[Header];Create;True;1;FABRICS;0;0;False;0;False;1,1,1,0;1,1,1,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;128;-10596.59,675.0156;Inherit;False;Property;_Fabric2color;Fabric 2 color;7;1;[HDR];Create;True;0;0;0;False;0;False;1,0,0.01978827,0;0.4684687,0,1,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;66;-10024.01,673.8386;Inherit;False;Property;_Fabric3color;Fabric 3 color;8;1;[HDR];Create;True;0;0;0;False;0;False;0,0.005197836,0.9245283,0;0.3773585,0,0.06650025,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;84;-1213.859,1111.218;Inherit;False;Constant;_Color13;Color 13;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.8627452,0.8470589,0.7215686,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;105;-7474.15,1153.183;Inherit;False;Constant;_Color31;Color 31;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.0509804,0.9803922,0.3529412,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.LerpOp;19;-6906.455,858.3113;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;245;-7923.934,434.5842;Inherit;False;243;BASETEXTURE;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;67;-8518.76,668.0576;Inherit;False;Property;_Leather1colour;Leather 1 colour;0;2;[HDR];[Header];Create;True;1;WALLS ;0;0;False;0;False;0.5283019,0.192425,0.1420434,1;0,0.1793142,0.7264151,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;100;-7926.76,668.0576;Inherit;False;Property;_leather2color;leather 2 color;1;1;[HDR];Create;True;0;0;0;False;0;False;0.6320754,0.3730699,0.2415005,1;0.6792453,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;68;-7357.584,658.3399;Inherit;False;Property;_Leather3color;Leather 3 color;2;1;[HDR];Create;True;0;0;0;False;0;False;0.8867924,0.6561894,0.23843,1;0.7735849,0.492613,0.492613,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;106;-8141.772,1121.834;Inherit;False;Constant;_Color12;Color 12;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.0627451,0.8274511,0.3098039,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.GetLocalVarNode;254;-8210.346,1503.124;Inherit;True;251;MASKTEXTURE;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;107;-8700.35,1150.605;Inherit;False;Constant;_Color6;Color 6;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.04705883,0.6196079,0.2392157,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.FunctionNode;7;-8447.35,1034.486;Inherit;True;Color Mask;-1;;172;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;11;-7854.699,1034.76;Inherit;True;Color Mask;-1;;173;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;13;-7205.061,1056.685;Inherit;True;Color Mask;-1;;174;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;316;-13998.71,1107.575;Inherit;True;Color Mask;-1;;176;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;314;-14248.8,1108.465;Inherit;False;Constant;_Color28;Color 24;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.9921569,0.9647059,0.0627451,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.FunctionNode;20;-6449.77,1032.8;Inherit;True;Color Mask;-1;;177;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.05;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;41;1555.567,1030.149;Inherit;True;Color Mask;-1;;178;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.05;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;116;2266.754,970.8533;Inherit;True;Color Mask;-1;;179;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.05;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;149;3049.118,978.9347;Inherit;True;Color Mask;-1;;180;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.05;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;120;-5723.415,-1347.72;Inherit;True;Property;_TextureSample9;Texture Sample 9;35;2;[HideInInspector];[NoScaleOffset];Create;True;0;0;0;False;0;False;-1;4adf5af92e3d7c74e8ac79e555d89b61;4adf5af92e3d7c74e8ac79e555d89b61;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;315;-14960.96,1014.247;Inherit;False;Constant;_Color29;Color 23;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.1294118,0.9803922,0.7921569,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;307;-14958,723.0109;Inherit;False;Constant;_color;color;3;1;[HDR];Create;True;0;0;0;False;0;False;1,1,1,0;0,0.1132075,0.01206957,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.FunctionNode;310;-14675.88,986.1497;Inherit;True;Color Mask;-1;;181;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;317;-14855.51,1220.265;Inherit;False;251;MASKTEXTURE;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;318;-14765.15,669.7604;Inherit;False;243;BASETEXTURE;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;102;-5494.277,1249.944;Inherit;False;Constant;_Color32;Color 32;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.6470588,0.5333334,0.3882353,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.FunctionNode;101;-5280.79,1033.502;Inherit;True;Color Mask;-1;;182;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;99;-5513.979,699.5049;Inherit;False;Property;_Wood3color;Wood 3 color;5;1;[HDR];Create;True;0;0;0;False;0;False;0.1886792,0.0907685,0.05606977,1;0.7735849,0.492613,0.492613,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;-5186.606,713.7711;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;134;-12544.18,1080.401;Inherit;False;Constant;_Color27;Color 24;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.2901961,0.2862745,0.2901961,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.FunctionNode;141;-12298.32,1020.326;Inherit;True;Color Mask;-1;;183;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.04;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;319;7890.689,788.4713;Float;False;False;-1;2;UnityEditor.ShaderGraphLitGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;ExtraPrePass;0;0;ExtraPrePass;5;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Lit;True;5;True;12;all;0;False;True;1;1;False;;0;False;;0;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;0;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;321;7890.689,788.4713;Float;False;False;-1;2;UnityEditor.ShaderGraphLitGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;ShadowCaster;0;2;ShadowCaster;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Lit;True;5;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;False;False;True;False;False;False;False;0;False;;False;False;False;False;False;False;False;False;False;True;1;False;;True;3;False;;False;True;1;LightMode=ShadowCaster;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;322;7890.689,788.4713;Float;False;False;-1;2;UnityEditor.ShaderGraphLitGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;DepthOnly;0;3;DepthOnly;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Lit;True;5;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;False;False;True;True;False;False;False;0;False;;False;False;False;False;False;False;False;False;False;True;1;False;;False;False;True;1;LightMode=DepthOnly;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;323;7890.689,788.4713;Float;False;False;-1;2;UnityEditor.ShaderGraphLitGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;Meta;0;4;Meta;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Lit;True;5;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=Meta;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;324;7890.689,788.4713;Float;False;False;-1;2;UnityEditor.ShaderGraphLitGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;Universal2D;0;5;Universal2D;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Lit;True;5;True;12;all;0;False;True;1;1;False;;0;False;;1;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;False;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;1;LightMode=Universal2D;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;325;7890.689,788.4713;Float;False;False;-1;2;UnityEditor.ShaderGraphLitGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;DepthNormals;0;6;DepthNormals;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Lit;True;5;True;12;all;0;False;True;1;1;False;;0;False;;0;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;False;;True;3;False;;False;True;1;LightMode=DepthNormals;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;326;7890.689,788.4713;Float;False;False;-1;2;UnityEditor.ShaderGraphLitGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;GBuffer;0;7;GBuffer;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Lit;True;5;True;12;all;0;False;True;1;1;False;;0;False;;1;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;1;LightMode=UniversalGBuffer;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;327;7890.689,788.4713;Float;False;False;-1;2;UnityEditor.ShaderGraphLitGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;SceneSelectionPass;0;8;SceneSelectionPass;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Lit;True;5;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;2;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=SceneSelectionPass;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;328;7890.689,788.4713;Float;False;False;-1;2;UnityEditor.ShaderGraphLitGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;ScenePickingPass;0;9;ScenePickingPass;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Lit;True;5;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=Picking;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;320;7637.306,767.7871;Float;False;True;-1;2;UnityEditor.ShaderGraphLitGUI;0;12;Polytope Studio/ PT_Medieval Props Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;Forward;0;1;Forward;21;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Lit;True;5;True;12;all;0;False;True;1;1;False;;0;False;;1;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;1;LightMode=UniversalForward;False;False;0;;0;0;Standard;45;Lighting Model;0;0;Workflow;1;0;Surface;0;0;  Refraction Model;0;0;  Blend;0;0;Two Sided;1;0;Alpha Clipping;1;0;  Use Shadow Threshold;0;0;Fragment Normal Space,InvertActionOnDeselection;0;0;Forward Only;0;0;Transmission;0;0;  Transmission Shadow;0.5,False,;0;Translucency;0;0;  Translucency Strength;1,False,;0;  Normal Distortion;0.5,False,;0;  Scattering;2,False,;0;  Direct;0.9,False,;0;  Ambient;0.1,False,;0;  Shadow;0.5,False,;0;Cast Shadows;1;0;Receive Shadows;1;0;Receive SSAO;1;0;Motion Vectors;1;0;  Add Precomputed Velocity;0;0;GPU Instancing;1;0;LOD CrossFade;1;0;Built-in Fog;1;0;_FinalColorxAlpha;0;0;Meta Pass;1;0;Override Baked GI;0;0;Extra Pre Pass;0;0;Tessellation;0;0;  Phong;0;0;  Strength;0.5,False,;0;  Type;0;0;  Tess;16,False,;0;  Min;10,False,;0;  Max;25,False,;0;  Edge Length;16,False,;0;  Max Displacement;25,False,;0;Write Depth;0;0;  Early Z;0;0;Vertex Position,InvertActionOnDeselection;1;0;Debug Display;0;0;Clear Coat;0;0;0;11;False;True;True;True;True;True;True;True;True;True;True;False;;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;329;7637.306,867.7871;Float;False;False;-1;3;UnityEditor.ShaderGraphLitGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;MotionVectors;0;10;MotionVectors;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Lit;True;5;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;True;False;False;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=MotionVectors;False;False;0;;0;0;Standard;0;False;0
WireConnection;9;0;126;0
WireConnection;9;1;6;0
WireConnection;9;2;135;0
WireConnection;10;0;9;0
WireConnection;10;1;8;0
WireConnection;10;2;7;0
WireConnection;15;0;10;0
WireConnection;15;1;12;0
WireConnection;15;2;11;0
WireConnection;22;0;19;0
WireConnection;22;1;18;0
WireConnection;22;2;20;0
WireConnection;25;1;302;0
WireConnection;25;3;104;0
WireConnection;26;1;75;0
WireConnection;26;2;92;0
WireConnection;27;0;22;0
WireConnection;27;1;23;0
WireConnection;27;2;25;0
WireConnection;31;0;26;0
WireConnection;31;1;76;0
WireConnection;31;2;95;0
WireConnection;32;0;27;0
WireConnection;32;1;29;0
WireConnection;32;2;101;0
WireConnection;33;0;247;0
WireConnection;33;1;89;0
WireConnection;34;0;31;0
WireConnection;34;1;77;0
WireConnection;34;2;96;0
WireConnection;39;0;93;0
WireConnection;39;1;35;0
WireConnection;39;2;96;0
WireConnection;42;0;34;0
WireConnection;42;1;79;0
WireConnection;42;2;41;0
WireConnection;43;0;42;0
WireConnection;43;1;80;0
WireConnection;43;2;116;0
WireConnection;46;0;43;0
WireConnection;46;1;81;0
WireConnection;46;2;149;0
WireConnection;47;0;72;0
WireConnection;47;1;147;0
WireConnection;47;2;41;0
WireConnection;69;0;248;0
WireConnection;69;1;154;0
WireConnection;70;0;248;0
WireConnection;70;1;73;0
WireConnection;71;0;39;0
WireConnection;71;1;69;0
WireConnection;71;2;87;0
WireConnection;72;0;71;0
WireConnection;72;1;70;0
WireConnection;72;2;86;0
WireConnection;86;1;304;0
WireConnection;86;3;85;0
WireConnection;87;1;304;0
WireConnection;87;3;84;0
WireConnection;91;0;32;0
WireConnection;91;1;30;0
WireConnection;91;2;92;0
WireConnection;92;1;303;0
WireConnection;92;3;17;0
WireConnection;93;0;91;0
WireConnection;93;1;33;0
WireConnection;93;2;95;0
WireConnection;95;1;303;0
WireConnection;95;3;24;0
WireConnection;96;1;303;0
WireConnection;96;3;94;0
WireConnection;110;1;78;0
WireConnection;110;2;41;0
WireConnection;112;0;110;0
WireConnection;112;1;114;0
WireConnection;112;2;116;0
WireConnection;113;0;112;0
WireConnection;113;1;115;0
WireConnection;113;2;149;0
WireConnection;122;1;253;0
WireConnection;122;3;121;0
WireConnection;124;0;133;0
WireConnection;124;1;123;0
WireConnection;124;2;122;0
WireConnection;126;0;124;0
WireConnection;126;1;125;0
WireConnection;126;2;137;0
WireConnection;135;1;253;0
WireConnection;135;3;136;0
WireConnection;137;1;253;0
WireConnection;137;3;138;0
WireConnection;148;0;249;0
WireConnection;148;1;111;0
WireConnection;150;0;47;0
WireConnection;150;1;148;0
WireConnection;150;2;116;0
WireConnection;151;0;150;0
WireConnection;151;1;153;0
WireConnection;151;2;149;0
WireConnection;55;0;113;0
WireConnection;55;1;50;0
WireConnection;55;2;156;0
WireConnection;123;0;244;0
WireConnection;123;1;127;0
WireConnection;125;0;244;0
WireConnection;125;1;128;0
WireConnection;6;0;244;0
WireConnection;6;1;66;0
WireConnection;8;0;245;0
WireConnection;8;1;67;0
WireConnection;12;0;245;0
WireConnection;12;1;100;0
WireConnection;16;0;245;0
WireConnection;16;1;68;0
WireConnection;18;0;246;0
WireConnection;18;1;97;0
WireConnection;23;0;246;0
WireConnection;23;1;98;0
WireConnection;30;0;247;0
WireConnection;30;1;88;0
WireConnection;35;0;247;0
WireConnection;35;1;90;0
WireConnection;147;0;249;0
WireConnection;147;1;74;0
WireConnection;153;0;249;0
WireConnection;153;1;152;0
WireConnection;243;0;119;0
WireConnection;251;0;120;0
WireConnection;139;0;250;0
WireConnection;139;1;142;0
WireConnection;132;0;313;0
WireConnection;132;1;139;0
WireConnection;132;2;131;0
WireConnection;131;1;252;0
WireConnection;131;3;130;0
WireConnection;140;0;250;0
WireConnection;140;1;143;0
WireConnection;133;0;132;0
WireConnection;133;1;140;0
WireConnection;133;2;141;0
WireConnection;308;0;318;0
WireConnection;308;1;307;0
WireConnection;309;1;308;0
WireConnection;309;2;310;0
WireConnection;312;0;318;0
WireConnection;312;1;311;0
WireConnection;313;0;309;0
WireConnection;313;1;312;0
WireConnection;313;2;316;0
WireConnection;64;0;62;0
WireConnection;65;0;63;0
WireConnection;165;0;151;0
WireConnection;165;1;65;0
WireConnection;165;2;64;0
WireConnection;166;1;55;0
WireConnection;173;1;46;0
WireConnection;49;0;157;4
WireConnection;56;1;49;0
WireConnection;56;2;54;0
WireConnection;56;3;51;0
WireConnection;56;4;53;1
WireConnection;56;5;53;2
WireConnection;59;0;56;0
WireConnection;60;0;156;0
WireConnection;62;0;60;0
WireConnection;63;0;59;0
WireConnection;156;0;49;0
WireConnection;19;0;15;0
WireConnection;19;1;16;0
WireConnection;19;2;13;0
WireConnection;7;1;254;0
WireConnection;7;3;107;0
WireConnection;11;1;254;0
WireConnection;11;3;106;0
WireConnection;13;1;254;0
WireConnection;13;3;105;0
WireConnection;316;1;317;0
WireConnection;316;3;314;0
WireConnection;20;1;302;0
WireConnection;20;3;103;0
WireConnection;41;1;305;0
WireConnection;41;3;38;0
WireConnection;116;1;305;0
WireConnection;116;3;117;0
WireConnection;149;1;305;0
WireConnection;149;3;118;0
WireConnection;310;1;317;0
WireConnection;310;3;315;0
WireConnection;101;1;302;0
WireConnection;101;3;102;0
WireConnection;29;0;246;0
WireConnection;29;1;99;0
WireConnection;141;1;252;0
WireConnection;141;3;134;0
WireConnection;320;0;165;0
WireConnection;320;3;166;0
WireConnection;320;4;173;0
ASEEND*/
//CHKSM=71AF423B6F993889D71CA3AD65C1BAF5E32B56B9