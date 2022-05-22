using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    float m_Speed = 4f;
    Vector2 posChange;

    
    public void MovePlayer(Vector2 playerMove)
    {
        posChange = playerMove;
    }

    private void Update()
    {
        MovePlayer();
        ChooseFacing();
    }

    private void MovePlayer()
    {
        transform.Translate(posChange * m_Speed * Time.deltaTime);
    }

    private void ChooseFacing()
    {
        if(posChange.x > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if(posChange.x < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }
}
