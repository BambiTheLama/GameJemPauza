using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpComponet : MonoBehaviour
{
    Rigidbody2D rb;
    public bool canJump = false;
    public bool canDoubleJump = false;
    public float jumpForce = 3.0f;
    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
    public void resetJump()
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
        return "";
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
            resetJump();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
            canJump = false;
    }

}
