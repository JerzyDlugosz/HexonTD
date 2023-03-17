using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SetWorldPaths : MonoBehaviour
{
    private List<PathDataNotMono> pathDataList;
    private List<bool> beatenPathsList;  //private bool[] beatenPathsList;
    private List<int> activePaths;
    [SerializeField]
    private MapNumber mapNumber;
    private int maxPaths = 20;
    private void Start()
    {
        WorldPathData worldPathData = GameStateManager.instance.GetComponent<WorldPathData>();

        if (mapNumber == MapNumber.Map1)
        {
            pathDataList = worldPathData.w1PathDatas;
            beatenPathsList = worldPathData.w1BeatenPaths.ToList();
            //foreach(PathDataNotMono pathData in worldPathData.w1PathDatas)
            //{
            //    if (pathData.isActive)
            //    {
            //        beatenPathsList.Add(pathData.isCompleted);
            //        pathDataList.Add(pathData);
            //        activePaths.Add(pathData.pathNumber);
            //    }
            //}

        }
        if (mapNumber == MapNumber.Map2)
        {
            pathDataList = worldPathData.w2PathDatas;
            beatenPathsList = worldPathData.w2BeatenPaths.ToList();
            //foreach (PathDataNotMono pathData in worldPathData.w2PathDatas)
            //{
            //    if (pathData.isActive)
            //    {
            //        beatenPathsList.Add(pathData.isCompleted);
            //        pathDataList.Add(pathData);
            //        activePaths.Add(pathData.pathNumber);
            //    }
            //}
        }
        if (mapNumber == MapNumber.Map3)
        {
            pathDataList = worldPathData.w3PathDatas;
            beatenPathsList = worldPathData.w3BeatenPaths.ToList();
            //foreach (PathDataNotMono pathData in worldPathData.w3PathDatas)
            //{
            //    if (pathData.isActive)
            //    {
            //        beatenPathsList.Add(pathData.isCompleted);
            //        pathDataList.Add(pathData);
            //        activePaths.Add(pathData.pathNumber);
            //    }
            //}
        }

        //List<int> randomNumbers = new List<int>();
        //int rand;
        //int maxPaths = transform.childCount;
        //do
        //{
        //    rand = Random.Range(0, maxPaths);
        //    if (!randomNumbers.Contains(rand))
        //    {
        //        randomNumbers.Add(rand);
        //    }
        //} while (randomNumbers.Count < 9);


        for (int i = 0; i < maxPaths; i++)
        {
            PathData childPathData = transform.GetChild(i).GetComponent<PathData>();
            childPathData.SetData(pathDataList[i]);
            childPathData.pathNumber = pathDataList[i].pathNumber;

            if (beatenPathsList[i])
            {
                childPathData.isCompleted = true;
                foreach (Material material in transform.GetChild(i).GetComponent<MeshRenderer>().materials)
                {
                    material.color = Color.black;
                }
            }

            if (pathDataList[i].isActive)
            {
                childPathData.gameObject.SetActive(true);
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