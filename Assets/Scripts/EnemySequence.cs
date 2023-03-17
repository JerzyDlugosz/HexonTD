using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySequence : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> enemies = new List<GameObject>();
    [SerializeField]
    public GameObject firstEnemy;
}
