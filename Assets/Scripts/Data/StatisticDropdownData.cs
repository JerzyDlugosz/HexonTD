using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatisticDropdownData : MonoBehaviour
{
    public TextMeshProUGUI statisticNameText;
    public Button dropdownButton;
    [HideInInspector]
    public List<string> statisticName;
    [HideInInspector]
    public List<float> statisticValue;
    [HideInInspector]
    public int numberOfStats;
    [HideInInspector]
    public int maxValue;
}
