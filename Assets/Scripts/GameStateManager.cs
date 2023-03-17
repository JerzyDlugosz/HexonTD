using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;
    public List<List<float>> Modifiers;
    public int scenePathNumber = 0;
    private int SaveFile = 1;

    PlayerData playerData;
    PathData pathData;
    WorldPathData worldPathData;

    List<bool[]> beatenPathsList;
    List<List<PathDataNotMono>> worldPathList;

    private int[] sceneNumbers = { 3, 4, 5};

    public bool isRunFinished = true;

    private SavingAndLoading savingAndLoading;
    public LeaderboardData leaderboardDataGlobal;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

            playerData = GetComponent<PlayerData>();
            pathData = GetComponent<PathData>();
            worldPathData = GetComponent<WorldPathData>();
            savingAndLoading = GetComponent<SavingAndLoading>();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        if(SceneManager.sceneCount == 1)
        {
            LoadScene(1);
        }
    }


    public void ApplyWinReward()
    {
        beatenPathsList = new List<bool[]>()
            {
                worldPathData.w1BeatenPaths,
                worldPathData.w2BeatenPaths,
                worldPathData.w3BeatenPaths
            };

        worldPathList = new List<List<PathDataNotMono>>()
            {
                worldPathData.w1PathDatas,
                worldPathData.w2PathDatas,
                worldPathData.w3PathDatas
            };

        beatenPathsList[playerData.currentWorld][pathData.pathNumber] = true;
        worldPathList[playerData.currentWorld][pathData.pathNumber].isCompleted = true;

        int randomCapturedTile = RandomizeCapturedTile(playerData.currentWorld);

        if(randomCapturedTile != -1)
        {
            beatenPathsList[playerData.currentWorld][randomCapturedTile] = true;
            worldPathList[playerData.currentWorld][randomCapturedTile].isCompleted = true;
        }
       
        int i = 0;
        Modifiers = new List<List<float>>()
        {
            playerData.BasicTowerModifiers,
            playerData.TeslaTowerModifiers,
            playerData.RailgunTowerModifiers,
            playerData.MissileTowerModifiers,
            playerData.HeroTowerModifiers
        };

        if ((int)GetComponent<PathData>().rewardType < 5)
        {

            //This code is for 6 modifiers, and ill probably only use 3
            //i = Random.Range(0, 6);
            //if (i % 2 == 0) //Additive bonus
            //{
            //    Modifiers[(int)GetComponent<PathData>().rewardType][i] += GetComponent<PathData>().rewardAmmount;
            //}
            //else  //Multiplicative bonus
            //{
            //    Modifiers[(int)GetComponent<PathData>().rewardType][i] += GetComponent<PathData>().rewardAmmount / 10;
            //}

            //This code is for 3 modifiers
            i = Random.Range(0, 3);

            Modifiers[(int)GetComponent<PathData>().rewardType][i*2] += GetComponent<PathData>().rewardAmmount;

        }
        if((int)GetComponent<PathData>().rewardType == 6)
        {
            GetComponent<PlayerData>().startingMaterials += GetComponent<PathData>().rewardAmmount;
        }
        if((int)GetComponent<PathData>().rewardType == 7)
        {
            GetComponent<PlayerData>().maxCardDraw += GetComponent<PathData>().rewardAmmount;
        }

        Debug.Log($"Rolled Upgrade : {GetComponent<PathData>().rewardType} {i}");

        if (randomCapturedTile == -1)
        {
            playerData.currentWorld += 1;
        }

        if (playerData.currentWorld == 3)
        {
            isRunFinished = true;
            SavePlayerData();
            SaveLeaderboardData();
        }
        else
        {
            SavePlayerData();
        }

        LoadScene(3);
    }

    private int RandomizeCapturedTile(int currentWorld)
    {
        List<int> list = new List<int>();

        for (int i = 0; i < beatenPathsList[currentWorld].Length; i++)
        {
            if (worldPathList[currentWorld][i].isActive)
            {
                if (!beatenPathsList[currentWorld][i])
                {
                    list.Add(i);
                }
            }
        }

        if(list.Count == 0)
        {
            return -1;
        }

        int rand = Random.Range(0, list.Count);

        return list[rand];
    }


    public void LoadScene(int sceneNumber)
    {
        StartCoroutine(LoadAsyncScene(sceneNumber));
    }
    public void LoadScenewithCameraZoomAnimation(int sceneNumber)
    {
        LoadScene(sceneNumber);
    }

    public void LoadSceneAndResetSave(int sceneNumber)
    {
        SavePlayerData();
        LoadScene(sceneNumber);
    }

    public void LoadSceneWithPathNumber(int sceneNumber)
    {
        scenePathNumber += 10;
        LoadScene(sceneNumber);
    }

    public void LoadSceneWithPathData(int sceneNumber, WorldTransformation worldTransformation)
    {
        PathData pathData = worldTransformation.GetComponent<WorldTransformation>().selectedTile.GetComponent<PathData>();
        GetComponent<PathData>().SetData(pathData);
        GetComponent<PathData>().SetMapType(pathData);
        LoadScene(sceneNumber);
    }

    public void LoadSceneAndSetPaths(int sceneNumber)
    {
        GetComponent<RandomizePathInfo>().OnGameStart();
        LoadScene(sceneNumber);
    }

    //public void LoadSceneWithPlayerData()
    //{
    //    if(!LoadSaveFile(SaveFile))
    //    {
    //        return;
    //    }
    //    LoadScene(sceneNumbers[playerData.currentWorld]);
    //}

    public void LoadSceneWithPlayerData()
    {
        if (!savingAndLoading.GetSaveFile(SaveFile, playerData))
        {
            return;
        }
        LoadScene(3);
    }

    public void LoadPlayerData()
    {
        savingAndLoading.GetSaveFile(SaveFile, playerData);
    }
    public void SavePlayerData()
    {
        SaveGameFile(SaveFile);
    }

    public bool CheckForSaveData(int saveFile)
    {
        string saveFilePath = Path.Combine(Application.persistentDataPath, $"gamesave{saveFile}.json");
        if(File.Exists(saveFilePath) )
        {
            return true;
        }
        return false;
    }

    public void SaveGameFile(int saveFile)
    {
        string saveFilePath = Path.Combine(Application.persistentDataPath, $"gamesave{saveFile}.json");

        Save save = CreateSaveGameObject();
        string json = JsonUtility.ToJson(save);
        File.WriteAllText(saveFilePath, json);

        Debug.Log($"Game Saved in {saveFilePath}");
    }

    Save CreateSaveGameObject()
    {
        Save save = new Save();
        savingAndLoading.SetSaveFile(save, playerData);
        return save;
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    IEnumerator LoadAsyncScene(int sceneNumber)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneNumber);
        while (!asyncLoad.isDone)
        {
            Debug.Log(asyncLoad.progress);
            yield return null;
        }
    }

    LeaderboardData CreateLeaderboardData()
    {
        LeaderboardData leaderboardData = new LeaderboardData();
        savingAndLoading.SetLeaderboardData(playerData, out leaderboardData);
        return leaderboardData;
    }

    public void SaveLeaderboardData()
    {
        string saveFilePath = Path.Combine(Application.persistentDataPath, $"Leaderboard.json");

        LeaderboardData leaderboardData = CreateLeaderboardData();
        string json = JsonUtility.ToJson(leaderboardData);
        File.WriteAllText(saveFilePath, json);

        Debug.Log($"Leaderboards saved in: {saveFilePath}");
    }

    //public bool CkeckForLeaderboardData()
    //{
    //    string saveFilePath = Path.Combine(Application.persistentDataPath, $"Leaderboard.json");
    //    if (File.Exists(saveFilePath))
    //    {
    //        return true;
    //    }
    //    return false;
    //}

    public void LoadLeaderboardData()
    {
        savingAndLoading.GetLeaderboardData(playerData, out leaderboardDataGlobal);
    }
}
