using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class InstinctPass : ScriptableRenderPass
{
    float strength;
    Material material;
    Color bossColor;
    Color peopleColor;
    Color filterColor;
    RenderTargetIdentifier currentTargetID;
    int tempID;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public InstinctPass(Shader shader, RenderPassEvent renderPassEvent)
    {
        this.renderPassEvent = renderPassEvent;
        material = new Material(shader);

        tempID = Shader.PropertyToID("_TempTex");
    }

    /// <summary>
    /// ポストエフェクトの強さを設定
    /// </summary>
    public void SetStrength(float strength)
    {
        this.strength = strength;
    }

    /// <summary>
    /// ポストエフェクトに使用する色を設定
    /// </summary>
    public void SetColors(Color bossColor, Color peopleColor, Color filterColor)
    {
        this.bossColor = bossColor;
        this.peopleColor = peopleColor;
        this.filterColor = filterColor;
    }

    /// <summary>
    /// レンダーターゲットのIDを設定
    /// </summary>
    public void SetRTID(RenderTargetIdentifier currentTargetID)
    {
        this.currentTargetID = currentTargetID;
    }

    /// <inheritdoc/>
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        //ゲームビューでのみ処理する
        var cameraData = renderingData.cameraData;
        if(cameraData.cameraType != CameraType.Game) return;

        //一時レンダーテクスチャーを作成
        var commandBuffer = CommandBufferPool.Get();
        var desc = new RenderTextureDescriptor(cameraData.camera.scaledPixelWidth, cameraData.camera.scaledPixelHeight);
        commandBuffer.GetTemporaryRT(tempID, desc);

        //色や値をセット
        material.SetColor("_BossColor", bossColor);
        material.SetColor("_PeopleColor", peopleColor);
        material.SetColor("_FilterColor", filterColor);
        material.SetFloat("_Strength", strength);

        //一時RTコピーしてからレンダーターゲットへ逆コピーすると共に、マテリアル適用でエフェクトをかける
        commandBuffer.Blit(currentTargetID, tempID);
        commandBuffer.Blit(tempID, currentTargetID, material);
        
        //処理実行
        context.ExecuteCommandBuffer(commandBuffer);

        //一時RT・コマンドバッファ解放
        commandBuffer.ReleaseTemporaryRT(tempID);
        CommandBufferPool.Release(commandBuffer);
    }
}