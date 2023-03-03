using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public List<GameObject> objects = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            objects.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        objects.Remove(other.gameObject);
    }

    public void OnBulletInpact(float damage)
    {
        foreach(GameObject obj in objects)
        {
            if(obj != null)
            {
                obj.GetComponent<FollowNavMesh>().TakeDamage(damage);
            }
        }
    }
}
