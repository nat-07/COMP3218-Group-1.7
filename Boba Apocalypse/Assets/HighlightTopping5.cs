using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightTopping5 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnMouseEnter ()
{
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = Resources.Load<Sprite>("Sprites/Topping5Highlight");
}

void OnMouseExit ()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = Resources.Load<Sprite>("Sprites/Topping5");
}
}