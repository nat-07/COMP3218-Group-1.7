using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePointer : MonoBehaviour
{
    [SerializeField] public float pressureSpeed;
    Boolean moving;
    public static Boolean finalStop;
    Boolean timerReached = false;
    float timer = 0;
    private void Start()
    {
        moving = false;
        finalStop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!DirectionPointer.moving && !finalStop)
        {
            if (!timerReached)
            {
                timer += Time.deltaTime;
            }
            if (!timerReached && timer > 1)
            {
                moving = true;
                timerReached = true;
            }
        }
        if (Input.GetKey(KeyCode.Space) && moving && timerReached)
        {
            moving = false;
            finalStop = true;
        }
        if (this.transform.position.y > 4.22 || this.transform.position.y < -4.22)
        {
            pressureSpeed = -pressureSpeed;
        }
        if (moving)
        {
            this.transform.Translate(0, pressureSpeed * Time.deltaTime, 0);
        }
    }
}
