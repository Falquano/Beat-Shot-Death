using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineRendererFader : MonoBehaviour
{
    private LineRenderer line;
    /// <summary>
    /// Longueur de l'effet. Apr�s ce temps l'objet sera invisible puis d�truit.
    /// </summary>
    [SerializeField] private float effectLength = 1f;
    /// <summary>
    /// Courbe qui repr�sente l'�volution de l'alpha de la ligne dans le temps
    /// </summary>
    [SerializeField] private AnimationCurve blendCurve;
    /// <summary>
    /// Couleur de la ligne
    /// </summary>
    [SerializeField] private Color color;
    private float timer;

    private float Advancement => timer / effectLength;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float value = blendCurve.Evaluate(Advancement);

        Color currentColor = color;
        currentColor.a = value;

        line.startColor = currentColor;
        line.endColor = currentColor;

        timer += Time.deltaTime;

        // Si on a fini l'effet on d�truit la ligne
        if (timer > effectLength)
            Destroy(gameObject);
    }
}
