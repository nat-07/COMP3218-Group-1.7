using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionPointer : MonoBehaviour
{
    [SerializeField] public float rotationSpeed = -30f;
    [SerializeField] private Transform rotateAround;

    public static Boolean moving;

    private void Start()
    {
        moving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            moving = false;
            Debug.Log(this.transform.position);
        }
        if (this.transform.position.x > 6.45 || this.transform.position.x < -6.45)
        {
            rotationSpeed = -rotationSpeed;
        }
        if (moving)
        {
            this.transform.RotateAround(rotateAround.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }
}
