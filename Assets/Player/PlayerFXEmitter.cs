using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFXEmitter : MonoBehaviour
{
    [SerializeField] private GameObject ZapLinePrefab;

    public void OnShoot(ShotInfo shotInfo)
    {
        if (shotInfo.Quality != ShotQuality.Failed)
            ZapLine(shotInfo);
    }

    private void ZapLine(ShotInfo shotInfo)
    {
        LineRenderer line = Instantiate(ZapLinePrefab, Vector3.zero, Quaternion.identity).GetComponent<LineRenderer>();
        line.SetPosition(0, shotInfo.StartPos);
        line.SetPosition(1, shotInfo.EndPos);
    }
}
