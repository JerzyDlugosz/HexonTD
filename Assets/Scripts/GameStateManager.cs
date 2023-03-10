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
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

            playerData = GetComponent<PlayerData>();
            pathData = GetComponent<PathData>();
            worldPathData = GetComponent<WorldPathData>();
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


        Debug.Log($"{playerData.currentWorld}, {pathData.pathNumber}, {beatenPathsList[playerData.currentWorld][pathData.pathNumber]}, {worldPathData.w2BeatenPaths[pathData.pathNumber]}");

        int randomCapturedTile = RandomizeCapturedTile(playerData.currentWorld);

        if(randomCapturedTile != -1)
        {
            beatenPathsList[playerData.currentWorld][randomCapturedTile] = true;
            worldPathList[playerData.currentWorld][randomCapturedTile].isCompleted = true;
        }

        int temp = 0;

        foreach (bool beatenPath in beatenPathsList[playerData.currentWorld])
        {
            if(!beatenPath)
            {
                temp += 1;
            }
        }

        if(temp == 0)
        {
            playerData.currentWorld += 1;
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

        SavePlayerData();

        if(randomCapturedTile == -1)
        {
            //LoadScene(sceneNumbers[playerData.currentWorld]);
        }

        LoadScene(3);
    }

    private int RandomizeCapturedTile(int currentWorld)
    {
        List<int> list = new List<int>();

        for (int i = 0; i < beatenPathsList[currentWorld].Length; i++)
        {
            if (!beatenPathsList[currentWorld][i])
            {
                list.Add(i);
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
        if (!LoadSaveFile(SaveFile))
        {
            return;
        }
        LoadScene(3);
    }

    public void LoadPlayerData()
    {
        LoadSaveFile(SaveFile);
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

    public bool LoadSaveFile(int saveFile)
    {
        string saveFilePath = Path.Combine(Application.persistentDataPath, $"gamesave{saveFile}.json");
        if (File.Exists(saveFilePath))
        {
            string jsonText = File.ReadAllText(saveFilePath);
            Save save = new Save();
            JsonUtility.FromJsonOverwrite(jsonText, save);

            playerData.BasicTowerModifiers = save.BasicTowerModifiers;
            playerData.MissileTowerModifiers = save.MissileTowerModifiers;
            playerData.TeslaTowerModifiers = save.TeslaTowerModifiers;
            playerData.RailgunTowerModifiers = save.RailgunTowerModifiers;
            playerData.HeroTowerModifiers = save.HeroTowerModifiers;
            playerData.startingMaterials = save.startingMaterials;
            playerData.maxCardDraw = save.maxCardDraw;
            playerData.currentWorld = save.currentWorld;

            GetComponent<WorldPathData>().w1BeatenPaths = save.w1BeatenPaths;
            GetComponent<WorldPathData>().w2BeatenPaths = save.w2BeatenPaths;
            GetComponent<WorldPathData>().w3BeatenPaths = save.w3BeatenPaths;

            foreach (SerializablePathData saveData in save.w1PathDatas)
            {
                PathDataNotMono pathData = new PathDataNotMono();

                pathData.pathNumber = saveData.pathNumber;
                pathData.pathName = saveData.pathName;
                pathData.pathDescription = saveData.pathDescription;
                pathData.pathDifficulty = saveData.pathDifficulty;
                pathData.rewardName = saveData.rewardName;
                pathData.rewardAmmount = saveData.rewardAmmount;
                pathData.rewardType = saveData.rewardType;

                GetComponent<WorldPathData>().w1PathDatas.Add(pathData);
            }

            foreach (SerializablePathData saveData in save.w2PathDatas)
            {
                PathDataNotMono pathData = new PathDataNotMono();

                pathData.pathNumber = saveData.pathNumber;
                pathData.pathName = saveData.pathName;
                pathData.pathDescription = saveData.pathDescription;
                pathData.pathDifficulty = saveData.pathDifficulty;
                pathData.rewardName = saveData.rewardName;
                pathData.rewardAmmount = saveData.rewardAmmount;
                pathData.rewardType = saveData.rewardType;

                GetComponent<WorldPathData>().w2PathDatas.Add(pathData);
            }

            foreach (SerializablePathData saveData in save.w3PathDatas)
            {
                PathDataNotMono pathData = new PathDataNotMono();

                pathData.pathNumber = saveData.pathNumber;
                pathData.pathName = saveData.pathName;
                pathData.pathDescription = saveData.pathDescription;
                pathData.pathDifficulty = saveData.pathDifficulty;
                pathData.rewardName = saveData.rewardName;
                pathData.rewardAmmount = saveData.rewardAmmount;
                pathData.rewardType = saveData.rewardType;

                GetComponent<WorldPathData>().w3PathDatas.Add(pathData);
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
        save.BasicTowerModifiers = playerData.BasicTowerModifiers;
        save.MissileTowerModifiers = playerData.MissileTowerModifiers;
        save.TeslaTowerModifiers = playerData.TeslaTowerModifiers;
        save.RailgunTowerModifiers = playerData.RailgunTowerModifiers;
        save.HeroTowerModifiers = playerData.HeroTowerModifiers;
        save.startingMaterials = playerData.startingMaterials;
        save.maxCardDraw = playerData.maxCardDraw;
        save.currentWorld = playerData.currentWorld;


        save.w1BeatenPaths = GetComponent<WorldPathData>().w1BeatenPaths;
        save.w2BeatenPaths = GetComponent<WorldPathData>().w2BeatenPaths;
        save.w3BeatenPaths = GetComponent<WorldPathData>().w3BeatenPaths;


        foreach (PathDataNotMono pathData in GetComponent<WorldPathData>().w1PathDatas)
        {
            SerializablePathData serializablePathData = new SerializablePathData();

            serializablePathData.pathNumber = pathData.pathNumber;
            serializablePathData.pathName = pathData.pathName;
            serializablePathData.pathDescription = pathData.pathDescription;
            serializablePathData.pathDifficulty = pathData.pathDifficulty;
            serializablePathData.rewardName = pathData.rewardName;
            serializablePathData.rewardAmmount = pathData.rewardAmmount;
            serializablePathData.rewardType = pathData.rewardType;

            save.w1PathDatas.Add(serializablePathData);
        }

        foreach (PathDataNotMono pathData in GetComponent<WorldPathData>().w2PathDatas)
        {
            SerializablePathData serializablePathData = new SerializablePathData();

            serializablePathData.pathNumber = pathData.pathNumber;
            serializablePathData.pathName = pathData.pathName;
            serializablePathData.pathDescription = pathData.pathDescription;
            serializablePathData.pathDifficulty = pathData.pathDifficulty;
            serializablePathData.rewardName = pathData.rewardName;
            serializablePathData.rewardAmmount = pathData.rewardAmmount;
            serializablePathData.rewardType = pathData.rewardType;

            save.w2PathDatas.Add(serializablePathData);
        }

        foreach (PathDataNotMono pathData in GetComponent<WorldPathData>().w3PathDatas)
        {
            SerializablePathData serializablePathData = new SerializablePathData();

            serializablePathData.pathNumber = pathData.pathNumber;
            serializablePathData.pathName = pathData.pathName;
            serializablePathData.pathDescription = pathData.pathDescription;
            serializablePathData.pathDifficulty = pathData.pathDifficulty;
            serializablePathData.rewardName = pathData.rewardName;
            serializablePathData.rewardAmmount = pathData.rewardAmmount;
            serializablePathData.rewardType = pathData.rewardType;

            save.w3PathDatas.Add(serializablePathData);
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
