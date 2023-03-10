using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugButtonScript : MonoBehaviour
{
    public LerpObject lerpObject;
    [SerializeField]
    private List<Transform> targets = new List<Transform>();
    [SerializeField]
    private List<Transform> origins = new List<Transform>();

    /// <summary>
    /// Changes target and origin values of lerpObject
    /// </summary>
    /// <param name="worldNumber">1 - World1 to World2, 2 - World1 to World3, 3 - World2 to World3</param>
    public void changeLerpValues(int worldNumber)
    {
        lerpObject.SetLerpValues(origins[worldNumber - 1], targets[worldNumber - 1]);
        lerpObject.StartAnimation();
    }

    /// <summary>
    /// Changes target value of lerpObject. Origin value of lerpObject will stay as object transform
    /// </summary>
    /// <param name="worldNumber">1 - World1, 2 - World2, 3 - World3</param>
    public void changeLerpTargetValue(int worldNumber)
    {
        lerpObject.SetLerpTargetValue(targets[worldNumber - 1]);
        lerpObject.StartAnimation();
    }
}
