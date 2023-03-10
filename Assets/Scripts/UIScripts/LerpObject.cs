using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpObject : MonoBehaviour
{
    [SerializeField]
    private Transform originTransform;
    [SerializeField]
    private Transform targetTransform;
    [SerializeField]
    private bool onStart;
    [SerializeField]
    private bool startAnimOnCurrentPosition;

    private bool isAnimationRunning;
    private float speed = 3f;


    private void Start()
    {
        if (onStart)
        {
            StartAnimation();
        }
    }

    private void FixedUpdate()
    {
        if(isAnimationRunning)
        {
            //targetDirection = targetPosition.position - objectPosition.position;
            float singleStep = speed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, targetTransform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetTransform.rotation, singleStep);

            if (Mathf.Abs(Vector3.Distance(transform.position, targetTransform.position)) < 0.01f)
            {
                transform.position = targetTransform.position;
                transform.rotation = targetTransform.rotation;
                isAnimationRunning = false;
                startAnimOnCurrentPosition = false;
                return;
            }
        }
    }

    public void StartAnimation()
    {
        if (!startAnimOnCurrentPosition)
        {
            transform.position = originTransform.position;
        }
        isAnimationRunning = true;
    }

    public void SetLerpValues(Transform _objectTransform, Transform _targetTransform)
    {
        originTransform = _objectTransform;
        targetTransform = _targetTransform;
    }

    public void SetLerpTargetValue(Transform _targetTransform)
    {
        targetTransform = _targetTransform;
        startAnimOnCurrentPosition = true;
    }
}
