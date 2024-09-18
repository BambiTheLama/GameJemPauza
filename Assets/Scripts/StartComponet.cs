using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartComponet : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        Player player = GameObject.FindFirstObjectByType<Player>();  
        if(player)
        {
            player.transform.position = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
