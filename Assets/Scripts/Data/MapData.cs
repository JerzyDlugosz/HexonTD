using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{
    public List<PlaceableTileData> placeableTiles;
    public GameObject spawnPoint;
    public GameObject destination;
    public int xSize;
    public int zSize;
    public GameObject pathArrowCanvas;
    public GameObject decorationTilesParent;
    public GameObject PathTilesParent;
    public List<Enemy> EnemyTypeForWave;
}
