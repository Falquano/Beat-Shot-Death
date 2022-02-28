using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ATTENTION
// Ce code est sale, il devrait être séparé en plusieurs sous-classes
// Je fais juste ça vite, pardon pour la saleté
public class MainMenu : MonoBehaviour
{
    public static Song SelectedSong { get; private set; }
    [SerializeField] private Dropdown songDropdown;
    [SerializeField] private Song[] songs;

    [SerializeField] private string gameScenePath;

    // Start is called before the first frame update
    void Start()
    {
        List<string> songList = new List<string>();

        foreach (Song song in songs)
		{
            songList.Add($"{song.name} by {song.Artist}");
		}

        songDropdown.AddOptions(songList);
        SelectedSong = songs[0];
    }

    public void OnValueChange(int index)
	{
        SelectedSong = songs[index];
        Debug.Log($"Selected song : {SelectedSong.Name}");
	}

    public void LoadGame()
	{
        SceneManager.LoadScene(gameScenePath);
	}
}
