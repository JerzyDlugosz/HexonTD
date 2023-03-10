using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWorldAround : MonoBehaviour
{
    [SerializeField]
    private float speed;
    void FixedUpdate()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0, Space.World);
    }
}
