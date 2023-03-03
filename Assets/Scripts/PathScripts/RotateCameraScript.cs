using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraScript : MonoBehaviour
{
    [SerializeField]
    private float multiplier;
    [SerializeField]
    private Transform directionalLightTransform;

    private Vector3 cameraAngle;
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //cameraAngle = new Vector3(Input.GetAxis("Mouse Y") * multiplier, -Input.GetAxis("Mouse X") * multiplier, 0);
            cameraAngle = new Vector3(Input.GetAxis("Mouse Y") * multiplier, 0, Input.GetAxis("Mouse X") * multiplier);
            transform.Rotate(cameraAngle, Space.Self);
            //directionalLightTransform.Rotate(cameraAngle, Space.Self);
        }
    }
}
