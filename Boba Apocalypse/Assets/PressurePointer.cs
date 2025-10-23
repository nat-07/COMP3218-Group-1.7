using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePointer : MonoBehaviour
{
    [SerializeField] public float pressureSpeed;
    Boolean moving;
    Boolean finalStop;
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
            moving = true;
        }
        if (Input.GetKey(KeyCode.Space) && moving)
        {
            moving = false;
            finalStop = true;
            Debug.Log(this.transform.position);
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
