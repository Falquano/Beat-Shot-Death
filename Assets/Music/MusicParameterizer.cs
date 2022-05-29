using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicParameterizer : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter songEmitter;

    public void SetCombo(int combo, int maxCombo)
    {
        //songEmitter.SetParameter("Combo", (float)combo / (float)maxCombo);

        if (combo >= 0 && combo <= 1)
        {
            songEmitter.SetParameter("Palier", 0);
        }
        else if (combo > 1 && combo <= 10)
        {
            songEmitter.SetParameter("Palier", 1);
        }
        else if (combo > 10 && combo <= 30)
        {
            songEmitter.SetParameter("Palier", 2);
        }
        else if (combo > 30 && combo <= 60)
        {
            songEmitter.SetParameter("Palier", 3);
        }
        else
        {
            songEmitter.SetParameter("Palier", 4);
        }
    }

    /*public void SetShotQuality(ShotInfo shot)
    {
        songEmitter.SetParameter("Last Shot Quality", (float)shot.Quality);
    }

    public void ResetQuality()
    {
        songEmitter.SetParameter("Last Shot Quality", 4f);
    }*/
}
