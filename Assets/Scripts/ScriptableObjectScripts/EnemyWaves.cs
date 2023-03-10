using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/EnemyWaves")]
public class EnemyWaves : ScriptableObject
{
    [SerializeField]
    public int normalEnemyAmmountEasy;
    [SerializeField]
    public int normalEnemyAmmountNormal;
    [SerializeField]
    public int normalEnemyAmmountHard;

    [SerializeField]
    public int armoredEnemyAmmountEasy;
    [SerializeField]
    public int armoredEnemyAmmountNormal;
    [SerializeField]
    public int armoredEnemyAmmountHard;

    [SerializeField]
    public int spawnerEnemyAmmountEasy;
    [SerializeField]
    public int spawnerEnemyAmmountNormal;
    [SerializeField]
    public int spawnerEnemyAmmountHard;

    //[SerializeField]
    //List<GameObject> Easy1 = new List<GameObject>();
    //[SerializeField]
    //List<GameObject> Easy2 = new List<GameObject>();
    //[SerializeField]
    //List<GameObject> Easy3 = new List<GameObject>();
    //[SerializeField]
    //List<GameObject> Norma1 = new List<GameObject>();
    //[SerializeField]
    //List<GameObject> Normal1 = new List<GameObject>();
    //[SerializeField]
    //List<GameObject> Normal2 = new List<GameObject>();
    //[SerializeField]
    //List<GameObject> Normal3 = new List<GameObject>();
    //[SerializeField]
    //List<GameObject> Hard1 = new List<GameObject>();
    //[SerializeField]
    //List<GameObject> Hard2 = new List<GameObject>();
    //[SerializeField]
    //List<GameObject> Hard3 = new List<GameObject>();
}