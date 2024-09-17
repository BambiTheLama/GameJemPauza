using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBody : MonoBehaviour
{
    // Start is called before the first frame update
    MovingPlatform movingPlatform;
    Rigidbody2D rigidBody;
    public float timer = 1.0f;
    public bool staticBody = false;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        movingPlatform = GetComponent<MovingPlatform>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!staticBody)
            return;
        timer -= Time.deltaTime;
        if(timer<=0)
        {
            rigidBody.bodyType = RigidbodyType2D.Static;
        }
    }

    public void Throw(Vector2 dir,float power,float timer)
    {
        if (movingPlatform)
            movingPlatform.stopMoving();
        rigidBody.bodyType = RigidbodyType2D.Dynamic;
        rigidBody.velocity = Vector2.zero;
        rigidBody.angularVelocity = 0;
        rigidBody.AddForce(dir * power, ForceMode2D.Impulse);
        this.timer = timer;
    }

}
