using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonScript : MonoBehaviour
{
    private bool isAnimating = false;
    private Animator animator;
    private float progress;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnMouseEnter()
    {
        GetComponent<Animator>().SetBool("Hover", true);
        isAnimating = true;
    }

    public void OnMouseExit()
    {
        GetComponent<Animator>().SetBool("Hover", false);
        isAnimating = false;
    }

    private void Update()
    {
        progress = animator.GetFloat("Blend");
        if (isAnimating && progress < 1)
        {
            animator.SetFloat("Blend", 1, .1f, Time.deltaTime);
        }
        if (!isAnimating && progress > 0)
        {
            animator.SetFloat("Blend", 0, .1f, Time.deltaTime);
        }
    }
}
