using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    public float forcePower = 100;
    public Vector2 dir;
    public int dotsNumber = 10;
    public float dotsSpaceing = 0.02f;
    public GameObject dotPrefab;
    public float gravity = 10.0f;
    public float mass = 1.0f;
    List<GameObject> dots = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        generateDots();
        showDots(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateThrowDotPos(GameObject throwObj)
    {
        float time = 0.0f;
        foreach (GameObject dot in dots)
        {
            Vector3 pos = throwObj.transform.position;
            pos.x += time * forcePower/(mass) * dir.x;
            pos.y += time * forcePower/(mass) * dir.y - (gravity * time * time) / 2.0f;
            dot.transform.position = pos;
            time += dotsSpaceing;
        }
    }

    public void updatePlatformDotPos(GameObject throwObj, float timerMax)
    {
        float dt = timerMax / (dots.Count - 1);
        Debug.Log(dt);
        Vector3 pos = throwObj.transform.position;
        foreach (GameObject dot in dots)
        {
            dot.transform.position = pos;
            pos.x += dt * forcePower * dir.x;
            pos.y += dt * forcePower * dir.y;

        }
    }
    void generateDots()
    {
        for (int i = 0; i < dotsNumber; i++)
        {
            GameObject dot = Instantiate(dotPrefab,null);
            dot.transform.parent = transform;
            dots.Add(dot);
        }
    }

    public void showDots(bool showDots)
    {
        foreach (GameObject dot in dots)
            dot.SetActive(showDots);
        
    }
}
