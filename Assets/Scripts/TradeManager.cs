using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TradeManager : MonoBehaviour
{
    public List<AudioClip> BuyAudioClips;
    public AudioClip SellClip;
    public AudioSource audioSource;
    public List<Item> AllItems;
    public List<ItemElement> DisplayItems;



    public void SellItems()
    {
        audioSource.PlayOneShot(SellClip);
    }
    private void BuyItems()
    {
        audioSource.PlayOneShot(BuyAudioClips[UnityEngine.Random.Range(0, BuyAudioClips.Count)]);

    }

  
}
