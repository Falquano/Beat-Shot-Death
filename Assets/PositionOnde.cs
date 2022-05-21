using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;
using UnityEditor.VFX;
using UnityEditor.VFX.UI;

public class PositionOnde : MonoBehaviour
{
    [SerializeField] private LayerMask MaskPlanOnde;
    [SerializeField] private ShootPlayer ScriptShootPlayer;
    [SerializeField] private VisualEffect Onde4;

    private void Update()
    {


        Ray pointerRay = Camera.main.ScreenPointToRay(ScriptShootPlayer.MouseScreenPosition);
        if (Physics.Raycast(pointerRay, out RaycastHit hitInfo, float.MaxValue, MaskPlanOnde))
        {
            transform.position = hitInfo.point;
        }
    }

    public void OnPerfectShootOnde()
    {
        Onde4.Play();
    }
}
