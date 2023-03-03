using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpecificStatData : MonoBehaviour
{
    public TextMeshProUGUI statisticNameText;
    public Slider slider;
    [HideInInspector]
    public string statisticName;
    [HideInInspector]
    public float statisticValue;
}
