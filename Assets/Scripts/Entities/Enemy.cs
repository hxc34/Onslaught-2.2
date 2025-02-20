using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [Tooltip("Waypoints in the order the enemy should follow")]
    public Transform[] waypoints;

    public float speed = 2f;
    private int currentWaypointIndex = 0;

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        // Move towards the current waypoint
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = targetWaypoint.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // If close enough to waypoint, move to next
        if (direction.magnitude <= distanceThisFrame)
        {
            // Snap to waypoint
            transform.position = targetWaypoint.position;
            currentWaypointIndex++;

            // If we've reached the last waypoint, do something (e.g., destroy or deal damage)
            if (currentWaypointIndex >= waypoints.Length)
            {
                // Reached end - destroy for now
                Destroy(gameObject);
            }
        }
        else
        {
            // Move forward
            transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        }
    }

}
