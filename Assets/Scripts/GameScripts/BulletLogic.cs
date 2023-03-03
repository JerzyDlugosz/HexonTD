using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Enumerables;

public class BulletLogic : MonoBehaviour
{
    GameObject target;
    float bulletDamage;
    float secondaryDamage;
    public float bulletSpeed;
    public BulletType bulletType;

    //float enemySpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(target)
        {
            var step = bulletSpeed * Time.deltaTime;
            Vector3 targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
            this.transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, step);
            this.transform.LookAt(targetPosition);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void SetTarget(GameObject Enemy)
    {
        target = Enemy;
        //enemySpeed = Enemy.GetComponent<EnemyStats>().speed;
    }

    public void SetDamage(float damage)
    {
        bulletDamage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            if(bulletType == BulletType.missile)
            { 
                //target.GetComponent<FollowNavMesh>().TakeDamage(bulletDamage);
                this.GetComponentInChildren<ExplosionScript>().OnBulletInpact(bulletDamage);
            }
            else if(bulletType == BulletType.lightning)
            {
                //target.GetComponent<FollowNavMesh>().TakeDamage(bulletDamage);
                this.GetComponentInChildren<LightningScript>().OnBulletInpact(bulletDamage);
            }
            else
            {
                target.GetComponent<FollowNavMesh>().TakeDamage(bulletDamage);
            }
            Destroy(this.gameObject);
        }
    }
}
