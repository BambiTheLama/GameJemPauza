using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ForceBodyI
{
    public void addForce(Vector2 dir, float power, float timer);
}

public interface PlatformI
{
    public void moveTo(Vector2 dir, float speed, float timer);
}

public class Box : MonoBehaviour, ForceBodyI, PlatformI
{
    Rigidbody2D rb;
    float dynamicBodyTimer = 0.0f;
    bool IsMoveingPlatform = false;
    float moveTimerMax = 1.0f;
    float moveTimer = 1.0f;
    float speed = 1.0f;
    Vector2 dir;
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
    }

    public void addForce(Vector2 dir,float power,float timer)
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        rb.AddForce(dir * power, ForceMode2D.Impulse);
        dynamicBodyTimer = timer;
        IsMoveingPlatform = false;

    }

    public void moveTo(Vector2 dir, float speed, float timer)
    {
        IsMoveingPlatform = true;
        rb.bodyType = RigidbodyType2D.Static;
        this.dir = dir;
        moveTimerMax = timer;
        moveTimer = timer;
        this.speed = speed;
        IsMoveingPlatform = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsMoveingPlatform)
        {
            Vector3 pos = transform.position + (Vector3)dir * Time.deltaTime * speed;
            transform.position = pos;
            moveTimer -= Time.deltaTime;
            if(moveTimer<=0.0f)
            {
                dir = -dir;
                moveTimer = moveTimerMax;
            }

        }
        else
        {
            dynamicBodyTimer -= Time.deltaTime;
            if (dynamicBodyTimer <= 0.0f)
            {
                rb.bodyType = RigidbodyType2D.Static;
            }
        }

    }
}
