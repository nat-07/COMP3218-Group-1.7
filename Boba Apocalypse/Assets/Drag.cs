using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Drag : MonoBehaviour
{
    AudioManager audioManager;
    private bool dragging = false;
    private Vector3 offset;
    private Vector3 originalPosition;

    bool activate = false;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        originalPosition = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (!activate)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.material.color = Color.white;
        }
        if (dragging)
        {
            // Move object, taking into account original offset
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;

        }
    }
    void OnMouseEnter()
    {
        if (activate)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.material.color = Color.yellow;
        }
    }
    void OnMouseExit()
    {
        if (activate)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.material.color = Color.white;
        }
    }
    private void OnMouseDown()
    {
        if (activate)
        {
            audioManager.PlaySFX(audioManager.pickingUp);
            // Record the difference between objects centre, and the clicked point on the camera plane. 
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragging = true;
        }
    }
    private void OnMouseUp()
    {
            //Stop Dragging 
        dragging = false;

            //Object returns to same position ((idk if we want smooth transmission or not))

        
        transform.position = originalPosition;
    }

    public void makeActive()
    {
        activate = true;
    }
    public void makeNotActive()
    {
        activate = false;
        Debug.Log("Deactivated");
    }

}