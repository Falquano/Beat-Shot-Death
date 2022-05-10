using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageIndicator : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float lifetime = 0.6f;
    public float minDist = 2f;
    public float maxDist = 3f;

    private Vector3 iniPos;
    private Vector3 targetPos;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        //transform.LookAt(2 * transform.position);

        float direction = Random.rotation.eulerAngles.x;
        iniPos = transform.position;
        float dist = Random.Range(minDist, maxDist);
        targetPos = iniPos + (Quaternion.Euler(0, 0, direction) * new Vector3(dist, dist, 0f));
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float fraction = lifetime / 2f;

        if (timer > lifetime) Destroy(gameObject);
        else if (timer > fraction) text.color = Color.Lerp(text.color, Color.clear, (timer - fraction) / (lifetime - fraction));

        transform.position = Vector3.Lerp(iniPos, targetPos, Mathf.Sin(timer / lifetime));
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, Mathf.Sin(timer / lifetime));
    }

    public void SetDamageText(int amount)
    {
        text.text = amount.ToString();

        if (amount >= 10 && amount <=20)
        {
            text.color = new Color (0,0,255, 1f);//blue
            text.fontSize = 28f;
        }
        else if (amount >= 30 && amount <=60)
        {
            text.color = new Color (0,255,0, 1f); //green
            text.fontSize = 35f;
        }  
        else if (amount >= 61 && amount <=80)
        {
            text.color = new Color (255,200,0, 1f);//yellow
            text.fontSize = 40f;
        }
        else if (amount == 120)
        {
            text.color = new Color (255,0,0, 1f);//rouge
            text.fontSize = 45f;
        } 
    }
}