using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float damage = 1.0f;
    public float invicibleFrame = 1.0f;
    public float knoback = 1.0f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Hitable hit = collision.gameObject.GetComponent<Hitable>();
        if (!hit)
            return;
        Vector2 dir = collision.transform.position - transform.position;
        hit.DealDamage(invicibleFrame, damage, dir.normalized, knoback);
    }
}
