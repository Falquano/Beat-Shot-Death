using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;


public class PositionOnde : MonoBehaviour
{
    [SerializeField] private LayerMask MaskPlanOnde;
    [SerializeField] private ShootPlayer ScriptShootPlayer;
    [SerializeField] private VisualEffect Onde4;

    
    private void LateUpdate()
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

    public void OnComboChange()
    {
        if (ScriptShootPlayer.combo <= 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (ScriptShootPlayer.combo <= 10)
        {
            transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
        }
        else if (ScriptShootPlayer.combo <= 30)
        {
            transform.localScale = new Vector3(1.50f, 1.5f, 1.5f);
        }
        else if (ScriptShootPlayer.combo <= 60)
        {
            transform.localScale = new Vector3(1.75f, 1.75f, 1.75f);
        }
        else if (ScriptShootPlayer.combo <= 100)
        {
            transform.localScale = new Vector3(2f, 2f, 2f);
        }
    }
}
