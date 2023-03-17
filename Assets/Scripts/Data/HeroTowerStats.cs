using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroTowerStats : MonoBehaviour
{

    public int connectedBasicTowers;
    public float damageMultiplierModifier = 1.5f;

    public int connectedTeslaTowers;
    public float speedMultiplierModifier = 1.5f;

    public int connectedRailgunTowers;
    public float rangeAddModifier = 2f;

    public int connectedMissleTowers;
    public GameObject misslePrefab;
    public float missleRangeAddModifier = 1f;

    public void ResetConnectedTowers()
    {
        connectedBasicTowers = 0;
        connectedTeslaTowers = 0;
        connectedRailgunTowers = 0;
        connectedMissleTowers = 0;
    }

}