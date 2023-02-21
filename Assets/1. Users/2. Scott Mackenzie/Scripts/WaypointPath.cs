using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    //Code acquired from this tutorial: https://www.youtube.com/watch?v=ly9mK0TGJJo&ab_channel=KetraGames

    public Transform GetWaypoint(int waypointIndex)
    {
        return transform.GetChild(waypointIndex); 
    }

    public int GetNextWaypointIndex(int currentWaypointIndex)
    {
        int nextWaypointIndex = currentWaypointIndex + 1;

        if (nextWaypointIndex == transform.childCount)
        {
            nextWaypointIndex = 0; //Loop it back around
        }

        return nextWaypointIndex;
    }
}
