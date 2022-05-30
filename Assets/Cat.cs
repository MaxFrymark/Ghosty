using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] protected Waypoint currentWayponint;
    [SerializeField] protected Waypoint nextWaypoint;
    [SerializeField] protected Vector2 destination;
    [SerializeField] protected float speed;
    [SerializeField] protected AudioSource audioSource;


    protected virtual void Start()
    {
        destination = SelectDestiantion();
    }

    protected virtual void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        ChangeFacing();
        CheckIfAtDestination();
    }

    protected Vector2 SelectDestiantion()
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

    protected void OnTriggerEnter2D(Collider2D collision)
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

    protected virtual void CheckIfAtDestination()
    {
        if ((Vector2)transform.position == destination)
        {
            currentWayponint = nextWaypoint;
            destination = SelectDestiantion();
        }
    }

    public void Meow()
    {
        audioSource.Play();
    }
}
