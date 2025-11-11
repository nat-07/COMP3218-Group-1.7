using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightTopping : MonoBehaviour
{
    [Header("Ingredient Settings")]
    public int tutorialStepRequired = 0; // Which step this ingredient should be highlighted
    public string normalSpritePath = "Sprites/Topping1";
    public string highlightSpritePath = "Sprites/Topping1Highlight";

    private SpriteRenderer sr;
    private bool isHighlighted = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    /* void Update()
    {
        // Auto-highlight if this is the current tutorial step
        if (TutorialManager.isTutorial && TutorialManager.currentTutorialStep == tutorialStepRequired)
        {
            if (!isHighlighted)
            {
                Highlight();
            }
        }
        else
        {
            if (isHighlighted && TutorialManager.currentTutorialStep != tutorialStepRequired)
            {
                RemoveHighlight();
            }
        }
    }
 */
    void OnMouseEnter()
    {
        // Only allow hover highlight if it's this ingredient's turn or tutorial is off
        if (!TutorialManager.isTutorial || TutorialManager.currentTutorialStep == tutorialStepRequired)
        {
            Highlight();
        }
    }

    void OnMouseExit()
    {
        // Keep highlight during tutorial if it's the current step
        if (TutorialManager.isTutorial && TutorialManager.currentTutorialStep == tutorialStepRequired)
        {
            return; // Don't remove highlight
        }

        RemoveHighlight();
    }

    void Highlight()
    {
        sr.sprite = Resources.Load<Sprite>(highlightSpritePath);
        isHighlighted = true;
    }

    void RemoveHighlight()
    {
        sr.sprite = Resources.Load<Sprite>(normalSpritePath);
        isHighlighted = false;
    }
}