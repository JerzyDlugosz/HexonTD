using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enumerables;

public class PlaceableTileData : MonoBehaviour
{
    public bool isTowerOnTile = false;
    public bool isHeroTowerOnTile = false;
    public bool isHeroTowerAdjacent = false;
    public bool alreadyCheckedForBonuses = false;

    public TowerType towerType = TowerType.Empty;

    public AdjacentSide adjacentSide;

    public GameObject adjacentTile;
    public GameObject tower;

    public int tileXCoord;
    public int tileZCoord;
}
