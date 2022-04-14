using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Initialization
    [SerializeField] private Transform player;


    [SerializeField] private float distanceAhead;
    [SerializeField] private float cameraSpeed;

    private float lookAhead; // Look ahead effect on camera

    // Start is called before the first frame update
    void Start()
    {
        // Set up camera position on player
        transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        // Follow player
        transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);

        // Camera Inertia
        // Use lerp for gradual value change
        lookAhead = Mathf.Lerp(lookAhead, (distanceAhead * player.localScale.x), Time.deltaTime * cameraSpeed);
    }
}
