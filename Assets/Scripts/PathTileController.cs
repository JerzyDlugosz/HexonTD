using System.Collections;
using UnityEngine;

public class PathTileController : MonoBehaviour
{
    [SerializeField]
    private RuntimeAnimatorController animator;
    void Start()
    {
        StartCoroutine(TimedPathAnim());
    }

    private IEnumerator TimedPathAnim()
    {
        foreach (Transform child in transform)
        {
            child.GetChild(0).gameObject.AddComponent<Animator>().runtimeAnimatorController = animator;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
