using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionOnde : MonoBehaviour
{
    [SerializeField] private LayerMask MaskPlanOnde;
    [SerializeField] private ShootPlayer ScriptShootPlayer;

    private void Update()
    {


        Ray pointerRay = Camera.main.ScreenPointToRay(ScriptShootPlayer.MouseScreenPosition);
        if (Physics.Raycast(pointerRay, out RaycastHit hitInfo, float.MaxValue, MaskPlanOnde))
        {
            transform.position = hitInfo.point;
        }
    }
}
