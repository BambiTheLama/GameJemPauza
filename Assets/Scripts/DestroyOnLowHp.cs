using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnLowHp : MonoBehaviour, DieTriggerI
{
    PauseController controller;
    private void Start()
    {
        controller = FindAnyObjectByType<PauseController>();
    }
    public void OnDie()
    {
        Destroy(gameObject);
        if (controller)
            controller.PauseGame();
    }

}
