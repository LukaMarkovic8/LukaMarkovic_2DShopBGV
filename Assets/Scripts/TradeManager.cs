using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TradeManager : MonoBehaviour
{
    public UIController UIController;
    public TextMeshProUGUI warningText;

    public DisplayItem DisplayItemBuy;
    public DisplayItem DisplayItemSell;
    public List<AudioClip> BuyAudioClips;
    public AudioClip SellClip;
    public AudioSource audioSource;
    [Header("Items")]
    public List<Item> HeadItems;
    public List<Item> ShoulderItems;
    public List<Item> ElbowItems;
    public List<Item> HandItems;
    public List<Item> TorsoItems;
    public List<Item> PelvisItems;
    public List<Item> LegItems;
    public List<Item> BootItems;


    ItemType itemsCategory;

    [Header("Sell")]
    public List<ItemElement> SellDisplayItems;

    [Header("SwichCategoryButtons")]
    public Button SellHeadButton;
    public Button SellShouldersButon;
    public Button SellElbowButton;
    public Button SellHand;
    public Button SellTorso;
    public Button SellPelvis;
    public Button SellLegs;
    public Button SellBoots;


    [Header("Buy")]
    public GameObject BuyButtonGo;
    public List<ItemElement> BuyDisplayItems;

    public Button BuyHeadButton;
    public Button BuyShouldersButon;
    public Button BuyElbowButton;
    public Button BuyHand;
    public Button BuyTorso;
    public Button BuyPelvis;
    public Button BuyLegs;
    public Button BuyBoots;



    private void OnEnable()
    {
        itemsCategory = ItemType.Torso;
        SetBuyItems();
    }


    public void CheckIfItemIsAvailableToBuy()
    {
        if (DataController.dataController.playerData.itemsOwned.Contains(((int)DisplayItemBuy.item.Type, DisplayItemBuy.item.Id)))
        {
            BuyButtonGo.SetActive(false);
            warningText.gameObject.SetActive(true);
            warningText.text = "Player Alredy Has This Item";
            return;
        }
        if (DisplayItemBuy.item.Price > DataController.dataController.playerData.playerBalance)
        {
            BuyButtonGo.SetActive(false);
            warningText.gameObject.SetActive(true);
            warningText.text = "Not Enough money";
            return;
        }
        warningText.gameObject.SetActive(false);
        BuyButtonGo.SetActive(true);
    }

    public void CheckIfItemIsAvailableForSale()
    {

        if (DataController.dataController.playerData.itemsOwned.Contains(((int)DisplayItemBuy.item.Type, DisplayItemBuy.item.Id)))
        {



        }

    }

    public void SellItems()
    {
        //handeling data
        DataController.dataController.SetPlayerBalance(DisplayItemBuy.item.Price);
        DataController.dataController.RemoveOwnedItem(DisplayItemBuy.item);

        audioSource.PlayOneShot(SellClip);
        UIController.SetPlayerBalance();

    }
    public void BuyItems()
    {
        //handeling data
        DataController.dataController.AddToOwnedItems(DisplayItemBuy.item);
        DataController.dataController.SetPlayerBalance(-DisplayItemBuy.item.Price);

        audioSource.PlayOneShot(BuyAudioClips[UnityEngine.Random.Range(0, BuyAudioClips.Count)]);
        //UI update
        UIController.SetPlayerBalance();
        CheckIfItemIsAvailableToBuy();

    }



    public void SetCategory(int type)
    {
        itemsCategory = (ItemType)type;
        SetBuyItems();
    }
    private void SetBuyItems()
    {
        List<Item> items = GetCategotyItems();

        for (int i = 0; i < BuyDisplayItems.Count; i++)
        {
            //Checking if there are items to display
            if (i < items.Count)
            {
                BuyDisplayItems[i].gameObject.SetActive(true);
                BuyDisplayItems[i].SetElement(items[i]);
            }
            else
            {
                BuyDisplayItems[i].gameObject.SetActive(false);
            }
        }
    }

    private void SetSellItems()
    {

    }


    //Getting the item list for relevant category
    private List<Item> GetCategotyItems()
    {
        switch (itemsCategory)
        {
            case ItemType.Head:
                return HeadItems;
            case ItemType.Shoulders:
                return ShoulderItems;
            case ItemType.Elbow:
                return ElbowItems;
            case ItemType.Hands:
                return HandItems;
            case ItemType.Torso:
                return TorsoItems;
            case ItemType.Pelvis:
                return PelvisItems;
            case ItemType.Legs:
                return LegItems;
            case ItemType.Boots:
                return BootItems;
            default:
                Debug.LogError("Invalid type" + itemsCategory.ToString());
                return null;

        }

    }

}
