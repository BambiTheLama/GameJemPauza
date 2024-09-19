using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartComponent : MonoBehaviour
{
    
    void Start()
    {
        Player player = GameObject.FindFirstObjectByType<Player>();  
        if(player)
        {
            player.transform.position = transform.position;
        }
    }
}
