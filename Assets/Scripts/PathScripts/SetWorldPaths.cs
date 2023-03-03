using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWorldPaths : MonoBehaviour
{

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            PathData childPathData = transform.GetChild(i).GetComponent<PathData>();
            childPathData.SetData(GameStateManager.instance.GetComponent<WorldPathData>().pathDatas[i]);
            childPathData.pathNumber = i;

            if (GameStateManager.instance.GetComponent<WorldPathData>().beatenPaths[childPathData.pathNumber])
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
