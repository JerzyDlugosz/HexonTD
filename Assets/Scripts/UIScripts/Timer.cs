using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public bool isTimerStarted = false;
    public TextMeshProUGUI timerText;
    public float time = 0;

    public void StartTimer()
    {
        isTimerStarted = true;
    }

    public void StopTimer()
    {
        isTimerStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTimerStarted)
        {
            time += Time.deltaTime;
            timerText.text = time.ToString();
        }
    }
}
