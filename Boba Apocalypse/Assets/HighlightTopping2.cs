using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightTopping2 : MonoBehaviour
{
    // Start is called before the first frame update
    void OnMouseEnter ()
{
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = Resources.Load<Sprite>("Sprites/Topping2Highlight");
}

void OnMouseExit ()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = Resources.Load<Sprite>("Sprites/Topping2");
}
}
