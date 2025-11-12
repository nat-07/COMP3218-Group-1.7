using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject moveManager;
    public static bool isTutorial = false;
    public static int currentTutorialStep = -1; // -1 = not started, 0+ = ingredient steps

    [Header("Tutorial UI")]
    public TextMeshProUGUI tutorialText;
    public GameObject tutorialBackground;
    public GameObject samIcon;

    public GameObject[] hearts;

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

    public string[] tutorialMessages = new string[]
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
        for (int i = 0; i < tutorialMessages.Length; i++)
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
        Debug.Log(currentTutorialStep);
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
            milkTea.GetComponent<DragTutorial>().makeActive();
            mt.color = Color.green;

        }
        if (bobaCup.GetComponent<SpriteRenderer>().sprite == firstStepCup && currentTutorialStep == 0)
        {
            milkTea.GetComponent<DragTutorial>().makeNotActive();
            SpriteRenderer mt = milkTea.GetComponent<SpriteRenderer>();
            mt.color = Color.white;
            AdvanceTutorialStep();


        }

        if (currentTutorialStep == 1)
        {

            tutorialText.text = "Now the Ice!";
            ice.GetComponent<DragTutorial>().makeActive();
            SpriteRenderer ic = ice.GetComponent<SpriteRenderer>();
            ic.color = Color.green;

        }
        if (bobaCup.GetComponent<SpriteRenderer>().sprite == secondStepCup && currentTutorialStep == 1)
        {
            SpriteRenderer ic = ice.GetComponent<SpriteRenderer>();
            ice.GetComponent<DragTutorial>().makeNotActive();
            ic.color = Color.white;
            AdvanceTutorialStep();
        }

        if (currentTutorialStep == 2)
        {
            tutorialText.text = "We musnt' forget the boba!!";
            topping.GetComponent<DragTutorial>().makeActive();
            SpriteRenderer tp = topping.GetComponent<SpriteRenderer>();
            tp.color = Color.green;

        }
        if (bobaCup.GetComponent<SpriteRenderer>().sprite == thirdStepCup && currentTutorialStep == 2)
        {

            topping.GetComponent<DragTutorial>().makeNotActive();
            AdvanceTutorialStep();
            SpriteRenderer tp = topping.GetComponent<SpriteRenderer>();
            tp.color = Color.white;
        }

        if (currentTutorialStep == 3)
        {

            tutorialText.text = "When you're rady, press the DOWN KEY";

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
            tutorialText.text = "Press Space to stop the arrow. This is the angle";
            AdvanceTutorialStep();
        }
        if (currentTutorialStep == 6)
        {
            tutorialText.gameObject.SetActive(true);
            if (tutorialBackground != null) tutorialBackground.SetActive(true);
            if (samIcon != null) samIcon.SetActive(true);
            milkTea.GetComponent<DragTutorial>().makeActive();
            topping.GetComponent<DragTutorial>().makeActive();
            ice.GetComponent<DragTutorial>().makeActive();
            moveManager.GetComponent<MoveDownKeyTutorial>().enableDownKey();
            StartCoroutine(samYapsMore());

            DirectionPointerTutorial.tutorialMode = false;
            DirectionPointerTutorial.tutorialMode2 = true;
        
            AdvanceTutorialStep();

            
        }
        if (currentTutorialStep == 7)
        {
         AdvanceTutorialStep();

        }

        if (currentTutorialStep == 9)
        {
            DirectionPointerTutorial.tutorialMode2 = false;
            tutorialText.gameObject.SetActive(true);
            if (tutorialBackground != null) tutorialBackground.SetActive(true);
            if (samIcon != null) samIcon.SetActive(true);
            StartCoroutine(samYapsMore3());
            AdvanceTutorialStep();
        }
        if (currentTutorialStep == 11)
        {
            DirectionPointerTutorial.tutorialMode2 = false;
            tutorialText.gameObject.SetActive(true);
            if (tutorialBackground != null) tutorialBackground.SetActive(true);
            if (samIcon != null) samIcon.SetActive(true);
            StartCoroutine(samYapsMore4());
            AdvanceTutorialStep();
        }
        

    }
    IEnumerator samYapsMore()
    {
        tutorialText.text = "Nice! Now do this one yourself.";
        yield return new WaitForSeconds(4);
        tutorialText.gameObject.SetActive(false);
        if (tutorialBackground != null) tutorialBackground.SetActive(false);
        if (samIcon != null) samIcon.SetActive(false);
 

    }

    IEnumerator samYapsMore2()
    {

        tutorialText.text = "Try again.. this not what the customer wants.";
        yield return new WaitForSeconds(4);
        tutorialText.gameObject.SetActive(false);
        if (tutorialBackground != null) tutorialBackground.SetActive(false);
        if (samIcon != null) samIcon.SetActive(false);

    }
    IEnumerator samYapsMore3()
    {

        tutorialText.text = "This next alien is impatient. Be quick!";
        yield return new WaitForSeconds(3);

        tutorialText.gameObject.SetActive(false);
        if (tutorialBackground != null) tutorialBackground.SetActive(false);
        if (samIcon != null) samIcon.SetActive(false);

    }
IEnumerator samYapsMore4()
    {
        tutorialText.text = "Good! You're all set.";
        yield return new WaitForSeconds(3);
        tutorialText.text = "You only have 5 chances in this job";
        for (int i = 0; i < hearts.Length; i++)
        {
            Debug.Log("hearts");
            hearts[i].SetActive(true);
        }
        yield return new WaitForSeconds(3);
        tutorialText.text = "If you give the wrong order, or did not complete in time. You lose a life!";
        yield return new WaitForSeconds(3);
        tutorialText.text = "Good luck pal! Don't die!";
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("URP2DSceneTemplate");
        tutorialText.gameObject.SetActive(false);
        if (tutorialBackground != null) tutorialBackground.SetActive(false);
        if (samIcon != null) samIcon.SetActive(false);
 

    }
}

