using System.Collections;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;
    public List<List<float>> Modifiers;
    public int scenePathNumber = 0;
    private int SaveFile = 1;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
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

        GetComponent<WorldPathData>().beatenPaths[GetComponent<PathData>().pathNumber] = true;
        GetComponent<WorldPathData>().pathDatas[GetComponent<PathData>().pathNumber].isCompleted = true;

        int i = 0;
        Modifiers = new List<List<float>>()
        {
            GetComponent<PlayerData>().BasicTowerModifiers,
            GetComponent<PlayerData>().TeslaTowerModifiers,
            GetComponent<PlayerData>().RailgunTowerModifiers,
            GetComponent<PlayerData>().MissileTowerModifiers,
            GetComponent<PlayerData>().HeroTowerModifiers
        };

        if ((int)GetComponent<PathData>().rewardType < 5)
        {
            i = Random.Range(0, 6);
            if (i % 2 == 0)
            {
                Modifiers[(int)GetComponent<PathData>().rewardType][i] += GetComponent<PathData>().rewardAmmount;
            }
            else
            {
                Modifiers[(int)GetComponent<PathData>().rewardType][i] += GetComponent<PathData>().rewardAmmount / 10;
            }
        }
        else
        {
            GetComponent<PlayerData>().startingMaterials += GetComponent<PathData>().rewardAmmount;
        }

        Debug.Log($"Rolled Upgrade : {GetComponent<PathData>().rewardType} {i}");

        SavePlayerData();
        LoadScene(3);
    }


    public void LoadScene(int sceneNumber)
    {
        StartCoroutine(LoadAsyncScene(sceneNumber));
    }

    public void LoadScenewithCameraZoomAnimation(int sceneNumber)
    {
        StartCoroutine(LoadAsyncScene(sceneNumber));
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

    public void LoadSceneWithPlayerData(int sceneNumber)
    {
        if(!LoadSaveFile(SaveFile))
        {
            return;
        }
        LoadScene(sceneNumber);
    }

    public void LoadPlayerData()
    {
        LoadSaveFile(SaveFile);
    }
    public void SavePlayerData()
    {
        SaveGameFile(SaveFile);
    }

    public bool LoadSaveFile(int saveFile)
    {
        string saveFilePath = Path.Combine(Application.persistentDataPath, $"gamesave{saveFile}.json");
        if (File.Exists(saveFilePath))
        {
            string jsonText = File.ReadAllText(saveFilePath);
            Save save = new Save();
            JsonUtility.FromJsonOverwrite(jsonText, save);

            GetComponent<PlayerData>().BasicTowerModifiers = save.BasicTowerModifiers;
            GetComponent<PlayerData>().MissileTowerModifiers = save.MissileTowerModifiers;
            GetComponent<PlayerData>().TeslaTowerModifiers = save.TeslaTowerModifiers;
            GetComponent<PlayerData>().RailgunTowerModifiers = save.RailgunTowerModifiers;
            GetComponent<PlayerData>().HeroTowerModifiers = save.HeroTowerModifiers;
            GetComponent<PlayerData>().startingMaterials = save.startingMaterials;
            GetComponent<PlayerData>().maxCardDraw = save.maxCardDraw;

            GetComponent<WorldPathData>().beatenPaths = save.beatenPaths;

            foreach(SerializablePathData saveData in save.pathDatas)
            {
                PathDataNotMono pathData = new PathDataNotMono();

                pathData.pathNumber = saveData.pathNumber;
                pathData.pathName = saveData.pathName;
                pathData.pathDescription = saveData.pathDescription;
                pathData.pathDifficulty = saveData.pathDifficulty;
                pathData.rewardName = saveData.rewardName;
                pathData.rewardAmmount = saveData.rewardAmmount;
                pathData.rewardType = saveData.rewardType;

                GetComponent<WorldPathData>().pathDatas.Add(pathData);
            }

            Debug.Log("Game Loaded");
            return true;
        }
        else
        {
            Debug.Log("No game saved!");
            return false;
        }

        //if (File.Exists(Application.persistentDataPath + $"/gamesave{saveFile}.txt"))
        //{
        //    BinaryFormatter bf = new BinaryFormatter();
        //    FileStream file = File.Open(Application.persistentDataPath + $"/gamesave{saveFile}.txt", FileMode.Open);
        //    Save save = (Save)bf.Deserialize(file);
        //    file.Close();

        //    GetComponent<PlayerData>().BasicTowerModifiers = save.BasicTowerModifiers;
        //    GetComponent<PlayerData>().MissileTowerModifiers = save.MissileTowerModifiers;
        //    GetComponent<PlayerData>().TeslaTowerModifiers = save.TeslaTowerModifiers;
        //    GetComponent<PlayerData>().RailgunTowerModifiers = save.RailgunTowerModifiers;
        //    GetComponent<PlayerData>().HeroTowerModifiers = save.HeroTowerModifiers;
        //    GetComponent<PlayerData>().startingMaterials = save.startingMaterials;
        //    GetComponent<PlayerData>().maxCardDraw = save.maxCardDraw;

        //    GetComponent<WorldPathData>().pathDatas = save.pathDatas;
        //    GetComponent<WorldPathData>().beatenPaths = save.beatenPaths;


        //    Debug.Log("Game Loaded");
        //}
        //else
        //{
        //    Debug.Log("No game saved!");
        //}
    }

    public void SaveGameFile(int saveFile)
    {
        string saveFilePath = Path.Combine(Application.persistentDataPath, $"gamesave{saveFile}.json");

        Save save = CreateSaveGameObject();
        string json = JsonUtility.ToJson(save);
        File.WriteAllText(saveFilePath, json);

        //Save save = CreateSaveGameObject();
        //BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Create
        //    (Application.persistentDataPath + $"/gamesave{saveFile}.txt");
        //bf.Serialize(file, save);
        //file.Close();

        Debug.Log($"Game Saved in {saveFilePath}");
    }

    Save CreateSaveGameObject()
    {
        Save save = new Save();
        SetSaveData(save);
        return save;
    }

    public void SetSaveData(Save save)
    {
        save.BasicTowerModifiers = GetComponent<PlayerData>().BasicTowerModifiers;
        save.MissileTowerModifiers = GetComponent<PlayerData>().MissileTowerModifiers;
        save.TeslaTowerModifiers = GetComponent<PlayerData>().TeslaTowerModifiers;
        save.RailgunTowerModifiers = GetComponent<PlayerData>().RailgunTowerModifiers;
        save.HeroTowerModifiers = GetComponent<PlayerData>().HeroTowerModifiers;
        save.startingMaterials = GetComponent<PlayerData>().startingMaterials;
        save.maxCardDraw = GetComponent<PlayerData>().maxCardDraw;

        save.beatenPaths = GetComponent<WorldPathData>().beatenPaths;

        foreach (PathDataNotMono pathData in GetComponent<WorldPathData>().pathDatas)
        {
            SerializablePathData serializablePathData = new SerializablePathData();

            serializablePathData.pathNumber = pathData.pathNumber;
            serializablePathData.pathName = pathData.pathName;
            serializablePathData.pathDescription = pathData.pathDescription;
            serializablePathData.pathDifficulty = pathData.pathDifficulty;
            serializablePathData.rewardName = pathData.rewardName;
            serializablePathData.rewardAmmount = pathData.rewardAmmount;
            serializablePathData.rewardType = pathData.rewardType;

            save.pathDatas.Add(serializablePathData);
        }  
    }

    //public void SetPathData()
    //{
    //    foreach(PathData pathData in GetComponent<WorldPathData>().pathDatas)
    //    {
    //        pathData.pathNumber = int.Parse(list[0]);
    //        pathData.pathName = list[1];
    //        pathData.pathDescription = list[2];
    //        pathData.pathDifficulty = (PathDifficulty)int.Parse(list[3]);
    //        pathData.rewardName = list[4];
    //        pathData.rewardAmmount = int.Parse(list[5]);
    //        pathData.rewardType = (RewardType)int.Parse(list[6]);
    //    }
    //}

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
}
