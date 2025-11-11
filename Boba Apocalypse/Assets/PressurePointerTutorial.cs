using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePointerTutorial : MonoBehaviour
{
    [SerializeField] public float pressureSpeed;
    AudioManager audioManager;
    public static Boolean moving;
    public static Boolean finalStop;
    Boolean timerReached = false;
    float timer = 0;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
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
            /* if (!timerReached && timer > 0.2)
            {
                moving = true;
                timerReached = true;
            } */
        }
        if (Input.GetKey(KeyCode.Space) && moving && timerReached)
        {
            audioManager.PlaySFX(audioManager.throwBoba);
            moving = false;
            finalStop = true;
            timer = 0;
            timerReached = false;
        }
        if (this.transform.position.y > 4.22f)
        {
            transform.position = new Vector3(transform.position.x, 4.22f, transform.position.z);
            pressureSpeed = -Mathf.Abs(pressureSpeed);
        }
        else if (this.transform.position.y < -4.22f)
        {
            transform.position = new Vector3(transform.position.x, -4.22f, transform.position.z);
            pressureSpeed = Mathf.Abs(pressureSpeed);
        }
        if (moving)
        {
            this.transform.Translate(0, pressureSpeed * Time.deltaTime, 0);
        }
    }
}
