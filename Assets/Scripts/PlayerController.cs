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
    }   

    private void FixedUpdate()
    {
        if (DataController.dataController==null)
        {
            return;
        }
        if (DataController.dataController.blockMoving) return;

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


    //Method that equips item on player and sends new item do Data controlled to save it
    public void EquipItem(Item item)
    {
        switch (item.Type)
        {
            case ItemType.Head:
                HeadSprite.sprite = item.Sprite;
                break;
            case ItemType.Shoulders:
                LeftShoulderSprite.sprite = item.SpriteL;
                RightShoulderSprite.sprite = item.SpriteR;
                break;
            case ItemType.Elbow:
                LeftElbowSptrie.sprite = item.SpriteL;
                RightElbowSptrie.sprite = item.SpriteR;
                break;
            case ItemType.Hands:
                LeftWristSprite.sprite = item.SpriteL;
                RightWristSprite.sprite = item.SpriteR;
                break;
            case ItemType.Torso:
                TorsoSprite.sprite = item.Sprite;
                break;
            case ItemType.Pelvis:
                PelvisSprite.sprite = item.Sprite;
                break;
            case ItemType.Legs:
                LeftLegSprite.sprite = item.SpriteL;
                RightLegSprite.sprite = item.SpriteR;
                break;
            case ItemType.Boots:
                LeftBootSprite.sprite = item.SpriteL;
                RightBootSprite.sprite = item.SpriteR;
                break;
        }
        //Update Data
        DataController.dataController.UpdateEquipedItems(item);

    }

}
