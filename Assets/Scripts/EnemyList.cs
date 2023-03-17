using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    public Dictionary<GameObject, float> enemyDictionary = new Dictionary<GameObject, float>();

    [SerializeField]
    private List<GameObject> enemies;
    [SerializeField]
    private List<float> distances;

    public void AddEnemyToEnemyList(GameObject enemy)
    {
        enemyDictionary.Add(enemy, 1000f);
        //enemies.Add(enemy);
        //distances.Add(enemy.GetComponent<FollowNavMesh>().GetDistanceFromEnd());
        StartCoroutine(RefreshDistance(enemy));
    }

    //public Dictionary<GameObject, float> SortDictionary(Dictionary<GameObject, float> dictionary)
    //{
    //    var orderedDictionary = dictionary.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
    //    return orderedDictionary;
    //}

    //public List<GameObject> SortList(List<GameObject> enemies)
    //{
    //    enemies.Sort();
    //    return enemies;
    //}

    //private void Update()
    //{
    //    for (int i = distances.Count; i < enemies.Count; i++)
    //    {
    //        distances.Add(enemies[i].GetComponent<FollowNavMesh>().GetDistanceFromEnd());
    //    }
    //    foreach(GameObject enemy in enemies)
    //    {
    //        distances.
    //    }
    //    enemyDictionary = SortDictionary(enemyDictionary);
    //    enemies = enemyDictionary.Keys.ToList();
    //    distances = enemyDictionary.Values.ToList();
    //}

    IEnumerator RefreshDistance(GameObject enemy)
    {
        bool exit = false;
        do
        {
            if (!enemyDictionary.ContainsKey(enemy)) exit = true;
            else
            {
                enemyDictionary[enemy] = enemy.GetComponent<FollowNavMesh>().GetDistanceFromEnd();
                yield return new WaitForEndOfFrame();
            }
        }while(!exit);
    }
}
