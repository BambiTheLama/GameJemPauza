using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFromTo : MonoBehaviour
{
    public GameObject start = null;
    public GameObject end = null;
    public GameObject moveingObj = null;
    public float timer=1.0f;
    public float moveTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!start || !end || !moveingObj) 
            return;
        moveTimer -= Time.deltaTime;
        if(moveTimer<=0)
        {
            GameObject gm = start;
            start = end;
            end = gm;
            moveTimer = timer;
        }
        Vector3 moveDir = end.transform.position - start.transform.position;
        moveingObj.transform.position = start.transform.position + moveDir * (1.0f - (moveTimer / timer));
    }
    
        
    
}
