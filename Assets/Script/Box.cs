using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ForceBodyI
{
    public void addForce(Vector2 dir, float power, float timer);
}
public class Box : MonoBehaviour, ForceBodyI
{
    Rigidbody2D rb;
    float dynamicBodyTimer = 0.0f;
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

    }

    // Update is called once per frame
    void Update()
    {
        dynamicBodyTimer -= Time.deltaTime;
        if(dynamicBodyTimer<=0.0f)
        {
            rb.bodyType = RigidbodyType2D.Static;
        }
    }
}
