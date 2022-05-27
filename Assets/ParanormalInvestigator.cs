using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParanormalInvestigator : Resident
{
    RoomList roomList;
    Waypoint target;
    


    private void Start()
    {
        destination = currentWayponint.transform.position;
        roomList = FindObjectOfType<RoomList>();
        target = roomList.GetLocation();
        moveSpeed = walkSpeed;
        
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
        ChangeFacing();
        CheckIfAtDestination();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Waypoint waypoint = collision.GetComponent<Waypoint>();
        if(waypoint != null && !waypoint.NormalPath)
        {
            if(target.transform.position.y > waypoint.transform.position.y)
            {
                nextWaypoint = waypoint.PreviousWaypoint;
            }
            else if(target.transform.position.y < waypoint.transform.position.y)
            {
                nextWaypoint = waypoint.NextWaypoint;
            }
            else
            {
                nextWaypoint = target;
            }
        }
    }
 

    private void StartInvestigating() 
    { 

    }

    public override void ResumeMovement()
    {
        moveSpeed = walkSpeed;
    }
}
