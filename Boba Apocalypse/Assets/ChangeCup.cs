using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;



public class ChangeCup : MonoBehaviour
{
    Boolean ice;
    Boolean milkTea;
    Boolean topping;

    void Start()
    {
        ice = false;
        milkTea = false;
        topping = false;

    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.color = new Color(0.58f, 0.87f, 0.90f);
            ice = false;
            milkTea = false;
            topping = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("MilkTea"))
        {
            Debug.Log("Detected MilkTea!");
            milkTea = true;
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (ice && topping)
            {
                sr.color = Color.white;
            }
            else if (ice)
            {
                sr.color = Color.magenta;
            }
            else if (topping)
            {
                sr.color = new Color(1f, 0.5f, 0f);
            }

            else
            {
                sr.color = Color.red;
            }

        }
        else if (other.CompareTag("Ice"))
        {
            Debug.Log("Detected Ice!");
            ice = true;
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (milkTea && topping)
            {
                sr.color = Color.white;
            }
            else if (milkTea)
            {
                sr.color = Color.magenta;
            }
            else if (topping)
            {
                sr.color = Color.cyan;
            }

            else
            {
                sr.color = Color.blue;
            }

        }
        else if (other.CompareTag("Topping"))
        {
            Debug.Log("Detected Topping!");
            topping = true;
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (milkTea && topping)
            {
                sr.color = Color.white;
            }
            else if (milkTea)
            {
                new Color(1f, 0.5f, 0f);
            }
            else if (ice)
            {
                sr.color = Color.cyan;
            }

            else
            {
                sr.color = Color.green;
            }
            
        }
    }
}
