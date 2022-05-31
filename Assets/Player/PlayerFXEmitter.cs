using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lance des effets visuels et audio li�s au joueur
/// </summary>
public class PlayerFXEmitter : MonoBehaviour
{
    [SerializeField] private GameObject PerfectShotLinePrefab;
    [SerializeField] private GameObject GoodShotLinePrefab;
    [SerializeField] private GameObject BadShotLinePrefab;
    //[SerializeField] private StudioEventEmitter GunshotSoundEmitter;
    [SerializeField] private EventReference GunshotSound;
    [SerializeField] private EventReference BadshotSound;
    [SerializeField] private StudioEventEmitter FootstepSoundEmitter;
    [SerializeField] private GameObject ImpactParticlesPrefab;
    [SerializeField] private float okayShotImpactSize = .333f;
    [SerializeField] private GameObject muzzleFlashPrefab;

    private int combo;

    public void ShotFX(ShotInfo shotInfo, int damage)
    {
        ZapLine(shotInfo);
        Impact(shotInfo.EndPos, shotInfo.EndNormal, shotInfo.Quality);
        MuzzleFlash(shotInfo);

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
        // Je cr�e un truc qui joue un son
        EventInstance gunshot;

        if (shotInfo.Quality == ShotQuality.Bad)
        {
            // Instancie la merde
            gunshot = FMODUnity.RuntimeManager.CreateInstance(BadshotSound); // Changer le EventReference
        }
        else
        {
            gunshot = FMODUnity.RuntimeManager.CreateInstance(GunshotSound);
        }

        // je met en place son param�tre
        gunshot.setParameterByNameWithLabel("Quality", shotInfo.Quality.ToString());

        if (combo >= 0 && combo <= 1)
        {
            gunshot.setParameterByName("Palier2", 0);
        }
        else if (combo > 1 && combo <= 10)
        {
            gunshot.setParameterByName("Palier2", 1);
        }
        else if (combo > 10 && combo <= 30)
        {
            gunshot.setParameterByName("Palier2", 2);
        }
        else if (combo > 30 && combo <= 60)
        {
            gunshot.setParameterByName("Palier2", 3);
        }
        else if (combo > 60 && combo <= 100)
        {
            gunshot.setParameterByName("Palier2", 4);
        }
        // je le lance
        gunshot.start();
    }

    private void MuzzleFlash(ShotInfo shotInfo)
	{
        GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, shotInfo.StartPos, Quaternion.identity);
        muzzleFlash.transform.forward = shotInfo.EndPos - shotInfo.StartPos;
	}

    public void OnFootstep()
    {
        FootstepSoundEmitter.Play();
    }

    public void ComboChange(int newCombo, int max)
    {
        combo = newCombo;
    }

    public void OnDeathSound()
    {

    }
}
