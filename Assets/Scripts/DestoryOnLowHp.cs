using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryOnLowHp : MonoBehaviour,DieTriggerI
{
    public void OnDie()
    {
        Destroy(gameObject);
    }

}
