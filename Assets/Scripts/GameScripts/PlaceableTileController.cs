using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableTileController : MonoBehaviour
{
    [SerializeField]
    private PlaceableTileData placeableTileData;

    private void Start()
    {
        placeableTileData.tileXCoord = GetComponentInParent<TileData>().Xcoord;
        placeableTileData.tileZCoord = GetComponentInParent<TileData>().Zcoord;
    }
}
