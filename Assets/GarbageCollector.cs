using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollector : MonoBehaviour
{
    public List<EnnemyBehavior> garbage = new List<EnnemyBehavior>();

    private void OnTriggerEnter(Collider other)
    {
        EnnemyBehavior zozo = other.GetComponent<EnnemyBehavior>();
        Debug.Log(other.name);

        if (zozo != null)
            garbage.Add(zozo);
    }

    private void OnTriggerExit(Collider other)
    {
        EnnemyBehavior zozo = other.GetComponent<EnnemyBehavior>();

        if (zozo != null)
            garbage.Remove(zozo);
    }

    public void TriSelectif()
    {
        foreach (EnnemyBehavior zozo in garbage)
            Destroy(zozo.gameObject);
    }
}
