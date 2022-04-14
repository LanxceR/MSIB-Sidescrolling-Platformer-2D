using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingWaveEffect : MonoBehaviour
{
    [Header("Wave Settings")]
    [SerializeField] private float amplitude = 0.5f;
    [SerializeField] private float frequency = 1f;
    [SerializeField] private bool offsetPhaseToXPosition = false;
    [SerializeField] private float offsetMultiplier = 1f;

    [Header("Position Setting")]
    [SerializeField] private bool isLocal = false;


    private Vector3 startingPosition;
    private Vector3 tempPosition;

    // Start is called before the first frame update
    void Start()
    {
        if (!isLocal)
            startingPosition = transform.position;
        else
            startingPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocal)
        {
            // Float up and down with sine function
            // Mathf.Sin( pi*2 ) = 0, pi = 180 deg, pi = 3.14 rad
            tempPosition = startingPosition;
            tempPosition.y += Mathf.Sin((Time.fixedTime + (offsetPhaseToXPosition ? startingPosition.x * offsetMultiplier : 0f)) * frequency * Mathf.PI) * amplitude;

            transform.position = tempPosition;
        }
        else
        {
            // Float up and down with sine function
            // Mathf.Sin( pi*2 ) = 0, pi = 180 deg, pi = 3.14 rad
            tempPosition = startingPosition;
            tempPosition.y += Mathf.Sin((Time.fixedTime + (offsetPhaseToXPosition ? startingPosition.x * offsetMultiplier : 0f)) * frequency * Mathf.PI) * amplitude;

            transform.localPosition = tempPosition;
        }
    }
}
