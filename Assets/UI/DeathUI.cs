using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathUI : MonoBehaviour
{
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject cursor;

    public void OnDeath()
    {
        Destroy(cursor);
        Cursor.visible = true;
        deathScreen.SetActive(true);
    }

    public void RestartLevel()
    {
        //Remettre le temps en play :!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //SceneManager.LoadScene("Hall_2", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
