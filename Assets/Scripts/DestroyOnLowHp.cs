using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnLowHp : MonoBehaviour, DieTriggerI
{
    LoseController controller;
    private void Start()
    {
        controller = FindAnyObjectByType<LoseController>();
    }
    public void OnDie()
    {
        Destroy(gameObject);
        if (controller)
            controller.SetVisible();
        AudioManager audioManager = FindObjectOfType<AudioManager>();

        if (audioManager != null)
        {
            audioManager.PlayFX(audioManager.lose);
        }
    }

}
