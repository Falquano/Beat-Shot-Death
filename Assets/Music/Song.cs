using FMODUnity;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Song", order = 1)]
public class Song : ScriptableObject
{
	public int BPM = 120;
	public string Title;
	public string Artist;
	public EventReference SongReference;
	public float Offset;

	public float TempoLength => GetTempoLength(BPM);

	private static float GetTempoLength(float bpm)
	{
		return 60 / bpm;
	}
}