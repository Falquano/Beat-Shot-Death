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
    private bool step0 = true;
    private bool step1 = false;
    private bool step2 = false;
    private bool step3 = false;
    private bool step4 = false;

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

        if (timer >= vignetteDuration && step0)
        {
            enabled = false;
            vignette.color = new Color(1f, 1f, 1f, 0.25f);
            step0 = false;
            step1 = true;
            return;
        }
        if (timer >= vignetteDuration && step1)
        {
            enabled = false;
            vignette.color = new Color(1f, 1f, 1f, 0.5f);
            step1 = false;
            step2 = true;
            return;
        }
        if (timer >= vignetteDuration && step2)
        {
            enabled = false;
            vignette.color = new Color(1f, 1f, 1f, 0.75f);
            step2 = false;
            step3 = true;
            return;
        }
        if (timer >= vignetteDuration && step3)
        {
            enabled = false;
            vignette.color = new Color(1f, 1f, 1f, 1f);
            step3 = false;
            step4 = true;
            return;
        }
        if (timer >= vignetteDuration && step4)
        {
            enabled = false;
            vignette.color = new Color(1f, 1f, 1f, 1f);
            return;
        }

        float progress = timer / vignetteDuration;
        float alpha = vignetteCurve.Evaluate(progress);
        Color color = Color.white;
        if (step0)
        {
            color.a = 0f;
        }
        if (step1)
        {
            color.a = 0.25f;
        }
        if (step2)
        {
            color.a = 0.50f;
        }
        if (step3)
        {
            color.a = 0.75f;
        }
        if (step4)
        {
            color.a = 1f;
        }
        vignette.color = color;
    }
}
