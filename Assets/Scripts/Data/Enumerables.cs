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

public enum WaveTemplate
{
    Normal1,
    Normal2,
    Normal3,
    Special1,
    Special2,
    Special3,
    Easy1,
    Easy2,
    Easy3,
    Medium1,
    Medium2,
    Medium3,
    Hard1,
    Hard2,
    Hard3
}