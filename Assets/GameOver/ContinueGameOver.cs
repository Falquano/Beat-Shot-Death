using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueGameOver : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Hall_2");
    }
}
