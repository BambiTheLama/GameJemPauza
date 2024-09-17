using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    // Start is called before the first frame update
    Camera camera;
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!camera) 
            return;
        Vector3 pos = transform.position;
        pos.z = camera.transform.position.z;
        camera.transform.position = pos;
    }

}
