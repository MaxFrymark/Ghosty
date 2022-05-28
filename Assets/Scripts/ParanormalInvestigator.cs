using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParanormalInvestigator : Resident
{
    RoomList roomList;
    [SerializeField] Waypoint target;
    [SerializeField] GameObject aura;
    List<GameObject> auraList = new List<GameObject>();
    


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
        SelectAnimation();
        CheckIfAtDestination();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Waypoint waypoint = collision.GetComponent<Waypoint>();
        if(waypoint != null && !waypoint.NormalPath)
        {
            if(target.transform.position.y > waypoint.transform.position.y || target.transform.position == waypoint.transform.position)
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

    protected override void CheckIfAtDestination()
    {
        
        
        if ((Vector2)transform.position == destination)
        {
            if(destination == (Vector2)target.transform.position)
            {
                StartInvestigating();
            }
            
            currentWayponint = nextWaypoint;
            destination = currentWayponint.transform.position;
        }
        
    }

    private void SelectAnimation()
    {
        if(moveSpeed == 0)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("isInvestigating", true);
        }

        else if(moveSpeed == walkSpeed)
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isInvestigating", false);
            animator.SetBool("isWalking", true);
        }

        else if(moveSpeed == runSpeed)
        {
            animator.SetBool("isInvestigating", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", true);
        }
    }
 

    private void StartInvestigating() 
    {
        GenerateAura();
        PauseMovement();
        StartCoroutine(Investigate());
    }

    private IEnumerator Investigate()
    {
        yield return new WaitForSeconds(5);
        target = roomList.GetLocation();
        DestroyAura();
        ResumeMovement();
    }

    private void GenerateAura()
    {
        for(int i = 0; i <= 1; i++)
        {
            for(float j = -2.5f; j <= 2.5; j += 1)
            {
                GameObject newAura = Instantiate(aura, new Vector2(j + transform.position.x, i + transform.position.y), transform.rotation, transform);
                newAura.GetComponent<GhostTrap>().SetPullpoint(transform.position.x + transform.localScale.x, transform.position.y + 1);
                auraList.Add(newAura);
            }
        }
    }

    private void DestroyAura()
    {
        for(int i = auraList.Count - 1; i >= 0; i--)
        {
            Destroy(auraList[i]);
        }
        auraList.Clear();
    }

    public override void ResumeMovement()
    {
        moveSpeed = walkSpeed;
    }
}
