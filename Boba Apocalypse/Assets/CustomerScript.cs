using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;

public class CustomerScript : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float waitTime = 10f;
    public GameObject bobaObject;
    public GameObject timerObject;
    public Timer timer;
    public HP hp;
    public ScoreSystem scoreSystem;
    public bool gotBoba;

    private enum State { MovingToCenter, Waiting, MovingToExit, GotBoba }
    private State currentState = State.MovingToCenter;
    private float waitTimer = 0f;
    private float finalX;
    private float finalY;
    

    void Start()
    {
        finalY = Random.Range(-1f, 1f);
        finalX = Random.Range(-6f, 8f);
        Debug.Log(finalX);
        transform.position = new Vector3(-10f, finalY, transform.position.z);
        transform.rotation = Quaternion.identity;
        currentState = State.MovingToCenter;

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
                if (transform.position.x >= finalX)
                {
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
                        waitTime = 10f;
                        timer.remainingTime = waitTime;
                    }
                }
                break;

            case State.Waiting:
                waitTimer += Time.deltaTime;

                if (waitTimer >= waitTime)
                {
                    hp.ReduceHP();
                    currentState = State.MovingToExit;


                    if (bobaObject != null)
                    {
                        bobaObject.SetActive(false);
                    }

                }
                break;

            case State.MovingToExit:
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

                if (transform.position.x > 10f)
                {
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
                    Start();
                }
                break;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if we collided with an object tagged "Square"
        if (collision.gameObject.CompareTag("Square") && currentState == State.Waiting)
        {
            // Get the color from the Renderer material
            Color squareColor = collision.gameObject.GetComponent<Renderer>().material.color;
            Debug.Log("Collided with a square! Its color is: " + squareColor);
            gotBoba = true;
            currentState = State.GotBoba;
            scoreSystem.AddScore();
        }

    }


}