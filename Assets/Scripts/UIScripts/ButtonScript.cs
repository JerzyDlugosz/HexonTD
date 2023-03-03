using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    GameStateManager gameStateManager;
    public WorldTransformation worldTransformation;

    void Start()
    {
        gameStateManager = GameObject.Find("PresistentGameController").GetComponent<GameStateManager>();
    }

    public void LoadScene(int sceneNumber)
    {
        gameStateManager.LoadScene(sceneNumber);
    }

    public void LoadScenewithCameraZoomAnimation(int sceneNumber)
    {
        gameStateManager.LoadScenewithCameraZoomAnimation(sceneNumber);
    }

    public void LoadSceneWithPathNumber(int sceneNumber)
    {
        gameStateManager.LoadSceneWithPathNumber(sceneNumber);
    }

    public void LoadSceneWithPathData(int sceneNumber)
    {
        gameStateManager.LoadSceneWithPathData(sceneNumber, worldTransformation);
    }

    public void LoadSceneAndSetPaths(int sceneNumber)
    {
        gameStateManager.LoadSceneAndSetPaths(sceneNumber);
    }

    public void LoadSceneWithPlayerData(int sceneNumber)
    {
        gameStateManager.LoadSceneWithPlayerData(sceneNumber);
    }

    public void LoadPlayerData()
    {
        gameStateManager.LoadPlayerData();
    }
    public void SavePlayerData()
    {
        gameStateManager.SavePlayerData();
    }

    public void LoadSceneAndResetSave(int sceneNumber)
    {
        gameStateManager.LoadSceneAndResetSave(sceneNumber);
    }

    public void ResetSave()
    {
        gameStateManager.SavePlayerData();
    }

    public void SetPaths()
    {
        gameStateManager.GetComponent<RandomizePathInfo>().OnGameStart();
    }

    public void ExitApplication()
    {
        gameStateManager.ExitApplication();
    }
}
