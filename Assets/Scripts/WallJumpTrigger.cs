using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpTrigger : MonoBehaviour
{
    public bool stickToTheWall = false;
    public Rigidbody2D rigidBody = null;
    public Player player = null;
    private void Awake()
    {
        rigidBody = GetComponentInParent<Rigidbody2D>();
        player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {            
            stickToTheWall = true;
            rigidBody.velocity = Vector2.zero;
            rigidBody.angularVelocity = 0;
            rigidBody.gravityScale = 0.0f;
            player.resetJump();
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            stickToTheWall = false;
            rigidBody.gravityScale = 1.0f;
        }
    }
}
