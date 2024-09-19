using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    Rigidbody2D rigidBody;
    public float timerMax = 1.0f;
    public float timer = 1.0f;
    public float stopTimerMax = 0.0f;
    public float stopTimer = 0.0f;
    public Vector2 dir= Vector2.zero;
    public float speed = 1.0f;
    public bool isMoving = false;
    GameObject player = null;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        timer = timerMax;
        stopTimer = stopTimerMax;
    }


    private void FixedUpdate()
    {
        if (!isMoving)
            return;
        if(stopTimer>0.0f)
        {
            stopTimer -= Time.deltaTime;
            return;
        }

        timer -= Time.deltaTime;
        Vector3 deltaPos= (Vector3)dir * speed * Time.deltaTime;
        if (player)
            player.transform.position += deltaPos;
        transform.position += deltaPos;

        if (timer <= 0)
        {
            speed = -speed;
            timer = timerMax;
            stopTimer = stopTimerMax;
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
        stopTimer = stopTimerMax;
    }
    public void stopMoving()
    {
        isMoving = false;
        stopTimer = 0.0f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;
        player = collision.gameObject;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player)
            player = null;
    }
}
