using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    Text timeUI;

    public float startTime;
    float ellapsedTime;
    public float myTime;
    bool startCounter = false;

    int minutes;
    int seconds;

    bool paused = false;


    // Start is called before the first frame update
    void Start()
    {
        timeUI = GetComponent<Text> ();
        
    }

    public void StartTimeCounter() {

        startTime = 0f;
        startCounter = true;
        paused = false;
    }

    public void resumeTimeCounter() {
        startCounter = true;
        paused = false;
    }

    public void StopTimeCounter() {
        paused = true;
        startCounter = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(startCounter) {

            if (!paused) {
                myTime += Time.deltaTime;
            }

            ellapsedTime = myTime - startTime;


            minutes = (int)ellapsedTime / 60;
            seconds = (int)ellapsedTime % 60;

            timeUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        
    }
}
