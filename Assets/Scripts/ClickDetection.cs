using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDetection : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Convert mouse position to world coordinates
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Raycast to detect if we clicked on a 2D Rigidbody
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // Check if the ray hit a GameObject
            if (hit.collider != null)
            {
                // Check if the GameObject has a Rigidbody
                Rigidbody2D clickedRigidbody = hit.collider.GetComponent<Rigidbody2D>();
                if (clickedRigidbody != null)
                {
                    // Check if the GameObject has a Shopkeeper component
                    Shopkeeper shopkeeperComponent = hit.collider.GetComponent<Shopkeeper>();
                    if (shopkeeperComponent != null)
                    {
                        // The click was on a Rigidbody GameObject with a Shopkeeper component
                        Debug.Log("Clicked on a Rigidbody with Shopkeeper component!");
                    }
                }
            }
           
        }
    }
}
