using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningScript : MonoBehaviour
{
    float closestDistance = 1000;

    GameObject closestTarget;

    public GameObject lightningBullet; 

    public List<GameObject> objects = new List<GameObject>();
    public List<GameObject> alreadyHit = new List<GameObject>();

    int numberOfBounces = 3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            objects.Add(other.gameObject);
            Debug.Log($"Enemy in trigger: {other.GetComponent<EnemyStats>().enemyId}");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        objects.Remove(other.gameObject);
    }

    public void OnBulletInpact(float damage)
    {
        if (objects.Count > 0)
        {
            foreach (GameObject hitEnemy in alreadyHit)
            {
                objects.Remove(hitEnemy);
            }
            //CheckAllDistances();
            //if (closestTarget != null)
            //{
            //    alreadyHit.Add(closestTarget);
            //    Debug.Log($"Hitting : {closestTarget.GetComponent<EnemyStats>().enemyId}");
            //    GameObject bullet = GameObject.Instantiate(lightningBullet, this.transform.position, Quaternion.identity);
            //    bullet.GetComponent<BulletLogic>().SetTarget(closestTarget);
            //    bullet.GetComponent<BulletLogic>().numberOfBounces = numberOfBounces - 1;
            //    bullet.GetComponent<BulletLogic>().alreadyHit = alreadyHit;
            //    bullet.GetComponent<BulletLogic>().SetDamage(damage);
            //    Debug.Log($"Bullet {2 - numberOfBounces}");
            //    closestDistance = 1000;
            //    closestTarget = null;
            //}


            for (int i = 0; i < numberOfBounces; i++)
            {
                foreach (GameObject hitEnemy in alreadyHit)
                {
                    objects.Remove(hitEnemy);
                }
                if(objects.Count > i)
                {
                    if (closestTarget != null)
                    {
                        closestTarget.GetComponent<FollowNavMesh>().TakeDamage(damage);
                        alreadyHit.Add(closestTarget);
                        Debug.Log($"Hitting : {closestTarget.GetComponent<EnemyStats>().enemyId}");
                        closestDistance = 1000;
                        closestTarget = null;
                    }
                }
            }

            //for (int i = 0; i < numberOfBounces; i++)
            //{
            //    foreach (GameObject hitEnemy in alreadyHit)
            //    {
            //        objects.Remove(hitEnemy);
            //    }
            //    CheckAllDistances();
            //    if (closestTarget != null)
            //    {
            //        closestTarget.GetComponent<FollowNavMesh>().TakeDamage(damage);
            //        alreadyHit.Add(closestTarget);
            //        Debug.Log($"Hitting : {closestTarget.GetComponent<EnemyStats>().enemyId}");
            //        closestDistance = 1000;
            //        closestTarget = null;
            //    }
            //}
        }
    }

    //private void CheckAllDistances()
    //{
    //    foreach (GameObject obj in objects)
    //    {
    //        if (obj != null)
    //        {
    //            if (closestTarget == null)
    //            {
    //                closestDistance = obj.GetComponent<FollowNavMesh>().GetDistanceFromEnd();
    //                closestTarget = obj;
    //            }

    //            if (closestDistance > obj.GetComponent<FollowNavMesh>().GetDistanceFromEnd())
    //            {
    //                closestDistance = obj.GetComponent<FollowNavMesh>().GetDistanceFromEnd();
    //                closestTarget = obj;
    //            }
    //        }
    //    }
    //}
}
