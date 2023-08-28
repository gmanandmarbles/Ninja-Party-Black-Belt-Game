using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reset : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
           
            transform.position = originalPosition;
            transform.rotation = originalRotation;
            GetComponent<Rigidbody>().isKinematic = true;         }
    }
}
