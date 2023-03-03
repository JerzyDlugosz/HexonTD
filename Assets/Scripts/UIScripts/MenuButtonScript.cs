using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonScript : MonoBehaviour
{
    public void OnMouseEnter()
    {
        GetComponent<Animator>().SetBool("Hover", true);
    }

    public void OnMouseExit()
    {
        GetComponent<Animator>().SetBool("Hover", false);
    }
}
