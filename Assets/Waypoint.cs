using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool normalPath;
    public bool NormalPath { get { return normalPath; } }
    
    [SerializeField] Vector2 direction;
    public Vector2 Direction { get { return direction; } }
}
