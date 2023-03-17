using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// This is (for now) needed to simplify gameStateManager inspector. Plus it allows me to create this class and add it to a list without making
/// More components in gameStateManager
/// </summary>
public class PathDataNotMono
{
    public int pathNumber;

    public string pathName;

    public string pathDescription;

    public PathDifficulty pathDifficulty;

    public string rewardName;

    public int rewardAmmount;

    public RewardType rewardType;

    public MapTypes mapType; //mapType on mapTiles needs to be set in inspector only!

    public bool isCompleted;

    public bool isActive = false;

    public void SetData(PathData data)
    {
        pathNumber = data.pathNumber;
        pathName = data.pathName;
        pathDescription = data.pathDescription;
        pathDifficulty = data.pathDifficulty;
        rewardName = data.rewardName;
        rewardAmmount = data.rewardAmmount;
        rewardType = data.rewardType;
        isCompleted = data.isCompleted;
    }

    public void SetMapType(PathData data)
    {
        mapType = data.mapType;
    }
}
