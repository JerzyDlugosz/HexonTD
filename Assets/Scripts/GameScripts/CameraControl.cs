using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public int maxScrollRange = 15;
    public int minScrollRange = 5;  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            Camera.main.transform.Translate(0, 0.1f, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Camera.main.transform.Translate(0.1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Camera.main.transform.Translate(0, -0.1f, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Camera.main.transform.Translate(-0.1f, 0, 0);
        }
        Camera.main.orthographicSize -= Input.mouseScrollDelta.y;
        Camera.main.orthographicSize = Mathf.Max(minScrollRange, Mathf.Min(maxScrollRange, Camera.main.orthographicSize));
        //if(Camera.main.orthographicSize < 5f)
        //{
        //    Camera.main.orthographicSize = 5f;
        //}
        //if (Camera.main.orthographicSize > 10f)
        //{
        //    Camera.main.orthographicSize = 10f;
        //}
    }
}
