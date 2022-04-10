using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameAnalyser : MonoBehaviour
{
    public Dictionary<Zone, GameSequence> Sequencer;
    private Zone currentZone;

    private void Start()
    {
        System.IO.Directory.CreateDirectory(defaultSavePath);

        Sequencer = new Dictionary<Zone, GameSequence>();

        foreach (Zone zone in FindObjectsOfType<Zone>())
        {
            Sequencer.Add(zone, new GameSequence()
            {
                Zone = zone
            });

            zone.onPlayerEnter.AddListener(EnterZone);
            zone.onRoomCleared.AddListener(ClearZone);
        }
    }

    public void EnterZone(Zone zone)
    {
        Sequencer[zone].TimeEntered = Time.time;
        currentZone = zone;
    }

    public void Shoot(ShotInfo shot)
    {
        if (currentZone == null)
            return;

        Sequencer[currentZone].Shots[shot.Quality]++;
    }

    public void ClearZone(Zone zone)
    {
        Sequencer[zone].TimeExited = Time.time;
    }

    public void SaveData(string path)
    {
        StreamWriter writer = new StreamWriter(GetCorrectSavePath(path));

        writer.WriteLine("Zone name,Duration,Time entered,Time exited,Failed shots,Okay shots,Perfect shots");

        foreach (GameSequence sequence in Sequencer.Values)
        {
            Debug.Log(sequence.CSVLine());
            writer.WriteLine(sequence.CSVLine());
        }

        writer.Close();
    }

    private string GetCorrectSavePath(string path) => GetCorrectSavePath(path, 1);

    private string GetCorrectSavePath(string path, int count)
    {
        if (File.Exists(path + count + defaultSaveSuffix))
        {
            return GetCorrectSavePath(path, count + 1);
        }

        return path + count + defaultSaveSuffix;
    }

    private void OnDestroy()
    {
        SaveData(defaultSavePath + defaultSaveName);
    }

    private const string defaultSavePath = "./PlaytestData/";
    private const string defaultSaveName = "gameSequencer";
    private const string defaultSaveSuffix = ".csv";
}

public class GameSequence
{
    public Zone Zone;
    public float TimeEntered { get; set; }
    public float TimeExited { get; set; }
    public Dictionary<ShotQuality, int> Shots { get; } = new Dictionary<ShotQuality, int>();

    public GameSequence()
	{
        Shots.Add(ShotQuality.Bad, 0);
        Shots.Add(ShotQuality.Good, 0);
        Shots.Add(ShotQuality.Perfect, 0);
    }

    public double Duration => TimeExited - TimeEntered;

    public string CSVLine()
    {
        return Zone.Name + "," + Duration.ToString("0.00").Replace(',', '.') + "," + TimeEntered.ToString("0.00").Replace(',', '.') + "," + TimeExited.ToString("0.00").Replace(',', '.')
            + "," + Shots[ShotQuality.Bad] + "," + Shots[ShotQuality.Good] + "," + Shots[ShotQuality.Perfect];
    }
}
