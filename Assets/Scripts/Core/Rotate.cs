using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [Header("Free Rotation Settings")] 
    [SerializeField] private float speedInRotation;

    [Header("Restrained Rotation Settings")]
    [SerializeField] private bool restrainRotation = false;
    [SerializeField] private float frequency = 0.5f;
    [SerializeField] private float maxRotation = 45f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!restrainRotation)
            FreeRotate();
        else
            RestrainedRotate();
    }

    private void FreeRotate()
    {
        transform.Rotate(0, 0, speedInRotation * 360 * Time.deltaTime);
    }

    private void RestrainedRotate()
    {
        transform.localRotation = Quaternion.Euler(0f, 0f, maxRotation * Mathf.Sin(Time.fixedTime * frequency * Mathf.PI));
    }
}
