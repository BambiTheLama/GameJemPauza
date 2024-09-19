using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnLowHp : MonoBehaviour, DieTriggerI
{
    public void OnDie()
    {
        Destroy(gameObject);
    }

}
