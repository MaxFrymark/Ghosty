using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool ignoreIfPanicked;
    public bool IgnoreIfPanicked { get { return ignoreIfPanicked; } }
    
    [SerializeField] Vector2 direction;
    public Vector2 Direction { get { return direction; } }
}
