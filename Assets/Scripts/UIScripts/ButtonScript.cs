using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    GameStateManager gameStateManager;
    public WorldTransformation worldTransformation;
    bool confirmationClick = false;

    [SerializeField]
    private GameObject Alert;

    void Start()
    {
        gameStateManager = GameStateManager.instance;
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

    public void LoadSceneWithPlayerData()
    {
        gameStateManager.LoadSceneWithPlayerData();
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

    public void CheckForPreviousSave()
    {
        if (gameStateManager.CheckForSaveData(1))
        {
            Debug.Log("A save already exists! Do you want to start a new game? It will erase the old save file!");
            Alert.SetActive(true);
            //confirmationClick = true;
            return;
        }
        else
        {
            ResetSaveSetPathAndLoadScene(3);
        }
    }

    public void ResetSaveSetPathAndLoadScene(int sceneNumber)
    {
        SetPaths();
        gameStateManager.SavePlayerData();
        LoadScene(sceneNumber);
        return; 
    }

    public void OnStartButtonClick(int sceneNumber)
    {
        //StartCoroutine(Anim());
        ResetSaveSetPathAndLoadScene(sceneNumber);
    }

    public void OnContinueButtonClick(int sceneNumber)
    {
        //StartCoroutine(Anim());
        ResetSaveSetPathAndLoadScene(sceneNumber);
    }

    public void SetPaths()
    {
        gameStateManager.GetComponent<RandomizePathInfo>().OnGameStart();
    }

    public void ExitApplication()
    {
        gameStateManager.ExitApplication();
    }

    //IEnumerator Anim()
    //{
    //    yield return new WaitForEndOfFrame();
    //}

    public void LoadLeaderboardData()
    {
        gameStateManager.LoadLeaderboardData();
    }
}
