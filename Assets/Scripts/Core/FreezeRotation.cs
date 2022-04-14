using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeRotation : MonoBehaviour
{
    private Quaternion worldRotation;

    // Start is called before the first frame update
    void Start()
    {
        worldRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = worldRotation;
    }
}
