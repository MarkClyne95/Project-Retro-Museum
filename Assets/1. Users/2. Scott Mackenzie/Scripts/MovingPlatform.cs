using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //Code acquired from this tutorial: https://www.youtube.com/watch?v=ly9mK0TGJJo&ab_channel=KetraGames

    [SerializeField]
    private WaypointPath waypointPath;

    [SerializeField]
    private float speed;

    private int targetWaypointIndex;

    private Transform previousWaypoint;
    private Transform targetWaypoint;

    private float timeToWaypoint;
    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        TargetNextWaypoint();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        elapsedTime += Time.deltaTime;

        //Change platform position by interpolating it between the previous and next waypoint
        //based on the elapsed time e.g half time elapsed = half way to target waypoint
        float elapsedPercentage = elapsedTime / timeToWaypoint;
        elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage); //Add smoothing at limits so movement eases in and out
        transform.position = Vector3.Lerp(previousWaypoint.position, targetWaypoint.position, elapsedPercentage);
        //transform.rotation = Quaternion.Lerp(previousWaypoint.rotation, targetWaypoint.rotation, elapsedPercentage);

        //Start moving to next waypoint once time is up
        if (elapsedPercentage >= 1) 
        {
            TargetNextWaypoint();
        }
    }

    private void TargetNextWaypoint()
    {
        previousWaypoint = waypointPath.GetWaypoint(targetWaypointIndex); //Pass in current waypoint
        targetWaypointIndex = waypointPath.GetNextWaypointIndex(targetWaypointIndex); //Find the next destination from the above
        targetWaypoint = waypointPath.GetWaypoint(targetWaypointIndex);

        elapsedTime = 0;

        float distanceToWaypoint = Vector3.Distance(previousWaypoint.position, targetWaypoint.position);
        timeToWaypoint = distanceToWaypoint / speed;
    }


    //Parent player to platform when they board it and reset it when they jump off
    private void OnTriggerEnter(Collider other)
    {
        other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);
    }
}
