using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEditor.VFX;
using UnityEditor.VFX.UI;


public class OndeScriptVFX : MonoBehaviour
{
    [SerializeField] private ShootPlayer ScriptShootPlayer;
    private int ComboPlayer;
    private Vector3 MousePositionOnPlanOnde;

    [SerializeField] private List<VisualEffect> VFXOndeList = new List<VisualEffect>();
    [SerializeField] private LayerMask MaskPlanOnde;



    private void Start()
    {
        ComboPlayer = ScriptShootPlayer.combo;
    }

    public void OnMesureStart()
    {
        //Appel� � chaque d�but de mesure
        //Calcul le combo et play l'anim de l'onde en fonction

        ComboPlayer = ScriptShootPlayer.combo;
        if (ComboPlayer >1 && ComboPlayer <= 10)
        {
            VFXOndeList[0].Play();
        }
        else if (ComboPlayer > 10 && ComboPlayer <= 30)
        {
            VFXOndeList[1].Play();
        }
        else if (ComboPlayer > 30 && ComboPlayer <= 60)
        {
            VFXOndeList[2].Play();
        }
        else if (ComboPlayer > 60 && ComboPlayer <= 100)
        {
            VFXOndeList[3].Play();
        }
    }

    private void Update()
    {
       
        /*Vector3 OndePosition = new Vector3(ScriptShootPlayer.MouseWorldPosition.x, ScriptShootPlayer.MouseWorldPosition.y, ScriptShootPlayer.MouseWorldPosition.z);
        transform.position = OndePosition;
        transform.rotation = new Quaternion(71.74f, 0f, -6.7f, 0f);*/


        Ray pointerRay = Camera.main.ScreenPointToRay(ScriptShootPlayer.MouseScreenPosition);
        if (Physics.Raycast(pointerRay, out RaycastHit hitInfo, float.MaxValue, MaskPlanOnde))
        {
            transform.position = hitInfo.point;
            
            
            
            /*Vector3 direction = MousePositionOnPlanOnde - transform.position;
            direction.y = 0;
            transform.forward = direction.normalized;
            Debug.DrawRay(transform.position, transform.right * 4, Color.white);*/
        }
    }
    

    
}
