// Made with Amplify Shader Editor v1.9.8
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Polytope Studio/ PT_Medieval Weapons Shader PBR"
{
	Properties
	{
		[HideInInspector] _EmissionColor("Emission Color", Color) = (1,1,1,1)
		[HideInInspector] _AlphaCutoff("Alpha Cutoff ", Range(0, 1)) = 0.5
		[HDR]_METAL1COLOR("METAL 1 COLOR", Color) = (0.7261481,0.7735849,0.7528313,0)
		_METAL1METALLIC("METAL 1 METALLIC", Range( 0 , 1)) = 0.65
		_METAL1SMOOTHNESS("METAL 1 SMOOTHNESS", Range( 0 , 1)) = 0.7
		[HDR]_METAL2COLOR("METAL 2 COLOR", Color) = (1.678431,1.003922,0.1176471,0)
		_METAL2METALLIC("METAL 2 METALLIC", Range( 0 , 1)) = 0.65
		_METAL2SMOOTHNESS("METAL 2 SMOOTHNESS", Range( 0 , 1)) = 0.7
		[HDR]_METAL3COLOR("METAL 3 COLOR", Color) = (0.597023,0.6237553,0.7395956,0)
		_METAL3METALLIC("METAL 3 METALLIC", Range( 0 , 1)) = 0.65
		_METAL3SMOOTHNESS("METAL 3 SMOOTHNESS", Range( 0 , 1)) = 0.7
		[HDR]_METAL4COLOR("METAL 4 COLOR", Color) = (0.8791043,0.8044721,0.9422547,0)
		_METAL4METALLIC("METAL 4 METALLIC", Range( 0 , 1)) = 0.65
		_METAL4SMOOTNESS("METAL 4 SMOOTNESS", Range( 0 , 1)) = 0.7
		[HDR]_WOOD1COLOR("WOOD 1 COLOR", Color) = (0.1981132,0.08345769,0.06261124,0)
		_WOOD1SMOOTHNESS("WOOD 1 SMOOTHNESS", Range( 0 , 1)) = 0.3
		[HDR]_WOOD2COLOR("WOOD 2 COLOR", Color) = (0.1320755,0.06452555,0.05420079,0)
		_WOOD2SMOOTHNESS("WOOD 2 SMOOTHNESS", Range( 0 , 1)) = 0.3
		[HDR]_WOOD3COLOR("WOOD 3 COLOR", Color) = (0.1037736,0.07509367,0.04650232,0)
		_WOOD3SMOOTHNESS("WOOD 3 SMOOTHNESS", Range( 0 , 1)) = 0.3
		[HDR]_LEATHER1COLOR("LEATHER 1 COLOR", Color) = (0.2924528,0.1296404,0.09242612,1)
		_LEATHER1SMOOTHNESS("LEATHER 1 SMOOTHNESS", Range( 0 , 1)) = 0.3
		[HDR]_LEATHER2COLOR("LEATHER 2 COLOR", Color) = (0.06603771,0.03523636,0.03146137,1)
		_LEATHER2SMOOTHNESS("LEATHER 2 SMOOTHNESS", Range( 0 , 1)) = 0.3
		[HDR]_LEATHER3COLOR("LEATHER 3 COLOR", Color) = (0.1320755,0.03139969,0.02180491,1)
		_LEATHER3SMOOTHNESS("LEATHER 3 SMOOTHNESS", Range( 0 , 1)) = 0.3
		[HDR]_PAINT1COLOR("PAINT 1 COLOR", Color) = (0.5450981,0.6936808,0.6980392,0)
		_PAINT1SMOOTHNESS("PAINT 1 SMOOTHNESS", Range( 0 , 1)) = 1
		[HDR]_PAINT2COLOR("PAINT 2 COLOR", Color) = (0.3649431,0.5566038,0.4386422,0)
		_PAINT2SMOOTHNESS("PAINT 2 SMOOTHNESS", Range( 0 , 1)) = 0
		[HDR]_PAINT3COLOR("PAINT 3 COLOR", Color) = (0.5849056,0.5418971,0.4331613,0)
		_PAINT3SMOOTHNESS("PAINT 3 SMOOTHNESS", Range( 0 , 1)) = 0
		[HDR]_GEMS1COLOR("GEMS 1 COLOR", Color) = (1,0,0,0)
		_GEMS1SMOOTHNESS("GEMS 1 SMOOTHNESS", Range( 0 , 1)) = 0.5
		[HDR]_GEMS2COLOR("GEMS 2 COLOR", Color) = (0,0.3218706,0.5754717,0)
		_GEMS2SMOOTHNESS("GEMS 2 SMOOTHNESS", Range( 0 , 1)) = 0.4
		[HDR]_GEMS3COLOR("GEMS 3 COLOR", Color) = (0,0.4716981,0.1359325,0)
		_GEMS3SMOOTHNESS("GEMS 3 SMOOTHNESS", Range( 0 , 1)) = 0.3
		[HDR]_FEATHERS1COLOR("FEATHERS 1 COLOR", Color) = (0.3301887,0.1241556,0.04516733,0)
		[HDR]_FEATHERS2COLOR("FEATHERS 2 COLOR", Color) = (0.509434,0.4260285,0.1802243,0)
		[HDR]_FEATHERS3COLOR("FEATHERS 3 COLOR", Color) = (0.509434,0.25712,0.25712,0)
		[HDR]_FEATHERS4COLOR("FEATHERS 4 COLOR", Color) = (0.8113208,0.2104842,0.2104842,0)
		[HDR]_FEATHERS5COLOR("FEATHERS 5 COLOR", Color) = (0.4150943,0.2615769,0.1468494,0)
		[HDR]_FEATHERS6COLOR("FEATHERS 6 COLOR", Color) = (0.7924528,0.7444169,0.6391954,0)
		[HDR]_COATOFARMSCOLOR("COAT OF ARMS COLOR", Color) = (1,0,0,0)
		[NoScaleOffset]_COATOFARMSMASK("COAT OF ARMS MASK", 2D) = "black" {}
		_OCCLUSION("OCCLUSION", Range( 0 , 1)) = 0.4139509
		[Toggle]_MetalicOn("Metalic On", Float) = 1
		[Toggle]_SmoothnessOn("Smoothness On", Float) = 1
		[HideInInspector]_TextureSample2("Texture Sample 2", 2D) = "white" {}
		[HideInInspector]_TextureSample9("Texture Sample 9", 2D) = "white" {}
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
			float4 _COATOFARMSCOLOR;
			float4 _GEMS2COLOR;
			float4 _GEMS3COLOR;
			float4 _METAL1COLOR;
			float4 _METAL2COLOR;
			float4 _METAL3COLOR;
			float4 _METAL4COLOR;
			float4 _LEATHER1COLOR;
			float4 _LEATHER2COLOR;
			float4 _LEATHER3COLOR;
			float4 _WOOD1COLOR;
			float4 _GEMS1COLOR;
			float4 _WOOD3COLOR;
			float4 _FEATHERS1COLOR;
			float4 _FEATHERS2COLOR;
			float4 _FEATHERS3COLOR;
			float4 _FEATHERS4COLOR;
			float4 _FEATHERS5COLOR;
			float4 _FEATHERS6COLOR;
			float4 _PAINT1COLOR;
			float4 _PAINT2COLOR;
			float4 _TextureSample9_ST;
			float4 _PAINT3COLOR;
			float4 _WOOD2COLOR;
			float _LEATHER2SMOOTHNESS;
			float _LEATHER1SMOOTHNESS;
			float _METAL4SMOOTNESS;
			float _GEMS2SMOOTHNESS;
			float _METAL2SMOOTHNESS;
			float _METAL1SMOOTHNESS;
			float _GEMS3SMOOTHNESS;
			float _LEATHER3SMOOTHNESS;
			float _METAL3SMOOTHNESS;
			float _WOOD1SMOOTHNESS;
			float _SmoothnessOn;
			float _WOOD3SMOOTHNESS;
			float _PAINT1SMOOTHNESS;
			float _PAINT2SMOOTHNESS;
			float _PAINT3SMOOTHNESS;
			float _METAL1METALLIC;
			float _METAL2METALLIC;
			float _METAL3METALLIC;
			float _METAL4METALLIC;
			float _MetalicOn;
			float _GEMS1SMOOTHNESS;
			float _WOOD2SMOOTHNESS;
			float _OCCLUSION;
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
			sampler2D _COATOFARMSMASK;


			
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
				float4 tex2DNode199 = tex2D( _TextureSample2, uv_TextureSample2 );
				float4 color684 = IsGammaSpace() ? float4(1,0.4980392,0.4980392,1) : float4(1,0.2122307,0.2122307,1);
				float2 uv_TextureSample9 = input.ase_texcoord9.xy * _TextureSample9_ST.xy + _TextureSample9_ST.zw;
				float4 tex2DNode647 = tex2D( _TextureSample9, uv_TextureSample9 );
				float temp_output_686_0 = saturate( ( 1.0 - ( ( distance( color684.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult219 = lerp( float4( 0,0,0,0 ) , ( tex2DNode199 * _PAINT3COLOR ) , temp_output_686_0);
				float4 color687 = IsGammaSpace() ? float4(0.4980392,0.4980392,0.4980392,1) : float4(0.2122307,0.2122307,0.2122307,1);
				float temp_output_683_0 = saturate( ( 1.0 - ( ( distance( color687.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult214 = lerp( lerpResult219 , ( tex2DNode199 * _PAINT2COLOR ) , temp_output_683_0);
				float4 color688 = IsGammaSpace() ? float4(0.4980392,0.4980392,0,1) : float4(0.2122307,0.2122307,0,1);
				float temp_output_685_0 = saturate( ( 1.0 - ( ( distance( color688.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult217 = lerp( lerpResult214 , ( tex2DNode199 * _PAINT1COLOR ) , temp_output_685_0);
				float4 color665 = IsGammaSpace() ? float4(0,0.4980392,0,1) : float4(0,0.2122307,0,1);
				float4 lerpResult182 = lerp( lerpResult217 , ( tex2DNode199 * _FEATHERS6COLOR ) , saturate( ( 1.0 - ( ( distance( color665.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color670 = IsGammaSpace() ? float4(0,0,0,1) : float4(0,0,0,1);
				float4 lerpResult189 = lerp( lerpResult182 , ( tex2DNode199 * _FEATHERS5COLOR ) , saturate( ( 1.0 - ( ( distance( color670.rgb , tex2DNode647.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color714 = IsGammaSpace() ? float4(1,1,0,1) : float4(1,1,0,1);
				float4 lerpResult184 = lerp( lerpResult189 , ( tex2DNode199 * _FEATHERS4COLOR ) , saturate( ( 1.0 - ( ( distance( color714.rgb , tex2DNode647.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color719 = IsGammaSpace() ? float4(0.4980392,0,0,1) : float4(0.2122307,0,0,1);
				float4 lerpResult700 = lerp( lerpResult184 , ( tex2DNode199 * _FEATHERS3COLOR ) , saturate( ( 1.0 - ( ( distance( color719.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color717 = IsGammaSpace() ? float4(1,0.4980392,0,1) : float4(1,0.2122307,0,1);
				float4 lerpResult701 = lerp( lerpResult700 , ( tex2DNode199 * _FEATHERS2COLOR ) , saturate( ( 1.0 - ( ( distance( color717.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color715 = IsGammaSpace() ? float4(1,0,0,1) : float4(1,0,0,1);
				float4 lerpResult703 = lerp( lerpResult701 , ( tex2DNode199 * _FEATHERS1COLOR ) , saturate( ( 1.0 - ( ( distance( color715.rgb , tex2DNode647.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color660 = IsGammaSpace() ? float4(0,0,1,0) : float4(0,0,1,0);
				float temp_output_659_0 = saturate( ( 1.0 - ( ( distance( color660.rgb , tex2DNode647.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult171 = lerp( lerpResult703 , ( tex2DNode199 * _WOOD3COLOR ) , temp_output_659_0);
				float4 color664 = IsGammaSpace() ? float4(0,1,1,1) : float4(0,1,1,1);
				float temp_output_663_0 = saturate( ( 1.0 - ( ( distance( color664.rgb , tex2DNode647.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult178 = lerp( lerpResult171 , ( tex2DNode199 * _WOOD2COLOR ) , temp_output_663_0);
				float4 color668 = IsGammaSpace() ? float4(0,1,0,1) : float4(0,1,0,1);
				float temp_output_667_0 = saturate( ( 1.0 - ( ( distance( color668.rgb , tex2DNode647.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult173 = lerp( lerpResult178 , ( tex2DNode199 * _WOOD1COLOR ) , temp_output_667_0);
				float4 color671 = IsGammaSpace() ? float4(1,0.4980392,1,1) : float4(1,0.2122307,1,1);
				float temp_output_672_0 = saturate( ( 1.0 - ( ( distance( color671.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult160 = lerp( lerpResult173 , ( tex2DNode199 * _LEATHER3COLOR ) , temp_output_672_0);
				float4 color674 = IsGammaSpace() ? float4(1,0,1,1) : float4(1,0,1,1);
				float temp_output_673_0 = saturate( ( 1.0 - ( ( distance( color674.rgb , tex2DNode647.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult167 = lerp( lerpResult160 , ( tex2DNode199 * _LEATHER2COLOR ) , temp_output_673_0);
				float4 color676 = IsGammaSpace() ? float4(1,1,0.4980392,1) : float4(1,1,0.2122307,1);
				float temp_output_675_0 = saturate( ( 1.0 - ( ( distance( color676.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult162 = lerp( lerpResult167 , ( tex2DNode199 * _LEATHER1COLOR ) , temp_output_675_0);
				float4 color680 = IsGammaSpace() ? float4(0.4980392,0.4980392,1,1) : float4(0.2122307,0.2122307,1,1);
				float temp_output_679_0 = saturate( ( 1.0 - ( ( distance( color680.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult727 = lerp( lerpResult162 , ( tex2DNode199 * _METAL4COLOR ) , temp_output_679_0);
				float4 color693 = IsGammaSpace() ? float4(0,0.4980392,0.4980392,1) : float4(0,0.2122307,0.2122307,1);
				float temp_output_694_0 = saturate( ( 1.0 - ( ( distance( color693.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult118 = lerp( lerpResult727 , ( tex2DNode199 * _METAL3COLOR ) , temp_output_694_0);
				float4 color681 = IsGammaSpace() ? float4(0,0,0.4980392,1) : float4(0,0,0.2122307,1);
				float temp_output_678_0 = saturate( ( 1.0 - ( ( distance( color681.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult128 = lerp( lerpResult118 , ( tex2DNode199 * _METAL2COLOR ) , temp_output_678_0);
				float4 color682 = IsGammaSpace() ? float4(0.4980392,0,0.4980392,1) : float4(0.2122307,0,0.2122307,1);
				float temp_output_677_0 = saturate( ( 1.0 - ( ( distance( color682.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult122 = lerp( lerpResult128 , ( tex2DNode199 * _METAL1COLOR ) , temp_output_677_0);
				float4 color689 = IsGammaSpace() ? float4(0.4980392,1,1,1) : float4(0.2122307,1,1,1);
				float temp_output_690_0 = saturate( ( 1.0 - ( ( distance( color689.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult148 = lerp( lerpResult122 , ( tex2DNode199 * _GEMS3COLOR ) , temp_output_690_0);
				float4 color729 = IsGammaSpace() ? float4(0.4980392,1,0.4980392,1) : float4(0.2122307,1,0.2122307,1);
				float temp_output_728_0 = saturate( ( 1.0 - ( ( distance( color729.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult151 = lerp( lerpResult148 , ( tex2DNode199 * _GEMS2COLOR ) , temp_output_728_0);
				float4 color661 = IsGammaSpace() ? float4(0.4980392,0,1,1) : float4(0.2122307,0,1,1);
				float temp_output_662_0 = saturate( ( 1.0 - ( ( distance( color661.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult153 = lerp( lerpResult151 , ( tex2DNode199 * _GEMS1COLOR ) , temp_output_662_0);
				float2 uv1_COATOFARMSMASK10 = input.ase_texcoord9.zw;
				float temp_output_9_0 = ( 1.0 - tex2D( _COATOFARMSMASK, uv1_COATOFARMSMASK10 ).a );
				float4 temp_cast_44 = (temp_output_9_0).xxxx;
				float4 temp_output_1_0_g153 = temp_cast_44;
				float4 color25 = IsGammaSpace() ? float4(0,0,0,0) : float4(0,0,0,0);
				float4 temp_output_2_0_g153 = color25;
				float temp_output_11_0_g153 = distance( temp_output_1_0_g153 , temp_output_2_0_g153 );
				float2 _Vector0 = float2(1.6,1);
				float4 lerpResult21_g153 = lerp( _COATOFARMSCOLOR , temp_output_1_0_g153 , saturate( ( ( temp_output_11_0_g153 - _Vector0.x ) / max( _Vector0.y , 1E-05 ) ) ));
				float temp_output_16_0 = ( 1.0 - temp_output_9_0 );
				float4 lerpResult64 = lerp( lerpResult153 , lerpResult21_g153 , temp_output_16_0);
				
				float lerpResult723 = lerp( 0.0 , _METAL4METALLIC , temp_output_679_0);
				float lerpResult315 = lerp( lerpResult723 , _METAL3METALLIC , temp_output_694_0);
				float lerpResult319 = lerp( lerpResult315 , _METAL2METALLIC , temp_output_678_0);
				float lerpResult316 = lerp( lerpResult319 , _METAL1METALLIC , temp_output_677_0);
				float lerpResult734 = lerp( lerpResult316 , 0.0 , temp_output_16_0);
				
				float lerpResult342 = lerp( 0.0 , _PAINT3SMOOTHNESS , temp_output_686_0);
				float lerpResult338 = lerp( lerpResult342 , _PAINT2SMOOTHNESS , temp_output_683_0);
				float lerpResult340 = lerp( lerpResult338 , _PAINT1SMOOTHNESS , temp_output_685_0);
				float lerpResult707 = lerp( lerpResult340 , _WOOD3SMOOTHNESS , temp_output_659_0);
				float lerpResult708 = lerp( lerpResult707 , _WOOD2SMOOTHNESS , temp_output_663_0);
				float lerpResult710 = lerp( lerpResult708 , _WOOD1SMOOTHNESS , temp_output_667_0);
				float lerpResult336 = lerp( lerpResult710 , _LEATHER3SMOOTHNESS , temp_output_672_0);
				float lerpResult332 = lerp( lerpResult336 , _LEATHER2SMOOTHNESS , temp_output_673_0);
				float lerpResult334 = lerp( lerpResult332 , _LEATHER1SMOOTHNESS , temp_output_675_0);
				float lerpResult722 = lerp( lerpResult334 , _METAL4SMOOTNESS , temp_output_679_0);
				float lerpResult327 = lerp( lerpResult722 , _METAL3SMOOTHNESS , temp_output_694_0);
				float lerpResult331 = lerp( lerpResult327 , _METAL2SMOOTHNESS , temp_output_678_0);
				float lerpResult328 = lerp( lerpResult331 , _METAL1SMOOTHNESS , temp_output_677_0);
				float lerpResult321 = lerp( lerpResult328 , _GEMS3SMOOTHNESS , temp_output_690_0);
				float lerpResult325 = lerp( lerpResult321 , _GEMS2SMOOTHNESS , temp_output_728_0);
				float lerpResult322 = lerp( lerpResult325 , _GEMS1SMOOTHNESS , temp_output_662_0);
				

				float3 BaseColor = lerpResult64.rgb;
				float3 Normal = float3(0, 0, 1);
				float3 Emission = 0;
				float3 Specular = 0.5;
				float Metallic = (( _MetalicOn )?( lerpResult734 ):( 0.0 ));
				float Smoothness = (( _SmoothnessOn )?( lerpResult322 ):( 0.0 ));
				float Occlusion = (1.0 + (_OCCLUSION - 0.0) * (0.5 - 1.0) / (1.0 - 0.0));
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
			float4 _COATOFARMSCOLOR;
			float4 _GEMS2COLOR;
			float4 _GEMS3COLOR;
			float4 _METAL1COLOR;
			float4 _METAL2COLOR;
			float4 _METAL3COLOR;
			float4 _METAL4COLOR;
			float4 _LEATHER1COLOR;
			float4 _LEATHER2COLOR;
			float4 _LEATHER3COLOR;
			float4 _WOOD1COLOR;
			float4 _GEMS1COLOR;
			float4 _WOOD3COLOR;
			float4 _FEATHERS1COLOR;
			float4 _FEATHERS2COLOR;
			float4 _FEATHERS3COLOR;
			float4 _FEATHERS4COLOR;
			float4 _FEATHERS5COLOR;
			float4 _FEATHERS6COLOR;
			float4 _PAINT1COLOR;
			float4 _PAINT2COLOR;
			float4 _TextureSample9_ST;
			float4 _PAINT3COLOR;
			float4 _WOOD2COLOR;
			float _LEATHER2SMOOTHNESS;
			float _LEATHER1SMOOTHNESS;
			float _METAL4SMOOTNESS;
			float _GEMS2SMOOTHNESS;
			float _METAL2SMOOTHNESS;
			float _METAL1SMOOTHNESS;
			float _GEMS3SMOOTHNESS;
			float _LEATHER3SMOOTHNESS;
			float _METAL3SMOOTHNESS;
			float _WOOD1SMOOTHNESS;
			float _SmoothnessOn;
			float _WOOD3SMOOTHNESS;
			float _PAINT1SMOOTHNESS;
			float _PAINT2SMOOTHNESS;
			float _PAINT3SMOOTHNESS;
			float _METAL1METALLIC;
			float _METAL2METALLIC;
			float _METAL3METALLIC;
			float _METAL4METALLIC;
			float _MetalicOn;
			float _GEMS1SMOOTHNESS;
			float _WOOD2SMOOTHNESS;
			float _OCCLUSION;
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
			ColorMask 0
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
			float4 _COATOFARMSCOLOR;
			float4 _GEMS2COLOR;
			float4 _GEMS3COLOR;
			float4 _METAL1COLOR;
			float4 _METAL2COLOR;
			float4 _METAL3COLOR;
			float4 _METAL4COLOR;
			float4 _LEATHER1COLOR;
			float4 _LEATHER2COLOR;
			float4 _LEATHER3COLOR;
			float4 _WOOD1COLOR;
			float4 _GEMS1COLOR;
			float4 _WOOD3COLOR;
			float4 _FEATHERS1COLOR;
			float4 _FEATHERS2COLOR;
			float4 _FEATHERS3COLOR;
			float4 _FEATHERS4COLOR;
			float4 _FEATHERS5COLOR;
			float4 _FEATHERS6COLOR;
			float4 _PAINT1COLOR;
			float4 _PAINT2COLOR;
			float4 _TextureSample9_ST;
			float4 _PAINT3COLOR;
			float4 _WOOD2COLOR;
			float _LEATHER2SMOOTHNESS;
			float _LEATHER1SMOOTHNESS;
			float _METAL4SMOOTNESS;
			float _GEMS2SMOOTHNESS;
			float _METAL2SMOOTHNESS;
			float _METAL1SMOOTHNESS;
			float _GEMS3SMOOTHNESS;
			float _LEATHER3SMOOTHNESS;
			float _METAL3SMOOTHNESS;
			float _WOOD1SMOOTHNESS;
			float _SmoothnessOn;
			float _WOOD3SMOOTHNESS;
			float _PAINT1SMOOTHNESS;
			float _PAINT2SMOOTHNESS;
			float _PAINT3SMOOTHNESS;
			float _METAL1METALLIC;
			float _METAL2METALLIC;
			float _METAL3METALLIC;
			float _METAL4METALLIC;
			float _MetalicOn;
			float _GEMS1SMOOTHNESS;
			float _WOOD2SMOOTHNESS;
			float _OCCLUSION;
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
			float4 _COATOFARMSCOLOR;
			float4 _GEMS2COLOR;
			float4 _GEMS3COLOR;
			float4 _METAL1COLOR;
			float4 _METAL2COLOR;
			float4 _METAL3COLOR;
			float4 _METAL4COLOR;
			float4 _LEATHER1COLOR;
			float4 _LEATHER2COLOR;
			float4 _LEATHER3COLOR;
			float4 _WOOD1COLOR;
			float4 _GEMS1COLOR;
			float4 _WOOD3COLOR;
			float4 _FEATHERS1COLOR;
			float4 _FEATHERS2COLOR;
			float4 _FEATHERS3COLOR;
			float4 _FEATHERS4COLOR;
			float4 _FEATHERS5COLOR;
			float4 _FEATHERS6COLOR;
			float4 _PAINT1COLOR;
			float4 _PAINT2COLOR;
			float4 _TextureSample9_ST;
			float4 _PAINT3COLOR;
			float4 _WOOD2COLOR;
			float _LEATHER2SMOOTHNESS;
			float _LEATHER1SMOOTHNESS;
			float _METAL4SMOOTNESS;
			float _GEMS2SMOOTHNESS;
			float _METAL2SMOOTHNESS;
			float _METAL1SMOOTHNESS;
			float _GEMS3SMOOTHNESS;
			float _LEATHER3SMOOTHNESS;
			float _METAL3SMOOTHNESS;
			float _WOOD1SMOOTHNESS;
			float _SmoothnessOn;
			float _WOOD3SMOOTHNESS;
			float _PAINT1SMOOTHNESS;
			float _PAINT2SMOOTHNESS;
			float _PAINT3SMOOTHNESS;
			float _METAL1METALLIC;
			float _METAL2METALLIC;
			float _METAL3METALLIC;
			float _METAL4METALLIC;
			float _MetalicOn;
			float _GEMS1SMOOTHNESS;
			float _WOOD2SMOOTHNESS;
			float _OCCLUSION;
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
			sampler2D _COATOFARMSMASK;


			
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
				float4 tex2DNode199 = tex2D( _TextureSample2, uv_TextureSample2 );
				float4 color684 = IsGammaSpace() ? float4(1,0.4980392,0.4980392,1) : float4(1,0.2122307,0.2122307,1);
				float2 uv_TextureSample9 = input.ase_texcoord4.xy * _TextureSample9_ST.xy + _TextureSample9_ST.zw;
				float4 tex2DNode647 = tex2D( _TextureSample9, uv_TextureSample9 );
				float temp_output_686_0 = saturate( ( 1.0 - ( ( distance( color684.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult219 = lerp( float4( 0,0,0,0 ) , ( tex2DNode199 * _PAINT3COLOR ) , temp_output_686_0);
				float4 color687 = IsGammaSpace() ? float4(0.4980392,0.4980392,0.4980392,1) : float4(0.2122307,0.2122307,0.2122307,1);
				float temp_output_683_0 = saturate( ( 1.0 - ( ( distance( color687.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult214 = lerp( lerpResult219 , ( tex2DNode199 * _PAINT2COLOR ) , temp_output_683_0);
				float4 color688 = IsGammaSpace() ? float4(0.4980392,0.4980392,0,1) : float4(0.2122307,0.2122307,0,1);
				float temp_output_685_0 = saturate( ( 1.0 - ( ( distance( color688.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult217 = lerp( lerpResult214 , ( tex2DNode199 * _PAINT1COLOR ) , temp_output_685_0);
				float4 color665 = IsGammaSpace() ? float4(0,0.4980392,0,1) : float4(0,0.2122307,0,1);
				float4 lerpResult182 = lerp( lerpResult217 , ( tex2DNode199 * _FEATHERS6COLOR ) , saturate( ( 1.0 - ( ( distance( color665.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color670 = IsGammaSpace() ? float4(0,0,0,1) : float4(0,0,0,1);
				float4 lerpResult189 = lerp( lerpResult182 , ( tex2DNode199 * _FEATHERS5COLOR ) , saturate( ( 1.0 - ( ( distance( color670.rgb , tex2DNode647.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color714 = IsGammaSpace() ? float4(1,1,0,1) : float4(1,1,0,1);
				float4 lerpResult184 = lerp( lerpResult189 , ( tex2DNode199 * _FEATHERS4COLOR ) , saturate( ( 1.0 - ( ( distance( color714.rgb , tex2DNode647.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color719 = IsGammaSpace() ? float4(0.4980392,0,0,1) : float4(0.2122307,0,0,1);
				float4 lerpResult700 = lerp( lerpResult184 , ( tex2DNode199 * _FEATHERS3COLOR ) , saturate( ( 1.0 - ( ( distance( color719.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color717 = IsGammaSpace() ? float4(1,0.4980392,0,1) : float4(1,0.2122307,0,1);
				float4 lerpResult701 = lerp( lerpResult700 , ( tex2DNode199 * _FEATHERS2COLOR ) , saturate( ( 1.0 - ( ( distance( color717.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color715 = IsGammaSpace() ? float4(1,0,0,1) : float4(1,0,0,1);
				float4 lerpResult703 = lerp( lerpResult701 , ( tex2DNode199 * _FEATHERS1COLOR ) , saturate( ( 1.0 - ( ( distance( color715.rgb , tex2DNode647.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color660 = IsGammaSpace() ? float4(0,0,1,0) : float4(0,0,1,0);
				float temp_output_659_0 = saturate( ( 1.0 - ( ( distance( color660.rgb , tex2DNode647.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult171 = lerp( lerpResult703 , ( tex2DNode199 * _WOOD3COLOR ) , temp_output_659_0);
				float4 color664 = IsGammaSpace() ? float4(0,1,1,1) : float4(0,1,1,1);
				float temp_output_663_0 = saturate( ( 1.0 - ( ( distance( color664.rgb , tex2DNode647.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult178 = lerp( lerpResult171 , ( tex2DNode199 * _WOOD2COLOR ) , temp_output_663_0);
				float4 color668 = IsGammaSpace() ? float4(0,1,0,1) : float4(0,1,0,1);
				float temp_output_667_0 = saturate( ( 1.0 - ( ( distance( color668.rgb , tex2DNode647.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult173 = lerp( lerpResult178 , ( tex2DNode199 * _WOOD1COLOR ) , temp_output_667_0);
				float4 color671 = IsGammaSpace() ? float4(1,0.4980392,1,1) : float4(1,0.2122307,1,1);
				float temp_output_672_0 = saturate( ( 1.0 - ( ( distance( color671.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult160 = lerp( lerpResult173 , ( tex2DNode199 * _LEATHER3COLOR ) , temp_output_672_0);
				float4 color674 = IsGammaSpace() ? float4(1,0,1,1) : float4(1,0,1,1);
				float temp_output_673_0 = saturate( ( 1.0 - ( ( distance( color674.rgb , tex2DNode647.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult167 = lerp( lerpResult160 , ( tex2DNode199 * _LEATHER2COLOR ) , temp_output_673_0);
				float4 color676 = IsGammaSpace() ? float4(1,1,0.4980392,1) : float4(1,1,0.2122307,1);
				float temp_output_675_0 = saturate( ( 1.0 - ( ( distance( color676.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult162 = lerp( lerpResult167 , ( tex2DNode199 * _LEATHER1COLOR ) , temp_output_675_0);
				float4 color680 = IsGammaSpace() ? float4(0.4980392,0.4980392,1,1) : float4(0.2122307,0.2122307,1,1);
				float temp_output_679_0 = saturate( ( 1.0 - ( ( distance( color680.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult727 = lerp( lerpResult162 , ( tex2DNode199 * _METAL4COLOR ) , temp_output_679_0);
				float4 color693 = IsGammaSpace() ? float4(0,0.4980392,0.4980392,1) : float4(0,0.2122307,0.2122307,1);
				float temp_output_694_0 = saturate( ( 1.0 - ( ( distance( color693.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult118 = lerp( lerpResult727 , ( tex2DNode199 * _METAL3COLOR ) , temp_output_694_0);
				float4 color681 = IsGammaSpace() ? float4(0,0,0.4980392,1) : float4(0,0,0.2122307,1);
				float temp_output_678_0 = saturate( ( 1.0 - ( ( distance( color681.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult128 = lerp( lerpResult118 , ( tex2DNode199 * _METAL2COLOR ) , temp_output_678_0);
				float4 color682 = IsGammaSpace() ? float4(0.4980392,0,0.4980392,1) : float4(0.2122307,0,0.2122307,1);
				float temp_output_677_0 = saturate( ( 1.0 - ( ( distance( color682.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult122 = lerp( lerpResult128 , ( tex2DNode199 * _METAL1COLOR ) , temp_output_677_0);
				float4 color689 = IsGammaSpace() ? float4(0.4980392,1,1,1) : float4(0.2122307,1,1,1);
				float temp_output_690_0 = saturate( ( 1.0 - ( ( distance( color689.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult148 = lerp( lerpResult122 , ( tex2DNode199 * _GEMS3COLOR ) , temp_output_690_0);
				float4 color729 = IsGammaSpace() ? float4(0.4980392,1,0.4980392,1) : float4(0.2122307,1,0.2122307,1);
				float temp_output_728_0 = saturate( ( 1.0 - ( ( distance( color729.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult151 = lerp( lerpResult148 , ( tex2DNode199 * _GEMS2COLOR ) , temp_output_728_0);
				float4 color661 = IsGammaSpace() ? float4(0.4980392,0,1,1) : float4(0.2122307,0,1,1);
				float temp_output_662_0 = saturate( ( 1.0 - ( ( distance( color661.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult153 = lerp( lerpResult151 , ( tex2DNode199 * _GEMS1COLOR ) , temp_output_662_0);
				float2 uv1_COATOFARMSMASK10 = input.ase_texcoord4.zw;
				float temp_output_9_0 = ( 1.0 - tex2D( _COATOFARMSMASK, uv1_COATOFARMSMASK10 ).a );
				float4 temp_cast_44 = (temp_output_9_0).xxxx;
				float4 temp_output_1_0_g153 = temp_cast_44;
				float4 color25 = IsGammaSpace() ? float4(0,0,0,0) : float4(0,0,0,0);
				float4 temp_output_2_0_g153 = color25;
				float temp_output_11_0_g153 = distance( temp_output_1_0_g153 , temp_output_2_0_g153 );
				float2 _Vector0 = float2(1.6,1);
				float4 lerpResult21_g153 = lerp( _COATOFARMSCOLOR , temp_output_1_0_g153 , saturate( ( ( temp_output_11_0_g153 - _Vector0.x ) / max( _Vector0.y , 1E-05 ) ) ));
				float temp_output_16_0 = ( 1.0 - temp_output_9_0 );
				float4 lerpResult64 = lerp( lerpResult153 , lerpResult21_g153 , temp_output_16_0);
				

				float3 BaseColor = lerpResult64.rgb;
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
			float4 _COATOFARMSCOLOR;
			float4 _GEMS2COLOR;
			float4 _GEMS3COLOR;
			float4 _METAL1COLOR;
			float4 _METAL2COLOR;
			float4 _METAL3COLOR;
			float4 _METAL4COLOR;
			float4 _LEATHER1COLOR;
			float4 _LEATHER2COLOR;
			float4 _LEATHER3COLOR;
			float4 _WOOD1COLOR;
			float4 _GEMS1COLOR;
			float4 _WOOD3COLOR;
			float4 _FEATHERS1COLOR;
			float4 _FEATHERS2COLOR;
			float4 _FEATHERS3COLOR;
			float4 _FEATHERS4COLOR;
			float4 _FEATHERS5COLOR;
			float4 _FEATHERS6COLOR;
			float4 _PAINT1COLOR;
			float4 _PAINT2COLOR;
			float4 _TextureSample9_ST;
			float4 _PAINT3COLOR;
			float4 _WOOD2COLOR;
			float _LEATHER2SMOOTHNESS;
			float _LEATHER1SMOOTHNESS;
			float _METAL4SMOOTNESS;
			float _GEMS2SMOOTHNESS;
			float _METAL2SMOOTHNESS;
			float _METAL1SMOOTHNESS;
			float _GEMS3SMOOTHNESS;
			float _LEATHER3SMOOTHNESS;
			float _METAL3SMOOTHNESS;
			float _WOOD1SMOOTHNESS;
			float _SmoothnessOn;
			float _WOOD3SMOOTHNESS;
			float _PAINT1SMOOTHNESS;
			float _PAINT2SMOOTHNESS;
			float _PAINT3SMOOTHNESS;
			float _METAL1METALLIC;
			float _METAL2METALLIC;
			float _METAL3METALLIC;
			float _METAL4METALLIC;
			float _MetalicOn;
			float _GEMS1SMOOTHNESS;
			float _WOOD2SMOOTHNESS;
			float _OCCLUSION;
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
			sampler2D _COATOFARMSMASK;


			
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
				float4 tex2DNode199 = tex2D( _TextureSample2, uv_TextureSample2 );
				float4 color684 = IsGammaSpace() ? float4(1,0.4980392,0.4980392,1) : float4(1,0.2122307,0.2122307,1);
				float2 uv_TextureSample9 = input.ase_texcoord2.xy * _TextureSample9_ST.xy + _TextureSample9_ST.zw;
				float4 tex2DNode647 = tex2D( _TextureSample9, uv_TextureSample9 );
				float temp_output_686_0 = saturate( ( 1.0 - ( ( distance( color684.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult219 = lerp( float4( 0,0,0,0 ) , ( tex2DNode199 * _PAINT3COLOR ) , temp_output_686_0);
				float4 color687 = IsGammaSpace() ? float4(0.4980392,0.4980392,0.4980392,1) : float4(0.2122307,0.2122307,0.2122307,1);
				float temp_output_683_0 = saturate( ( 1.0 - ( ( distance( color687.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult214 = lerp( lerpResult219 , ( tex2DNode199 * _PAINT2COLOR ) , temp_output_683_0);
				float4 color688 = IsGammaSpace() ? float4(0.4980392,0.4980392,0,1) : float4(0.2122307,0.2122307,0,1);
				float temp_output_685_0 = saturate( ( 1.0 - ( ( distance( color688.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult217 = lerp( lerpResult214 , ( tex2DNode199 * _PAINT1COLOR ) , temp_output_685_0);
				float4 color665 = IsGammaSpace() ? float4(0,0.4980392,0,1) : float4(0,0.2122307,0,1);
				float4 lerpResult182 = lerp( lerpResult217 , ( tex2DNode199 * _FEATHERS6COLOR ) , saturate( ( 1.0 - ( ( distance( color665.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color670 = IsGammaSpace() ? float4(0,0,0,1) : float4(0,0,0,1);
				float4 lerpResult189 = lerp( lerpResult182 , ( tex2DNode199 * _FEATHERS5COLOR ) , saturate( ( 1.0 - ( ( distance( color670.rgb , tex2DNode647.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color714 = IsGammaSpace() ? float4(1,1,0,1) : float4(1,1,0,1);
				float4 lerpResult184 = lerp( lerpResult189 , ( tex2DNode199 * _FEATHERS4COLOR ) , saturate( ( 1.0 - ( ( distance( color714.rgb , tex2DNode647.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color719 = IsGammaSpace() ? float4(0.4980392,0,0,1) : float4(0.2122307,0,0,1);
				float4 lerpResult700 = lerp( lerpResult184 , ( tex2DNode199 * _FEATHERS3COLOR ) , saturate( ( 1.0 - ( ( distance( color719.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color717 = IsGammaSpace() ? float4(1,0.4980392,0,1) : float4(1,0.2122307,0,1);
				float4 lerpResult701 = lerp( lerpResult700 , ( tex2DNode199 * _FEATHERS2COLOR ) , saturate( ( 1.0 - ( ( distance( color717.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color715 = IsGammaSpace() ? float4(1,0,0,1) : float4(1,0,0,1);
				float4 lerpResult703 = lerp( lerpResult701 , ( tex2DNode199 * _FEATHERS1COLOR ) , saturate( ( 1.0 - ( ( distance( color715.rgb , tex2DNode647.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color660 = IsGammaSpace() ? float4(0,0,1,0) : float4(0,0,1,0);
				float temp_output_659_0 = saturate( ( 1.0 - ( ( distance( color660.rgb , tex2DNode647.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult171 = lerp( lerpResult703 , ( tex2DNode199 * _WOOD3COLOR ) , temp_output_659_0);
				float4 color664 = IsGammaSpace() ? float4(0,1,1,1) : float4(0,1,1,1);
				float temp_output_663_0 = saturate( ( 1.0 - ( ( distance( color664.rgb , tex2DNode647.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult178 = lerp( lerpResult171 , ( tex2DNode199 * _WOOD2COLOR ) , temp_output_663_0);
				float4 color668 = IsGammaSpace() ? float4(0,1,0,1) : float4(0,1,0,1);
				float temp_output_667_0 = saturate( ( 1.0 - ( ( distance( color668.rgb , tex2DNode647.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult173 = lerp( lerpResult178 , ( tex2DNode199 * _WOOD1COLOR ) , temp_output_667_0);
				float4 color671 = IsGammaSpace() ? float4(1,0.4980392,1,1) : float4(1,0.2122307,1,1);
				float temp_output_672_0 = saturate( ( 1.0 - ( ( distance( color671.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult160 = lerp( lerpResult173 , ( tex2DNode199 * _LEATHER3COLOR ) , temp_output_672_0);
				float4 color674 = IsGammaSpace() ? float4(1,0,1,1) : float4(1,0,1,1);
				float temp_output_673_0 = saturate( ( 1.0 - ( ( distance( color674.rgb , tex2DNode647.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult167 = lerp( lerpResult160 , ( tex2DNode199 * _LEATHER2COLOR ) , temp_output_673_0);
				float4 color676 = IsGammaSpace() ? float4(1,1,0.4980392,1) : float4(1,1,0.2122307,1);
				float temp_output_675_0 = saturate( ( 1.0 - ( ( distance( color676.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult162 = lerp( lerpResult167 , ( tex2DNode199 * _LEATHER1COLOR ) , temp_output_675_0);
				float4 color680 = IsGammaSpace() ? float4(0.4980392,0.4980392,1,1) : float4(0.2122307,0.2122307,1,1);
				float temp_output_679_0 = saturate( ( 1.0 - ( ( distance( color680.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult727 = lerp( lerpResult162 , ( tex2DNode199 * _METAL4COLOR ) , temp_output_679_0);
				float4 color693 = IsGammaSpace() ? float4(0,0.4980392,0.4980392,1) : float4(0,0.2122307,0.2122307,1);
				float temp_output_694_0 = saturate( ( 1.0 - ( ( distance( color693.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult118 = lerp( lerpResult727 , ( tex2DNode199 * _METAL3COLOR ) , temp_output_694_0);
				float4 color681 = IsGammaSpace() ? float4(0,0,0.4980392,1) : float4(0,0,0.2122307,1);
				float temp_output_678_0 = saturate( ( 1.0 - ( ( distance( color681.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult128 = lerp( lerpResult118 , ( tex2DNode199 * _METAL2COLOR ) , temp_output_678_0);
				float4 color682 = IsGammaSpace() ? float4(0.4980392,0,0.4980392,1) : float4(0.2122307,0,0.2122307,1);
				float temp_output_677_0 = saturate( ( 1.0 - ( ( distance( color682.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult122 = lerp( lerpResult128 , ( tex2DNode199 * _METAL1COLOR ) , temp_output_677_0);
				float4 color689 = IsGammaSpace() ? float4(0.4980392,1,1,1) : float4(0.2122307,1,1,1);
				float temp_output_690_0 = saturate( ( 1.0 - ( ( distance( color689.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult148 = lerp( lerpResult122 , ( tex2DNode199 * _GEMS3COLOR ) , temp_output_690_0);
				float4 color729 = IsGammaSpace() ? float4(0.4980392,1,0.4980392,1) : float4(0.2122307,1,0.2122307,1);
				float temp_output_728_0 = saturate( ( 1.0 - ( ( distance( color729.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult151 = lerp( lerpResult148 , ( tex2DNode199 * _GEMS2COLOR ) , temp_output_728_0);
				float4 color661 = IsGammaSpace() ? float4(0.4980392,0,1,1) : float4(0.2122307,0,1,1);
				float temp_output_662_0 = saturate( ( 1.0 - ( ( distance( color661.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult153 = lerp( lerpResult151 , ( tex2DNode199 * _GEMS1COLOR ) , temp_output_662_0);
				float2 uv1_COATOFARMSMASK10 = input.ase_texcoord2.zw;
				float temp_output_9_0 = ( 1.0 - tex2D( _COATOFARMSMASK, uv1_COATOFARMSMASK10 ).a );
				float4 temp_cast_44 = (temp_output_9_0).xxxx;
				float4 temp_output_1_0_g153 = temp_cast_44;
				float4 color25 = IsGammaSpace() ? float4(0,0,0,0) : float4(0,0,0,0);
				float4 temp_output_2_0_g153 = color25;
				float temp_output_11_0_g153 = distance( temp_output_1_0_g153 , temp_output_2_0_g153 );
				float2 _Vector0 = float2(1.6,1);
				float4 lerpResult21_g153 = lerp( _COATOFARMSCOLOR , temp_output_1_0_g153 , saturate( ( ( temp_output_11_0_g153 - _Vector0.x ) / max( _Vector0.y , 1E-05 ) ) ));
				float temp_output_16_0 = ( 1.0 - temp_output_9_0 );
				float4 lerpResult64 = lerp( lerpResult153 , lerpResult21_g153 , temp_output_16_0);
				

				float3 BaseColor = lerpResult64.rgb;
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
			float4 _COATOFARMSCOLOR;
			float4 _GEMS2COLOR;
			float4 _GEMS3COLOR;
			float4 _METAL1COLOR;
			float4 _METAL2COLOR;
			float4 _METAL3COLOR;
			float4 _METAL4COLOR;
			float4 _LEATHER1COLOR;
			float4 _LEATHER2COLOR;
			float4 _LEATHER3COLOR;
			float4 _WOOD1COLOR;
			float4 _GEMS1COLOR;
			float4 _WOOD3COLOR;
			float4 _FEATHERS1COLOR;
			float4 _FEATHERS2COLOR;
			float4 _FEATHERS3COLOR;
			float4 _FEATHERS4COLOR;
			float4 _FEATHERS5COLOR;
			float4 _FEATHERS6COLOR;
			float4 _PAINT1COLOR;
			float4 _PAINT2COLOR;
			float4 _TextureSample9_ST;
			float4 _PAINT3COLOR;
			float4 _WOOD2COLOR;
			float _LEATHER2SMOOTHNESS;
			float _LEATHER1SMOOTHNESS;
			float _METAL4SMOOTNESS;
			float _GEMS2SMOOTHNESS;
			float _METAL2SMOOTHNESS;
			float _METAL1SMOOTHNESS;
			float _GEMS3SMOOTHNESS;
			float _LEATHER3SMOOTHNESS;
			float _METAL3SMOOTHNESS;
			float _WOOD1SMOOTHNESS;
			float _SmoothnessOn;
			float _WOOD3SMOOTHNESS;
			float _PAINT1SMOOTHNESS;
			float _PAINT2SMOOTHNESS;
			float _PAINT3SMOOTHNESS;
			float _METAL1METALLIC;
			float _METAL2METALLIC;
			float _METAL3METALLIC;
			float _METAL4METALLIC;
			float _MetalicOn;
			float _GEMS1SMOOTHNESS;
			float _WOOD2SMOOTHNESS;
			float _OCCLUSION;
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
			float4 _COATOFARMSCOLOR;
			float4 _GEMS2COLOR;
			float4 _GEMS3COLOR;
			float4 _METAL1COLOR;
			float4 _METAL2COLOR;
			float4 _METAL3COLOR;
			float4 _METAL4COLOR;
			float4 _LEATHER1COLOR;
			float4 _LEATHER2COLOR;
			float4 _LEATHER3COLOR;
			float4 _WOOD1COLOR;
			float4 _GEMS1COLOR;
			float4 _WOOD3COLOR;
			float4 _FEATHERS1COLOR;
			float4 _FEATHERS2COLOR;
			float4 _FEATHERS3COLOR;
			float4 _FEATHERS4COLOR;
			float4 _FEATHERS5COLOR;
			float4 _FEATHERS6COLOR;
			float4 _PAINT1COLOR;
			float4 _PAINT2COLOR;
			float4 _TextureSample9_ST;
			float4 _PAINT3COLOR;
			float4 _WOOD2COLOR;
			float _LEATHER2SMOOTHNESS;
			float _LEATHER1SMOOTHNESS;
			float _METAL4SMOOTNESS;
			float _GEMS2SMOOTHNESS;
			float _METAL2SMOOTHNESS;
			float _METAL1SMOOTHNESS;
			float _GEMS3SMOOTHNESS;
			float _LEATHER3SMOOTHNESS;
			float _METAL3SMOOTHNESS;
			float _WOOD1SMOOTHNESS;
			float _SmoothnessOn;
			float _WOOD3SMOOTHNESS;
			float _PAINT1SMOOTHNESS;
			float _PAINT2SMOOTHNESS;
			float _PAINT3SMOOTHNESS;
			float _METAL1METALLIC;
			float _METAL2METALLIC;
			float _METAL3METALLIC;
			float _METAL4METALLIC;
			float _MetalicOn;
			float _GEMS1SMOOTHNESS;
			float _WOOD2SMOOTHNESS;
			float _OCCLUSION;
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
			sampler2D _COATOFARMSMASK;


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
				float4 tex2DNode199 = tex2D( _TextureSample2, uv_TextureSample2 );
				float4 color684 = IsGammaSpace() ? float4(1,0.4980392,0.4980392,1) : float4(1,0.2122307,0.2122307,1);
				float2 uv_TextureSample9 = input.ase_texcoord9.xy * _TextureSample9_ST.xy + _TextureSample9_ST.zw;
				float4 tex2DNode647 = tex2D( _TextureSample9, uv_TextureSample9 );
				float temp_output_686_0 = saturate( ( 1.0 - ( ( distance( color684.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult219 = lerp( float4( 0,0,0,0 ) , ( tex2DNode199 * _PAINT3COLOR ) , temp_output_686_0);
				float4 color687 = IsGammaSpace() ? float4(0.4980392,0.4980392,0.4980392,1) : float4(0.2122307,0.2122307,0.2122307,1);
				float temp_output_683_0 = saturate( ( 1.0 - ( ( distance( color687.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult214 = lerp( lerpResult219 , ( tex2DNode199 * _PAINT2COLOR ) , temp_output_683_0);
				float4 color688 = IsGammaSpace() ? float4(0.4980392,0.4980392,0,1) : float4(0.2122307,0.2122307,0,1);
				float temp_output_685_0 = saturate( ( 1.0 - ( ( distance( color688.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult217 = lerp( lerpResult214 , ( tex2DNode199 * _PAINT1COLOR ) , temp_output_685_0);
				float4 color665 = IsGammaSpace() ? float4(0,0.4980392,0,1) : float4(0,0.2122307,0,1);
				float4 lerpResult182 = lerp( lerpResult217 , ( tex2DNode199 * _FEATHERS6COLOR ) , saturate( ( 1.0 - ( ( distance( color665.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color670 = IsGammaSpace() ? float4(0,0,0,1) : float4(0,0,0,1);
				float4 lerpResult189 = lerp( lerpResult182 , ( tex2DNode199 * _FEATHERS5COLOR ) , saturate( ( 1.0 - ( ( distance( color670.rgb , tex2DNode647.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color714 = IsGammaSpace() ? float4(1,1,0,1) : float4(1,1,0,1);
				float4 lerpResult184 = lerp( lerpResult189 , ( tex2DNode199 * _FEATHERS4COLOR ) , saturate( ( 1.0 - ( ( distance( color714.rgb , tex2DNode647.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color719 = IsGammaSpace() ? float4(0.4980392,0,0,1) : float4(0.2122307,0,0,1);
				float4 lerpResult700 = lerp( lerpResult184 , ( tex2DNode199 * _FEATHERS3COLOR ) , saturate( ( 1.0 - ( ( distance( color719.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color717 = IsGammaSpace() ? float4(1,0.4980392,0,1) : float4(1,0.2122307,0,1);
				float4 lerpResult701 = lerp( lerpResult700 , ( tex2DNode199 * _FEATHERS2COLOR ) , saturate( ( 1.0 - ( ( distance( color717.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color715 = IsGammaSpace() ? float4(1,0,0,1) : float4(1,0,0,1);
				float4 lerpResult703 = lerp( lerpResult701 , ( tex2DNode199 * _FEATHERS1COLOR ) , saturate( ( 1.0 - ( ( distance( color715.rgb , tex2DNode647.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color660 = IsGammaSpace() ? float4(0,0,1,0) : float4(0,0,1,0);
				float temp_output_659_0 = saturate( ( 1.0 - ( ( distance( color660.rgb , tex2DNode647.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult171 = lerp( lerpResult703 , ( tex2DNode199 * _WOOD3COLOR ) , temp_output_659_0);
				float4 color664 = IsGammaSpace() ? float4(0,1,1,1) : float4(0,1,1,1);
				float temp_output_663_0 = saturate( ( 1.0 - ( ( distance( color664.rgb , tex2DNode647.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult178 = lerp( lerpResult171 , ( tex2DNode199 * _WOOD2COLOR ) , temp_output_663_0);
				float4 color668 = IsGammaSpace() ? float4(0,1,0,1) : float4(0,1,0,1);
				float temp_output_667_0 = saturate( ( 1.0 - ( ( distance( color668.rgb , tex2DNode647.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult173 = lerp( lerpResult178 , ( tex2DNode199 * _WOOD1COLOR ) , temp_output_667_0);
				float4 color671 = IsGammaSpace() ? float4(1,0.4980392,1,1) : float4(1,0.2122307,1,1);
				float temp_output_672_0 = saturate( ( 1.0 - ( ( distance( color671.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult160 = lerp( lerpResult173 , ( tex2DNode199 * _LEATHER3COLOR ) , temp_output_672_0);
				float4 color674 = IsGammaSpace() ? float4(1,0,1,1) : float4(1,0,1,1);
				float temp_output_673_0 = saturate( ( 1.0 - ( ( distance( color674.rgb , tex2DNode647.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult167 = lerp( lerpResult160 , ( tex2DNode199 * _LEATHER2COLOR ) , temp_output_673_0);
				float4 color676 = IsGammaSpace() ? float4(1,1,0.4980392,1) : float4(1,1,0.2122307,1);
				float temp_output_675_0 = saturate( ( 1.0 - ( ( distance( color676.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult162 = lerp( lerpResult167 , ( tex2DNode199 * _LEATHER1COLOR ) , temp_output_675_0);
				float4 color680 = IsGammaSpace() ? float4(0.4980392,0.4980392,1,1) : float4(0.2122307,0.2122307,1,1);
				float temp_output_679_0 = saturate( ( 1.0 - ( ( distance( color680.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult727 = lerp( lerpResult162 , ( tex2DNode199 * _METAL4COLOR ) , temp_output_679_0);
				float4 color693 = IsGammaSpace() ? float4(0,0.4980392,0.4980392,1) : float4(0,0.2122307,0.2122307,1);
				float temp_output_694_0 = saturate( ( 1.0 - ( ( distance( color693.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult118 = lerp( lerpResult727 , ( tex2DNode199 * _METAL3COLOR ) , temp_output_694_0);
				float4 color681 = IsGammaSpace() ? float4(0,0,0.4980392,1) : float4(0,0,0.2122307,1);
				float temp_output_678_0 = saturate( ( 1.0 - ( ( distance( color681.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult128 = lerp( lerpResult118 , ( tex2DNode199 * _METAL2COLOR ) , temp_output_678_0);
				float4 color682 = IsGammaSpace() ? float4(0.4980392,0,0.4980392,1) : float4(0.2122307,0,0.2122307,1);
				float temp_output_677_0 = saturate( ( 1.0 - ( ( distance( color682.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult122 = lerp( lerpResult128 , ( tex2DNode199 * _METAL1COLOR ) , temp_output_677_0);
				float4 color689 = IsGammaSpace() ? float4(0.4980392,1,1,1) : float4(0.2122307,1,1,1);
				float temp_output_690_0 = saturate( ( 1.0 - ( ( distance( color689.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult148 = lerp( lerpResult122 , ( tex2DNode199 * _GEMS3COLOR ) , temp_output_690_0);
				float4 color729 = IsGammaSpace() ? float4(0.4980392,1,0.4980392,1) : float4(0.2122307,1,0.2122307,1);
				float temp_output_728_0 = saturate( ( 1.0 - ( ( distance( color729.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult151 = lerp( lerpResult148 , ( tex2DNode199 * _GEMS2COLOR ) , temp_output_728_0);
				float4 color661 = IsGammaSpace() ? float4(0.4980392,0,1,1) : float4(0.2122307,0,1,1);
				float temp_output_662_0 = saturate( ( 1.0 - ( ( distance( color661.rgb , tex2DNode647.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult153 = lerp( lerpResult151 , ( tex2DNode199 * _GEMS1COLOR ) , temp_output_662_0);
				float2 uv1_COATOFARMSMASK10 = input.ase_texcoord9.zw;
				float temp_output_9_0 = ( 1.0 - tex2D( _COATOFARMSMASK, uv1_COATOFARMSMASK10 ).a );
				float4 temp_cast_44 = (temp_output_9_0).xxxx;
				float4 temp_output_1_0_g153 = temp_cast_44;
				float4 color25 = IsGammaSpace() ? float4(0,0,0,0) : float4(0,0,0,0);
				float4 temp_output_2_0_g153 = color25;
				float temp_output_11_0_g153 = distance( temp_output_1_0_g153 , temp_output_2_0_g153 );
				float2 _Vector0 = float2(1.6,1);
				float4 lerpResult21_g153 = lerp( _COATOFARMSCOLOR , temp_output_1_0_g153 , saturate( ( ( temp_output_11_0_g153 - _Vector0.x ) / max( _Vector0.y , 1E-05 ) ) ));
				float temp_output_16_0 = ( 1.0 - temp_output_9_0 );
				float4 lerpResult64 = lerp( lerpResult153 , lerpResult21_g153 , temp_output_16_0);
				
				float lerpResult723 = lerp( 0.0 , _METAL4METALLIC , temp_output_679_0);
				float lerpResult315 = lerp( lerpResult723 , _METAL3METALLIC , temp_output_694_0);
				float lerpResult319 = lerp( lerpResult315 , _METAL2METALLIC , temp_output_678_0);
				float lerpResult316 = lerp( lerpResult319 , _METAL1METALLIC , temp_output_677_0);
				float lerpResult734 = lerp( lerpResult316 , 0.0 , temp_output_16_0);
				
				float lerpResult342 = lerp( 0.0 , _PAINT3SMOOTHNESS , temp_output_686_0);
				float lerpResult338 = lerp( lerpResult342 , _PAINT2SMOOTHNESS , temp_output_683_0);
				float lerpResult340 = lerp( lerpResult338 , _PAINT1SMOOTHNESS , temp_output_685_0);
				float lerpResult707 = lerp( lerpResult340 , _WOOD3SMOOTHNESS , temp_output_659_0);
				float lerpResult708 = lerp( lerpResult707 , _WOOD2SMOOTHNESS , temp_output_663_0);
				float lerpResult710 = lerp( lerpResult708 , _WOOD1SMOOTHNESS , temp_output_667_0);
				float lerpResult336 = lerp( lerpResult710 , _LEATHER3SMOOTHNESS , temp_output_672_0);
				float lerpResult332 = lerp( lerpResult336 , _LEATHER2SMOOTHNESS , temp_output_673_0);
				float lerpResult334 = lerp( lerpResult332 , _LEATHER1SMOOTHNESS , temp_output_675_0);
				float lerpResult722 = lerp( lerpResult334 , _METAL4SMOOTNESS , temp_output_679_0);
				float lerpResult327 = lerp( lerpResult722 , _METAL3SMOOTHNESS , temp_output_694_0);
				float lerpResult331 = lerp( lerpResult327 , _METAL2SMOOTHNESS , temp_output_678_0);
				float lerpResult328 = lerp( lerpResult331 , _METAL1SMOOTHNESS , temp_output_677_0);
				float lerpResult321 = lerp( lerpResult328 , _GEMS3SMOOTHNESS , temp_output_690_0);
				float lerpResult325 = lerp( lerpResult321 , _GEMS2SMOOTHNESS , temp_output_728_0);
				float lerpResult322 = lerp( lerpResult325 , _GEMS1SMOOTHNESS , temp_output_662_0);
				

				float3 BaseColor = lerpResult64.rgb;
				float3 Normal = float3(0, 0, 1);
				float3 Emission = 0;
				float3 Specular = 0.5;
				float Metallic = (( _MetalicOn )?( lerpResult734 ):( 0.0 ));
				float Smoothness = (( _SmoothnessOn )?( lerpResult322 ):( 0.0 ));
				float Occlusion = (1.0 + (_OCCLUSION - 0.0) * (0.5 - 1.0) / (1.0 - 0.0));
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
			float4 _COATOFARMSCOLOR;
			float4 _GEMS2COLOR;
			float4 _GEMS3COLOR;
			float4 _METAL1COLOR;
			float4 _METAL2COLOR;
			float4 _METAL3COLOR;
			float4 _METAL4COLOR;
			float4 _LEATHER1COLOR;
			float4 _LEATHER2COLOR;
			float4 _LEATHER3COLOR;
			float4 _WOOD1COLOR;
			float4 _GEMS1COLOR;
			float4 _WOOD3COLOR;
			float4 _FEATHERS1COLOR;
			float4 _FEATHERS2COLOR;
			float4 _FEATHERS3COLOR;
			float4 _FEATHERS4COLOR;
			float4 _FEATHERS5COLOR;
			float4 _FEATHERS6COLOR;
			float4 _PAINT1COLOR;
			float4 _PAINT2COLOR;
			float4 _TextureSample9_ST;
			float4 _PAINT3COLOR;
			float4 _WOOD2COLOR;
			float _LEATHER2SMOOTHNESS;
			float _LEATHER1SMOOTHNESS;
			float _METAL4SMOOTNESS;
			float _GEMS2SMOOTHNESS;
			float _METAL2SMOOTHNESS;
			float _METAL1SMOOTHNESS;
			float _GEMS3SMOOTHNESS;
			float _LEATHER3SMOOTHNESS;
			float _METAL3SMOOTHNESS;
			float _WOOD1SMOOTHNESS;
			float _SmoothnessOn;
			float _WOOD3SMOOTHNESS;
			float _PAINT1SMOOTHNESS;
			float _PAINT2SMOOTHNESS;
			float _PAINT3SMOOTHNESS;
			float _METAL1METALLIC;
			float _METAL2METALLIC;
			float _METAL3METALLIC;
			float _METAL4METALLIC;
			float _MetalicOn;
			float _GEMS1SMOOTHNESS;
			float _WOOD2SMOOTHNESS;
			float _OCCLUSION;
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
			float4 _COATOFARMSCOLOR;
			float4 _GEMS2COLOR;
			float4 _GEMS3COLOR;
			float4 _METAL1COLOR;
			float4 _METAL2COLOR;
			float4 _METAL3COLOR;
			float4 _METAL4COLOR;
			float4 _LEATHER1COLOR;
			float4 _LEATHER2COLOR;
			float4 _LEATHER3COLOR;
			float4 _WOOD1COLOR;
			float4 _GEMS1COLOR;
			float4 _WOOD3COLOR;
			float4 _FEATHERS1COLOR;
			float4 _FEATHERS2COLOR;
			float4 _FEATHERS3COLOR;
			float4 _FEATHERS4COLOR;
			float4 _FEATHERS5COLOR;
			float4 _FEATHERS6COLOR;
			float4 _PAINT1COLOR;
			float4 _PAINT2COLOR;
			float4 _TextureSample9_ST;
			float4 _PAINT3COLOR;
			float4 _WOOD2COLOR;
			float _LEATHER2SMOOTHNESS;
			float _LEATHER1SMOOTHNESS;
			float _METAL4SMOOTNESS;
			float _GEMS2SMOOTHNESS;
			float _METAL2SMOOTHNESS;
			float _METAL1SMOOTHNESS;
			float _GEMS3SMOOTHNESS;
			float _LEATHER3SMOOTHNESS;
			float _METAL3SMOOTHNESS;
			float _WOOD1SMOOTHNESS;
			float _SmoothnessOn;
			float _WOOD3SMOOTHNESS;
			float _PAINT1SMOOTHNESS;
			float _PAINT2SMOOTHNESS;
			float _PAINT3SMOOTHNESS;
			float _METAL1METALLIC;
			float _METAL2METALLIC;
			float _METAL3METALLIC;
			float _METAL4METALLIC;
			float _MetalicOn;
			float _GEMS1SMOOTHNESS;
			float _WOOD2SMOOTHNESS;
			float _OCCLUSION;
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
			float4 _COATOFARMSCOLOR;
			float4 _GEMS2COLOR;
			float4 _GEMS3COLOR;
			float4 _METAL1COLOR;
			float4 _METAL2COLOR;
			float4 _METAL3COLOR;
			float4 _METAL4COLOR;
			float4 _LEATHER1COLOR;
			float4 _LEATHER2COLOR;
			float4 _LEATHER3COLOR;
			float4 _WOOD1COLOR;
			float4 _GEMS1COLOR;
			float4 _WOOD3COLOR;
			float4 _FEATHERS1COLOR;
			float4 _FEATHERS2COLOR;
			float4 _FEATHERS3COLOR;
			float4 _FEATHERS4COLOR;
			float4 _FEATHERS5COLOR;
			float4 _FEATHERS6COLOR;
			float4 _PAINT1COLOR;
			float4 _PAINT2COLOR;
			float4 _TextureSample9_ST;
			float4 _PAINT3COLOR;
			float4 _WOOD2COLOR;
			float _LEATHER2SMOOTHNESS;
			float _LEATHER1SMOOTHNESS;
			float _METAL4SMOOTNESS;
			float _GEMS2SMOOTHNESS;
			float _METAL2SMOOTHNESS;
			float _METAL1SMOOTHNESS;
			float _GEMS3SMOOTHNESS;
			float _LEATHER3SMOOTHNESS;
			float _METAL3SMOOTHNESS;
			float _WOOD1SMOOTHNESS;
			float _SmoothnessOn;
			float _WOOD3SMOOTHNESS;
			float _PAINT1SMOOTHNESS;
			float _PAINT2SMOOTHNESS;
			float _PAINT3SMOOTHNESS;
			float _METAL1METALLIC;
			float _METAL2METALLIC;
			float _METAL3METALLIC;
			float _METAL4METALLIC;
			float _MetalicOn;
			float _GEMS1SMOOTHNESS;
			float _WOOD2SMOOTHNESS;
			float _OCCLUSION;
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
Node;AmplifyShaderEditor.CommentaryNode;228;-21033.04,106.9413;Inherit;False;2305.307;694.8573;Comment;15;223;219;218;221;214;217;229;230;231;683;685;686;687;688;684;PAINT COLORS;0.05562881,0.9716981,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;650;-21721.24,508.767;Inherit;False;336;252;Comment;1;647;MASK TEXTURE;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;200;-21726.61,107.3437;Inherit;False;341.4902;248.4146;Comment;1;199;BASE TEXTURE;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;212;-18328.62,116.0087;Inherit;False;4637.659;753.5428;Comment;30;669;670;666;665;184;189;186;183;226;182;225;188;224;701;700;703;702;698;695;696;697;699;713;714;715;716;719;720;718;717;FEATHERS COLORS;0.735849,0.7152051,0.3158597,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;344;-20636.31,1664.684;Inherit;False;1768.5;211.4459;Comment;6;342;343;341;338;339;340;GEMS SMOOTHNESS;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;208;-13458.26,101.9824;Inherit;False;2359.399;695.7338;Comment;15;175;173;178;172;177;171;209;211;210;660;659;664;667;668;663;WOOD COLORS;0.4690726,0.7830189,0.47128,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;704;-12902.89,1664.754;Inherit;False;1768.502;211.4459;Comment;6;710;709;708;707;706;705;WOOD SMOOTHNESS;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;204;-10950.96,81.85144;Inherit;False;2342.301;700.673;Comment;15;203;166;202;160;161;201;164;167;162;676;673;671;674;675;672;LEATHER COLORS;0.7735849,0.5371538,0.1788003,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;345;-10556.04,1660.959;Inherit;False;1768.502;211.4459;Comment;6;337;336;335;332;333;334;LEATHER SMOOTHNESS;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;194;-8397.117,81.08585;Inherit;False;3503.256;733.6797;Comment;20;677;682;680;679;693;678;694;681;122;119;128;120;195;118;126;196;197;725;727;726;METAL COLORS;0.259434,0.8569208,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;346;-8048,1616;Inherit;False;2776.582;252.5363;Comment;8;328;330;331;326;327;329;722;721;METAL SMOOTHNESS;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;354;-8055.428,1093.562;Inherit;False;2789;206.5676;Comment;8;316;319;318;315;314;317;723;724;METAL METALLIC;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;24;-1812.858,-1400.5;Inherit;False;1262.249;589.4722;;7;16;18;26;25;17;9;10;COAT OF ARMS;1,0,0.7651567,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;190;-3908.523,39.87776;Inherit;False;2305.296;707.5152;Comment;15;156;155;148;154;151;153;191;192;193;690;689;662;661;728;729;GEMS COLORS;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;347;-3513.938,1597.351;Inherit;False;1768.499;211.4459;Comment;6;324;320;325;322;321;323;GEMS SMOOTHNESS;1,1,1,1;0;0
Node;AmplifyShaderEditor.ColorNode;684;-20734.43,578.3253;Inherit;False;Constant;_Color23;Color 23;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;1,0.4980392,0.4980392,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SamplerNode;647;-21691.15,558.071;Inherit;True;Property;_TextureSample9;Texture Sample 9;48;1;[HideInInspector];Create;True;0;0;0;False;0;False;-1;f88a8d19ac8f5724aa4e309405c476b6;f88a8d19ac8f5724aa4e309405c476b6;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SamplerNode;199;-21693.97,153.2156;Inherit;True;Property;_TextureSample2;Texture Sample 2;47;1;[HideInInspector];Create;True;0;0;0;False;0;False;-1;7772baab606a3874d99a1e30d4b6b891;7772baab606a3874d99a1e30d4b6b891;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;231;-20508.99,168.219;Inherit;False;Property;_PAINT3COLOR;PAINT 3 COLOR;28;1;[HDR];Create;True;0;0;0;False;0;False;0.5849056,0.5418971,0.4331613,0;0,0.1132075,0.01206957,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.FunctionNode;686;-20479.05,562.692;Inherit;True;Color Mask;-1;;114;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;230;-19901.83,180.3345;Inherit;False;Property;_PAINT2COLOR;PAINT 2 COLOR;26;1;[HDR];Create;True;0;0;0;False;0;False;0.3649431,0.5566038,0.4386422,0;0.2023368,0,0.4339623,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;223;-20284.78,156.9412;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;687;-20134.99,575.8022;Inherit;False;Constant;_Color24;Color 24;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.4980392,0.4980392,0.4980392,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;229;-19329.25,179.1574;Inherit;False;Property;_PAINT1COLOR;PAINT 1 COLOR;24;1;[HDR];Create;True;0;0;0;False;0;False;0.5450981,0.6936808,0.6980392,0;0.3773585,0,0.06650025,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;688;-19468.44,572.0334;Inherit;False;Constant;_Color25;Color 25;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.4980392,0.4980392,0,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.FunctionNode;683;-19880.84,557.9328;Inherit;True;Color Mask;-1;;115;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;219;-20108.96,377.3257;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;221;-19671.91,159.3201;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;665;-18004.08,617.7224;Inherit;False;Constant;_Color6;Color 6;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0,0.4980392,0,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.FunctionNode;685;-19214.12,567.0978;Inherit;True;Color Mask;-1;;116;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;214;-19488.5,362.7935;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;218;-19095.11,164.3822;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;224;-17824,176;Inherit;False;Property;_FEATHERS6COLOR;FEATHERS 6 COLOR;41;1;[HDR];Create;True;0;0;0;False;0;False;0.7924528,0.7444169,0.6391954,0;0,0.1793142,0.7264151,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;670;-17403.49,613.3688;Inherit;False;Constant;_Color12;Color 12;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0,0,0,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.FunctionNode;666;-17752.59,542.4282;Inherit;True;Color Mask;-1;;117;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;188;-17584,160;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;225;-17232,176;Inherit;False;Property;_FEATHERS5COLOR;FEATHERS 5 COLOR;40;1;[HDR];Create;True;0;0;0;False;0;False;0.4150943,0.2615769,0.1468494,0;0.6792453,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.LerpOp;217;-18911.72,372.1087;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;182;-17408,384;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;669;-17158.94,542.7025;Inherit;True;Color Mask;-1;;118;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;183;-16976,160;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;343;-20586.31,1742.003;Inherit;False;Property;_PAINT3SMOOTHNESS;PAINT 3 SMOOTHNESS;29;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;226;-16688,178.8702;Inherit;False;Property;_FEATHERS4COLOR;FEATHERS 4 COLOR;39;1;[HDR];Create;True;0;0;0;False;0;False;0.8113208,0.2104842,0.2104842,0;0.7735849,0.492613,0.492613,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;714;-16783.93,614.2518;Inherit;False;Constant;_Color31;Color 31;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;1,1,0,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.FunctionNode;713;-16510.3,564.6273;Inherit;True;Color Mask;-1;;119;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;719;-16021.37,609.8835;Inherit;False;Constant;_Color34;Color 34;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.4980392,0,0,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.LerpOp;189;-16784,368;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;695;-15927.61,168.5475;Inherit;False;Property;_FEATHERS3COLOR;FEATHERS 3 COLOR;38;1;[HDR];Create;True;0;0;0;False;0;False;0.509434,0.25712,0.25712,0;0,0.1793142,0.7264151,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.LerpOp;342;-20287.19,1719.139;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;341;-19953.91,1742.184;Inherit;False;Property;_PAINT2SMOOTHNESS;PAINT 2 SMOOTHNESS;27;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;186;-16400,160;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;660;-13146.11,601.5983;Inherit;False;Constant;_Color10;Color 10;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0,0,1,0;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;717;-15422.64,619.6623;Inherit;False;Constant;_Color33;Color 33;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;1,0.4980392,0,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;697;-15335.62,168.5475;Inherit;False;Property;_FEATHERS2COLOR;FEATHERS 2 COLOR;37;1;[HDR];Create;True;0;0;0;False;0;False;0.509434,0.4260285,0.1802243,0;0.6792453,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;696;-15687.61,152.5475;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;339;-19420.67,1743.94;Inherit;False;Property;_PAINT1SMOOTHNESS;PAINT 1 SMOOTHNESS;25;0;Create;True;0;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;184;-16216.73,366.2538;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;338;-19660.9,1720.131;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;720;-15755.01,540.7425;Inherit;True;Color Mask;-1;;121;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;715;-14851.21,595.0234;Inherit;False;Constant;_Color32;Color 32;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;1,0,0,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.LerpOp;340;-19100.86,1721.451;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;659;-12846.13,537.9135;Inherit;True;Color Mask;-1;;138;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;699;-14816.62,177.5475;Inherit;False;Property;_FEATHERS1COLOR;FEATHERS 1 COLOR;36;1;[HDR];Create;True;0;0;0;False;0;False;0.3301887,0.1241556,0.04516733,0;0.7735849,0.492613,0.492613,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.LerpOp;700;-15513.36,367.8164;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;698;-15079.62,152.5475;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;664;-12483.35,570.6799;Inherit;False;Constant;_Color5;Color 5;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0,1,1,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.FunctionNode;718;-15177.69,547.3597;Inherit;True;Color Mask;-1;;139;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;705;-12852.89,1742.072;Inherit;False;Property;_WOOD3SMOOTHNESS;WOOD 3 SMOOTHNESS;17;0;Create;True;0;0;0;False;0;False;0.3;0.3;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;707;-12553.76,1719.208;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;668;-11884.62,588.9177;Inherit;False;Constant;_Color11;Color 11;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0,1,0,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.FunctionNode;663;-12212.44,541.7576;Inherit;True;Color Mask;-1;;140;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;211;-12946.7,169.5253;Inherit;False;Property;_WOOD3COLOR;WOOD 3 COLOR;16;1;[HDR];Create;True;0;0;0;False;0;False;0.1037736,0.07509367,0.04650232,0;0.9056604,0.6815338,0.4229263,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.LerpOp;701;-14885.87,369.2787;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;716;-14586.03,541.4448;Inherit;True;Color Mask;-1;;141;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;706;-12220.48,1742.253;Inherit;False;Property;_WOOD2SMOOTHNESS;WOOD 2 SMOOTHNESS;15;0;Create;True;0;0;0;False;0;False;0.3;0.3;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;702;-14503.62,152.5475;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;177;-12655.9,151.9825;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;708;-11927.48,1720.2;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;667;-11614.71,541.9955;Inherit;True;Color Mask;-1;;142;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;210;-12318.56,167.2369;Inherit;False;Property;_WOOD2COLOR;WOOD 2 COLOR;14;1;[HDR];Create;True;0;0;0;False;0;False;0.1320755,0.06452555,0.05420079,0;1,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;671;-10519.1,616.537;Inherit;False;Constant;_Color13;Color 13;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;1,0.4980392,1,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.LerpOp;703;-14313.36,350.0702;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;709;-11638.2,1737.243;Inherit;False;Property;_WOOD1SMOOTHNESS;WOOD 1 SMOOTHNESS;13;0;Create;True;0;0;0;False;0;False;0.3;0.3;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;672;-10246.41,537.961;Inherit;True;Color Mask;-1;;143;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;209;-11737.9,176.3902;Inherit;False;Property;_WOOD1COLOR;WOOD 1 COLOR;12;1;[HDR];Create;True;0;0;0;False;0;False;0.1981132,0.08345769,0.06261124,0;0,0.1142961,0.1698113,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;674;-9908.799,597.8498;Inherit;False;Constant;_Color15;Color 15;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;1,0,1,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.RangedFloatNode;337;-10506.04,1738.277;Inherit;False;Property;_LEATHER3SMOOTHNESS;LEATHER 3 SMOOTHNESS;23;0;Create;True;0;0;0;False;0;False;0.3;0.3;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;171;-12480.08,372.3667;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;172;-12043.04,154.3614;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;710;-11318.39,1714.754;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;175;-11466.22,159.4234;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;178;-11859.63,357.8348;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;336;-10206.91,1715.413;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;203;-10437.53,159.2697;Inherit;False;Property;_LEATHER3COLOR;LEATHER 3 COLOR;22;1;[HDR];Create;True;0;0;0;False;0;False;0.1320755,0.03139969,0.02180491,1;0.1698113,0.04637412,0.02963688,1;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.RangedFloatNode;335;-9873.636,1738.458;Inherit;False;Property;_LEATHER2SMOOTHNESS;LEATHER 2 SMOOTHNESS;21;0;Create;True;0;0;0;False;0;False;0.3;0.3;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;676;-9260.005,596.0192;Inherit;False;Constant;_Color16;Color 16;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;1,1,0.4980392,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.FunctionNode;673;-9646.89,534.0915;Inherit;True;Color Mask;-1;;144;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;202;-9846.154,165.072;Inherit;False;Property;_LEATHER2COLOR;LEATHER 2 COLOR;20;1;[HDR];Create;True;0;0;0;False;0;False;0.06603771,0.03523636,0.03146137,1;0.4245283,0.190437,0.09011215,1;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;166;-10170.95,137.1077;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;680;-7974.828,632.4999;Inherit;False;Constant;_Color17;Color 17;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.4980392,0.4980392,1,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.LerpOp;332;-9580.631,1716.405;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;173;-11282.86,367.1498;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;333;-9291.35,1733.448;Inherit;False;Property;_LEATHER1SMOOTHNESS;LEATHER 1 SMOOTHNESS;19;0;Create;True;0;0;0;False;0;False;0.3;0.3;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;675;-9014.268,536.3026;Inherit;True;Color Mask;-1;;145;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;201;-9256.154,158.072;Inherit;False;Property;_LEATHER1COLOR;LEATHER 1 COLOR;18;1;[HDR];Create;True;0;0;0;False;0;False;0.2924528,0.1296404,0.09242612,1;0.4811321,0.2041155,0.08851016,1;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;161;-9552.838,134.2304;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;334;-8971.539,1710.959;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;721;-7727.325,1728.861;Inherit;False;Property;_METAL4SMOOTNESS;METAL 4 SMOOTNESS;11;0;Create;True;0;0;0;False;0;False;0.7;0.721;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;160;-9989.884,352.2355;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;679;-7749.673,535.4676;Inherit;True;Color Mask;-1;;146;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;693;-7303.453,623.1536;Inherit;False;Constant;_Color7;Color 7;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0,0.4980392,0.4980392,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;681;-6666.504,620.7065;Inherit;False;Constant;_Color18;Color 18;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0,0,0.4980392,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.RangedFloatNode;724;-7743.325,1216.861;Inherit;False;Property;_METAL4METALLIC;METAL 4 METALLIC;10;0;Create;True;0;0;0;False;0;False;0.65;0.903;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;167;-9369.433,349.9297;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;725;-7828.683,173.1321;Inherit;False;Property;_METAL4COLOR;METAL 4 COLOR;9;1;[HDR];Create;True;0;0;0;False;0;False;0.8791043,0.8044721,0.9422547,0;0.9528302,0.9528302,0.9528302,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.LerpOp;722;-7439.325,1712.861;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;694;-7068.888,533.7717;Inherit;True;Color Mask;-1;;147;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;164;-8976.024,139.2924;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;329;-7056,1712;Inherit;False;Property;_METAL3SMOOTHNESS;METAL 3 SMOOTHNESS;8;0;Create;True;0;0;0;False;0;False;0.7;0.721;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;682;-5967.115,631.6647;Inherit;False;Constant;_Color19;Color 19;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.4980392,0,0.4980392,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.RangedFloatNode;326;-6432,1712;Inherit;False;Property;_METAL2SMOOTHNESS;METAL 2 SMOOTHNESS;5;0;Create;True;0;0;0;False;0;False;0.7;0.7;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;197;-7183.908,160;Inherit;False;Property;_METAL3COLOR;METAL 3 COLOR;6;1;[HDR];Create;True;0;0;0;False;0;False;0.597023,0.6237553,0.7395956,0;0.9528302,0.9528302,0.9528302,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.FunctionNode;678;-6401.288,529.8658;Inherit;True;Color Mask;-1;;148;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;327;-6768,1696;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;162;-8792.659,347.0186;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;317;-7072,1200;Inherit;False;Property;_METAL3METALLIC;METAL 3 METALLIC;7;0;Create;True;0;0;0;False;0;False;0.65;0.903;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;726;-7520.575,149.1602;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;723;-7439.325,1184.861;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;331;-6128,1696;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;677;-5766.83,526.6884;Inherit;True;Color Mask;-1;;149;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;10;-1769.44,-1325.746;Inherit;True;Property;_COATOFARMSMASK;COAT OF ARMS MASK;43;1;[NoScaleOffset];Create;True;0;0;0;False;0;False;-1;None;None;True;1;False;black;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.RangedFloatNode;330;-5840,1712;Inherit;False;Property;_METAL1SMOOTHNESS;METAL 1 SMOOTHNESS;2;0;Create;True;0;0;0;False;0;False;0.7;0.7;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;315;-6768,1168;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;689;-3805.406,537.4455;Inherit;False;Constant;_Color26;Color 26;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.4980392,1,1,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;126;-6863.908,144;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;314;-6441.22,1197.316;Inherit;False;Property;_METAL2METALLIC;METAL 2 METALLIC;4;0;Create;True;0;0;0;False;0;False;0.65;0.65;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;196;-6510.026,160.9428;Inherit;False;Property;_METAL2COLOR;METAL 2 COLOR;3;1;[HDR];Create;True;0;0;0;False;0;False;1.678431,1.003922,0.1176471,0;0.3301887,0.3301887,0.3301887,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.LerpOp;727;-7352.555,346.1685;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;328;-5520,1680;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;195;-5904.329,159.8789;Inherit;False;Property;_METAL1COLOR;METAL 1 COLOR;0;1;[HDR];Create;True;0;0;0;False;0;False;0.7261481,0.7735849,0.7528313,0;2,0.682353,0.1960784,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.OneMinusNode;9;-1435.856,-1221.265;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;729;-3062.219,553.774;Inherit;False;Constant;_Color35;Color 35;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.4980392,1,0.4980392,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.LerpOp;118;-6687.908,352;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;318;-5849.22,1213.316;Inherit;False;Property;_METAL1METALLIC;METAL 1 METALLIC;1;0;Create;True;0;0;0;False;0;False;0.65;0.65;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;319;-6137.22,1165.316;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;690;-3504.796,522.8221;Inherit;True;Color Mask;-1;;150;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;120;-6255.908,144;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;323;-3465.506,1674.669;Inherit;False;Property;_GEMS3SMOOTHNESS;GEMS 3 SMOOTHNESS;35;0;Create;True;0;0;0;False;0;False;0.3;0.3;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;735;-989.6353,1292.709;Inherit;False;Constant;_Float0;Float 0;50;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;26;-1063.837,-1185.19;Inherit;False;Property;_COATOFARMSCOLOR;COAT OF ARMS COLOR;42;1;[HDR];Create;True;0;0;0;False;0;False;1,0,0,0;1,0.0990566,0.0990566,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.RangedFloatNode;320;-2831.536,1674.85;Inherit;False;Property;_GEMS2SMOOTHNESS;GEMS 2 SMOOTHNESS;33;0;Create;True;0;0;0;False;0;False;0.4;0.4;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;16;-1243.999,-947.4095;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;728;-2785.851,530.1637;Inherit;True;Color Mask;-1;;151;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;316;-5529.22,1165.316;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;321;-3164.81,1651.805;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;661;-2314.383,546.2698;Inherit;False;Constant;_Color3;Color 3;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.4980392,0,1,1;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;193;-3690.784,155.962;Inherit;False;Property;_GEMS3COLOR;GEMS 3 COLOR;34;1;[HDR];Create;True;0;0;0;False;0;False;0,0.4716981,0.1359325,0;0.8490566,0.5037117,0.3884835,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.LerpOp;128;-6067.673,351.0603;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.Vector2Node;18;-1043.557,-1002.802;Inherit;False;Constant;_Vector0;Vector 0;1;0;Create;True;0;0;0;False;0;False;1.6,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.ColorNode;25;-1067.646,-1364.901;Inherit;False;Constant;_Color4;Color 4;1;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;119;-5631.474,143.8497;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;734;-690.5518,1149.833;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;192;-2986.811,159.3325;Inherit;False;Property;_GEMS2COLOR;GEMS 2 COLOR;32;1;[HDR];Create;True;0;0;0;False;0;False;0,0.3218706,0.5754717,0;0.8301887,0.3185886,0.2780349,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.LerpOp;122;-5448.109,351.576;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;324;-2249.249,1669.84;Inherit;False;Property;_GEMS1SMOOTHNESS;GEMS 1 SMOOTHNESS;31;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;662;-2050.042,523.783;Inherit;True;Color Mask;-1;;152;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;17;-814.7589,-1227.691;Inherit;False;Replace Color;-1;;153;896dccb3016c847439def376a728b869;1,12,0;5;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;325;-2538.531,1652.797;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;156;-3323.069,139.2701;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;148;-3225.723,324.7957;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;574;-328.0893,-1201.828;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;576;-386.0649,-898.4222;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;322;-1929.438,1647.351;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;155;-2643.126,139.1111;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;191;-2264.291,165.833;Inherit;False;Property;_GEMS1COLOR;GEMS 1 COLOR;30;1;[HDR];Create;True;0;0;0;False;0;False;1,0,0,0;0.9056604,0.8159487,0.8159487,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.LerpOp;151;-2493.217,331.0835;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;154;-1934.769,142.0169;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;575;-357.1902,-846.472;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;573;-296.0682,-1167.712;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;577;-363.4679,348.5246;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;153;-1793.627,314.5027;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;578;-316.0852,310.8136;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;64;-240.9823,333.7983;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;736;680.5474,1155.744;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;584;773.4498,417.1816;Inherit;False;Property;_MetalicOn;Metalic On;45;0;Create;True;0;0;0;False;0;False;1;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;737;741.7505,1088.712;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;738;730.0928,1662.855;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;739;810.2396,1592.909;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;585;807.7573,528.1333;Inherit;False;Property;_SmoothnessOn;Smoothness On;46;0;Create;True;0;0;0;False;0;False;1;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;579;826.733,696.309;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;1;False;4;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;252;398.2736,703.8671;Inherit;False;Property;_OCCLUSION;OCCLUSION;44;0;Create;True;0;0;0;False;0;False;0.4139509;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;740;1178.721,338.6894;Float;False;False;-1;2;UnityEditor.ShaderGraphLitGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;ExtraPrePass;0;0;ExtraPrePass;5;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Lit;True;5;True;12;all;0;False;True;1;1;False;;0;False;;0;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;0;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;741;1178.721,338.6894;Float;False;True;-1;2;UnityEditor.ShaderGraphLitGUI;0;12;Polytope Studio/ PT_Medieval Weapons Shader PBR;94348b07e5e8bab40bd6c8a1e3df54cd;True;Forward;0;1;Forward;21;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Lit;True;5;True;12;all;0;False;True;1;1;False;;0;False;;1;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;1;LightMode=UniversalForward;False;False;0;;0;0;Standard;45;Lighting Model;0;0;Workflow;1;0;Surface;0;0;  Refraction Model;0;0;  Blend;0;0;Two Sided;1;0;Alpha Clipping;1;0;  Use Shadow Threshold;0;0;Fragment Normal Space,InvertActionOnDeselection;0;0;Forward Only;0;0;Transmission;0;0;  Transmission Shadow;0.5,False,;0;Translucency;0;0;  Translucency Strength;1,False,;0;  Normal Distortion;0.5,False,;0;  Scattering;2,False,;0;  Direct;0.9,False,;0;  Ambient;0.1,False,;0;  Shadow;0.5,False,;0;Cast Shadows;1;0;Receive Shadows;1;0;Receive SSAO;1;0;Motion Vectors;1;0;  Add Precomputed Velocity;0;0;GPU Instancing;1;0;LOD CrossFade;1;0;Built-in Fog;1;0;_FinalColorxAlpha;0;0;Meta Pass;1;0;Override Baked GI;0;0;Extra Pre Pass;0;0;Tessellation;0;0;  Phong;0;0;  Strength;0.5,False,;0;  Type;0;0;  Tess;16,False,;0;  Min;10,False,;0;  Max;25,False,;0;  Edge Length;16,False,;0;  Max Displacement;25,False,;0;Write Depth;0;0;  Early Z;0;0;Vertex Position,InvertActionOnDeselection;1;0;Debug Display;0;0;Clear Coat;0;0;0;11;False;True;True;True;True;True;True;True;True;True;True;False;;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;742;1178.721,338.6894;Float;False;False;-1;2;UnityEditor.ShaderGraphLitGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;ShadowCaster;0;2;ShadowCaster;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Lit;True;5;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;False;False;True;False;False;False;False;0;False;;False;False;False;False;False;False;False;False;False;True;1;False;;True;3;False;;False;True;1;LightMode=ShadowCaster;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;743;1178.721,338.6894;Float;False;False;-1;2;UnityEditor.ShaderGraphLitGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;DepthOnly;0;3;DepthOnly;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Lit;True;5;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;False;False;True;False;False;False;False;0;False;;False;False;False;False;False;False;False;False;False;True;1;False;;False;False;True;1;LightMode=DepthOnly;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;744;1178.721,338.6894;Float;False;False;-1;2;UnityEditor.ShaderGraphLitGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;Meta;0;4;Meta;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Lit;True;5;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=Meta;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;745;1178.721,338.6894;Float;False;False;-1;2;UnityEditor.ShaderGraphLitGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;Universal2D;0;5;Universal2D;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Lit;True;5;True;12;all;0;False;True;1;1;False;;0;False;;1;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;False;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;1;LightMode=Universal2D;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;746;1178.721,338.6894;Float;False;False;-1;2;UnityEditor.ShaderGraphLitGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;DepthNormals;0;6;DepthNormals;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Lit;True;5;True;12;all;0;False;True;1;1;False;;0;False;;0;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;False;;True;3;False;;False;True;1;LightMode=DepthNormals;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;747;1178.721,338.6894;Float;False;False;-1;2;UnityEditor.ShaderGraphLitGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;GBuffer;0;7;GBuffer;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Lit;True;5;True;12;all;0;False;True;1;1;False;;0;False;;1;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;1;LightMode=UniversalGBuffer;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;748;1178.721,338.6894;Float;False;False;-1;2;UnityEditor.ShaderGraphLitGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;SceneSelectionPass;0;8;SceneSelectionPass;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Lit;True;5;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;2;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=SceneSelectionPass;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;749;1178.721,338.6894;Float;False;False;-1;2;UnityEditor.ShaderGraphLitGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;ScenePickingPass;0;9;ScenePickingPass;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Lit;True;5;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=Picking;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;750;1178.721,438.6894;Float;False;False;-1;3;UnityEditor.ShaderGraphLitGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;MotionVectors;0;10;MotionVectors;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Lit;True;5;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;True;False;False;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=MotionVectors;False;False;0;;0;0;Standard;0;False;0
WireConnection;686;1;647;0
WireConnection;686;3;684;0
WireConnection;223;0;199;0
WireConnection;223;1;231;0
WireConnection;683;1;647;0
WireConnection;683;3;687;0
WireConnection;219;1;223;0
WireConnection;219;2;686;0
WireConnection;221;0;199;0
WireConnection;221;1;230;0
WireConnection;685;1;647;0
WireConnection;685;3;688;0
WireConnection;214;0;219;0
WireConnection;214;1;221;0
WireConnection;214;2;683;0
WireConnection;218;0;199;0
WireConnection;218;1;229;0
WireConnection;666;1;647;0
WireConnection;666;3;665;0
WireConnection;188;0;199;0
WireConnection;188;1;224;0
WireConnection;217;0;214;0
WireConnection;217;1;218;0
WireConnection;217;2;685;0
WireConnection;182;0;217;0
WireConnection;182;1;188;0
WireConnection;182;2;666;0
WireConnection;669;1;647;0
WireConnection;669;3;670;0
WireConnection;183;0;199;0
WireConnection;183;1;225;0
WireConnection;713;1;647;0
WireConnection;713;3;714;0
WireConnection;189;0;182;0
WireConnection;189;1;183;0
WireConnection;189;2;669;0
WireConnection;342;1;343;0
WireConnection;342;2;686;0
WireConnection;186;0;199;0
WireConnection;186;1;226;0
WireConnection;696;0;199;0
WireConnection;696;1;695;0
WireConnection;184;0;189;0
WireConnection;184;1;186;0
WireConnection;184;2;713;0
WireConnection;338;0;342;0
WireConnection;338;1;341;0
WireConnection;338;2;683;0
WireConnection;720;1;647;0
WireConnection;720;3;719;0
WireConnection;340;0;338;0
WireConnection;340;1;339;0
WireConnection;340;2;685;0
WireConnection;659;1;647;0
WireConnection;659;3;660;0
WireConnection;700;0;184;0
WireConnection;700;1;696;0
WireConnection;700;2;720;0
WireConnection;698;0;199;0
WireConnection;698;1;697;0
WireConnection;718;1;647;0
WireConnection;718;3;717;0
WireConnection;707;0;340;0
WireConnection;707;1;705;0
WireConnection;707;2;659;0
WireConnection;663;1;647;0
WireConnection;663;3;664;0
WireConnection;701;0;700;0
WireConnection;701;1;698;0
WireConnection;701;2;718;0
WireConnection;716;1;647;0
WireConnection;716;3;715;0
WireConnection;702;0;199;0
WireConnection;702;1;699;0
WireConnection;177;0;199;0
WireConnection;177;1;211;0
WireConnection;708;0;707;0
WireConnection;708;1;706;0
WireConnection;708;2;663;0
WireConnection;667;1;647;0
WireConnection;667;3;668;0
WireConnection;703;0;701;0
WireConnection;703;1;702;0
WireConnection;703;2;716;0
WireConnection;672;1;647;0
WireConnection;672;3;671;0
WireConnection;171;0;703;0
WireConnection;171;1;177;0
WireConnection;171;2;659;0
WireConnection;172;0;199;0
WireConnection;172;1;210;0
WireConnection;710;0;708;0
WireConnection;710;1;709;0
WireConnection;710;2;667;0
WireConnection;175;0;199;0
WireConnection;175;1;209;0
WireConnection;178;0;171;0
WireConnection;178;1;172;0
WireConnection;178;2;663;0
WireConnection;336;0;710;0
WireConnection;336;1;337;0
WireConnection;336;2;672;0
WireConnection;673;1;647;0
WireConnection;673;3;674;0
WireConnection;166;0;199;0
WireConnection;166;1;203;0
WireConnection;332;0;336;0
WireConnection;332;1;335;0
WireConnection;332;2;673;0
WireConnection;173;0;178;0
WireConnection;173;1;175;0
WireConnection;173;2;667;0
WireConnection;675;1;647;0
WireConnection;675;3;676;0
WireConnection;161;0;199;0
WireConnection;161;1;202;0
WireConnection;334;0;332;0
WireConnection;334;1;333;0
WireConnection;334;2;675;0
WireConnection;160;0;173;0
WireConnection;160;1;166;0
WireConnection;160;2;672;0
WireConnection;679;1;647;0
WireConnection;679;3;680;0
WireConnection;167;0;160;0
WireConnection;167;1;161;0
WireConnection;167;2;673;0
WireConnection;722;0;334;0
WireConnection;722;1;721;0
WireConnection;722;2;679;0
WireConnection;694;1;647;0
WireConnection;694;3;693;0
WireConnection;164;0;199;0
WireConnection;164;1;201;0
WireConnection;678;1;647;0
WireConnection;678;3;681;0
WireConnection;327;0;722;0
WireConnection;327;1;329;0
WireConnection;327;2;694;0
WireConnection;162;0;167;0
WireConnection;162;1;164;0
WireConnection;162;2;675;0
WireConnection;726;0;199;0
WireConnection;726;1;725;0
WireConnection;723;1;724;0
WireConnection;723;2;679;0
WireConnection;331;0;327;0
WireConnection;331;1;326;0
WireConnection;331;2;678;0
WireConnection;677;1;647;0
WireConnection;677;3;682;0
WireConnection;315;0;723;0
WireConnection;315;1;317;0
WireConnection;315;2;694;0
WireConnection;126;0;199;0
WireConnection;126;1;197;0
WireConnection;727;0;162;0
WireConnection;727;1;726;0
WireConnection;727;2;679;0
WireConnection;328;0;331;0
WireConnection;328;1;330;0
WireConnection;328;2;677;0
WireConnection;9;0;10;4
WireConnection;118;0;727;0
WireConnection;118;1;126;0
WireConnection;118;2;694;0
WireConnection;319;0;315;0
WireConnection;319;1;314;0
WireConnection;319;2;678;0
WireConnection;690;1;647;0
WireConnection;690;3;689;0
WireConnection;120;0;199;0
WireConnection;120;1;196;0
WireConnection;16;0;9;0
WireConnection;728;1;647;0
WireConnection;728;3;729;0
WireConnection;316;0;319;0
WireConnection;316;1;318;0
WireConnection;316;2;677;0
WireConnection;321;0;328;0
WireConnection;321;1;323;0
WireConnection;321;2;690;0
WireConnection;128;0;118;0
WireConnection;128;1;120;0
WireConnection;128;2;678;0
WireConnection;119;0;199;0
WireConnection;119;1;195;0
WireConnection;734;0;316;0
WireConnection;734;1;735;0
WireConnection;734;2;16;0
WireConnection;122;0;128;0
WireConnection;122;1;119;0
WireConnection;122;2;677;0
WireConnection;662;1;647;0
WireConnection;662;3;661;0
WireConnection;17;1;9;0
WireConnection;17;2;25;0
WireConnection;17;3;26;0
WireConnection;17;4;18;1
WireConnection;17;5;18;2
WireConnection;325;0;321;0
WireConnection;325;1;320;0
WireConnection;325;2;728;0
WireConnection;156;0;199;0
WireConnection;156;1;193;0
WireConnection;148;0;122;0
WireConnection;148;1;156;0
WireConnection;148;2;690;0
WireConnection;574;0;17;0
WireConnection;576;0;16;0
WireConnection;322;0;325;0
WireConnection;322;1;324;0
WireConnection;322;2;662;0
WireConnection;155;0;199;0
WireConnection;155;1;192;0
WireConnection;151;0;148;0
WireConnection;151;1;155;0
WireConnection;151;2;728;0
WireConnection;154;0;199;0
WireConnection;154;1;191;0
WireConnection;575;0;576;0
WireConnection;573;0;574;0
WireConnection;577;0;575;0
WireConnection;153;0;151;0
WireConnection;153;1;154;0
WireConnection;153;2;662;0
WireConnection;578;0;573;0
WireConnection;64;0;153;0
WireConnection;64;1;578;0
WireConnection;64;2;577;0
WireConnection;736;0;734;0
WireConnection;584;1;737;0
WireConnection;737;0;736;0
WireConnection;738;0;322;0
WireConnection;739;0;738;0
WireConnection;585;1;739;0
WireConnection;579;0;252;0
WireConnection;741;0;64;0
WireConnection;741;3;584;0
WireConnection;741;4;585;0
WireConnection;741;5;579;0
ASEEND*/
//CHKSM=98466892AAAFB1633887765B1D29593E7AB28171