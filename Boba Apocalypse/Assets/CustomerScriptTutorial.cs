using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;
using TMPro;

public class CustomerScriptTutorial : MonoBehaviour
{

    AudioManager audioManager;
    public Animator npcAnimator;
    public float moveSpeed = 2f;
    public float waitTime = 20f;
    public GameObject bobaObject;
    public GameObject timerObject;
    public Timer timer;
    public bool gotBoba;
    
    public int currentBobaItem; //id of the boba choice
    public Sprite[] bobaChoices;
    public TextMeshProUGUI tutorialText;
    public GameObject tutorialBackground;
    public GameObject samIcon;

    public bool hasBobaBeenSet;

    private int customerNumber = 0;

    private enum State { MovingToCenter, Waiting, MovingToExit, GotBoba }
    private State currentState = State.MovingToCenter;
    private float waitTimer = 0f;
    private float finalX;
    private float finalY;
    private int nextBoba;
    private Rigidbody2D rb;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    void Start()
    {
        hasBobaBeenSet = false;
        finalY = 0.25f;
        finalX = 0;
        if (customerNumber > 0)
        {
            finalY = Random.Range(0.25f, 1f);
            finalX = Random.Range(-6f, 8f);
        }
    
        transform.position = new Vector3(-10f, finalY, transform.position.z);
        transform.rotation = Quaternion.identity;
        currentState = State.MovingToCenter;
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        timer.remainingTime = 100f;

        //HIDE BOBA 
        if (bobaObject != null)
        {
            bobaObject.SetActive(false);
        }

        // Hide timer during tutorial
        if (timerObject != null)
        {
            if (TutorialManager.isTutorial)
            {
                timerObject.SetActive(false); // Don't show timer in tutorial
            }
            else
            {
                timerObject.SetActive(false); // Will show later
            }
        }
    }

    void Update()
    {
        
        switch (currentState)
        {
            case State.MovingToCenter:
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                if (transform.position.x >= finalX)
                {
                    npcAnimator.SetTrigger("stopMoving");
                    currentState = State.Waiting;
                    waitTimer = 0f;

                    // Show at center
                    if (bobaObject != null)
                    {
                        bobaObject.SetActive(true);
                    }

                    // Only show timer if NOT in tutorial
                    if (customerNumber == 2)
                    {
                        timerObject.SetActive(true);
                        waitTime = 20f;
                        timer.remainingTime = waitTime;
                    }
                }
                break;

            case State.Waiting:
                rb.bodyType = RigidbodyType2D.Static;
                if (waitTimer == 0f && customerNumber > 0 && !hasBobaBeenSet)
                {
                    setBobaItem();
                    hasBobaBeenSet = true;
                }

                // Only count down timer if NOT in tutorial mode
                if (customerNumber == 2)
                {
                    waitTimer += Time.deltaTime;
                    if (waitTimer >= waitTime)
                    {
                        audioManager.PlaySFX(audioManager.fail);
                        npcAnimator.ResetTrigger("stopMoving");
                        npcAnimator.SetTrigger("fail");
                        tutorialText.gameObject.SetActive(true);
                    if (tutorialBackground != null) tutorialBackground.SetActive(true);
                    if (samIcon != null) samIcon.SetActive(true);
                    StartCoroutine(samYapsMore());
                        currentState = State.MovingToExit;

                        if (bobaObject != null)
                        {
                            bobaObject.SetActive(false);
                        }
                    }
                }
                break;


            case State.MovingToExit:
           
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

                if (transform.position.x > 10f)
                {
                    npcAnimator.ResetTrigger("fail");
                   
                    Start();
                }
                break;

            case State.GotBoba:
                
                timer.remainingTime = 100f;
                if (bobaObject != null)
                {
                    bobaObject.SetActive(false);
                }
                if (timerObject != null)
                {
                    timerObject.SetActive(false);
                }
                
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

                if (transform.position.x > 10f)
                {
                    npcAnimator.ResetTrigger("hitSuccess");
                    customerNumber += 1;

                    if (customerNumber == 3)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                       
                             Start();
                    }
                }
                break;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("alien") && currentState == State.MovingToCenter && this.transform.position.x > -7.32)
        {
            npcAnimator.SetTrigger("stopMoving");
            currentState = State.Waiting;
            waitTimer = 0f;

            // Show at center
            if (bobaObject != null)
            {
                bobaObject.SetActive(true);
            }
            if (timerObject != null)
            {
                timerObject.SetActive(true);
                waitTime = 20f;
                timer.remainingTime = waitTime;
            }
        }
        // Check if we collided with an object tagged "Square"
        if (collision.gameObject.CompareTag("Square") && currentState == State.Waiting)
        {
           
        
            // Get the color from the Renderer material
            Color squareColor = collision.gameObject.GetComponent<Renderer>().material.color;
            Debug.Log("Collided with a square! Its color is: " + squareColor);
            gotBoba = true;
            npcAnimator.ResetTrigger("stopMoving");
            if (collision.gameObject.GetComponent<SpriteRenderer>().sprite == bobaObject.GetComponent<SpriteRenderer>().sprite)
            {
                npcAnimator.SetTrigger("hitSuccess");
                audioManager.PlaySFX(audioManager.success);
                currentState = State.GotBoba;
            }
            else
            {
                audioManager.PlaySFX(audioManager.fail);
                npcAnimator.ResetTrigger("stopMoving");
                npcAnimator.SetTrigger("fail");
                tutorialText.gameObject.SetActive(true);
                    if (tutorialBackground != null) tutorialBackground.SetActive(true);
                    if (samIcon != null) samIcon.SetActive(true);
                    StartCoroutine(samYapsMore2());
                currentState = State.MovingToExit;
            }

        }

    }

    void setBobaItem()
    {


        if (bobaObject != null)
        {
            nextBoba = (int)Random.Range(0, 4);
             bobaObject.GetComponent<SpriteRenderer>().sprite = bobaChoices[nextBoba];


        }


    }
    IEnumerator samYapsMore()
    {

        tutorialText.text = "You didn't complete the order in time. The alien will storm off.";
        yield return new WaitForSeconds(3);
        tutorialText.text = "Don't worry, try this next Alien";
        yield return new WaitForSeconds(3);
        tutorialText.gameObject.SetActive(false);
        if (tutorialBackground != null) tutorialBackground.SetActive(false);
        if (samIcon != null) samIcon.SetActive(false);

    }

 IEnumerator samYapsMore2()
{

        tutorialText.text = "You got the order wrong.. I'm in trouble now.";
        yield return new WaitForSeconds(3);
        tutorialText.text = "Don't worry, try this next Alien";
        yield return new WaitForSeconds(3);
        tutorialText.gameObject.SetActive(false);
        if (tutorialBackground != null) tutorialBackground.SetActive(false);
        if (samIcon != null) samIcon.SetActive(false);
    
}


}