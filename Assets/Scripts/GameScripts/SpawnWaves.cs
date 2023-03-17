using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    private int maxEnemies = 0;

    private RewardScreenData rewardScreenData;

    float scoreReso = 0f;
    float scoreEnem = 0f;
    float scoreLive = 0f;
    float scoreTime = 0f;
    float score = 0f;
    float timeSpent = 0f;

    // Start is called before the first frame update
    void Start()
    {
        gameStateManager = GameStateManager.instance;
        rewardScreenData = winScreen.GetComponentInChildren<RewardScreenData>();
    }

    private bool onGameEnd = true; //bool so the if in runs works only once

    // Update is called once per frame
    void Update()
    {            
        if ((enemyNumber <= 0 && lastEnemySpawned) && (enemiesOnScreen.Count == 0 && onGameEnd))
        {
            winScreen.SetActive(true);
            winScreen.GetComponentInChildren<Animator>().SetBool("StartAnim", true);

            scoreReso = GetComponent<BaseController>().currentResources * 50;
            scoreEnem = maxEnemies * 50;
            scoreTime = -(int)(GetComponent<Timer>().time * 10);
            scoreLive = GetComponent<BaseController>().baseHealth * 100;
            timeSpent = (int)GetComponent<Timer>().time;

            score = scoreReso + scoreEnem + scoreTime + scoreLive;

            gameStateManager.GetComponent<PlayerData>().score += score;
            gameStateManager.GetComponent<PlayerData>().allEnemiesKilled += (int)scoreEnem;
            gameStateManager.GetComponent<PlayerData>().allResourcesSaved += (int)scoreReso;
            gameStateManager.GetComponent<PlayerData>().allTimeSpent += (int)timeSpent;

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
        rewardScreenData.reward.text = gameStateManager.GetComponent<PathData>().rewardName;

        yield return new WaitForSeconds(1);
        //for (int i = 100; i > 1; i--)
        for (int i = 1; i <= 50; i++)
        {
            int scoreEnemAnim = (int)(i * scoreEnem / 50);
            int scoreResoAnim = (int)(i * scoreReso / 50);
            int scoreLiveAnim = (int)(i * scoreLive / 50);
            int scoreTimeAnim = (int)(i * scoreTime / 50);
            int scoreAnim = (int)(i * score / 50);
            int globalScoreAnim = (int)(i * gameStateManager.GetComponent<PlayerData>().score / 50);

            Debug.Log(scoreAnim);

            rewardScreenData.destroyedEnemies.text = $"{maxEnemies} * 50 = {scoreEnemAnim}";
            rewardScreenData.remainingResources.text = $"{GetComponent<BaseController>().currentResources} * 50 = {scoreResoAnim}";
            rewardScreenData.livesRemaining.text = $"{scoreLive} * 10 = {scoreLiveAnim}";
            rewardScreenData.timeSpend.text = $"-{timeSpent} * 10 = {scoreTimeAnim}";
            rewardScreenData.score.text = scoreAnim.ToString();
            rewardScreenData.globalScore.text = globalScoreAnim.ToString();
            yield return new WaitForSeconds(0.05f);
        }
        rewardScreenData.continueButton.interactable = true;
        yield return new WaitForSeconds(10);
        GameStateManager.instance.ApplyWinReward();
    }

    public void OnContinueButtonClick()
    {
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
        maxEnemies = enemyNumber;
    }
}
