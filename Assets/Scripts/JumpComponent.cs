using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpComponent : MonoBehaviour
{
    Rigidbody2D rb;
    public bool canJump = false;
    public bool canDoubleJump = false;
    public float jumpForce = 3.0f;
    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    public void Jump()
    {
        if (!canJump && !canDoubleJump)
            return;
        if (!rb)
            return;

        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        if (canJump)
            canJump = false;
        else
            canDoubleJump = false;

    }
    public void ResetJump()
    {
        canJump = true;
        canDoubleJump = true;
    }
    public string GetJumpType()
    {
        if (canJump)
            return "Jump";
        if (canDoubleJump)
            return "DoubleJump";
        return "Idle";
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
            ResetJump();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
            canJump = false;
    }

}
