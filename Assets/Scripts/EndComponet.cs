using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndComponet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (!player)
            return;
        Debug.Log("Wygra³eœ");
    }
}
