using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 offset;
    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            // Move object, taking into account original offset
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }

    

    private void OnMouseDown()
    {
        // Record the difference between objects centre, and the clicked point on the camera plane. 
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
    }

    private void OnMouseUp()
    {
        //Stop Dragging 

        dragging = false;
        
        //Object returns to same position ((idk if we want smooth transmission or not))
        transform.position = originalPosition;

    }
        

}
