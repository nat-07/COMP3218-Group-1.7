using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting.Dependencies.Sqlite;

public class TutorialManager : MonoBehaviour
{
    public GameObject moveManager;
    public static bool isTutorial = false;
    public static int currentTutorialStep = -1; // -1 = not started, 0+ = ingredient steps

    [Header("Tutorial UI")]
    public TextMeshProUGUI tutorialText;
    public GameObject tutorialBackground;
    public GameObject samIcon;

    [Header("Settings")]
    public float displayTime = 5f;

    [Header("Tutorial Messages")]

    public GameObject milkTea;
    public GameObject ice;



    public GameObject topping;

    public GameObject bobaCup;

    public Sprite firstStepCup;
    public Sprite secondStepCup;

    public Sprite thirdStepCup;

    private string[] tutorialMessages = new string[]
    {
        "Hello, you're new here! My name is Sam!",
        "Let me help so you won't die hehe...",
        "Make the Boba for the alien so they take mercy on you!",
        "Match the ingredients to make the correct boba tea!",
        "Let's try making one together!"
    };

    void Start()
    {
        isTutorial = true;
        currentTutorialStep = -1;

        if (tutorialText != null)
        {
            tutorialText.gameObject.SetActive(true);
            if (tutorialBackground != null) tutorialBackground.SetActive(true);
            if (samIcon != null) samIcon.SetActive(true);

            StartCoroutine(ShowMessages());
        }
    }

    IEnumerator ShowMessages()
    {
        //TODO: Change this back
        for (int i = 0; i < 0; i++)
        {

            tutorialText.text = "";
            yield return new WaitForSeconds(0.1f);
            tutorialText.text = tutorialMessages[i];
            yield return new WaitForSeconds(displayTime);
        }

        // Hide text UI after messages
        tutorialText.gameObject.SetActive(false);
        if (tutorialBackground != null) tutorialBackground.SetActive(false);
        if (samIcon != null) samIcon.SetActive(false);

        // Start ingredient highlighting tutorial
        currentTutorialStep = 0; // Start highlighting first ingredient
    }

    public static void AdvanceTutorialStep()
    {
        currentTutorialStep++;
        Debug.Log("Step increase");
    }

    public void CompleteTutorial()
    {
        isTutorial = false;
        currentTutorialStep = -1;
    }

    void OnDestroy()
    {
        isTutorial = false;
        currentTutorialStep = -1;
    }

    void Update()
    {
        if (currentTutorialStep == 0)
        {
            tutorialText.gameObject.SetActive(true);
            if (tutorialBackground != null) tutorialBackground.SetActive(true);
            if (samIcon != null) samIcon.SetActive(true);
            tutorialText.text = "Drag the Milk Tea to the cup in the middle";

            SpriteRenderer mt = milkTea.GetComponent<SpriteRenderer>();
            milkTea.GetComponent<Drag>().makeActive();
            mt.color = Color.green;

        }
        if (bobaCup.GetComponent<SpriteRenderer>().sprite == firstStepCup && currentTutorialStep == 0)
        {
            milkTea.GetComponent<Drag>().makeNotActive();
            SpriteRenderer mt = milkTea.GetComponent<SpriteRenderer>();
            mt.color = Color.white;
            AdvanceTutorialStep();


        }

        if (currentTutorialStep == 1)
        {

            tutorialText.text = "Now the Ice!";
            ice.GetComponent<Drag>().makeActive();
            SpriteRenderer ic = ice.GetComponent<SpriteRenderer>();
            ic.color = Color.green;

        }
        if (bobaCup.GetComponent<SpriteRenderer>().sprite == secondStepCup && currentTutorialStep == 1)
        {
            SpriteRenderer ic = ice.GetComponent<SpriteRenderer>();
            ice.GetComponent<Drag>().makeNotActive();
            ic.color = Color.white;
            AdvanceTutorialStep();
        }

        if (currentTutorialStep == 2)
        {
            tutorialText.text = "We musnt' forget the boba!!";
            topping.GetComponent<Drag>().makeActive();
            SpriteRenderer tp = topping.GetComponent<SpriteRenderer>();
            tp.color = Color.green;

        }
        if (bobaCup.GetComponent<SpriteRenderer>().sprite == thirdStepCup && currentTutorialStep == 2)
        {

            topping.GetComponent<Drag>().makeNotActive();
            AdvanceTutorialStep();
            SpriteRenderer tp = topping.GetComponent<SpriteRenderer>();
            tp.color = Color.white;
        }

        if (currentTutorialStep == 3)
        {

            tutorialText.text = "Now, press the DOWN ARROW";

            moveManager.GetComponent<MoveDownKeyTutorial>().enableDownKey();

        }

        if (currentTutorialStep == 3 && Input.GetKeyDown(KeyCode.DownArrow))
        {
            AdvanceTutorialStep();

        }
        if (currentTutorialStep == 4)
        {
            tutorialText.gameObject.SetActive(true);
            if (tutorialBackground != null) tutorialBackground.SetActive(true);
            if (samIcon != null) samIcon.SetActive(true);
            tutorialText.text = "We must aim the boba properly to the customer";
            AdvanceTutorialStep();
        }
        if (currentTutorialStep == 6)
        {
            milkTea.GetComponent<Drag>().makeActive();
            topping.GetComponent<Drag>().makeActive();
            ice.GetComponent<Drag>().makeActive();
            StartCoroutine(samYapsMore());
           
            AdvanceTutorialStep();
        }

    }
    IEnumerator samYapsMore()
{
        tutorialText.text = "Nice! Now do this yourself.";
        yield return new WaitForSeconds(4);
          tutorialText.gameObject.SetActive(false);
            if (tutorialBackground != null) tutorialBackground.SetActive(false);
            if (samIcon != null) samIcon.SetActive(false);
    
}
}

