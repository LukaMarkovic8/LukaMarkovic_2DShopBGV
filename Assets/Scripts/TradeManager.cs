using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeManager : MonoBehaviour
{
    public List<AudioClip> BuyAudioClips;
    public AudioClip SellClip;
    public AudioSource audioSource;
    public void SellItems()
    {

    }

    private void BuyItems()
    {

        audioSource.PlayOneShot(BuyAudioClips[UnityEngine.Random.Range(0, BuyAudioClips.Count)]);

    }
}
