using System.Collections.Generic;
using UnityEngine;

public class PathArrowController : MonoBehaviour
{
    public List<PathArrowScript> arrowPaths;
    //private PathArrowScript currentPathArrow;
    private List<PathArrowScript> activeArrowPaths;
    [SerializeField, Range(0, 10)]
    public float fillSpeed = 1;
    [SerializeField]
    private int arrowDistance;

    public void StartPathAnimation()
    {
        activeArrowPaths = new List<PathArrowScript>();
        for (int i = 0; i < arrowPaths.Count - 1; i++)
        {
            arrowPaths[i].nextArrow = arrowPaths[i + 1];
            if (i % arrowDistance == 0)
            {
                activeArrowPaths.Add(arrowPaths[i]);
                activeArrowPaths[activeArrowPaths.Count - 1].currentArrow = true;
                activeArrowPaths[activeArrowPaths.Count - 1].FillArrow();
            }
        }
        arrowPaths[arrowPaths.Count - 1].nextArrow = arrowPaths[0];
    }

    private void Update()
    {
        for (int i = 0; i < activeArrowPaths.Count; i++)
        {
            if (activeArrowPaths[i].arrowState == true)
            {
                activeArrowPaths[i].EmptyArrow();
                activeArrowPaths[i].nextArrow.currentArrow = true;
                activeArrowPaths[i].nextArrow.FillArrow();
                activeArrowPaths[i] = activeArrowPaths[i].nextArrow;
            }
        }
    }
}
