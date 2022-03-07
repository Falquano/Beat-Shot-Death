using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputAction;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private string levelToLoad;

    public void GotoLevel(CallbackContext context)
    {
        if (context.performed)
            SceneManager.LoadScene(levelToLoad);
    }

    public void GotoLevel()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
