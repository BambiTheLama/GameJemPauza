using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    Rigidbody2D rigidBody;
    public float timerMax = 1.0f;
    public float timer = 1.0f;
    public Vector2 dir= Vector2.zero;
    public float speed = 1.0f;
    public bool isMoving = false;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }
    void Update()
    {
        if (!isMoving)
            return;

        timer -= Time.deltaTime;
        transform.position += (Vector3)dir * speed * Time.deltaTime;
        if (timer <= 0)
        {
            speed = -speed;
            timer = timerMax;
        }
    }
    
    public void Move(Vector2 dir,float speed,float timer)
    {
        this.dir = dir;
        this.speed = speed;
        this.timer = timer;
        timerMax = timer;
        rigidBody.bodyType = RigidbodyType2D.Static;
        isMoving = true;
    }
    public void stopMoving()
    {
        isMoving = false;
    }
}
