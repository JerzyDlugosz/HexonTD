using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public List<float> BasicTowerModifiers = new List<float>(); //DamageAdditive, DamageMultiplicative, RangeAdditive, ..., AttackSpeedAdditive, ...
    public List<float> TeslaTowerModifiers = new List<float>();
    public List<float> MissileTowerModifiers = new List<float>();
    public List<float> RailgunTowerModifiers = new List<float>();
    public List<float> HeroTowerModifiers = new List<float>();

    public float startingMaterials;
    public float maxCardDraw;

    public List<SerializablePathData> w1PathDatas = new List<SerializablePathData>();
    public bool[] w1BeatenPaths = new bool[10];

    public List<SerializablePathData> w2PathDatas = new List<SerializablePathData>();
    public bool[] w2BeatenPaths = new bool[10];

    public List<SerializablePathData> w3PathDatas = new List<SerializablePathData>();
    public bool[] w3BeatenPaths = new bool[10];

    public int currentWorld;
}
