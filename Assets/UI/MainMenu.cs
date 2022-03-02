using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ATTENTION
// Ce code est sale, il devrait �tre s�par� en plusieurs sous-classes
// Je fais juste �a vite, pardon pour la salet�
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
        songList.Add($"Rien (il y a un m�tronome)");

        songDropdown.value = songs.Length;
    }

    public void OnValueChange(int index)
	{
        if (index >= songs.Length)
		{
            SelectedSong = null; 
		}
        else
		{
            SelectedSong = songs[index];
		}
	}

    public void LoadGame()
	{
        SceneManager.LoadScene(gameScenePath);
	}
}
