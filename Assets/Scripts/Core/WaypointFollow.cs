using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollow : MonoBehaviour
{
    [Header("Main Settings")]
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float idleDuration;

    [SerializeField] private int currentWayPointIndex = 0;

    private float idleTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check the distance between current position and the next waypoint to follow
        if (Vector2.Distance(waypoints[currentWayPointIndex].transform.position, transform.position) < 0.01f)
        {
            ChangeDirection();
        }
        else
        {
            MoveTowardsWaypoint();
        }

    }

    private void ChangeDirection()
    {
        // Tick down idle timer
        idleTimer = idleTimer - Time.deltaTime < 0 ? 0 : idleTimer - Time.deltaTime;

        if (idleTimer <= 0)
        {
            // Go to the next waypoint, cycling back to 0
            currentWayPointIndex = currentWayPointIndex + 1 >= waypoints.Length ? 0 : currentWayPointIndex + 1;
        }
    }

    private void MoveTowardsWaypoint()
    {
        // Reset idle timer
        idleTimer = idleDuration;

        // Move towards waypoint
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWayPointIndex].transform.position, speed * Time.deltaTime);
    }
}
