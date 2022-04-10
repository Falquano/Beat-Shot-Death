using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicParameterizer : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter songEmitter;

    public void SetCombo(int combo, int maxCombo)
    {
        songEmitter.SetParameter("Combo", (float)combo / (float)maxCombo);
    }

    public void SetShotQuality(ShotInfo shot)
    {
        songEmitter.SetParameter("Last Shot Quality", (float)shot.Quality);
    }

    public void ResetQuality()
    {
        songEmitter.SetParameter("Last Shot Quality", 4f);
    }
}
