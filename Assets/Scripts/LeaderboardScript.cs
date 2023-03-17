using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardScript : MonoBehaviour
{
    [SerializeField]
    private GameObject namePrefab;
    [SerializeField]
    private GameObject scorePrefab;

    [SerializeField]
    private GameObject namePanel;
    [SerializeField]
    private GameObject scorePanel;

    public void onClick()
    {
        LeaderboardData leaderboardData = GameStateManager.instance.leaderboardDataGlobal;
        ResetLeaderboard();
        SetLeaderboard(leaderboardData.playerNames, leaderboardData.scores);
    }

    private void SetLeaderboard(List<string> names, List<int> scores)
    {
        for (int i = 0; i < names.Count; i++)
        {
            var name = Instantiate(namePrefab, namePanel.transform);
            name.GetComponent<TextMeshProUGUI>().text = names[i];

            var score = Instantiate(scorePrefab, scorePanel.transform);
            score.GetComponent<TextMeshProUGUI>().text = scores[i].ToString();
        }
    }

    public void ResetLeaderboard()
    {
        foreach(Transform transform in namePanel.transform)
        {
            Destroy(transform.gameObject);
        }
        foreach (Transform transform in scorePanel.transform)
        {
            Destroy(transform.gameObject);
        }
    }
}
