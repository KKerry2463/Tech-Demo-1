using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed;
    [SerializeField] private float checkDistance = 0.05f;

    private Transform targetWaypoint;
    private int currentWaypointIndex = 0;

    private void Start()
    {
        targetWaypoint = waypoints[0];
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetWaypoint.position) < checkDistance)
        { 
            targetWaypoint = GetNextWaypoint();
        }
    }

    private Transform GetNextWaypoint()
    {
        currentWaypointIndex++;
        if (currentWaypointIndex >= waypoints.Length)
        { 
            currentWaypointIndex = 0;
        }
        return waypoints[currentWaypointIndex];
    }
}
