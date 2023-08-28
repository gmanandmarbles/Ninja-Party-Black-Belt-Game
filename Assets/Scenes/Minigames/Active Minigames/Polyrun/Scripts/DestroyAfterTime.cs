using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [Header("Destruction Timer")]
    public float timeToDestruction;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyObject", timeToDestruction);
    }
    void DestroyObject()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {

    }
}

