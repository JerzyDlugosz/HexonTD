using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateLoadingGamespace : MonoBehaviour
{
    float speed = 0.05f;
    // Start is called before the first frame update 
    void Start()
    {
        StartCoroutine(StartTileTranslation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MoveTile(Transform tile)
    {
        for (int i = 0; i < 120; i++)
        {
            tile.Translate(Vector3.down * speed);
            yield return null;
        }
    }

    IEnumerator StartTileTranslation()
    {
        foreach (Transform tile in transform)
        {
            Debug.Log($"{tile.name}");
            StartCoroutine(MoveTile(tile));
            //yield return new WaitForSeconds(0.1f);
            yield return null;
        }
    }
}
