using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempoGhost : MonoBehaviour
{
    private Material material;
    public float TargetValue { get => material.GetFloat("_TargetValue"); set => material.SetFloat("_TargetValue", value); }

    [SerializeField] private float lifetime = .3f;
    private float timer;

    public void Setup(float value, ShotInfo info)
    {
        material = GetComponent<Image>().material;
        Cursor.visible = false;
        timer = 0;

        TargetValue = value;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > lifetime)
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        TargetValue = 0f;
    }
}
