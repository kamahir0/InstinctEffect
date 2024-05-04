Shader "Custom/Hitman/PeopleStencil"
{
    SubShader
    {
        Tags
        {
            "RenderPipeline"="UniversalPipeline"
        }

        LOD 100
        
        Pass
        {
            Name "InstinctStencil_L1"
            Tags
            {
                "Queue"="Geometory"
            }

            ZTest Always
            Cull Off
            Lighting Off
            ZWrite Off
            ColorMask 0

            Stencil
            {
                Ref 1
                Comp Greater
                Pass Replace
            }
        }
    }
}
