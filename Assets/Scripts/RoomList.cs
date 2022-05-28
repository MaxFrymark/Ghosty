using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomList : MonoBehaviour
{
    [SerializeField] private Waypoint[] rooms;

    
    

    public Waypoint GetLocation()
    {
        Waypoint target = rooms[Random.Range(0, rooms.Length)];
        return target;
    }
}
