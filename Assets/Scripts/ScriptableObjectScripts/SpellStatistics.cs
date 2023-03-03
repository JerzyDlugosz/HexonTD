using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/CardSO")]
public class SpellStatistics : ScriptableObject
{
    [SerializeField]
    public string spellName;
    [SerializeField]
    public string spellDescription;
    [SerializeField]
    public SpellType spellType;
    [SerializeField]
    public float damage;
    [SerializeField]
    public float lingerTime;
    [SerializeField]
    public float range;
    [SerializeField]
    public Sprite spellSprite;
    [SerializeField]
    public float resourceGain;
}
