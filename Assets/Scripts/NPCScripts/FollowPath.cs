using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowPath : MonoBehaviour
{
    public float speed = 1f;
    public List<GameObject> resources;
    public List<BuyTowerLogic> items = new List<BuyTowerLogic>();
    GameObject gameController;
    bool isEnemyDead = false;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameControllerObject");
        speed = this.GetComponent<EnemyStats>().speed;
        resources = new List<GameObject>();
        foreach (Transform GO in GameObject.Find("MoneyText").transform)
        {
            resources.Add(GO.gameObject);
        }
        foreach(Transform item in GameObject.Find("Towers").transform)
        { 
            items.Add(item.gameObject.GetComponent<BuyTowerLogic>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.parent.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Path"))
        {
            this.transform.parent.rotation = other.transform.rotation;
            this.transform.parent.position = new Vector3(other.transform.position.x, this.transform.position.y, other.transform.position.z);
            Debug.Log($"{this.gameObject.name} reached {other.gameObject.name}");
        }
        if (other.CompareTag("End"))
        {
            gameController.GetComponent<BaseController>().baseHealth -= 1;
            gameController.GetComponent<EndGame>().onDamageTaken();
            gameController.GetComponent<SpawnWaves>().enemiesOnScreen.Remove(this.gameObject);
            Destroy(this.transform.parent.gameObject);
        }
    }

    void AddResource()
    {
        float resourceToAdd = int.Parse(resources[0].GetComponent<Text>().text);
        resourceToAdd += this.GetComponent<EnemyStats>().reward;
        resources[0].GetComponent<Text>().text = resourceToAdd.ToString();
    }

    public void TakeDamage(float damage)
    {
        if(!isEnemyDead)
        {
            this.GetComponent<EnemyStats>().health -= damage;
            this.transform.parent.GetComponentInChildren<Slider>().value -= damage;
            if (this.GetComponent<EnemyStats>().health <= 0)
            {
                isEnemyDead = true;
                OnDeath();
            }
        }
    }

    void OnDeath()
    {
        gameController.GetComponent<SpawnWaves>().enemiesOnScreen.Remove(this.gameObject);
        AddResource();

        gameController.GetComponent<SpawnWaves>().OnEnemyKilled();
        Destroy(this.transform.parent.gameObject);
    }
}
