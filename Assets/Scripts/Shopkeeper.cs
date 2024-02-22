using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSourceFx;

    public AudioClip welcomeAudio;

    private void Start()
    {
        animator.Play("Rogue_idle_01");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        audioSourceFx.PlayOneShot(welcomeAudio);
    }
}
