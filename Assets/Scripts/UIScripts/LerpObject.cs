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

    private Vector3 previousFramePosition;
    private Vector3 currentFramePosition;


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
            currentFramePosition = Vector3.Lerp(transform.position, targetTransform.position, speed * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, targetTransform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetTransform.rotation, singleStep);

            Debug.Log($"{currentFramePosition}, {previousFramePosition}, Distance: {Mathf.Abs(Vector3.Distance(previousFramePosition, currentFramePosition))}");

            // this code is making sure that if the lerp has slowed down enough, it will cancel lerping and force the position
            if (Mathf.Abs(Vector3.Distance(previousFramePosition, currentFramePosition)) < 0.01f)
            {
                Debug.Log("Lerp has slowed down enough");
                transform.position = targetTransform.position;
                transform.rotation = targetTransform.rotation;
                isAnimationRunning = false;
                startAnimOnCurrentPosition = false;
                return;
            }

            // this code is making sure that if the lerp is close to finishing, it will cancel lerping and force the position
            if (Mathf.Abs(Vector3.Distance(transform.position, targetTransform.position)) < 0.01f) 
            {
                transform.position = targetTransform.position;
                transform.rotation = targetTransform.rotation;
                isAnimationRunning = false;
                startAnimOnCurrentPosition = false;
                return;
            }

            previousFramePosition = currentFramePosition;
        }
    }

    public void StartAnimation()
    {
        if (!startAnimOnCurrentPosition)
        {
            transform.position = originTransform.position;
        }
        transform.SetParent(targetTransform.parent);
        currentFramePosition = Vector3.Lerp(transform.position, targetTransform.position, speed * Time.deltaTime);
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
