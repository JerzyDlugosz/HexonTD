using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanelScript : MonoBehaviour
{
    [SerializeField]
    private GameObject panelPrefab;
    [SerializeField]
    public GameObject thisMenu;
    [SerializeField]
    public GameObject otherMenu;

    private void OnEnable()
    {
        Instantiate(panelPrefab, transform);
        if(TryGetComponent<LeaderboardScript>(out LeaderboardScript leaderboardScript))
        {
            leaderboardScript.onClick();
        }
    }

    private void OnDisable()
    {
        foreach(Transform transform in transform)
        {
            Destroy(transform.gameObject);
        }
    }
}
