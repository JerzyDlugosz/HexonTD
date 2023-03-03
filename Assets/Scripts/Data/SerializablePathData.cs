using System;

[Serializable]
public class SerializablePathData
{
    public int pathNumber;
    public string pathName;
    public string pathDescription;
    public PathDifficulty pathDifficulty;
    public string rewardName;
    public int rewardAmmount;
    public RewardType rewardType;
    public bool isCompleted;
}
