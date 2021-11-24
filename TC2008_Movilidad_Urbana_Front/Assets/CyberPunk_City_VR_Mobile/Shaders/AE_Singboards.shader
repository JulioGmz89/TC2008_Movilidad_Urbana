// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "AE_Singboards"
{
	Properties
	{
		_Speed_X("Speed_X", Range( 0 , 0.3)) = 0.6593578
		_Speed_Y("Speed_Y", Range( 0 , 0.3)) = 0.6593578
		[HDR]_Emissev_Power("Emissev_Power", Range( 0 , 5)) = 1
		_Albedo("Albedo", 2D) = "white" {}
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Albedo;
		uniform float _Speed_X;
		uniform float _Speed_Y;
		uniform float _Emissev_Power;
		uniform float _Smoothness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 appendResult5 = (float3(_Speed_X , _Speed_Y , 0.0));
			float2 panner2 = ( _Time.z * appendResult5.xy + i.uv_texcoord);
			float4 tex2DNode13 = tex2D( _Albedo, panner2 );
			o.Albedo = tex2DNode13.rgb;
			o.Emission = ( tex2DNode13 * _Emissev_Power ).rgb;
			float temp_output_20_0 = _Smoothness;
			o.Metallic = temp_output_20_0;
			o.Smoothness = temp_output_20_0;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17500
-1913;7;1906;972;2963.326;365.0795;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;21;-1922.076,343.717;Inherit;False;Property;_Speed_Y;Speed_Y;1;0;Create;True;0;0;False;0;0.6593578;0;0;0.3;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-2041.015,77.11203;Inherit;False;Property;_Speed_X;Speed_X;0;0;Create;True;0;0;False;0;0.6593578;0;0;0.3;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;5;-1546.573,148.3427;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;1;-1887.503,-307.6696;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TimeNode;7;-1376.802,408.004;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;2;-1120.81,-42.32357;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-975.6718,289.1434;Inherit;False;Property;_Emissev_Power;Emissev_Power;2;1;[HDR];Create;True;0;0;False;0;1;4.42;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;13;-916.478,-44.83083;Inherit;True;Property;_Albedo;Albedo;3;0;Create;True;0;0;False;0;-1;d479f6bb36914df48ac5a3d4698e6e89;ecad1d3651faa72498dae3fcd1bc849b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-269.9227,163.4227;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;20;183.4155,384.1458;Inherit;False;Property;_Smoothness;Smoothness;4;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;573.0565,-136.6684;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;AE_Singboards;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Translucent;0.5;True;True;0;False;Opaque;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;1;False;-1;1;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;5;0;3;0
WireConnection;5;1;21;0
WireConnection;2;0;1;0
WireConnection;2;2;5;0
WireConnection;2;1;7;3
WireConnection;13;1;2;0
WireConnection;10;0;13;0
WireConnection;10;1;11;0
WireConnection;0;0;13;0
WireConnection;0;2;10;0
WireConnection;0;3;20;0
WireConnection;0;4;20;0
ASEEND*/
//CHKSM=D5BE5D41020741F50DEC67A89E2CF8DF08BB6F50