using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerScript : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float waitTime = 5f;
    public GameObject bobaObject;
    public GameObject timerObject;  

    private enum State { MovingToCenter, Waiting, MovingToExit }
    private State currentState = State.MovingToCenter;
    private float waitTimer = 0f;

    void Start()
    {
        transform.position = new Vector3(-10f, transform.position.y, transform.position.z);

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

                if (transform.position.x >= 0f)
                {
                    currentState = State.Waiting;
                    waitTimer = 0f;

                    // SHow at center
                    if (bobaObject != null)
                    {
                        bobaObject.SetActive(true);
                    }
                    if (timerObject != null)
                    {
                        timerObject.SetActive(true);
                    }
                }
                break;

            case State.Waiting:
                waitTimer += Time.deltaTime;

                if (waitTimer >= waitTime)
                {
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
                    Destroy(gameObject);
                }
                break;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if we collided with an object tagged "Square"
        if (collision.gameObject.CompareTag("Square"))
        {
            // Get the color from the Renderer material
            Color squareColor = collision.gameObject.GetComponent<Renderer>().material.color;
            Debug.Log("Collided with a square! Its color is: " + squareColor);
            Start();
            
        }

    }
}