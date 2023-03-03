using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    //[SerializeField]
    //private AvailableTowers Towers;
    [SerializeField]
    private AvailableTowersNotSO towers;
    [SerializeField]
    private MapDecorations decorations;
    [SerializeField]
    private List<List<float>> modifiers;
    [SerializeField]
    private PlayableMaps playableMaps;
    [SerializeField]
    MapData mapData;
    [SerializeField]
    private Button startWaveButton;

    private GameObject[,] placeableTileArray;
    /// <summary>
    /// -1 = Tile with placeableTile
    /// 0 = Empty tile (void)
    /// 1 = Tile without decoration
    /// 2+ = Tile with decoration
    /// </summary>
    private int[,] decorationTilesDecorations;
    private GameObject[,] decorationTileArray;
    List<PlaceableTileData> placeableTiles;
    private MapTypes mapType;
    private List<Transform> pathTiles = new List<Transform>();

    [SerializeField]
    private GameObject pathArrowParent;
    [SerializeField]
    private GameObject arrowPrefab;


    [SerializeField]
    private TextMeshProUGUI DebugArrayText;

    void Start()
    {
        SetStartingResources();
        SetTowerStats();
        GenerateMap();
        SetEnemyNavMesh();
        UpdateTileList();
        DecorateMap();
        SetPathArrows();
        StartPathArrowsAnimation();
        AddListenerToStartWave();
        GetComponent<BaseController>().CheckIfTowersCanBeBought();
        ZoomInAnim();
    }

    private void SetTowerStats()
    {
        modifiers = new List<List<float>>()
        {
            GameStateManager.instance.GetComponent<PlayerData>().BasicTowerModifiers,
            GameStateManager.instance.GetComponent<PlayerData>().TeslaTowerModifiers,
            GameStateManager.instance.GetComponent<PlayerData>().RailgunTowerModifiers,
            GameStateManager.instance.GetComponent<PlayerData>().MissileTowerModifiers,
            GameStateManager.instance.GetComponent<PlayerData>().HeroTowerModifiers
        };

        foreach (TowerStats towerStats in towers.towerStats)
        {
            towerStats.damage += modifiers[(int)towerStats.towerType][0];
            towerStats.damage *= modifiers[(int)towerStats.towerType][1];

            towerStats.range += modifiers[(int)towerStats.towerType][2];
            towerStats.range *= modifiers[(int)towerStats.towerType][3];

            towerStats.attackSpeed += modifiers[(int)towerStats.towerType][4];
            towerStats.attackSpeed *= modifiers[(int)towerStats.towerType][5];

            Debug.Log("done some stuff");
        }

        //For Scriptable object AvailableTowers
        //foreach (TowerStats towerStats in Towers.GetTowerStats())
        //{
        //    towerStats.damage += Modifiers[(int)towerStats.towerType][0];
        //    towerStats.damage *= Modifiers[(int)towerStats.towerType][1];

        //    towerStats.range += Modifiers[(int)towerStats.towerType][2];
        //    towerStats.range *= Modifiers[(int)towerStats.towerType][3];

        //    towerStats.attackSpeed += Modifiers[(int)towerStats.towerType][4];
        //    towerStats.attackSpeed *= Modifiers[(int)towerStats.towerType][5];

        //    Debug.Log("done some stuff");
        //}
    }

    private void GenerateMap()
    {
        int mapNumber = Random.Range(0, playableMaps.maps.Count);
        mapData = Instantiate(playableMaps.maps[mapNumber]).GetComponent<MapData>();
        //NavMeshBuilder.BuildNavMeshData( mapData.gameObject
    }

    private void SetEnemyNavMesh()
    {
        this.GetComponent<SpawnWaves>().spawnPoint = mapData.spawnPoint;
        this.GetComponent<SpawnWaves>().destination = mapData.destination;
    }

    private void UpdateTileList()
    {
        UpdatePlaceableTileArray();
        UpdateDecorationTileList();
        UpdatePathTileList();
    }

    private void UpdatePlaceableTileArray()
    {
        placeableTiles = mapData.placeableTiles;
        placeableTileArray = new GameObject[mapData.xSize, mapData.zSize];

        for (int i = 0; i < mapData.xSize; i++)
        {
            for (int j = 0; j < mapData.zSize; j++)
            {
                placeableTileArray[i, j] = null;
            }
        }

        for (int i = 0; i < placeableTiles.Count; i++)
        {
            //placeableTiles[tiles[i].tileXCoord, tiles[i].tileZCoord] = tiles[i].gameObject;
            placeableTileArray[placeableTiles[i].GetComponentInParent<TileData>().Xcoord, placeableTiles[i].GetComponentInParent<TileData>().Zcoord] = placeableTiles[i].gameObject;

        }

        this.GetComponent<TileList>().placeableTileArray = placeableTileArray;
        this.GetComponent<TileList>().tiles = mapData.placeableTiles;

        for (int i = 0; i < mapData.xSize; i++)
        {
            for (int j = 0; j < mapData.zSize; j++)
            {
                if (placeableTileArray[i, j] != null)
                {
                    DebugArrayText.text += "1 ";
                }
                else
                {
                    DebugArrayText.text +="0 ";
                }
                Debug.Log(placeableTileArray[i, j]);

            }
            DebugArrayText.text += "\n";
        }
    }

    private void UpdateDecorationTileList()
    {
        decorationTilesDecorations = new int[mapData.xSize, mapData.zSize];
        decorationTileArray = new GameObject[mapData.xSize, mapData.zSize];

        for (int i = 0; i < mapData.xSize; i++)
        {
            for (int j = 0; j < mapData.zSize; j++)
            {
                decorationTilesDecorations[i, j] = 0;
                decorationTileArray[i, j] = null;
            }
        }

        mapType = GameStateManager.instance.GetComponent<PathData>().mapType;

        int ammount = 1;
        int rand;

        switch (mapType)
        {
            case MapTypes.Plains:
                ammount = decorations.plainsDecoration.Count;
                break;
            case MapTypes.Forest:
                ammount = decorations.forestDecorations.Count;
                break;
            case MapTypes.Taiga:
                ammount = decorations.taigaDecorations.Count;
                break;
            case MapTypes.Tundra:
                ammount = decorations.tundraDecorations.Count;
                break;
            case MapTypes.Desert:
                ammount = decorations.desertDecorations.Count;
                break;
            case MapTypes.Fungal:
                ammount = decorations.fungalDecorations.Count;
                break;
        }

        foreach(TileData tile in mapData.decorationTilesParent.transform.GetComponentsInChildren<TileData>())
        {
            rand = Random.Range(1, ammount + 2);
            decorationTilesDecorations[tile.Xcoord, tile.Zcoord] = rand;
            decorationTileArray[tile.Xcoord, tile.Zcoord] = tile.gameObject;
        }

        foreach (PlaceableTileData placeableTile in placeableTiles)
        {
            decorationTilesDecorations[placeableTile.GetComponentInParent<TileData>().Xcoord, placeableTile.GetComponentInParent<TileData>().Zcoord] = -1;
        }
    }

    private void UpdatePathTileList()
    {
        foreach(Transform tile in mapData.PathTilesParent.transform)
        {
            pathTiles.Add(tile);
        }
    }

    private void SetStartingResources()
    {
        this.GetComponent<BaseController>().SetResources(GameStateManager.instance.GetComponent<PlayerData>().startingMaterials);
    }

    private void AddListenerToStartWave()
    {
        startWaveButton.onClick.AddListener(RemovePathArrows);
    }

    private void RemovePathArrows()
    {
        Destroy(pathArrowParent.transform.parent.gameObject);
    }

    private void DecorateMap()
    {
        for (int i = 0; i < decorationTilesDecorations.Length; i++)
        {
            int decorNumber = decorationTilesDecorations[i % mapData.xSize, i / mapData.zSize] - 2;
            if(decorNumber < -1)
            {
                continue;
            }

            switch (mapType)
            {
                case MapTypes.Plains:
                    decorationTileArray[i % mapData.xSize, i / mapData.zSize].transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = decorations.plainsTileMaterial;
                    if (decorNumber > 0)
                    {
                        Instantiate(decorations.plainsDecoration[decorNumber], decorationTileArray[i % mapData.xSize, i / mapData.zSize].transform);
                    }
                    break;
                case MapTypes.Forest:
                    decorationTileArray[i % mapData.xSize, i / mapData.zSize].transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = decorations.forestTileMaterial;
                    if (decorNumber > 0)
                    {
                        Instantiate(decorations.forestDecorations[decorNumber], decorationTileArray[i % mapData.xSize, i / mapData.zSize].transform);
                    }
                    break;
                case MapTypes.Taiga:
                    decorationTileArray[i % mapData.xSize, i / mapData.zSize].transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = decorations.taigaTileMaterial;
                    if (decorNumber > 0)
                    {
                        Instantiate(decorations.taigaDecorations[decorNumber], decorationTileArray[i % mapData.xSize, i / mapData.zSize].transform);
                    }
                    break;
                case MapTypes.Tundra:
                    decorationTileArray[i % mapData.xSize, i / mapData.zSize].transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = decorations.tundraTileMaterial;
                    if (decorNumber > 0)
                    {
                        Instantiate(decorations.tundraDecorations[decorNumber], decorationTileArray[i % mapData.xSize, i / mapData.zSize].transform);
                    }
                    break;
                case MapTypes.Desert:
                    decorationTileArray[i % mapData.xSize, i / mapData.zSize].transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = decorations.desertTileMaterial;
                    if (decorNumber > 0)
                    {
                        Instantiate(decorations.desertDecorations[decorNumber], decorationTileArray[i % mapData.xSize, i / mapData.zSize].transform);
                    }
                    break;
                case MapTypes.Fungal:
                    decorationTileArray[i % mapData.xSize, i / mapData.zSize].transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = decorations.fungalTileMaterial;
                    if (decorNumber > 0)
                    {
                        Instantiate(decorations.fungalDecorations[decorNumber], decorationTileArray[i % mapData.xSize, i / mapData.zSize].transform);
                    }
                    break;
            }

            //if (mapType == MapTypes.Plains)
            //{
            //    decorationTileArray[i % mapData.xSize, i / mapData.zSize].transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = decorations.plainsTileMaterial;
            //    if(decorNumber > 0)
            //    {
            //        Instantiate(decorations.plainsDecoration[decorNumber], decorationTileArray[i % mapData.xSize, i / mapData.zSize].transform);
            //    }
            //}
            //if (mapType == MapTypes.Taiga)
            //{
            //    decorationTileArray[i % mapData.xSize, i / mapData.zSize].transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = decorations.taigaTileMaterial;
            //    if (decorNumber > 0)
            //    {
            //        Instantiate(decorations.taigaDecorations[decorNumber], decorationTileArray[i % mapData.xSize, i / mapData.zSize].transform);
            //    }
            //}
            //if (mapType == MapTypes.Fungal)
            //{
            //    if (decorNumber > 0)
            //    {
            //        Instantiate(decorations.fungalDecorations[decorNumber], decorationTileArray[i % mapData.xSize, i / mapData.zSize].transform);
            //    }
            //}
        }
    }

    private void SetPathArrows()
    {
        GameObject arrow;
        List<PathArrowScript> tempList = new List<PathArrowScript>();

        for (int i = 0; i < pathTiles.Count; i++)
        {
            arrow = Instantiate(arrowPrefab, pathTiles[i]);
            arrow.GetComponent<PathArrowScript>().pathArrowController = pathArrowParent.GetComponent<PathArrowController>();
            tempList.Add(arrow.GetComponent<PathArrowScript>());
            arrow.transform.SetParent(pathArrowParent.transform);
            if (i >= pathTiles.Count - 1)
            {
                arrow.transform.position += new Vector3(0f, 0.1f, 0f);
                arrow.transform.rotation = Quaternion.Euler(90f, arrow.transform.rotation.y, arrow.transform.rotation.z);
                continue;
            }
            arrow.transform.LookAt(pathTiles[i + 1]);
            arrow.transform.position += new Vector3(0f, 0.1f, 0f);
        }

        //This Now Works so yay i guess...
        for (int i = 0; i < tempList.Count - 1; i++)
        {
            tempList[i].transform.LookAt(tempList[i + 1].transform);
            tempList[i].transform.rotation = Quaternion.Euler(90f, tempList[i].transform.rotation.eulerAngles.y, tempList[i].transform.rotation.eulerAngles.z);
        }
        tempList[tempList.Count - 1].transform.LookAt(mapData.destination.transform);
        tempList[tempList.Count - 1].transform.rotation = Quaternion.Euler(90f, tempList[tempList.Count - 1].transform.rotation.eulerAngles.y, tempList[tempList.Count - 1].transform.rotation.eulerAngles.z);


        pathArrowParent.GetComponent<PathArrowController>().arrowPaths = tempList;
    }

    private void StartPathArrowsAnimation()
    {
        pathArrowParent.GetComponent<PathArrowController>().StartPathAnimation();
    }

    private float zoomAnimationSpeed = 1f;
    [SerializeField]
    private Image fadeToBlackImage;
    private bool isAnimationRunning;
    private int i = 0;

    private void ZoomInAnim()
    {
        Camera.main.GetComponent<CameraControl>().enabled = false;
        fadeToBlackImage.enabled = true;
        isAnimationRunning = true;
    }

    private void Update()
    {
        if (isAnimationRunning)
        {
            if(i < 100)
            {
                fadeToBlackImage.color = new Color(fadeToBlackImage.color.r, fadeToBlackImage.color.g, fadeToBlackImage.color.b, (float)(100 - i) / 100);
            }
            i++;
            if (i == 100)
            {
                fadeToBlackImage.enabled = false;
                Debug.Log("ay!");
            }

            if(i < 300) 
            {
                Camera.main.orthographicSize = 20f - i/30f;
            }

            //if (i < 300 && i > 250)
            //{
            //    Camera.main.orthographicSize = 20f - i / 30f;
            //}

            if (i == 300)
            {
                Camera.main.GetComponent<CameraControl>().enabled = true;
                isAnimationRunning = false;
            }    
        }
    }
}
