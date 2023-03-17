using System;
using UnityEngine;

public class PathData : MonoBehaviour
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

    public bool isActive;

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

    public void SetMapType (PathData data)
    {
        mapType = data.mapType;
    }

    public void SetData(PathDataNotMono data)
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
}