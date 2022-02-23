using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineRendererFader : MonoBehaviour
{
    private LineRenderer line;
    [SerializeField] private float effectLength = 1f;
    [SerializeField] private AnimationCurve blendCurve;
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

        // Si on a fini l'effet on détruit la ligne
        if (timer > effectLength)
            Destroy(gameObject);
    }
}
