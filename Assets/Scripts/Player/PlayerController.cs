using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Controls_Listener input;
    [SerializeField]
    private Rigidbody2D body;
    [SerializeField]
    private float speed;

    private float moveDir;

    private bool isDropping;

    private void Start()
    {
        input.PlayerMoveEvent += HandleMove;
        input.DropEvent += HandleDrop;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void HandleDrop()
    {
        isDropping = true;
    }
    private void HandleMove(float dir)
    {
        moveDir = dir;
    }
    private void Move()
    {
        if (moveDir == 0.0f)
            return;
        if (body != null)
        {
            body.position += new Vector2(moveDir, 0.0f) * (speed * Time.deltaTime);
        }
        else
        {
            Debug.Log("Moving with no body!");
            //Rotate without body
            transform.Rotate(new Vector3(0, 0, 1), moveDir * speed * Time.deltaTime);
        }
    }
}
