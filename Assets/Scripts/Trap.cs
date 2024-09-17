using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Hitable hit = collision.gameObject.GetComponent<Hitable>();
        if (!hit)
            return;

    }
}
