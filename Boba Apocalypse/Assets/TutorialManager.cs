using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public static bool isTutorial = false;
    public static int currentTutorialStep = -1; // -1 = not started, 0+ = ingredient steps

    [Header("Tutorial UI")]
    public TextMeshProUGUI tutorialText;
    public GameObject tutorialBackground;
    public GameObject samIcon;

    [Header("Settings")]
    public float displayTime = 5f;

    [Header("Tutorial Messages")]
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
        // Show initial tutorial messages
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
}