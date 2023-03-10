using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundSun : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private void FixedUpdate()
    {
        transform.Rotate(0f, 0f, speed * Time.deltaTime);
    }
}
