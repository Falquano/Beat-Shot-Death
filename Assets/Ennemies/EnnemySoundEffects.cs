using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemySoundEffects : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter GunshotSoundEmitter;
    [SerializeField] private StudioEventEmitter FootstepSoundEmitter;
    [SerializeField] private StudioEventEmitter HurtSoundEmitter;
    [SerializeField] private StudioEventEmitter DieSoundEmitter;


    public void OnShoot()
    {
        EmitShotSound();

    }

    private void EmitShotSound()
    {
        GunshotSoundEmitter.Play();

    }

    public void OnFootstep()
    {
        FootstepSoundEmitter.Play();
        //Debug.Log("JeMarche");
    }

    public void HurtSoundEffect(int damage, int newLife)
    {
        if (newLife > 0)
            HurtSoundEmitter.Play();
    }

    public void DieSoundEffect()
    {
        DieSoundEmitter.Play();
    }
}
