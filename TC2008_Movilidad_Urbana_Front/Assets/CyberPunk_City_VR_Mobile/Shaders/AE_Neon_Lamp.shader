// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "AE_Neon_Lamp"
{
	Properties
	{
		[HDR]_Color0("Color 0", Color) = (0.9056604,0.1153435,0.1153435,0)
		_Speed("Speed", Range( 0 , 5)) = 2.57
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		[HDR]_Color1("Color 1", Color) = (0,0,0,0)
		[HDR]_Emmisive_Power("Emmisive_Power", Range( 0 , 5)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _Color1;
		uniform float4 _Color0;
		uniform float _Speed;
		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;
		uniform float _Emmisive_Power;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			float4 lerpResult16 = lerp( _Color1 , ( _Color0 * frac( ( _Speed * _Time.y ) ) ) , tex2D( _TextureSample0, uv_TextureSample0 ));
			o.Emission = ( lerpResult16 * _Emmisive_Power ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17500
-1913;4;1906;954;1027.356;584.0001;1.483292;True;False
Node;AmplifyShaderEditor.RangedFloatNode;11;-1074.915,-85.40617;Inherit;False;Property;_Speed;Speed;1;0;Create;True;0;0;False;0;2.57;2.57;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;1;-1048.11,252.85;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-710.7134,110.7962;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;9;-414.6201,201.0651;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;2;-418.1218,-239.2966;Inherit;False;Property;_Color0;Color 0;0;1;[HDR];Create;True;0;0;False;0;0.9056604,0.1153435,0.1153435,0;0,0.1647059,2,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-163.9029,-22.50258;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;17;-158.4846,405.0912;Inherit;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;19;74.19971,-144.4554;Inherit;False;Property;_Color1;Color 1;3;1;[HDR];Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;16;207.3928,169.0463;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;21;346.9625,568.0447;Inherit;False;Property;_Emmisive_Power;Emmisive_Power;4;1;[HDR];Create;True;0;0;False;0;0;2.76;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;541.3577,85.76956;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;786.1437,15.45253;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;AE_Neon_Lamp;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;10;0;11;0
WireConnection;10;1;1;2
WireConnection;9;0;10;0
WireConnection;15;0;2;0
WireConnection;15;1;9;0
WireConnection;16;0;19;0
WireConnection;16;1;15;0
WireConnection;16;2;17;0
WireConnection;20;0;16;0
WireConnection;20;1;21;0
WireConnection;0;2;20;0
ASEEND*/
//CHKSM=AA480FCE0C9A528E3A2DCA0853BF471B293FD89B