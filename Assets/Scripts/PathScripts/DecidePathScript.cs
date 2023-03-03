using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecidePathScript : MonoBehaviour
{

    GameStateManager gameStateManager;

    void Start()
    {
        gameStateManager = GameObject.Find("PresistentGameController").GetComponent<GameStateManager>();
        foreach (Transform child in this.transform.GetChild(1))
        {
            if(child.GetChild(0).GetComponent<PathData>().pathNumber < gameStateManager.scenePathNumber)
            {
                child.GetChild(0).GetComponent<Button>().interactable = false;
            }
        }
    }
}
