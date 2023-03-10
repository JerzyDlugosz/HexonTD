using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemy")]
public class Enemy : ScriptableObject
{
    [SerializeField]
    public GameObject EnemyPrefab;
    [SerializeField]
    public int EnemyAmount;
    [SerializeField]
    public float SpawnSpeed;
    [SerializeField]
    public EnemyTypes enemyType;
}
