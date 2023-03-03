using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelControls : MonoBehaviour
{
    public Transform panel;
    public bool isVisible = false;
    Animator panelAnimator;

    private void Start()
    {
        panelAnimator = panel.GetComponent<Animator>();
    }

    public void onClick()
    {
        if(isVisible)
        {
            panelAnimator.Play("CloseUI");
            isVisible = false;
        }
        else
        {
            panelAnimator.Play("OpenUI");
            isVisible = true;
        }
    }
}
