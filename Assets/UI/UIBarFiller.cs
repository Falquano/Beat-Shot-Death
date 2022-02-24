using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBarFiller : MonoBehaviour
{
    private RectTransform rect;

    [SerializeField] private float maxWidth = 500f;

    private float progress;
    public float Progress { get => progress; set => SetProgress(value); }

    private float visualProgress;
    [SerializeField] private float interpolationTime;
    [SerializeField] private AnimationCurve interpolationCurve;
    private float interpolationOrigin;
    private float timer;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    private void SetProgress(float value)
    {
        interpolationOrigin = progress;
        progress = value;
        UpdateBar();
        timer = 0f;
        enabled = true;
    }

    private void UpdateBar()
    {
        rect.sizeDelta = new Vector2(visualProgress * maxWidth, rect.sizeDelta.y);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        float timeProgress = interpolationCurve.Evaluate(timer / interpolationTime);

        if (timeProgress >= 1f)
        {
            visualProgress = progress;
            UpdateBar();
            enabled = false;
            return;
        }

        visualProgress = Mathf.Lerp(interpolationOrigin, Progress, timeProgress);
        UpdateBar();
    }
}
