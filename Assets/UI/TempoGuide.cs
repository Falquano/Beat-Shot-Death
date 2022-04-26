using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempoGuide : MonoBehaviour
{
    public ShootPlayer player { get; set; }
    public TempoManager tempo { get; set; }
    private Material material;
    public float TargetValue { get => material.GetFloat("_TargetValue"); set => material.SetFloat("_TargetValue", value); }

    [SerializeField] private GameObject tempoGhost;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Image>().material;
        Cursor.visible = false;

        TargetValue = tempo.ObjectiveShoot;

        tempo.onMesureStart.AddListener(BeatEnds);
        player.OnShotEvent.AddListener(Shot);
    }

    // Update is called once per frame
    void Update()
    {
        // On met la variable "tempo" du material à jour
        TargetValue = tempo.Tempo;
    }

    public void BeatEnds(int newBeat)
	{
        Destroy(gameObject);
	}

    public void Shot(ShotInfo info)
	{
        /*material.SetColor("_GuideColor", Color.yellow);
        enabled = false;*/
        // Instancier un guide "fantôme" et supprimer celui ci !
        TempoGhost ghost = Instantiate(tempoGhost, transform.parent).GetComponent<TempoGhost>();
        ghost.Setup(tempo.Tempo, new ShotInfo());
	}

	private void OnDestroy()
	{
        TargetValue = 0f;
	}
}
