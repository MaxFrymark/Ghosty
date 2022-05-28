using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTrap : MonoBehaviour
{
    Vector2 pullPoint;
    
    public void SetPullpoint(float x, float y)
    {
        pullPoint = new Vector2(x, y);
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMover ghost = collision.gameObject.GetComponent<PlayerMover>();
        if (ghost != null)
        {
            if (!ghost.BeingPulled)
            {
                ghost.PullGhost(pullPoint);
            }
        }
    }
}
