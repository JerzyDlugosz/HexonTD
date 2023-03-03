using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseController : MonoBehaviour
{
    [SerializeField]
    public int baseHealth;
    [SerializeField]
    public float currentResources;
    [SerializeField]
    private AvailableTowersNotSO Towers;

    [SerializeField]
    private TextMeshProUGUI resources;

    public void SetResources(float resourceAmmount)
    {
        currentResources = resourceAmmount;
        SetText(currentResources);
    }

    public void AddResources(float resourceAmmount)
    {
        currentResources += resourceAmmount;
        SetText(currentResources);
        CheckIfTowersCanBeBought();
    }

    public bool RemoveResources(float resourceAmmount) 
    {
        currentResources -= resourceAmmount;
        if(currentResources < 0)
        {
            currentResources += resourceAmmount;
            return false;
        }
        SetText(currentResources);
        CheckIfTowersCanBeBought();
        return true;
    }

    public void CheckIfTowersCanBeBought()
    {
        foreach (TowerStats tower in Towers.towerStats)
        {
            if (currentResources >= tower.price)
            {
                tower.GetComponent<Button>().interactable = true;
                tower.GetComponent<BuyTowerLogic>().grayOutPanel.SetActive(false);
            }
            else
            {
                tower.GetComponent<Button>().interactable = false;
                tower.GetComponent<BuyTowerLogic>().grayOutPanel.SetActive(true);
            }
        }
    }

    public void SetText(float currentResources)
    {
        resources.text = currentResources.ToString();
    }
}
