using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum MoveDirection
    {
        none,
        vertical,
        left,
        right
    }

    public Rigidbody2D rb;

    public Animator animator;
    public GameObject playerHolder;


    private float moveSpeed = 5f;
    public Vector2 movement;
    public Vector2 mousePos;
    private bool isMoving;
    public AudioSource walkingAudio;
    private MoveDirection moveDirection;


    [Header("SpriteRenderers")]
    public SpriteRenderer PelvisSprite;
    public SpriteRenderer HeadSprite;
    public SpriteRenderer TorsoSprite;
    public SpriteRenderer LeftShoulderSprite;
    public SpriteRenderer RightShoulderSprite;
    public SpriteRenderer LeftElbowSptrie;
    public SpriteRenderer RightElbowSptrie;
    public SpriteRenderer RightWristSprite;
    public SpriteRenderer LeftWristSprite;
    public SpriteRenderer RightLegSprite;
    public SpriteRenderer LeftLegSprite;
    public SpriteRenderer RightBootSprite;
    public SpriteRenderer LeftBootSprite;




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
        moveDirection = GetMoveDirection(movement);



        if (IsMoving())
        {
            walkingAudio.enabled = true;

            AnimatorClipInfo[] animatorinfo = animator.GetCurrentAnimatorClipInfo(0);
            string current_animation = animatorinfo[0].clip.name;
            if (current_animation != "Rogue_walk_01")
            {
                animator.Play("Rogue_walk_01");
            }
        }
        else
        {
            AnimatorClipInfo[] animatorinfo = animator.GetCurrentAnimatorClipInfo(0);
            string current_animation = animatorinfo[0].clip.name;
            if (current_animation != "Rogue_idle_01")
            {
                animator.Play("Rogue_idle_01");
            }

            walkingAudio.enabled = false;
        }
    }


    MoveDirection GetMoveDirection(Vector2 movement)
    {
        //if (Mathf.Abs(movement.y) > Mathf.Abs(movement.x))
        //{
        //    return MoveDirection.vertical;

        //}
        //else
        if (movement.x < 0f)
        {
            if (playerHolder.transform.localScale.x > 0)
            {
                playerHolder.transform.localScale = new Vector3(-0.4f, 0.4f, 1f);

            }


            return MoveDirection.left;
        }
        else if (movement.x > 0f)
        {
            if (playerHolder.transform.localScale.x < 0)
            {
                playerHolder.transform.localScale = new Vector3(0.4f, 0.4f, 1f);
            }
            return MoveDirection.right;

        }
        return MoveDirection.none;

    }


    bool IsMoving()
    {
        // Check if velocity is greater than a small threshold
        return movement.magnitude > 0.001f;
    }
}
