using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] protected Waypoint currentWayponint;
    [SerializeField] protected Waypoint nextWaypoint;
    [SerializeField] protected Vector2 destination;
    [SerializeField] float speed;


    private void Start()
    {
        destination = SelectDestiantion();
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        ChangeFacing();
        CheckIfAtDestination();
    }

    private Vector2 SelectDestiantion()
    {
        return new Vector2(currentWayponint.transform.position.x, transform.position.y);
    }

    public void ChangeFacing()
    {
        if (destination.x >= transform.position.x)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Waypoint waypoint = collision.GetComponent<Waypoint>();
        if(waypoint != null)
        {
            if (!waypoint.AgentOnly)
            {
                nextWaypoint = waypoint.NextWaypoint;
            }
        }
    }

    protected void CheckIfAtDestination()
    {
        if ((Vector2)transform.position == destination)
        {
            currentWayponint = nextWaypoint;
            destination = SelectDestiantion();
        }
    }
}
