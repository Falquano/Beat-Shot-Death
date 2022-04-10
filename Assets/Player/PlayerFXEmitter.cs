using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lance des effets visuels et audio liés au joueur
/// </summary>
public class PlayerFXEmitter : MonoBehaviour
{
    [SerializeField] private GameObject PerfectShotLinePrefab;
    [SerializeField] private GameObject GoodShotLinePrefab;
    [SerializeField] private GameObject BadShotLinePrefab;
    //[SerializeField] private StudioEventEmitter GunshotSoundEmitter;
    [SerializeField] private EventReference GunshotSound;
    [SerializeField] private StudioEventEmitter FootstepSoundEmitter;
    [SerializeField] private GameObject ImpactParticlesPrefab;
    [SerializeField] private float okayShotImpactSize = .333f;

    public void ShotFX(ShotInfo shotInfo)
    {
        ZapLine(shotInfo);
        Impact(shotInfo.EndPos, shotInfo.EndNormal, shotInfo.Quality);

        EmitShotSound(shotInfo);
    }

    private void ZapLine(ShotInfo shotInfo)
    {
        LineRenderer line;
        if (shotInfo.Quality == ShotQuality.Perfect)
            line = Instantiate(PerfectShotLinePrefab, Vector3.zero, Quaternion.identity).GetComponent<LineRenderer>();
        else if (shotInfo.Quality == ShotQuality.Good)
            line = Instantiate(GoodShotLinePrefab, Vector3.zero, Quaternion.identity).GetComponent<LineRenderer>();
        else
            line = Instantiate(BadShotLinePrefab, Vector3.zero, Quaternion.identity).GetComponent<LineRenderer>();

        line.SetPosition(0, shotInfo.StartPos);
        line.SetPosition(1, shotInfo.EndPos);
    }

    private void Impact(Vector3 position, Vector3 normal, ShotQuality quality)
    {
        Transform particle = Instantiate(ImpactParticlesPrefab, position, Quaternion.identity).transform;
        Vector3 size = Vector3.one;
        if (quality == ShotQuality.Good)
            size = Vector3.one * okayShotImpactSize;
        particle.localScale = size;
        particle.up = normal;
    }

    private void EmitShotSound(ShotInfo shotInfo)
    {
        // Je crée un truc qui joue un son
        EventInstance gunshot = FMODUnity.RuntimeManager.CreateInstance(GunshotSound);
        // je met en place son paramètre
        gunshot.setParameterByNameWithLabel("Quality", shotInfo.Quality.ToString());
        // je le lance
        gunshot.start();
    }

    public void OnFootstep()
    {
        FootstepSoundEmitter.Play();
    }
}
