using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellStats : MonoBehaviour
{
    public string spellName;
    public string spellDescription;
    public SpellType spellType;
    public float damage;
    public float lingerTime;
    public float range;
    public Sprite spellSprite;
}
public enum SpellType
{
    AOEInstant,
    AOELinger,
    Buff,
    Terrain,
    ResourceGain
}
