using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractiveType
{
    Throw,
    MovePlatform
}

public struct InteractiveData
{
    public InteractiveType type;
    public GameObject toThrow;
    public float power;
    public float timer;

    public InteractiveData(InteractiveType type, GameObject toThrow, float power, float timer)
    {
        this.type = type;
        this.toThrow = toThrow;
        this.power = power;
        this.timer = timer;
    }
}

public class Interactive : MonoBehaviour
{
    public float power = 1.0f;
    public GameObject target;
    public float timer = 1.0f;
    public InteractiveType type;
    bool used = false;
    float endTimer = 0.4f;
    float maxEndTimer = 0.4f;
    float scale = 1.0f;
    private void Update()
    {
        if (!used)
            return;
        endTimer -= Time.deltaTime;
        float procent = Mathf.Pow(endTimer / maxEndTimer, 4);
        transform.localScale = Vector3.one * scale * procent;
        if (endTimer < 0.0f)
            Destroy(gameObject);
    }
    public InteractiveData GetData()
    {
        InteractiveData id = new(type, target, power, timer);
        return id;
    }

    public void Interact()
    {
        used = true;
        endTimer = maxEndTimer;
        scale = transform.localScale.x;
    }

    public bool CanInteract()
    { 
        return !used; 
    }
}
