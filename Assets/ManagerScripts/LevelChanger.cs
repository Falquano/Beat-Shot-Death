using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputAction;

public class LevelChanger : MonoBehaviour
{
   

    public void GotoLevel()
    {
        SceneManager.LoadScene("Hall_2");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
