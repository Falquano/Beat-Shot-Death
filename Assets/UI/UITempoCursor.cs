using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITempoCursor : MonoBehaviour
{
    [SerializeField] private ShootPlayer player;
    

   

    private void Update()
    {
        transform.position = player.MouseScreenPosition;
    }

    

    
}
