Shader "Custom/Hitman/BossStencil"
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
            Name "InstinctStencil_L2"
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
                Ref 2
                Comp Always
                Pass Replace
            }
        }
    }
}
