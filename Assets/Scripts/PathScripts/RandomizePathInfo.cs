using System.Collections.Generic;
using UnityEngine;

public class RandomizePathInfo : MonoBehaviour
{
    public List<TextAsset> pathInformations = new List<TextAsset>();
    List<List<PathDataNotMono>> pathDataList;
    public int maxPaths = 20;

    public void OnGameStart()
    {

        //if (mapNumber == MapNumber.Map1)
        //{
        //    pathDataList = GetComponent<WorldPathData>().w1PathDatas;
        //}
        //if (mapNumber == MapNumber.Map2)
        //{
        //    pathDataList = GetComponent<WorldPathData>().w2PathDatas;
        //}
        //if (mapNumber == MapNumber.Map3)
        //{
        //    pathDataList = GetComponent<WorldPathData>().w3PathDatas;
        //}

        GetComponent<WorldPathData>().w1PathDatas = new List<PathDataNotMono>();
        GetComponent<WorldPathData>().w2PathDatas = new List<PathDataNotMono>();
        GetComponent<WorldPathData>().w3PathDatas = new List<PathDataNotMono>();

        GetComponent<WorldPathData>().w1BeatenPaths = new bool[maxPaths];
        GetComponent<WorldPathData>().w2BeatenPaths = new bool[maxPaths];
        GetComponent<WorldPathData>().w3BeatenPaths = new bool[maxPaths];

        pathDataList = new List<List<PathDataNotMono>>()
        {
            GetComponent<WorldPathData>().w1PathDatas,
            GetComponent<WorldPathData>().w2PathDatas,
            GetComponent<WorldPathData>().w3PathDatas
        };


        List<int> randomNumbers = new List<int>();
        int rand;
        int escape = 0;
        do
        {
            escape++;
            rand = Random.Range(0, maxPaths);
            if (!randomNumbers.Contains(rand))
            {
                randomNumbers.Add(rand);
            }
            if(escape == 1000)
            {
                Debug.Log("Oops!");
                randomNumbers = new List<int> {0,1,2,3,4,5,6,7,8};
                break;

            }
        } while (randomNumbers.Count < 9);

        foreach(int number in randomNumbers) 
        {
            Debug.Log(number);
        }


        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 20; i++)
            {
                PathDataNotMono pathData = new PathDataNotMono(); //gameObject.AddComponent<PathData>();

                string text = RandomizeFile();

                Debug.Log(text);

                List<string> list = SetInfo(text);

                //pathData.pathNumber = int.Parse(list[0]);
                pathData.pathNumber = i;
                pathData.pathName = list[1];
                pathData.pathDescription = list[2];
                //pathData.pathDifficulty = (PathDifficulty)int.Parse(list[3]);
                pathData.pathDifficulty = (PathDifficulty)j;
                pathData.rewardName = list[4];
                pathData.rewardAmmount = int.Parse(list[5]);
                pathData.rewardType = (RewardType)int.Parse(list[6]);
                pathData.isActive = false;

                Debug.Log(pathData);

                pathDataList[j].Add(pathData);
            }
        }

        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 9; i++)
            {
                pathDataList[j][randomNumbers[i]].isActive = true;
            }
        }
    }

    public string RandomizeFile()
    {
        int random = Random.Range(0, pathInformations.Count);
        return pathInformations[random].text;
    }

    public List<string> SetInfo(string text)
    {
        List<string> list = new List<string>();
        int positionOfNewLine;

        for(int i = 0; i < 7; i++)
        {
            positionOfNewLine = text.IndexOf("\r\n");
            list.Add(text.Substring(0, positionOfNewLine));

           // Debug.Log($"pos = {positionOfNewLine} / text = {text.Substring(0, positionOfNewLine)}");

            text = text.Remove(0, positionOfNewLine + 2);

        }

        return list;
    }
}
