using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionPointer : MonoBehaviour
{
    [SerializeField] public float rotationSpeed = -30f;
    [SerializeField] public GameObject bobaCupThrow;
    [SerializeField] private Transform rotateAround;

    public static Boolean moving;

    private void Start()
    {
        moving = true;

    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<SpriteRenderer>().sprite = bobaCupThrow.GetComponent<SpriteRenderer>().sprite;
        if (Input.GetKey(KeyCode.Space))
        {
            moving = false;
        }
        if (this.transform.position.x > 6.45 || this.transform.position.x < -6.45)
        {
            rotationSpeed = -rotationSpeed;
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, -6.45f, 6.45f);
            transform.position = pos;
        }
        if (moving)
        {
            this.transform.RotateAround(rotateAround.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }
}
