using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    float m_Speed = 2f;
    Vector2 posChange;

    float maxX = 7.6f;
    [SerializeField] float maxY;

    
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
        //transform.Translate(posChange * m_Speed * Time.deltaTime);
        transform.position = UpdatePos();
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

    private Vector2 UpdatePos()
    {
        Vector2 pos = ((Vector2)transform.position + posChange * m_Speed * Time.deltaTime) ;
        Vector2 newPos = new Vector2(Mathf.Clamp(pos.x, -maxX, maxX), Mathf.Clamp(pos.y, -6.5f, maxY));
        return newPos;
    }
}
