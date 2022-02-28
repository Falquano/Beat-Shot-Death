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
    [SerializeField] private GameObject ZapLinePrefab;
    //[SerializeField] private StudioEventEmitter GunshotSoundEmitter;
    [SerializeField] private EventReference GunshotSound;
    [SerializeField] private StudioEventEmitter FootstepSoundEmitter;
    [SerializeField] private GameObject ImpactParticlesPrefab;

    public void OnShoot(ShotInfo shotInfo)
    {
        if (shotInfo.Quality != ShotQuality.Failed)
        {
            ZapLine(shotInfo);
            Impact(shotInfo.EndPos, shotInfo.EndNormal);
        }


        EmitShotSound(shotInfo);
    }

    private void ZapLine(ShotInfo shotInfo)
    {
        LineRenderer line = Instantiate(ZapLinePrefab, Vector3.zero, Quaternion.identity).GetComponent<LineRenderer>();
        line.SetPosition(0, shotInfo.StartPos);
        line.SetPosition(1, shotInfo.EndPos);
    }

    private void Impact(Vector2 position, Vector2 normal)
    {
        Transform particle = Instantiate(ImpactParticlesPrefab, position, Quaternion.identity).transform;
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
