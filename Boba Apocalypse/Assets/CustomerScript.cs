using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class CustomerScript : MonoBehaviour
{

    AudioManager audioManager;
    public Animator npcAnimator;
    public float moveSpeed = 5f;
    public float waitTime = 20f;
    public GameObject bobaObject;
    public GameObject timerObject;
    public Timer timer;
    public HP hp;
    public ScoreSystem scoreSystem;
    public bool gotBoba;
    public int currentBobaItem; //id of the boba choice
    public Sprite[] bobaChoices;

    private enum State { MovingToCenter, Waiting, MovingToExit, GotBoba }
    private State currentState = State.MovingToCenter;
    private float waitTimer = 0f;
    private float finalX;
    private float finalY;
    private int nextBoba;
    private Rigidbody2D rb;
    private bool alienWaiting;
    private bool alienPaused = false;
   

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    

    void Start()
    {
        if (this.name != "Customer" && scoreSystem.getBeginningLevel() && !alienPaused)
        {
            Debug.Log("Only 1 Alien with New Topping");
            StartCoroutine(OnlyRunAlienWithNewTopping());
        }
       
        finalY = Random.Range(0.25f, 1f);
        finalX = Random.Range(-6f, 8f);
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
        if (timerObject != null)
        {
            timerObject.SetActive(false);
        }
    }

    void Update()
    {

        switch (currentState)
        {
            case State.MovingToCenter:
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                if (transform.position.x > 3f)
                {
                    rb.bodyType = RigidbodyType2D.Dynamic;
                }
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
                    if (timerObject != null)
                    {
                        timerObject.SetActive(true);

                        if (scoreSystem.getBeginningLevel() || scoreSystem.getLevel() == 1 || scoreSystem.getLevel() == 2)
                        {
                            waitTime = 30f;
                        }else if (scoreSystem.getLevel() <= 5 && scoreSystem.getLevel() >= 3)
                        {
                            waitTime = Random.Range(13f, 18f);
                        }
                        else
                        {
                            waitTime = Random.Range(10f, 13f);
                        }
                        timer.remainingTime = waitTime;
                    }
                }
                break;

            case State.Waiting:
                rb.bodyType = RigidbodyType2D.Static;
                if (waitTimer == 0f)
                {
                    setBobaItem();
                }
                waitTimer += Time.deltaTime;
                if (waitTimer >= waitTime)
                {
                    audioManager.PlaySFX(audioManager.fail);
                    hp.ReduceHP();
                    npcAnimator.ResetTrigger("stopMoving");
                    npcAnimator.SetTrigger("fail");
                    currentState = State.MovingToExit;


                    if (bobaObject != null)
                    {
                        bobaObject.SetActive(false);

                    }

                }
                break;

            case State.MovingToExit:
                if (bobaObject != null)
                {
                    bobaObject.SetActive(false);
                }
                if (timerObject != null)
                {
                    timerObject.SetActive(false);
                }
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

                if (transform.position.x > 10f && !alienWaiting)
                {
                    npcAnimator.ResetTrigger("fail");
                    if (this.name != "Customer")
                    {
                        int next = Random.Range(0, 2);
                        if (next == 0)
                        {
                            Debug.Log("Customer skipped this round");
                            StartCoroutine((WaitOneRound()));
                        }
                    }
                    else
                    {
                        Start();
                    }
                 

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

                if (transform.position.x > 10f && !alienWaiting)
                {
                    npcAnimator.ResetTrigger("hitSuccess");
                    if (this.name != "Customer")
                    {

                        int next = Random.Range(0, 2);

                        if (next == 0)
                        {
                            Debug.Log("Customer skipped this round");
                            StartCoroutine((WaitOneRound()));
                        }
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
        if (collision.gameObject.CompareTag("alien") && currentState == State.MovingToCenter && this.transform.position.x >= -7.32)
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
        else if (collision.gameObject.CompareTag("alien") && currentState == State.MovingToCenter && this.transform.position.x < -7.32)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            finalX = Random.Range(0f, 7f);
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
                scoreSystem.AddScore();
            }
            else
            {
                audioManager.PlaySFX(audioManager.fail);
                hp.ReduceHP();
                npcAnimator.ResetTrigger("stopMoving");
                npcAnimator.SetTrigger("fail");
                currentState = State.MovingToExit;
            }

        }

    }

    void setBobaItem()
    {
        Debug.Log(scoreSystem.getBeginningLevel());

        if (bobaObject != null)
        { 
                if (scoreSystem.getBeginningLevel())
                {
                    switch (scoreSystem.getLevel())
                    {
                        case 2:
                            nextBoba = 5;
                            bobaObject.GetComponent<SpriteRenderer>().sprite = bobaChoices[nextBoba];
                            break;
                        case 4:
                            nextBoba = 7;
                            bobaObject.GetComponent<SpriteRenderer>().sprite = bobaChoices[nextBoba];
                            break;
                        case 6:
                            nextBoba = 9;
                            bobaObject.GetComponent<SpriteRenderer>().sprite = bobaChoices[nextBoba];
                            break;
                        case 8:
                            nextBoba = 11;
                            bobaObject.GetComponent<SpriteRenderer>().sprite = bobaChoices[nextBoba];
                            break;
                        case 10:
                            nextBoba = 13;
                            bobaObject.GetComponent<SpriteRenderer>().sprite = bobaChoices[nextBoba];
                            break;
                    }
                    scoreSystem.setBeginningLevelFalse();
            }
                else
                {
                    switch (ScoreSystem.currentUnlockedToppings)
                    {
                        case -1:
                            nextBoba = (int)Random.Range(0, 4);
                            bobaObject.GetComponent<SpriteRenderer>().sprite = bobaChoices[nextBoba];
                            break;
                        case 0:
                            nextBoba = (int)Random.Range(0, 6);
                            bobaObject.GetComponent<SpriteRenderer>().sprite = bobaChoices[nextBoba];
                            break;
                        case 1:
                            nextBoba = (int)Random.Range(0, 8);
                            bobaObject.GetComponent<SpriteRenderer>().sprite = bobaChoices[nextBoba];
                            break;
                        case 2:
                            nextBoba = (int)Random.Range(0, 10);
                            bobaObject.GetComponent<SpriteRenderer>().sprite = bobaChoices[nextBoba];
                            break;
                        case 3:
                            nextBoba = (int)Random.Range(0, 12);
                            bobaObject.GetComponent<SpriteRenderer>().sprite = bobaChoices[nextBoba];
                            break;
                        case 4:
                            nextBoba = (int)Random.Range(0, 14);
                            bobaObject.GetComponent<SpriteRenderer>().sprite = bobaChoices[nextBoba];
                            break;
                    }

            }
        }


    }

    IEnumerator WaitOneRound()
    {
        alienWaiting = true;
        yield return new WaitForSeconds(20);
        alienWaiting = false;
        Start();
    }

    IEnumerator OnlyRunAlienWithNewTopping()
    {
        alienPaused = true;
        yield return new WaitForSeconds(35);
        Debug.Log("Alien Finish Waiting");
        alienPaused = false;
        Start();

    }





}