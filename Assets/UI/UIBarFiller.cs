using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBarFiller : MonoBehaviour
{
    private RectTransform rect;

    [SerializeField] private float maxWidth = 500f;

    private float progress;
    public float Progress { get => progress; set => SetProgress(value); }

    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    private void SetProgress(float value)
    {
        progress = value;
        UpdateBar();
    }

    private void UpdateBar()
    {
        rect.sizeDelta = new Vector2(Progress * maxWidth, rect.sizeDelta.y);
    }
}
