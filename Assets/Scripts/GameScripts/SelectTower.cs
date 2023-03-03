using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTower : MonoBehaviour
{
    public GameObject TowerPrefab;
    public GameObject Marker;
    GameObject Tower;
    bool TowerSelected = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TowerSelect();
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            TowerDeselect();
        }
        if (TowerSelected)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Tower.transform.Rotate(90, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Tower.transform.Rotate(-90, 0, 0);
            }
        }
    }

    public void TowerSelect()
    {
        if (Marker.transform.childCount < 5)
        {
            Tower = GameObject.Instantiate(TowerPrefab);
            Tower.transform.position = new Vector3(Marker.transform.position.x,3f, Marker.transform.position.z);
            Tower.GetComponent<Rigidbody>().isKinematic = true;
            Tower.transform.SetParent(Marker.transform);
            TowerSelected = true;
        }
        
    }

    public void TowerDeselect()
    {
        if (Marker.transform.childCount > 4)
        {
            Destroy(Marker.transform.GetChild(4).gameObject);
        }

    }
}
