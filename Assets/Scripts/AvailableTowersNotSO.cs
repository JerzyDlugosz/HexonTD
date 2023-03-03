using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableTowersNotSO : MonoBehaviour
{
    [SerializeField]
    public List<TowerStats> towerStats;

    private void Start()
    {
        foreach (Transform child in this.transform)
        {
            towerStats.Add(child.GetComponent<TowerStats>());
        }
    }
}
