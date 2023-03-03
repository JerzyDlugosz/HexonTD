using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellRelease : MonoBehaviour
{
    public List<Transform> enemiesInRange = new List<Transform>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log($"Here be target: {other.transform}");
            enemiesInRange.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        enemiesInRange.Remove(other.transform);
    }
}
