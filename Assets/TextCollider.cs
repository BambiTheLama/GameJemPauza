using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextCollider : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
                gameObject.GetComponent<TextMeshPro>().enabled = true; 
        }
    }


}
