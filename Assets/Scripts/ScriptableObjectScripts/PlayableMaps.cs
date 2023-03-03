using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/MapsSO")]
public class PlayableMaps : ScriptableObject
{
    [SerializeField]
    public List<GameObject> maps;
}
