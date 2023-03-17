using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheck : MonoBehaviour
{
    private EnemySequence enemySequence;
    private bool firstInSequence = false;
    void Start()
    {
        enemySequence = FindObjectOfType<EnemySequence>();
        if (enemySequence.enemies.Count == 0)
        {
            enemySequence.firstEnemy = transform.parent.gameObject;
            firstInSequence = true;
        }
        enemySequence.enemies.Add(transform.parent.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == this.gameObject)
        {
            return;
        }
        if(other.CompareTag("EnemyCenter"))
        {
            Debug.Log("HIT!");
            if (firstInSequence)
            {
                other.transform.parent.GetComponentInChildren<EnemyCheck>().firstInSequence = true;
                enemySequence.firstEnemy = other.transform.parent.gameObject;
                //if (other.TryGetComponent<EnemyCheck>(out EnemyCheck otherEnemy))
                //{
                //    otherEnemy.firstInSequence = true;
                //    enemySequence.firstEnemy = otherEnemy.gameObject;
                //}
                //else
                //{
                //    other.GetComponentInChildren<EnemyCheck>().firstInSequence = true;
                //    enemySequence.firstEnemy = other.gameObject;
                //}
                firstInSequence = false;
            }
        }
    }
}
