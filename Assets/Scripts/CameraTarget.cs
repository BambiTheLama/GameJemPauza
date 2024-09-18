using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (!mainCamera) 
            return;
        Vector3 pos = transform.position;
        pos.z = mainCamera.transform.position.z;
        mainCamera.transform.position = pos;
    }

}
