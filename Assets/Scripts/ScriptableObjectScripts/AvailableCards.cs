using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/AvailableCards")]
public class AvailableCards : ScriptableObject
{
    [SerializeField]
    public List<SpellStatistics> cards;
}
