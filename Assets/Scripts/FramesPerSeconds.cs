using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramesPerSeconds : MonoBehaviour
{

    float timerTank;
    int frame;
    public float FPS;
    float anotherFPS;

    // Start is called before the first frame update
    void Start()
    {
        timerTank = 0.0f;
        frame = 0;
        anotherFPS = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        ++frame;
        //Debug.Log("Frames passed: " + frame);
        
        timerTank += Time.deltaTime;

        FPS = 1 / Time.deltaTime;
        //Debug.Log("FPS: " + FPS);

        if (timerTank > 1.0f)
        {
            anotherFPS = frame / timerTank;

            Debug.Log("Frames per seconds: " + frame + " /anotherFPS: " + anotherFPS);
            frame = 0;
            timerTank = 0;
        }

    }
}
