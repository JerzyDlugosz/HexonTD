using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/AvailableEnemies")]
public class AvailableEnemies : ScriptableObject
{
    [SerializeField]
    public List<GameObject> EnemyPrefabs;
}
