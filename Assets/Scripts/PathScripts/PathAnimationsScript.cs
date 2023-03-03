using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PathAnimationsScript : MonoBehaviour
{
    private float speed = 1f;
    [SerializeField]
    private Image fadeToBlackImage;
    private bool isAnimationRunning;
    private int i = 0;
    public void OnClick(int pathNumber)
    {
        Debug.Log("Clicked!");
        fadeToBlackImage.enabled = true;
        //StartCoroutine(startAnimation(pathNumber));
        isAnimationRunning = true;
    }

    private void Update()
    {
        if(isAnimationRunning)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, Camera.main.transform.parent.position, speed * Time.deltaTime);
            fadeToBlackImage.color = new Color(fadeToBlackImage.color.r, fadeToBlackImage.color.g, fadeToBlackImage.color.b, i/100f);
            i++;
            if(i == 100)
            {
                GetComponent<ButtonScript>().LoadSceneWithPathData(2);
            }
        }
    }

    //IEnumerator startAnimation(int pathNumber)
    //{
    //    for (int i = 0; i < 10; i++)
    //    {
    //        Debug.Log("i = " + i);
    //        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, Camera.main.transform.parent.position, speed * Time.deltaTime);
    //        fadeToBlackImage.color = new Color(fadeToBlackImage.color.r, fadeToBlackImage.color.g, fadeToBlackImage.color.b, i/10);

    //        yield return new WaitForEndOfFrame(); 
    //    }
    //    GetComponent<ButtonScript>().LoadSceneWithPathData(pathNumber);
    //}

}
