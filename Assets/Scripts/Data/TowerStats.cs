using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerStats : MonoBehaviour
{
    public float damage;
    public float range;
    public float attackSpeed;
    public float bulletSpeed;

    public float bulletAOESize;

    public GameObject towerPrefab;
    public GameObject towerPlaceholderPrefab;
    public GameObject towerTop;

    public string towerName;
    public string towerDescription;

    public TowerType towerType;

    public Sprite towerSprite;

    public int price;

    public bool isStationary;
    public bool isPassive;

    public float passiveStat;

    public Target target;
}

public enum Target
{
    First,
    Nearest
}