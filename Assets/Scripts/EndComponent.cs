using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndComponent : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (!player)
            return;
        Debug.Log("Wygra³eœ");
    }
}
