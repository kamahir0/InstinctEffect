using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class InstinctFeature : ScriptableRendererFeature
{
    [SerializeField] Shader shader;
    [SerializeField, Range(0, 1)] float strength;
    [SerializeField] Color bossColor = Color.red;
    [SerializeField] Color peopleColor = Color.white;
    [SerializeField] Color filterColor = Color.gray;
    [SerializeField] RenderPassEvent renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;

    InstinctPass instinctPass;

    /// <summary>
    /// ポストエフェクトの強さを設定
    /// </summary>
    public void SetFilter(float strength)
    {
        this.strength = Mathf.Clamp(strength, 0, 1);
    }

    /// <inheritdoc/>
    public override void Create()
    {
        instinctPass = new InstinctPass(shader , renderPassEvent);
    }

    /// <inheritdoc/>
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        instinctPass.SetStrength(strength);
        instinctPass.SetColors(bossColor, peopleColor, filterColor);
        instinctPass.SetRTID(renderer.cameraColorTarget);
        renderer.EnqueuePass(instinctPass);
    }
}
