using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnWaves : MonoBehaviour
{
    public AvailableEnemies availableEnemies;

    public List<GameObject> enemiesOnScreen = new List<GameObject>();

    [SerializeField]
    private GameObject cardTemplate;
    [SerializeField]
    private AvailableCards availableCards;

    public GameObject spawnPoint;
    public GameObject destination;
    public GameObject button;
    public GameObject winScreen;

    public Transform cardsHolder;

    public int waveNumber = 0;
    public int maxCards = 5;
    public int enemyNumber = 0;
    private int cardDrawAmmount = 1;
    private int cardDrawSpeed = 5;
    private int enemyIDNumber = 0;

    public float spawnSpeed;

    public bool lastEnemySpawned = false;

    private GameStateManager gameStateManager;

    private MapData mapData;
    private float difficultyModifier;

    // Start is called before the first frame update
    void Start()
    {
        gameStateManager = GameObject.Find("PresistentGameController").GetComponent<GameStateManager>();
    }

    private bool onGameEnd = true; //bool so the if in runs works only once

    // Update is called once per frame
    void Update()
    {            
        if ((enemyNumber <= 0 && lastEnemySpawned) && (enemiesOnScreen.Count == 0 && onGameEnd))
        {
            winScreen.SetActive(true);
            StartCoroutine(PlayWinAnimation());
            onGameEnd = false;
        }
    }

    public void OnEnemyKilled()
    {
        enemyNumber--;
        //if ((enemyNumber == 0 && lastEnemySpawned) && enemiesOnScreen.Count == 0)
        //{
        //    winScreen.SetActive(true);
        //    StartCoroutine(PlayWinAnimation());
        //}
    }

    public void StartWave()
    {
        mapData = GetComponent<StartGame>().mapData;
        if (gameStateManager.GetComponent<PathData>().pathDifficulty == PathDifficulty.Easy)
        {
            difficultyModifier = 1f;
        }
        if (gameStateManager.GetComponent<PathData>().pathDifficulty == PathDifficulty.Normal)
        {
            difficultyModifier = 1.5f;
        }
        if (gameStateManager.GetComponent<PathData>().pathDifficulty == PathDifficulty.Hard)
        {
            difficultyModifier = 2f;
        }
        SetEnemyNumber();
        StartCoroutine(spawnUnit());
        StartCoroutine(StartDrawingCards());
    }

    IEnumerator spawnUnit()
    {
        GameObject Enemy;
        Debug.Log(mapData.EnemyTypeForWave.Count);
        for (int i = 0; i < mapData.EnemyTypeForWave.Count; i++)
        {
            Debug.Log(mapData.EnemyTypeForWave[i].EnemyAmount);
            int enemyAmount = (int)(mapData.EnemyTypeForWave[i].EnemyAmount * difficultyModifier);
            for (int j = 0; j < enemyAmount; j++)
            {
                yield return new WaitForSeconds(spawnSpeed);
                Debug.Log($"Enemy! ID: {enemyIDNumber} / wave number: {i + 1} / Max Enemies: {enemyAmount}");
                Enemy = GameObject.Instantiate(mapData.EnemyTypeForWave[i].EnemyPrefab, spawnPoint.transform.position, Quaternion.identity);
                Enemy.GetComponent<FollowNavMesh>().start = spawnPoint.transform;
                Enemy.GetComponent<FollowNavMesh>().finish = destination.transform;
                enemiesOnScreen.Add(Enemy);
                ModifyStats(Enemy, enemyIDNumber, waveNumber, difficultyModifier);
                SetVisuals(Enemy);
                spawnSpeed = mapData.EnemyTypeForWave[i].SpawnSpeed;
                //spawnSpeed = Enemy.GetComponent<EnemyStats>().spawnSpeed;
                enemyIDNumber++;
            }
            waveNumber++;
        }
        lastEnemySpawned = true;

        //GameObject Enemy;
        //for (int i = 0; i < waves.Count; i++)
        //{
        //    //Debug.Log($"Wave {waveNumber}");
        //    for (int j = 0; j < waves[waveNumber]; j++)
        //    {
        //        yield return new WaitForSeconds(spawnSpeed);
        //        Debug.Log($"Enemy! ID: {enemyIDNumber} / wave number: {waveNumber} / Max Enemies: {waves[waveNumber]}");
        //        Enemy = GameObject.Instantiate(availableEnemies.Enemies[waveType[waveNumber]].EnemyPrefab, spawnPoint.transform.position, Quaternion.identity);
        //        Enemy.GetComponent<FollowNavMesh>().start = spawnPoint.transform;
        //        Enemy.GetComponent<FollowNavMesh>().finish = destination.transform;
        //        enemiesOnScreen.Add(Enemy);
        //        ModifyStats(Enemy, enemyIDNumber, waveNumber);
        //        SetVisuals(Enemy);
        //        spawnSpeed = Enemy.GetComponent<EnemyStats>().spawnSpeed;
        //        enemyIDNumber++;
        //    }
        //    waveNumber++;
        //}
        //lastEnemySpawned = true;
    }


    IEnumerator StartDrawingCards()
    {
        while (enemyNumber > 0)
        { 
            DrawCards(cardDrawAmmount);
            yield return new WaitForSeconds(cardDrawSpeed);
        }
    }

    IEnumerator PlayWinAnimation()
    {
        yield return new WaitForSeconds(5);
        GameStateManager.instance.ApplyWinReward();
    }

    void ModifyStats(GameObject enemy, int enemyId, int waveNumber, float _difficultyModifier)
    {
        enemy.GetComponent<EnemyStats>().maxHealth += (waveNumber * 2.2f) * _difficultyModifier;
        enemy.GetComponent<EnemyStats>().health += (waveNumber * 2.2f) * _difficultyModifier;
        //enemy.transform.GetChild(0).GetComponent<EnemyStats>().speed = 1;
        //enemy.transform.GetChild(0).GetComponent<EnemyStats>().damage = 1;
        //enemy.transform.GetChild(0).GetComponent<EnemyStats>().reward = 1;
        enemy.GetComponent<EnemyStats>().enemyId = enemyId;
        //enemy.transform.GetChild(0).GetComponent<EnemyStats>().spawnSpeed = 2f;
    }

    void SetVisuals(GameObject enemy)
    {
        enemy.GetComponent<EnemyStats>().hpSlider.maxValue = enemy.GetComponent<EnemyStats>().maxHealth;
        enemy.GetComponent<EnemyStats>().hpSlider.value = enemy.GetComponent<EnemyStats>().health;
    }

    void DrawCards(int cardDrawAmmount)
    {
        int cardNumber;
        Debug.Log("drawing");

        for(int i = 0; i < cardDrawAmmount; i++)
        {
            if (cardsHolder.childCount < 5)
            {
                cardNumber = Random.Range(0, availableCards.cards.Count);
                DrawCard(cardNumber);
            }
        }
    }

    //void CardDiscard()
    //{
    //    Debug.Log("discarding");
    //    foreach (Transform card in cardsHolder)
    //    {
    //        Debug.Log(card.name);
    //        Destroy(card.gameObject);
    //    }
    //}    

    void DrawCard(int cardNumber)
    {
        GameObject card;
        card = Instantiate(cardTemplate);
        card.GetComponent<SpellLogic>().spellStatistics = availableCards.cards[cardNumber];
        card.transform.SetParent(cardsHolder);
        card.transform.localScale = Vector3.one;
    }

    void SetEnemyNumber()
    {
        foreach (Enemy enemy in mapData.EnemyTypeForWave)
        {
            if (enemy.enemyType == EnemyTypes.Multiply)
            {
                enemyNumber += (int)(enemy.EnemyAmount * difficultyModifier) * 3;
            }
            enemyNumber += (int)(enemy.EnemyAmount * difficultyModifier);
        }
    }
}
