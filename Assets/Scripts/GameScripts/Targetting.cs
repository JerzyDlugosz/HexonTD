using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Targetting : MonoBehaviour
{
    List<GameObject> enemies;
    List<GameObject> enemiesInSight = new List<GameObject>();
    float shootCooldown = 1f;
    public float bulletSpeed;
    public float bulletAOESize;
    float closestDistance = 1000;

    public GameObject bulletPrefab;
    public Transform BulletSpawnpoint;

    GameObject closestTarget = null;

    TowerStats towerStats;
    private Target target;


    void Start()
    {
        enemies = GameObject.Find("GameControllerObject").GetComponent<SpawnWaves>().enemiesOnScreen;
        towerStats = this.GetComponent<TowerStats>();
    }

    void FixedUpdate()
    {
        if (enemiesInSight.Count > 0)
        {
            if (enemiesInSight[0] == null)
            {
                enemiesInSight.RemoveAt(0);
            }
            else if(closestTarget == null)
            {
                OnEnemiesInSightRemove(closestTarget);
                //enemiesInSight.Remove(closestTarget);
            }
            else
            {
                Shoot(towerStats.target);
            }
        }
    }

    private void Shoot(Target target)
    {
        if(target == Target.Nearest)
        {
            float previousDistance = 1000f;
            foreach(GameObject enemy in enemiesInSight)
            {
                if(enemy == null)
                {
                    continue;
                }
                float distance = Vector3.Distance(this.transform.position, enemy.transform.position);
                if(distance < previousDistance)
                {
                    previousDistance = distance;
                    closestTarget = enemy;
                    Debug.Log($"Distance: {distance} to: {closestTarget}");
                }
            }

        }

        if(towerStats.isPassive)
        {
            return;
        }

        if (!towerStats.isStationary)
        {
            //Vector3 enemyPositionXZ = new Vector3(enemiesInSight[0].transform.position.x, this.gameObject.transform.position.y, enemiesInSight[0].transform.position.z);
            Vector3 enemyPositionXZ = new Vector3(closestTarget.transform.position.x, closestTarget.transform.position.y, closestTarget.transform.position.z);

            towerStats.towerTop.transform.LookAt(enemyPositionXZ);
            if (towerStats.towerTop.transform.GetChild(0).name != "Bullet Spawn Point")
            {
                //towerStats.towerTop.transform.GetChild(0).LookAt(enemiesInSight[0].transform.position);
                towerStats.towerTop.transform.GetChild(0).LookAt(closestTarget.transform.position);

            }
        }

        if (shootCooldown <= 0)
        {
            shootCooldown = 60 / towerStats.attackSpeed;
            if (closestTarget.GetComponent<EnemyStats>().health <= 0)  //enemiesInSight[0].GetComponent<EnemyStats>().
            {
                OnEnemiesInSightRemove(closestTarget);
                //enemiesInSight.Remove(closestTarget);  //enemiesInSight[0].gameObject
            }
            if (this.TryGetComponent<Animator>(out Animator component))
            {
                component.Play("Shoot");
            }
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = BulletSpawnpoint.position;
            bullet.GetComponent<BulletLogic>().SetTarget(closestTarget);
            bullet.GetComponent<BulletLogic>().bulletSpeed = bulletSpeed;
            if (bulletAOESize > 0)
            {
                bullet.transform.GetChild(1).GetComponent<SphereCollider>().radius = bulletAOESize;
            }
            //bullet.GetComponent<BulletLogic>().SetTarget(enemiesInSight[0]);
            bullet.GetComponent<BulletLogic>().SetDamage(this.GetComponent<TowerStats>().damage);
        }
        else
        {
            shootCooldown -= 1f;
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (enemies.Count > 0 && other.CompareTag("Enemy"))
    //    {
    //        this.gameObject.transform.GetChild(1).LookAt(enemies[0].transform);
    //        if (enemies[0].GetComponent<EnemyStats>().health < 0)
    //        {
    //            Destroy(enemies[0].gameObject);
    //            enemies.Remove(enemies[0].gameObject);
    //        }
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if(towerStats.isPassive)
            {
                other.GetComponent<EnemyStats>().speed *= towerStats.passiveStat;
                other.GetComponent<NavMeshAgent>().speed *= towerStats.passiveStat;
            }
            OnEnemiesInSightAdd(other.gameObject);
        }
        if (other.CompareTag("Spawn"))
        {
            if (towerStats.isPassive)
            {
                other.GetComponent<EnemyStats>().speed *= towerStats.passiveStat;
                other.GetComponent<NavMeshAgent>().speed *= towerStats.passiveStat;
            }
            OnEnemiesInSightAdd(other.gameObject, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (towerStats.isPassive)
            {
                other.GetComponent<EnemyStats>().speed /= towerStats.passiveStat;
                other.GetComponent<NavMeshAgent>().speed /= towerStats.passiveStat;
            }
            if (enemiesInSight.Contains(other.gameObject))
            {
                OnEnemiesInSightRemove(other.gameObject);
            }
            return;
        }
    }

    private void OnEnemiesInSightAdd(GameObject enemy)
    {
        enemiesInSight.Add(enemy);
        if (enemiesInSight.Count > 1)
        {
            return;
        }
        closestTarget = enemy;
    }
    /// <summary>
    /// Force targetting system to target the enemy
    /// </summary>
    /// <param name="enemy"></param>
    /// <param name="forceTarget"></param>
    private void OnEnemiesInSightAdd(GameObject enemy, bool forceTarget)
    {
        enemiesInSight.Add(enemy);
        if (forceTarget)
        {
            closestTarget = enemy;
            return;
        }
        if(enemiesInSight.Count > 1)
        {
            return;
        }
        closestTarget = enemy;
    }

    private void OnEnemiesInSightRemove(GameObject enemy)
    {
        enemiesInSight.Remove(enemy);
        if (enemiesInSight.Count > 0)
        {
            closestTarget = enemiesInSight[0];
            return;
        }
    }

    //This code is really slow, I either should not use it, or use it sparringly
    //public void CheckAllDistances()
    //{
    //    foreach (GameObject enemy in enemiesInSight)
    //    {
    //        if (enemy != null)
    //        {
    //            if(enemy.GetComponent<FollowNavMesh>().GetDistanceFromEnd() < 0)
    //            {
    //                Debug.Log("Fix the targetting system!");
    //                continue;
    //            }
    //            if (closestTarget == null)
    //            {
    //                closestDistance = enemy.GetComponent<FollowNavMesh>().GetDistanceFromEnd();
    //                closestTarget = enemy;
    //            }

    //            if (closestDistance > enemy.GetComponent<FollowNavMesh>().GetDistanceFromEnd())
    //            {
    //                closestDistance = enemy.GetComponent<FollowNavMesh>().GetDistanceFromEnd();
    //                closestTarget = enemy;
    //            }
    //            Debug.Log("closest distance is: " + closestDistance);
    //        }
    //    }
    //}
}
