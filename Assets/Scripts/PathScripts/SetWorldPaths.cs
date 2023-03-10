using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWorldPaths : MonoBehaviour
{
    private List<PathDataNotMono> pathDataList;
    private bool[] beatenPathsList;
    [SerializeField]
    private MapNumber mapNumber;
    private void Start()
    {
        WorldPathData worldPathData = GameStateManager.instance.GetComponent<WorldPathData>();

        if (mapNumber == MapNumber.Map1)
        {
            pathDataList = worldPathData.w1PathDatas;
            beatenPathsList = worldPathData.w1BeatenPaths;
        }
        if (mapNumber == MapNumber.Map2)
        {
            pathDataList = worldPathData.w2PathDatas;
            beatenPathsList = worldPathData.w2BeatenPaths;
        }
        if (mapNumber == MapNumber.Map3)
        {
            pathDataList = worldPathData.w3PathDatas;
            beatenPathsList = worldPathData.w3BeatenPaths;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            PathData childPathData = transform.GetChild(i).GetComponent<PathData>();
            childPathData.SetData(pathDataList[i]);
            childPathData.pathNumber = i;

            if (beatenPathsList[i])
            {
                childPathData.isCompleted = true;
                foreach (Material material in transform.GetChild(i).GetComponent<MeshRenderer>().materials)
                {
                    material.color = Color.black;
                }
            }
            Debug.Log("Ive set data");
        }
    }
}

enum MapNumber
{
    Map1,
    Map2,
    Map3
}