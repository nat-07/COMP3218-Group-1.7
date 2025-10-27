using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BobaMovement : MonoBehaviour
{
    [SerializeField] public Transform directionPointer;
    [SerializeField] public Transform pressurePointer;
    float directionPointerX;
    float directionPointerY;
    float pressureSpeed;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (PressurePointer.finalStop)
        {
            directionPointerX = directionPointer.position.x;
            directionPointerY = directionPointer.position.y;
            pressureSpeed = (pressurePointer.position.y + 4.22f) / 8.44f;
            float finalX = 0;
            if (directionPointerX < 0)
            {
                finalX = directionPointerX - Math.Abs(directionPointerY * (3 * pressureSpeed)) * (float)Math.Tan(0.174);
            }
            else
            {
                finalX = Math.Abs(directionPointerY * (3 * pressureSpeed)) * (float)Math.Tan(0.174) + directionPointerX;
            }

            float finalY = Math.Abs(directionPointerY * (3 * pressureSpeed) - directionPointerY);
            Vector3 finalPos = new Vector3(finalX, finalY, 0);
            transform.position = Vector3.MoveTowards(transform.position, finalPos, pressureSpeed * 30f * Time.deltaTime);
        }
    }
}

