using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Collections;
using UnityEngine;
using TMPro;

public class DirectionPointerTutorial : MonoBehaviour
{
    [SerializeField] public float rotationSpeed = -30f;
    [SerializeField] public GameObject bobaCupThrow;
    [SerializeField] private Transform rotateAround;

    public TextMeshProUGUI tutorialText;
    public GameObject tutorialBackground;
    public GameObject samIcon;

    public static Boolean moving;
    bool tutorialMode = true;

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
            
            if (tutorialMode)
            {

                if (!(this.transform.position.x < 1.5 && this.transform.position.x > -1.5))
                {
                    tutorialText.text = "Not Quite right, try again!";
                    moving = true;

                }
                else
                {

                    tutorialText.text = "Great! Now control the speed. The lower the pointer, the slower it is!";
                }
                
        }
        }
        
         if (this.transform.position.x > 6.45 || this.transform.position.x < -6.45)
        {
            Debug.Log(transform.position.x);
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
     public bool wrongAim()
    {
        return true;
    }
    
   
}
