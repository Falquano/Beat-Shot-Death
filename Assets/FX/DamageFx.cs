using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageFx : MonoBehaviour
{
    [SerializeField] private CameraShaker camShaker;

    [SerializeField] private float shakeFrequency = 8f;
    [SerializeField] private float shakeAmplitude = .5f;
    [SerializeField] private float shakeDuration = .3f;

    [SerializeField] private Image vignette;
    private float timer;
    [SerializeField] private float vignetteDuration = .4f;
    [SerializeField] private AnimationCurve vignetteCurve;

    private void Start()
    {
        enabled = false;
    }

    public void EffectsOnDamage(int damage, int newLife)
    {
        // On commence par le camshake
        camShaker.Shake(shakeFrequency, shakeAmplitude, shakeDuration);

        // Ensuite on lance l'effet de vignette
        timer = 0f;
        enabled = true;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= vignetteDuration)
        {
            enabled = false;
            vignette.color = new Color(1f, 1f, 1f, 0f);
            return;
        }

        float progress = timer / vignetteDuration;
        float alpha = vignetteCurve.Evaluate(progress);
        Color color = Color.white;
        color.a = alpha;
        vignette.color = color;
    }
}
