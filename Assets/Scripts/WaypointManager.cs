using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
  public Waypoint[] waypoints;

  void Awake()
  {

    waypoints = GetComponentsInChildren<Waypoint>();
    
  }

  public Waypoint GetNeWaypoint(int currentIndex)
  {
        if (!waypoints[currentIndex].goal)
            return waypoints[currentIndex++];
        else
        {
            Debug.Log("Enemy has reached the Castle");
            return null;
        }
  }
}
