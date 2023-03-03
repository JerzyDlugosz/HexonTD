using System.Collections.Generic;
using UnityEngine;

public class StatisticDropdownController : MonoBehaviour
{
    [SerializeField]
    private GameObject specificStatPrefab;
    private List<GameObject> specificStats = new List<GameObject>();
    public void DropdownOnClickEvent()
    {
        if(specificStats.Count > 0)
        {
            foreach (GameObject stat in specificStats)
            {
                Destroy(stat);
            }
            specificStats.RemoveRange(0, specificStats.Count);
            return;
        }

        for (int i = 0; i < GetComponent<StatisticDropdownData>().numberOfStats; i++)
        {
            GameObject item = Instantiate(specificStatPrefab, transform.parent);
            specificStats.Add(item);
            item.transform.SetSiblingIndex(gameObject.transform.GetSiblingIndex() + i + 1);
            item.GetComponent<SpecificStatData>().statisticNameText.text = GetComponent<StatisticDropdownData>().statisticName[i];
            item.GetComponent<SpecificStatData>().slider.value = GetComponent<StatisticDropdownData>().statisticValue[i];
            item.GetComponent<SpecificStatData>().slider.maxValue = GetComponent<StatisticDropdownData>().maxValue;
        }
    }
}
