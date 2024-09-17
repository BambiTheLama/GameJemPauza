using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    public float forcePower = 100;
    public Vector2 dir;
    public int dotsNumber = 10;
    public float dotsSpacing = 0.02f;
    public GameObject dotPrefab;
    public float gravity = 10.0f;
    public float mass = 1.0f;
    private readonly List<GameObject> dots = new();
    
    void Start()
    {
        GenerateDots();
        ShowDots(false);
    }


    public void UpdateThrowDotPos(GameObject throwObj)
    {
        float time = 0.0f;
        foreach (GameObject dot in dots)
        {
            Vector3 pos = throwObj.transform.position;
            pos.x += time * forcePower/(mass) * dir.x;
            pos.y += time * forcePower/(mass) * dir.y - (gravity * time * time) / 2.0f;
            dot.transform.position = pos;
            time += dotsSpacing;
        }
    }

    public void UpdatePlatformDotPos(GameObject throwObj, float timerMax)
    {
        float dt = timerMax / (dots.Count - 1);
        Vector3 pos = throwObj.transform.position;
        foreach (GameObject dot in dots)
        {
            dot.transform.position = pos;
            pos.x += dt * forcePower * dir.x;
            pos.y += dt * forcePower * dir.y;

        }
    }
    void GenerateDots()
    {
        for (int i = 0; i < dotsNumber; i++)
        {
            GameObject dot = Instantiate(dotPrefab, null);
            dot.transform.parent = transform;
            dots.Add(dot);
        }
    }

    public void ShowDots(bool showDots)
    {
        foreach (GameObject dot in dots)
        {
            dot.SetActive(showDots);
        }
    }
}
