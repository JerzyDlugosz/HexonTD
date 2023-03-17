using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class RunEndPanelScript : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private TextMeshProUGUI destroyedEnemies;
    [SerializeField]
    private TextMeshProUGUI remainingResources;
    [SerializeField]
    private TextMeshProUGUI timeSpent;
    [SerializeField]
    private TextMeshProUGUI globalScore;
    [SerializeField]
    private Button exitButton;

    private PlayerData playerData;

    private int allEnemKilled;
    private int allResoSaved;
    private int allTimeSpent;
    private int allScore;

    private string allEnemKilledText;
    private string allResoSavedText;
    private string allTimeSpentText;
    private string globalScoreText;

    void Start()
    {
        if(GameStateManager.instance.isRunFinished)
        {
            panel.SetActive(true);
            SetTextUI();
        }
    }

    private void SetTextUI()
    {
        playerData = GameStateManager.instance.GetComponent<PlayerData>();
        allEnemKilled = playerData.allEnemiesKilled / 50;
        allResoSaved = playerData.allResourcesSaved / 50;
        allTimeSpent = playerData.allTimeSpent;
        allScore = (int)playerData.score;
        StartCoroutine(PlayWinAnimation());
    }

    IEnumerator PlayWinAnimation()
    {
        panel.GetComponent<Animator>().SetBool("StartAnim", true);
        yield return new WaitForSeconds(1);
        for (int i = 1; i <= 50; i++)
        {
            allEnemKilledText = (i * allEnemKilled / 50).ToString();
            allResoSavedText = (i * allResoSaved / 50).ToString();
            allTimeSpentText = (i * allTimeSpent / 50).ToString();
            globalScoreText = (i * allScore / 50).ToString();

            destroyedEnemies.text = allEnemKilledText;
            remainingResources.text = allResoSavedText;
            timeSpent.text = allTimeSpentText;
            globalScore.text = globalScoreText;
            yield return new WaitForSeconds(0.05f);
        }
        exitButton.interactable = true;
    }
}
