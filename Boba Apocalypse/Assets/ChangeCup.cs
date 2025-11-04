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

    public void ResetBoba()
    {
        ice = false;
        milkTea = false;
        topping = false;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = Resources.Load<Sprite>("Sprites/BobaCupSprite1");
    }
    
    void Update()
    {
    
    
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
                sr.sprite = Resources.Load<Sprite>("Sprites/BobaCupSprite6");
            }
            else if (ice)
            {
               sr.sprite = Resources.Load<Sprite>("Sprites/BobaCupSprite7");
            }
            else if (topping)
            {
                sr.sprite = Resources.Load<Sprite>("Sprites/BobaCupSprite8");
            }

            else
            {
                sr.sprite = Resources.Load<Sprite>("Sprites/BobaCupSprite5");
            }

        }
        else if (other.CompareTag("Ice"))
        {
            Debug.Log("Detected Ice!");
            ice = true;
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (milkTea && topping)
            {
               sr.sprite = Resources.Load<Sprite>("Sprites/BobaCupSprite6");
            }
            else if (milkTea)
            {
                sr.sprite = Resources.Load<Sprite>("Sprites/BobaCupSprite7");
            }
            else if (topping)
            {
                sr.sprite = Resources.Load<Sprite>("Sprites/BobaCupSprite3");
            }

            else
            {
               sr.sprite = Resources.Load<Sprite>("Sprites/BobaCupSprite4");
            }

        }
        else if (other.CompareTag("Topping"))
        {
            Debug.Log("Detected Topping!");
            topping = true;
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (milkTea && ice)
            {
                sr.sprite = Resources.Load<Sprite>("Sprites/BobaCupSprite6");
            }
            else if (milkTea)
            {
               sr.sprite = Resources.Load<Sprite>("Sprites/BobaCupSprite8");
            }
            else if (ice)
            {
                sr.sprite = Resources.Load<Sprite>("Sprites/BobaCupSprite3");
            }

            else
            {
                sr.sprite = Resources.Load<Sprite>("Sprites/BobaCupSprite2");
            }
            
        }
    }
}
