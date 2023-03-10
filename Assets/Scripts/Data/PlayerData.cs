using Microsoft.SqlServer.Server;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enumerables;

public class PlayerData : MonoBehaviour
{
    public List<float> BasicTowerModifiers = new List<float>(); //DamageAdditive, DamageMultiplicative, RangeAdditive, ..., AttackSpeedAdditive, ...
    public List<float> TeslaTowerModifiers = new List<float>();
    public List<float> RailgunTowerModifiers = new List<float>();
    public List<float> MissileTowerModifiers = new List<float>();
    public List<float> HeroTowerModifiers = new List<float>();

    public float startingMaterials;
    public float maxCardDraw;

    public int currentWorld = 0;

    public void SetData(PlayerData data)
    {
        BasicTowerModifiers = data.BasicTowerModifiers;
        TeslaTowerModifiers = data.TeslaTowerModifiers;
        MissileTowerModifiers = data.MissileTowerModifiers;
        RailgunTowerModifiers = data.RailgunTowerModifiers;
        HeroTowerModifiers = data.HeroTowerModifiers;
        startingMaterials = data.startingMaterials;
        maxCardDraw = data.maxCardDraw;
        currentWorld = data.currentWorld;
    }
}
