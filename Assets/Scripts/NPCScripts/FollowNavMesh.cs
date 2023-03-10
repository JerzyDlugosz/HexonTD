using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class FollowNavMesh : MonoBehaviour
{
    
    NavMeshAgent agent;
    public Transform finish;
    public Transform start;

    public float speed = 1f;
    public List<BuyTowerLogic> items = new List<BuyTowerLogic>();
    GameObject gameController;
    bool isEnemyDead = false;

    [SerializeField]
    private GameObject spawnPrefab;

    [SerializeField]
    private TextMeshProUGUI distanceText;

    //public float timeAlive = 0;


    void Start()
    {
        gameController = GameObject.Find("GameControllerObject");

        if (start == null)
        {
            start = gameController.GetComponent<SpawnWaves>().spawnPoint.transform;
        }
        if(finish == null)
        {
            finish = gameController.GetComponent<SpawnWaves>().destination.transform;
        }

        agent = GetComponent<NavMeshAgent>();

        NavMesh.SamplePosition(this.transform.position, out NavMeshHit hit, 1f, NavMesh.AllAreas);
        Debug.Log(hit.position); 
        agent.Warp(hit.position);
        agent.SetDestination(finish.position);
        this.transform.rotation = start.rotation;
       
        speed = this.GetComponent<EnemyStats>().speed;

        foreach (Transform item in GameObject.Find("Towers").transform)
        {
            items.Add(item.gameObject.GetComponent<BuyTowerLogic>());
        }

    }

    private void Update()
    {
        distanceText.text = $"Distance: {Extensions.GetPathRemainingDistance(agent)}";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("End"))
        {
            Debug.Log("Entered!");
            gameController.GetComponent<BaseController>().baseHealth -= (int)GetComponent<EnemyStats>().damage;
            gameController.GetComponent<EndGame>().onDamageTaken();
            gameController.GetComponent<SpawnWaves>().enemyNumber -= (int)GetComponent<EnemyStats>().damage;
            gameController.GetComponent<SpawnWaves>().enemiesOnScreen.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    void AddResource()
    {
        gameController.GetComponent<BaseController>().AddResources(this.GetComponent<EnemyStats>().reward);
    }

    public void TakeDamage(float damage)
    {
        if (!isEnemyDead)
        {
            float takenDamage = damage / GetComponent<EnemyStats>().armor;
            this.GetComponent<EnemyStats>().health -= takenDamage;
            this.GetComponentInChildren<Slider>().value -= takenDamage;
            if (this.GetComponent<EnemyStats>().health <= 0)
            {
                isEnemyDead = true;
                OnDeath();
            }
        }
    }

    void OnDeath()
    {
        if(GetComponent<EnemyStats>().enemyType == EnemyTypes.Multiply) 
        {
            for (int i = 0; i < GetComponent<EnemyStats>().spawnNumber; i++)
            {
                Vector3 vector3 = new Vector3(0, 0, (float)i/2 - 0.5f);
                GameObject spawn = Instantiate(spawnPrefab, transform.position + vector3, Quaternion.identity);
                Collider collider = spawn.GetComponent<BoxCollider>();
                collider.enabled = false;
                collider.enabled = true;
            }
        }

        gameController.GetComponent<SpawnWaves>().enemiesOnScreen.Remove(this.gameObject);
        AddResource();

        gameController.GetComponent<SpawnWaves>().OnEnemyKilled();

        Destroy(this.gameObject);
    }

    public float GetDistanceFromEnd()
    {
        return Extensions.GetPathRemainingDistance(agent);
    }
}