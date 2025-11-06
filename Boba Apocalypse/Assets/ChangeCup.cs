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
    AudioManager audioManager;
    Boolean topping;
    Boolean topping2;
    Boolean topping3;
    Boolean topping4;
    Boolean topping5;
    Boolean topping6;


    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        ice = false;
        milkTea = false;
        topping = false;
        topping2 = false;
        topping3 = false;
        

    }

    public void ResetBoba()
    {
        ice = false;
        milkTea = false;
        topping = false; 
        topping2 = false;
        topping3 = false;
        
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = Resources.Load<Sprite>("Sprites/BobaCupSprite1");
    }
    
    void Update()
    {
    
    
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        audioManager.PlaySFX(audioManager.putTopping);
        if (other.CompareTag("MilkTea"))
        {
            Debug.Log("Detected MilkTea!");
            milkTea = true;
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (ice && topping)
            {
                sr.sprite = Resources.Load<Sprite>("Sprites/BobaCupSprite6");
            }
            else if (ice && topping2)
            {
                sr.sprite = Resources.Load<Sprite>("Sprites/BobaCupSprite11");
            }
            else if (ice && topping3)
            {
                sr.sprite = Resources.Load<Sprite>("Sprites/BobaCupSprite14");
            }
            else if (ice)
            {
                sr.sprite = Resources.Load<Sprite>("Sprites/BobaCupSprite7");
            }
            else if (topping)
            {
                sr.sprite = Resources.Load<Sprite>("Sprites/BobaCupSprite8");
            }
            else if (topping2)
            {
                sr.sprite = Resources.Load<Sprite>("Sprites/BobaCupSprite12");
            }
            else if (topping3)
            {
                sr.sprite = Resources.Load<Sprite>("Sprites/BobaCupSprite15");
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
            else if (milkTea && topping2)
            {

                sr.sprite = Resources.Load<Sprite>("Sprites/BobaCupSprite11");
            }
            else if (milkTea && topping3)
            {

                sr.sprite = Resources.Load<Sprite>("Sprites/BobaCupSprite14");
            }
            else if (milkTea)
            {
                sr.sprite = Resources.Load<Sprite>("Sprites/BobaCupSprite7");
            }

            else if (topping)
            {
                sr.sprite = Resources.Load<Sprite>("Sprites/BobaCupSprite3");
            }

            else if (topping2)
            {
                sr.sprite = Resources.Load<Sprite>("Sprites/BobaCupSprite10");
            }
            else if (topping3)
            {
                sr.sprite = Resources.Load<Sprite>("Sprites/BobaCupSprite17");
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
            topping2 = false;
            topping3 = false;
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
        else if (other.CompareTag("Topping2"))
        {
            Debug.Log("Detected Topping!");
            topping2 = true;
            topping = false;
            topping3 = false;
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (milkTea && ice)
            {
                sr.sprite = Resources.Load<Sprite>("Sprites/BobaCupSprite11");
            }
            else if (milkTea)
            {
                sr.sprite = Resources.Load<Sprite>("Sprites/BobaCupSprite12");
            }
            else if (ice)
            {
                sr.sprite = Resources.Load<Sprite>("Sprites/BobaCupSprite10");
            }

            else
            {
                sr.sprite = Resources.Load<Sprite>("Sprites/BobaCupSprite9");
            }

        }
        else if (other.CompareTag("Topping3"))
        {
            Debug.Log("Detected Topping!");
            topping3 = true;
            topping = false;
            topping2 = false;
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (milkTea && ice)
            {
                sr.sprite = Resources.Load<Sprite>("Sprites/BobaCupSprite14");
            }
            else if (milkTea)
            {
                sr.sprite = Resources.Load<Sprite>("Sprites/BobaCupSprite15");
            }
            else if (ice)
            {
                sr.sprite = Resources.Load<Sprite>("Sprites/BobaCupSprite17");
            }

            else
            {
                sr.sprite = Resources.Load<Sprite>("Sprites/BobaCupSprite16");
            }

        }
    }
    
}
