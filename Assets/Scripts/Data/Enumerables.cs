using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enumerables : MonoBehaviour
{
    
}

public enum PathDifficulty
{
    Easy,
    Normal,
    Hard,
    Boss
}

public enum RewardType
{
    BasicReward,
    TeslaReward,
    RailgunReward,
    MissileReward,
    HeroReward,
    ResourceIncreaseReward
}

public enum BulletType
{
    missile,
    basic,
    lightning,
    etc
}

public enum TowerType
{
    Basic,
    Tesla,
    Railgun,
    Missle,
    Hero,
    Freezing,
    Empty
}

public enum AdjacentSide
{
    Left,
    Right,
    Down,
    Up
}

public enum EnemyTypes
{
    Basic,
    Armored,
    Regen,
    Multiply
}

public enum MapTypes
{
    Plains,
    Forest,
    Taiga,
    Tundra,
    Desert,
    Fungal
}