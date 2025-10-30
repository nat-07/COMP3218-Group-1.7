using UnityEngine;

public class OnCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if we collided with an object tagged "Square"
        if (collision.gameObject.CompareTag("Square"))
        {
            // Get the color from the Renderer material
            Color squareColor = collision.gameObject.GetComponent<Renderer>().material.color;
            Debug.Log("Collided with a square! Its color is: " + squareColor);
            
        }

    }
}
