using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ForceBodyI
{
    public void AddForceI(Vector2 dir, float power, float timer);
}

public interface PlatformI
{
    public void MoveTo(Vector2 dir, float speed, float timer);
}

public class Box : MonoBehaviour, ForceBodyI, PlatformI
{
    private Rigidbody2D rb;
    private float dynamicBodyTimer = 0.0f;
    private bool isMovingPlatform = false;
    private float moveTimerMax = 1.0f;
    private float moveTimer = 1.0f;
    private float speed = 1.0f;
    private Vector2 dir;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void AddForceI(Vector2 dir, float power, float timer)
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        rb.AddForce(dir * power, ForceMode2D.Impulse);
        dynamicBodyTimer = timer;
        isMovingPlatform = false;
    }

    public void MoveTo(Vector2 dir, float speed, float timer)
    {
        isMovingPlatform = true;
        rb.bodyType = RigidbodyType2D.Static;
        this.dir = dir;
        moveTimerMax = timer;
        moveTimer = timer;
        this.speed = speed;
    }

    void Update()
    {
        if (isMovingPlatform)
        {
            HandlePlatformMovement();
        }
        else
        {
            HandleDynamicBody();
        }
    }

    private void HandlePlatformMovement()
    {
        Vector3 pos = transform.position + speed * Time.deltaTime * (Vector3)dir;
        transform.position = pos;
        moveTimer -= Time.deltaTime;

        if (moveTimer <= 0.0f)
        {
            dir = -dir;
            moveTimer = moveTimerMax;
        }
    }

    private void HandleDynamicBody()
    {
        dynamicBodyTimer -= Time.deltaTime;

        if (dynamicBodyTimer <= 0.0f)
        {
            rb.bodyType = RigidbodyType2D.Static;
        }
    }
}
