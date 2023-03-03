using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileList : MonoBehaviour
{
    public List<PlaceableTileData> tiles;    //A list of all placeable tiles
    public GameObject[,] placeableTileArray; //An array containing x and y coordinate of all placeable tiles
}
