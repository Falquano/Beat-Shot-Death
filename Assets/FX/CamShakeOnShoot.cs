using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraShaker))]
public class CamShakeOnShoot : MonoBehaviour
{
    private CameraShaker shaker;

    [SerializeField] private float frequency = 1f;
    [SerializeField] private float time = .25f;
    [SerializeField] private float failedAmplitude = 0f;
    [SerializeField] private float okayAmplitude = 1f;
    [SerializeField] private float perfectAmplitude = 2f;

    private void Start()
    {
        shaker = GetComponent<CameraShaker>();
    }

    public void OnEvent(ShotInfo info)
    {
        shaker.Shake(GetAmplitude(info.Quality), frequency, time);
    }

    public float GetAmplitude(ShotQuality quality)
    {
        switch (quality)
        {
            case ShotQuality.Failed:
                return failedAmplitude;
            case ShotQuality.Okay:
                return okayAmplitude;
            case ShotQuality.Perfect:
                return perfectAmplitude;
            default:
                return 0f;
        }
    }
}
