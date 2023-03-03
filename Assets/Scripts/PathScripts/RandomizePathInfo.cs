using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static Enumerables;
using Random = UnityEngine.Random;

public class RandomizePathInfo : MonoBehaviour
{
    public List<TextAsset> pathInformations = new List<TextAsset>();

    public void OnGameStart()
    {
        for (int i = 0; i < 10; i++)
        {
            PathDataNotMono pathData = new PathDataNotMono(); //gameObject.AddComponent<PathData>();

            string text = RandomizeFile();

            Debug.Log(text);

            List<string> list = SetInfo(text);

            pathData.pathNumber = int.Parse(list[0]);
            pathData.pathName = list[1];
            pathData.pathDescription = list[2];
            pathData.pathDifficulty = (PathDifficulty)int.Parse(list[3]);
            pathData.rewardName = list[4];
            pathData.rewardAmmount = int.Parse(list[5]);
            pathData.rewardType = (RewardType)int.Parse(list[6]);

            Debug.Log(pathData);

            GetComponent<WorldPathData>().pathDatas.Add(pathData);
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
