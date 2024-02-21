using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;

    private float moveSpeed = 5f;
    private Vector2 movement;
    public Vector2 mousePos;
    public bool isMoving;
    public AudioSource walkingAudio;

    void Update()
    {
        movement.x = Input.GetAxis("Horizontal") * moveSpeed;
        movement.y = Input.GetAxis("Vertical") * moveSpeed;

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {

            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }


    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);


        if (IsMoving())
        {
            walkingAudio.enabled = true;
        }
        else
        {
            walkingAudio.enabled = false;
        }
    }



    bool IsMoving()
    {
        // Check if velocity is greater than a small threshold
        return movement.magnitude > 0.001f;
    }
}
