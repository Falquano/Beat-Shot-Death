using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraShaker : MonoBehaviour
{
    private CinemachineBasicMultiChannelPerlin noise;
    [SerializeField] private AnimationCurve ShakeFalloff;
    private float amplitude;
    private float timer;
    private float shakeTime = 1;

    private void Start()
    {
        noise = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake(float amplitude, float frequency, float time)
    {
        this.amplitude = amplitude;
        noise.m_FrequencyGain = frequency;
        timer = 0f;
        shakeTime = time;
        enabled = true;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > shakeTime)
        {
            amplitude = 0f;
            enabled = false;
            noise.m_AmplitudeGain = 0f;
            return;
        }

        noise.m_AmplitudeGain = ShakeFalloff.Evaluate(timer / shakeTime) * amplitude;
    }
}
