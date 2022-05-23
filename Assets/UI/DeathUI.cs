using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathUI : MonoBehaviour
{
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject cursor;
    [SerializeField] private PlayerHealthSystem scripthealth;

    public void OnDeath()
    {
        Destroy(cursor);
        Cursor.visible = true;
        deathScreen.SetActive(true);
        
    }

    public void RestartLevel()
    {
        print("scene");
        scripthealth.PlayerisDead = false;
        SceneManager.LoadScene("Hall_2", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
