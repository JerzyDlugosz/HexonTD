using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObjects/AvailableTowers")]
public class AvailableTowers : ScriptableObject
{
    [SerializeField]
    public List<GameObject> towerPrefabs;

    [SerializeField]
    public List<TowerStats> towerStats;

    public List<TowerStats> GetTowerStats()
    {
        if (towerStats.Count == 0)
        {
            foreach (GameObject tower in towerPrefabs)
            {
                towerStats.Add(tower.transform.GetChild(0).GetComponent<TowerStats>());
            }
        }
        return towerStats;
    }

}
