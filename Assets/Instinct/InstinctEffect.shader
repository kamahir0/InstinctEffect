Shader "Hidden/InstinctEffect"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	HLSLINCLUDE
	#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
	#include "Packages/com.unity.render-pipelines.universal/Shaders/PostProcessing/Common.hlsl"

	sampler2D _MainTex;
    float4 _FilterColor;
    float4 _PeopleColor;
    float4 _BossColor;
    float _Strength;

	half4 Frag0(Varyings input) : COLOR
	{
		half4 col = tex2D(_MainTex, input.uv);
        half4 effected = lerp(col, _FilterColor * col, _Strength);
        return effected;
	}

    half4 Frag1(Varyings input) : COLOR
	{
		half4 col = tex2D(_MainTex, input.uv);
        half4 effected = lerp(col, _PeopleColor * col, _Strength);
        return effected;
	}

    half4 Frag2(Varyings input) : COLOR
	{
		half4 col = tex2D(_MainTex, input.uv);
        half4 effected = lerp(col, _BossColor * col, _Strength);
        return effected;
	}
	
	ENDHLSL
	SubShader
	{
		Tags { "RenderType" = "Qpaque" "RenderPipeline" = "UniversalPipeline" }
		LOD 100
		Cull Off ZWrite Off ZTest Always

		//何もいない
        Pass
		{
            Stencil
            {
                Ref 0
                Comp Equal
            }

			HLSLPROGRAM
			#pragma vertex Vert
			#pragma fragment Frag0
			ENDHLSL
		}
        //People
        Pass
		{
            Stencil
            {
                Ref 1
                Comp Equal
            }

			HLSLPROGRAM
			#pragma vertex Vert
			#pragma fragment Frag1
			ENDHLSL
		}
        //Target
        Pass
		{
            Stencil
            {
                Ref 2
                Comp Equal
            }
            
			HLSLPROGRAM
			#pragma vertex Vert
			#pragma fragment Frag2
			ENDHLSL
		}
	}
}
