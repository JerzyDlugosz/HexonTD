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
        if(GameStateManager.instance.GetComponent<PlayerData>().currentWorld < 3)
        {
            lerpObject.SetLerpTargetValue(targets[GameStateManager.instance.GetComponent<PlayerData>().currentWorld]);
            lerpObject.StartAnimation();
        }
    }
}
