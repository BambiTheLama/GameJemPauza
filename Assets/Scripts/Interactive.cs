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

    public InteractiveData GetData()
    {
        InteractiveData id = new(type, target, power, timer);
        return id;
    }
}
