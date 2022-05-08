using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboUIJauge : MonoBehaviour
{
    [SerializeField] private Image ComboIMage1;
    [SerializeField] private Image ComboIMage2;
    [SerializeField] private Image ComboIMage3;
    [SerializeField] private Image ComboIMage4;
    

   


    public void OnComboChange(int combo, int  max)
    {



        if(combo == 0)
        {
            ComboIMage1.fillAmount = 0;
            ComboIMage2.fillAmount = 0;
            ComboIMage3.fillAmount = 0;
            ComboIMage4.fillAmount = 0;
        }
        else if(combo > 0 && combo <= 10)
        {
            
            //Le combo 1 se remplit en fonction
            ComboIMage1.fillAmount = (float) combo / 42.0f ;
            ComboIMage2.fillAmount = 0;
            ComboIMage3.fillAmount = 0;
            ComboIMage4.fillAmount = 0;
        }
        else if (combo > 10 && combo <= 30)
        {

            //Le combo 2 se remplit en fonction
            ComboIMage1.fillAmount = 0.238f;
            ComboIMage2.fillAmount = (float)(combo - 10) / 84.0f;
            ComboIMage3.fillAmount = 0;
            ComboIMage4.fillAmount = 0;
        }
        else if (combo > 30 && combo <= 60)
        {

            //Le combo 3 se remplit en fonction
            ComboIMage1.fillAmount = 0.238f;
            ComboIMage2.fillAmount = 0.238f;
            ComboIMage3.fillAmount = (float)(combo - 30) / 126.0f;
            ComboIMage4.fillAmount = 0;
        }
        else if (combo > 60 && combo <= 100)
        {

            //Le combo 4 se remplit en fonction
            ComboIMage1.fillAmount = 0.238f;
            ComboIMage2.fillAmount = 0.238f;
            ComboIMage3.fillAmount = 0.238f;
            ComboIMage4.fillAmount = (float)(combo - 60) / 168.0f;
        }
    }
}
