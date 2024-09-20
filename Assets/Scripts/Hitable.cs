using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DieTriggerI
{
    public void OnDie();
}

public class Hitable : MonoBehaviour
{
    public float hp = 1.0f;
    float invicibleFrames=0.0f;
    Rigidbody2D rb;
    DieTriggerI dieTriggerI;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        dieTriggerI = GetComponent<DieTriggerI>();
    }
    private void Update()
    {
        invicibleFrames -= Time.deltaTime;
    }
    public void DealDamage(float invicibleFrames,float damage,Vector2 dir,float knockbackPower)
    {
        if (this.invicibleFrames > 0.0f)
            return;
        this.invicibleFrames = invicibleFrames;
        hp -= damage;
        if (hp <= 0.0f)
        {
            if (dieTriggerI != null)
                dieTriggerI.OnDie();
            else
                Destroy(gameObject);
        }
        if (rb)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            Debug.Log(dir);
            rb.AddForce(dir * knockbackPower, ForceMode2D.Impulse);
        }
        
    }
}
