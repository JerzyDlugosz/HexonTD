using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsPathController : MonoBehaviour
{
    private PlayerData playerData;
    private List<string> mainStatNames;
    private List<string> statNames;

    [SerializeField]
    private GameObject customDropdownPrefab;

    private void Start()
    {
        playerData = GameStateManager.instance.GetComponent<PlayerData>();
        mainStatNames = new List<string>
        {
            "Basic Tower",
            "Tesla Tower",
            "Railgun Tower",
            "Missile Tower",
            "Hero Tower",
            "Starting Materials",
            "Max Card Draw"
        };
        statNames = new List<string>
        {
            "Damage Add",
            "Damage Mult",
            "Range Add",
            "Range Mult",
            "Attack Speed Add",
            "Attack Speed Mult"
        };
        SetupDropdowns();   
    }

    private void SetupDropdowns()
    {
        for (int i = 0; i < 7; i++)
        {
            GameObject item = Instantiate(customDropdownPrefab, transform);
            StatisticDropdownData itemStatisticDropdownData = item.GetComponent<StatisticDropdownData>();
            itemStatisticDropdownData.statisticNameText.text = mainStatNames[i];
            if(i < 5)
            {
                itemStatisticDropdownData.statisticName = statNames;
                itemStatisticDropdownData.numberOfStats = 6;
                itemStatisticDropdownData.maxValue = 5;
                switch (i)
                {
                    case 0:
                        itemStatisticDropdownData.statisticValue = playerData.BasicTowerModifiers;
                        break;

                    case 1:
                        itemStatisticDropdownData.statisticValue = playerData.TeslaTowerModifiers;
                        break;

                    case 2:
                        itemStatisticDropdownData.statisticValue = playerData.RailgunTowerModifiers;
                        break;

                    case 3:
                        itemStatisticDropdownData.statisticValue = playerData.MissileTowerModifiers;
                        break;

                    case 4:
                        itemStatisticDropdownData.statisticValue = playerData.HeroTowerModifiers;
                        break;

                    default:
                        break;
                }
                //if (i == 0)
                //{
                //    item.GetComponent<StatisticDropdownData>().statisticValue = playerData.BasicTowerModifiers;
                //}
                //if (i == 1)
                //{
                //    item.GetComponent<StatisticDropdownData>().statisticValue = playerData.TeslaTowerModifiers;
                //}
                //if (i == 2)
                //{
                //    item.GetComponent<StatisticDropdownData>().statisticValue = playerData.RailgunTowerModifiers;
                //}
                //if (i == 3)
                //{
                //    item.GetComponent<StatisticDropdownData>().statisticValue = playerData.MissileTowerModifiers;
                //}
                //if (i == 4)
                //{
                //    item.GetComponent<StatisticDropdownData>().statisticValue = playerData.HeroTowerModifiers;
                //}
            }


            if(i == 5)
            {
                itemStatisticDropdownData.statisticName.Add(mainStatNames[i]);
                itemStatisticDropdownData.statisticValue.Add(playerData.startingMaterials);
                itemStatisticDropdownData.numberOfStats = 1;
                itemStatisticDropdownData.maxValue = 100;
            }
            if (i == 6)
            {
                itemStatisticDropdownData.statisticName.Add(mainStatNames[i]);
                itemStatisticDropdownData.statisticValue.Add(playerData.maxCardDraw);
                itemStatisticDropdownData.numberOfStats = 1;
                itemStatisticDropdownData.maxValue = 10;
            }
            itemStatisticDropdownData.dropdownButton.onClick.AddListener(item.GetComponent<StatisticDropdownController>().DropdownOnClickEvent);
        }
    }
}
