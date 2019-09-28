//水波纹屏幕后处理
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RipperPostEffect : PostEffectBase
{
    /// <summary>
    /// uv到中心点向量的距离系数    //通过下面的系数可以调节波纹的振幅大小等。
    /// </summary>
    public float distanceFactor = 5.0f;
    /// <summary>
    /// 时间系数
    /// </summary>
    public float timeFactor = -30.0f;
    /// <summary>
    /// 波纹幅度系数
    /// </summary>
    public float totalFactor = 15.0f;
    /// <summary>
    /// 波纹半径限制
    /// </summary>
    public float waveWidth = 0.03f;
    /// <summary>
    /// 波纹扩散的速度
    /// </summary>
    public float waveSpeed = 0.03f;
    /// <summary>
    /// 波纹开始时间
    /// </summary>
    private float waveStartTime;
    /// <summary>
    /// 鼠标点击屏幕的位置
    /// </summary>
    public Vector4 startPos = new Vector4(0, 0, 0, 0);
    // Use this for initialization
    public float time;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //计算波纹移动的距离（时间*速度）
        float curWaveDistance = (Time.time - waveStartTime) * waveSpeed;
        //设置一系列参数
        _Material.SetFloat("_distanceFactor", distanceFactor);
        _Material.SetFloat("_timeFactor", timeFactor);
        _Material.SetFloat("_totalFactor", totalFactor);
        _Material.SetFloat("_waveWidth", waveWidth);
        _Material.SetFloat("_curWaveDis", curWaveDistance);
        _Material.SetVector("_startPos", startPos);
        Graphics.Blit(source, destination, _Material);

    }

    private void Start()
    {
        //记录开始时间
        waveStartTime = Time.time;
    }
    private void Update()
    {
        //自动销毁此组件
        time += Time.deltaTime;
        if (time >= 1)
        {
            Destroy(this);
        }
    }
}