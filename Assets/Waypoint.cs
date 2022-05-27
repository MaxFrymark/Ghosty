using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool normalPath;
    public bool NormalPath { get { return normalPath; } }

    [SerializeField] bool agentOnly;
    public bool AgentOnly { get { return agentOnly; } }

    [SerializeField] Waypoint nextWaypoint;
    public Waypoint NextWaypoint { get { return nextWaypoint; } }
    [SerializeField] Waypoint previousWaypoint;
    public Waypoint PreviousWaypoint { get { return previousWaypoint; } }

}
