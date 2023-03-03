using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpObject : MonoBehaviour
{
    [SerializeField]
    private Transform objectPosition;
    [SerializeField]
    private Transform targetPosition;
    [SerializeField]
    private bool onStart;

    private bool isAnimationRunning;
    private float speed = 3f;
    private Vector3 targetDirection;


    private void Start()
    {
        if (onStart)
        {
            isAnimationRunning = true;
        }
    }

    private void Update()
    {
        if(isAnimationRunning)
        {
            //targetDirection = targetPosition.position - objectPosition.position;
            float singleStep = speed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, targetPosition.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetPosition.rotation, singleStep);

            if (Mathf.Abs(Vector3.Distance(transform.position, targetPosition.position)) < 0.01f)
            {
                transform.position = targetPosition.position;
                transform.rotation = targetPosition.rotation;
                isAnimationRunning = false;
                return;
            }
        }
    }
}
