using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public GameObject LoseScreen;
    GameStateManager gameStateManager;

    private void Start()
    {
        gameStateManager = GameObject.Find("PresistentGameController").GetComponent<GameStateManager>();
    }

    public void onDamageTaken()
    {
        if(this.GetComponent<BaseController>().baseHealth == 0)
        {
            LoseScreen.SetActive(true);
            StartCoroutine(OnGameEnd());
        }
    }

    IEnumerator OnGameEnd()
    {
        GetComponent<Timer>().StopTimer();
        yield return new WaitForSeconds(5);
        gameStateManager.LoadScene(1);
    }
}
