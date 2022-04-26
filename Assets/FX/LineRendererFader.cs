using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineRendererFader : MonoBehaviour
{
    private LineRenderer line;
    /// <summary>
    /// Longueur de l'effet. Après ce temps l'objet sera invisible puis détruit.
    /// </summary>
    [SerializeField] private float effectLength = 1f;
    /*
    /// <summary>
    /// Courbe qui représente l'évolution de l'alpha de la ligne dans le temps
    /// </summary>
    [SerializeField] private AnimationCurve blendOverTime;*/
    [SerializeField] private AnimationCurve startWidthOverTime;
    [SerializeField] private AnimationCurve endWidthOverTime;
    [SerializeField] private float maxWidth = .2f;
    /*
    /// <summary>
    /// Couleur de la ligne
    /// </summary>
    [ColorUsageAttribute(true, true)]
    [SerializeField] private Color startColor;
    [ColorUsageAttribute(true, true)]
    [SerializeField] private Color endColor;*/

    private float timer;

    private float Advancement => timer / effectLength;


    private Vector3 initialPosition;
    private Vector3 targetPosition;
    [SerializeField] private AnimationCurve tailMovementSpeedCurve = AnimationCurve.Linear(0f, 1f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        initialPosition = line.GetPosition(0);
        targetPosition = line.GetPosition(line.positionCount - 1);
    }

    // Update is called once per frame
    void Update()
    {
        /*float value = blendOverTime.Evaluate(Advancement);

        Color currentColor = Color.Lerp(startColor, endColor, value);
        
        line.material.SetColor("_EmissionColor", currentColor);*/

        float newStartWidth = startWidthOverTime.Evaluate(Advancement) * maxWidth;
        float newEndWidth = endWidthOverTime.Evaluate(Advancement) * maxWidth;
        //line.widthMultiplier = newWidth;
        line.startWidth = newStartWidth;
        line.endWidth = newEndWidth;

        Vector3 newPosition = initialPosition + (targetPosition - initialPosition) * tailMovementSpeedCurve.Evaluate(Advancement);
        line.SetPosition(0, newPosition);

        timer += Time.deltaTime;

        // Si on a fini l'effet on détruit la ligne
        if (timer > effectLength)
            Destroy(gameObject);
    }
}
