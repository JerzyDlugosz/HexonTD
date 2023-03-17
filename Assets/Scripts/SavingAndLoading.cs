using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavingAndLoading : MonoBehaviour
{
    public bool GetSaveFile(int saveFile, PlayerData playerData)
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
            playerData.score = save.score;
            playerData.allEnemiesKilled = save.allEnemiesKilled;
            playerData.allResourcesSaved = save.allResourcesSaved;
            playerData.allTimeSpent = save.allTimeSpent;


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
                pathData.isActive = saveData.isActive;

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
                pathData.isActive = saveData.isActive;

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
                pathData.isActive = saveData.isActive;

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
    }

    public void SetSaveFile(Save save, PlayerData playerData)
    {
        save.BasicTowerModifiers = playerData.BasicTowerModifiers;
        save.MissileTowerModifiers = playerData.MissileTowerModifiers;
        save.TeslaTowerModifiers = playerData.TeslaTowerModifiers;
        save.RailgunTowerModifiers = playerData.RailgunTowerModifiers;
        save.HeroTowerModifiers = playerData.HeroTowerModifiers;
        save.startingMaterials = playerData.startingMaterials;
        save.maxCardDraw = playerData.maxCardDraw;
        save.currentWorld = playerData.currentWorld;
        save.score = playerData.score;
        save.allEnemiesKilled = playerData.allEnemiesKilled;
        save.allResourcesSaved = playerData.allResourcesSaved;
        save.allTimeSpent = playerData.allTimeSpent;

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
            serializablePathData.isActive = pathData.isActive;

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
            serializablePathData.isActive = pathData.isActive;

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
            serializablePathData.isActive = pathData.isActive;

            save.w3PathDatas.Add(serializablePathData);
        }
    }

    public void SetLeaderboardData(PlayerData playerData, out LeaderboardData leaderboardData)
    {
        if(GetLeaderboardData(playerData, out leaderboardData))
        {
            leaderboardData.playerNames.Add($"Mateusz{playerData.score % 1000}");
            leaderboardData.scores.Add((int)playerData.score);
        }
        else
        {
            leaderboardData.playerNames = new List<string>
            {
                $"Mateusz{playerData.score % 1000}"
            };
            leaderboardData.scores = new List<int>
            {
                (int)playerData.score
            };
        }

    }

    public bool GetLeaderboardData(PlayerData playerData, out LeaderboardData leaderboardData)
    {
        LeaderboardData lData = new LeaderboardData();
        string saveFilePath = Path.Combine(Application.persistentDataPath, $"Leaderboard.json");
        if (File.Exists(saveFilePath))
        {
            string jsonText = File.ReadAllText(saveFilePath);
            JsonUtility.FromJsonOverwrite(jsonText, lData);

            leaderboardData = lData;
            Debug.Log("Leaderboard loaded");
            return true;
        }
        else
        {
            leaderboardData = lData;
            Debug.Log("No Leaderboard found!");
            return false;
        }
    }
}
