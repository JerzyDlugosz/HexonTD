using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetScript : MonoBehaviour
{
    [SerializeField]
    private LerpObject lerpObject;
    [SerializeField]
    private List<Transform> targets = new List<Transform>();
    void Start()
    {
        lerpObject.SetLerpTargetValue(targets[GameStateManager.instance.GetComponent<PlayerData>().currentWorld]);
        lerpObject.StartAnimation();
    }
}
