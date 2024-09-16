using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractiveType
{
    Throw
}

public struct InteractiveData
{
    public InteractiveType type;
    public GameObject toThrow;
    public float power;
    public float timer;
    InteractiveData(InteractiveType type = InteractiveType.Throw, GameObject toThrow = null, float power = 100, float timer = 1.0f)
    {
        this.type = type;
        this.toThrow = toThrow;
        this.power = power;
        this.timer = timer;

    }
}
public class Interactive : MonoBehaviour
{
    public InteractiveData interactiveData;
    public float power = 1.0f;
    public GameObject target;
    public float timer = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        interactiveData.power = power;
        interactiveData.toThrow = target;
        interactiveData.timer = timer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
