using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuyTowerLogic : MonoBehaviour
{
    [SerializeField]
    private GameObject gameController;
    //[SerializeField]
    //private AvailableTowers Towers;
    [SerializeField]
    private AvailableTowersNotSO Towers;
    [SerializeField]
    private bool clickedOnCard;
    [SerializeField]
    private TowerType towerType;

    GameObject tower;
    GameObject towerPrefab;
    GameObject towerPlaceholder;
    GameObject towerPlaceholderPrefab;

    TowerStats towerStats;

    public GameObject grayOutPanel;
    [SerializeField]
    private GameObject megaGrayOutPanel;
    public GameObject Marker;

    public Transform zoomedCardHolder;
    public Transform descriptionPanel;

    //int[,] tilesToCheck = new int[,] { {-1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };
    int[,] tilesToCheckForOddZ = new int[,] { { -1, -1 }, { 0, -1 }, { -1, 0 }, { 1, 0 }, { -1, 1 }, { 0, 1 } };
    int[,] tilesToCheckForEvenZ = new int[,] { { 0, -1 }, { 1, -1 }, { -1, 0 }, { 1, 0 }, { 0, 1 }, { 1, 1 } };
    int[] connectorIndexes = new int[] { 5, 4, 0, 3, 1, 2 };
    void Start()
    {
        Towers = this.GetComponentInParent<AvailableTowersNotSO>();
        if (Marker == null)
        {
            Marker = GameObject.Find("Marker");
        }
        towerStats = this.GetComponent<TowerStats>();
        towerPrefab = towerStats.towerPrefab;
        towerPlaceholderPrefab = towerStats.towerPlaceholderPrefab;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DraggingTower()
    {
        GameObject.Find("GameControllerObject").GetComponent<HighlightPlacement>().isDraggingTower = true;
        if (Marker.transform.childCount < 7 && !grayOutPanel.activeSelf)
        {
            towerPlaceholder = Instantiate(towerPlaceholderPrefab);
            towerPlaceholder.transform.position = Marker.transform.position;
            towerPlaceholder.transform.SetParent(Marker.transform);
        }
    }

    public void ReleaseTower()
    {
        GameObject.Find("GameControllerObject").GetComponent<HighlightPlacement>().isDraggingTower = false;
        if (!grayOutPanel.activeSelf)
        {
            //Debug.Log($"Releasing Card: {this.transform.GetChild(2).GetComponentInChildren<Text>().text}");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            //bool isPlacementAvaiable = Physics.Raycast(ray, out RaycastHit raycastHit, LayerMask.GetMask("Placeable Tile"));

            if (hits.Length > 0)
            {
                foreach (var hit in hits)
                {
                    if (hit.collider.CompareTag("Placeable Tile") && !hit.transform.GetComponentInParent<PlaceableTileData>().isTowerOnTile)
                    {
                        PlaceableTileData tileData = hit.transform.GetComponentInParent<PlaceableTileData>();
                        if(towerStats.towerType == TowerType.Hero)
                        {


                            megaGrayOutPanel.SetActive(true);
                            GetComponent<Button>().interactable = false;
                            Destroy(GetComponent<EventTrigger>());

                            tileData.isHeroTowerOnTile = true;

                            tileData.isTowerOnTile = true;
                            tileData.towerType = towerStats.towerType;
                            tower = GameObject.Instantiate(towerPrefab);
                            tower.transform.position = hit.transform.position + new Vector3(0f,0.15f,0f); //Marker.transform.position;
                            SetTowerStats(tower);

                            CheckAdjacentTiles(hit.transform.gameObject, tower);
                            tower.GetComponentInChildren<SphereCollider>().radius = tower.GetComponentInChildren<TowerStats>().range;
                            PayPrice();

                            tileData.tower = tower;
                        }
                        else
                        {
                            if(tileData.isHeroTowerAdjacent)
                            {
                                tileData.isTowerOnTile = true;
                                tileData.towerType = towerStats.towerType;
                                tower = GameObject.Instantiate(towerPrefab);
                                tower.transform.position = hit.transform.position + new Vector3(0f, 0.15f, 0f);
                                SetTowerStats(tower);
                                tower.GetComponentInChildren<SphereCollider>().radius = tower.GetComponentInChildren<TowerStats>().range;
                                PayPrice();

                                tileData.tower = tower;

                                GameObject adjacentTile = tileData.adjacentTile;

                                CheckAdjacentTiles(adjacentTile, adjacentTile.GetComponentInParent<PlaceableTileData>().tower);
                            }
                            else
                            {
                                tileData.isTowerOnTile = true;
                                tileData.towerType = towerStats.towerType;
                                tower = GameObject.Instantiate(towerPrefab);
                                tower.transform.position = hit.transform.position + new Vector3(0f, 0.15f, 0f);
                                SetTowerStats(tower);
                                tower.GetComponentInChildren<SphereCollider>().radius = tower.GetComponentInChildren<TowerStats>().range;
                                PayPrice();

                                tileData.tower = tower;
                            }
                        }
                        
                        Debug.Log($"Can Spawn Here: {hit.point}");

                        //PlaceTower(hit.transform.gameObject);

                    }
                }
            }
            if (Marker.transform.childCount > 6)
            {
                Destroy(Marker.transform.GetChild(6).gameObject);
            }
        }
    }

    void SetTowerStats(GameObject tower)
    {
        tower.GetComponentInChildren<TowerStats>().attackSpeed = towerStats.attackSpeed;
        tower.GetComponentInChildren<TowerStats>().damage = towerStats.damage;
        tower.GetComponentInChildren<TowerStats>().range = towerStats.range;
        tower.GetComponentInChildren<TowerStats>().towerDescription = towerStats.towerDescription;
        tower.GetComponentInChildren<TowerStats>().towerName = towerStats.towerName;
        tower.GetComponentInChildren<TowerStats>().towerType = towerStats.towerType;
        tower.GetComponentInChildren<TowerStats>().bulletSpeed = towerStats.bulletSpeed;
        tower.GetComponentInChildren<Targetting>().bulletSpeed = towerStats.bulletSpeed;
        tower.GetComponentInChildren<TowerStats>().bulletAOESize = towerStats.bulletAOESize;
        tower.GetComponentInChildren<Targetting>().bulletAOESize = towerStats.bulletAOESize;
    }

    void RemoveCard()
    {
        Destroy(this.GetComponent<Button>());
        Destroy(this.GetComponent<EventTrigger>());
        Destroy(this.gameObject, 1.0f);
    }

    void PayPrice()
    {
        if(gameController.GetComponent<BaseController>().RemoveResources(this.GetComponent<TowerStats>().price))
        {
            Debug.Log("Tower Bought");
        }
        Debug.Log("Cannot buy tower, not enough resources");
        //int moneyNeeded = int.Parse(avaibleResources.text) - this.GetComponent<TowerStats>().price;
        //avaibleResources.text = moneyNeeded.ToString();
        gameController.GetComponent<BaseController>().CheckIfTowersCanBeBought();
    }

    //public void CheckIfTowersCanBeBought()
    //{
    //    //foreach (TowerStats tower in Towers.towerStats)
    //    //{
    //    //    if(gameController.GetComponent<BaseStats>().CheckIfTowersCanBeBought(tower.price))
    //    //    {
    //    //        tower.GetComponent<Button>().interactable = true;
    //    //        tower.GetComponent<BuyTowerLogic>().grayOutPanel.SetActive(false);
    //    //    }
    //    //    else
    //    //    {
    //    //        tower.GetComponent<Button>().interactable = false;
    //    //        tower.GetComponent<BuyTowerLogic>().grayOutPanel.SetActive(true);
    //    //    }
    //    //}

    //    //Script below for Scriptable object AvailableTowers
    //    //foreach (TowerStats tower in Towers.GetTowerStats())
    //    //{
    //    //    if (gameController.GetComponent<BaseStats>().currentResources < tower.price)
    //    //    {
    //    //        tower.GetComponent<Button>().interactable = false;
    //    //        tower.GetComponent<BuyTowerLogic>().grayOutPanel.SetActive(true);
    //    //    }
    //    //    else
    //    //    {
    //    //        tower.GetComponent<Button>().interactable = true;
    //    //        tower.GetComponent<BuyTowerLogic>().grayOutPanel.SetActive(false);
    //    //    }
    //    //}
    //}

    public void OnItemHover()
    {
        SetDescription();
    }

    public void OnItemLeave()
    {
        NullDescription();
    }

    public void NullDescription()
    {
        if (descriptionPanel.GetComponent<DescriptionLogic>().isVisible)
        {
            //descriptionPanel.GetComponent<Animator>().Play("CloseDescription");
            descriptionPanel.GetComponent<DescriptionLogic>().NullDescription(this.transform.gameObject);
            //descriptionPanel.GetComponent<DescriptionLogic>().isVisible = false;
        }
    }

    public void SetDescription()
    {
        if (!descriptionPanel.GetComponent<DescriptionLogic>().isVisible)
        {
            //descriptionPanel.GetComponent<Animator>().Play("OpenDescription");
            descriptionPanel.GetComponent<DescriptionLogic>().SetDescription(this.transform.gameObject);
            //descriptionPanel.GetComponent<DescriptionLogic>().isVisible = true;
        }
    }

    //public void PlaceTower(GameObject hit)
    //{
    //    if (!hit.GetComponentInParent<PlaceableTileData>().isTowerOnTile)
    //    {
    //        hit.GetComponentInParent<PlaceableTileData>().isTowerOnTile = true;
    //        tower = GameObject.Instantiate(towerPrefab);
    //        tower.transform.position = Marker.transform.position;

            

    //        SetTowerStats(tower);
    //        tower.GetComponentInChildren<SphereCollider>().radius = tower.GetComponentInChildren<TowerStats>().range;
    //        PayPrice();
    //    }
    //}

    public void CheckAdjacentTiles(GameObject tile, GameObject tower)
    {
        PlaceableTileData tileData = tile.GetComponentInParent<PlaceableTileData>();
        GameObject[,] placeableTiles = GameObject.Find("GameControllerObject").GetComponent<TileList>().placeableTileArray;
        tower.GetComponentInChildren<HeroTowerStats>().ResetConnectedTowers();
        int[,] tileArray;
        if (tileData.tileZCoord % 2 == 0)
        {
            tileArray = tilesToCheckForEvenZ;
        }
        else
        {
            tileArray = tilesToCheckForOddZ;
        }

        for (int i = 0; i < 6; i++)
        {
            GameObject adjacentTile;

            
            adjacentTile = placeableTiles[tileData.tileXCoord + tileArray[i, 0], tileData.tileZCoord + tileArray[i, 1]];
            Debug.Log(adjacentTile);
            if (adjacentTile != null)
            {
                PlaceableTileData adjacentTileData = adjacentTile.GetComponentInParent<PlaceableTileData>();
                if(!adjacentTileData.isHeroTowerAdjacent)
                {
                    adjacentTileData.isHeroTowerAdjacent = true;
                    adjacentTileData.adjacentSide = (AdjacentSide)i;
                    adjacentTileData.adjacentTile = tile;
                }

                if (adjacentTileData.isTowerOnTile)
                {
                    if (adjacentTileData.towerType == TowerType.Basic && !adjacentTileData.alreadyCheckedForBonuses)
                    {
                        tower.GetComponentInChildren<HeroTowerStats>().connectedBasicTowers++;
                        tower.GetComponentInChildren<TowerStats>().damage *= tower.GetComponentInChildren<HeroTowerStats>().damageMultiplierModifier;
                        adjacentTileData.alreadyCheckedForBonuses = true;
                    }
                    if (adjacentTileData.towerType == TowerType.Tesla && !adjacentTileData.alreadyCheckedForBonuses)
                    {
                        tower.GetComponentInChildren<HeroTowerStats>().connectedTeslaTowers++;
                        tower.GetComponentInChildren<TowerStats>().attackSpeed *= tower.GetComponentInChildren<HeroTowerStats>().speedMultiplierModifier;
                        adjacentTileData.alreadyCheckedForBonuses = true;
                    }
                    if (adjacentTileData.towerType == TowerType.Railgun && !adjacentTileData.alreadyCheckedForBonuses)
                    {
                        tower.GetComponentInChildren<HeroTowerStats>().connectedRailgunTowers++;
                        tower.GetComponentInChildren<TowerStats>().range += tower.GetComponentInChildren<HeroTowerStats>().rangeAddModifier;
                        tower.GetComponentInChildren<SphereCollider>().radius += tower.GetComponentInChildren<HeroTowerStats>().rangeAddModifier;
                        adjacentTileData.alreadyCheckedForBonuses = true;
                    }
                    if (adjacentTileData.towerType == TowerType.Missle && !adjacentTileData.alreadyCheckedForBonuses)
                    {
                        tower.GetComponentInChildren<HeroTowerStats>().connectedMissleTowers++;
                        if (tower.GetComponentInChildren<HeroTowerStats>().connectedMissleTowers == 1)
                        {
                            tower.GetComponentInChildren<Targetting>().bulletPrefab = tower.GetComponentInChildren<HeroTowerStats>().misslePrefab;
                        }
                        tower.GetComponentInChildren<Targetting>().bulletAOESize += tower.GetComponentInChildren<HeroTowerStats>().missleRangeAddModifier;
                        adjacentTileData.alreadyCheckedForBonuses = true;
                    }

                    tile.transform.parent.GetChild(connectorIndexes[i]).gameObject.SetActive(true);
                }
            }
        } 
    }
}
