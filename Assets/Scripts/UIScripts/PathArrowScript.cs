using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathArrowScript : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    public PathArrowController pathArrowController;
    private int number = 1;
    /// <summary>
    /// State of the arrow. False = empty, True - filled
    /// </summary>
    public bool arrowState = false;
    [SerializeField]
    public PathArrowScript nextArrow;
    public bool currentArrow = false;

    void Start()
    {
        image.fillAmount = 0;
    }

    private void Update()
    {
        if(currentArrow)
        {
            image.fillAmount += Time.deltaTime * pathArrowController.fillSpeed * number;
        }
        if(image.fillAmount <= 0 && arrowState == true)
        {
            arrowState = false;
            currentArrow = false;
        }
        if (image.fillAmount >= 1 && arrowState == false)
        {
            arrowState = true;
        }
    }

    public void FillArrow()
    {
        image.fillAmount = 0;
        image.fillOrigin = 0;
        number = 1;
    }

    public void EmptyArrow()
    {
        image.fillAmount = 1;
        image.fillOrigin = 1;
        number = -1;
    }

}
