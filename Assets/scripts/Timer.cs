using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    public float elapsedTime;

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        PrintTime(elapsedTime);
    }


    private void PrintTime(float elapsedTime)
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void SubtractMinute()
    {
        elapsedTime -= 60f;

        if (elapsedTime < 0)
        {
            elapsedTime = 0;
        }

        PrintTime(elapsedTime);
    }
}
