using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPathData : MonoBehaviour
{

    public List<PathDataNotMono> w1PathDatas = new List<PathDataNotMono>();
    public bool[] w1BeatenPaths = new bool[20];

    public List<PathDataNotMono> w2PathDatas = new List<PathDataNotMono>();
    public bool[] w2BeatenPaths = new bool[20];

    public List<PathDataNotMono> w3PathDatas = new List<PathDataNotMono>();
    public bool[] w3BeatenPaths = new bool[20];


}
