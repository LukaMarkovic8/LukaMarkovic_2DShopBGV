using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TradeManager : MonoBehaviour
{
    public enum TradeScreenType
    {
        Buy,
        Sell
    }

    public TradeScreenType currentScreen;
    public UIController UIController;
    public TextMeshProUGUI warningText;

    public DisplayItem DisplayItemUI;
    public List<AudioClip> BuyAudioClips;
    public AudioClip SellClip;
    public AudioSource audioSource;

    ItemType itemsCategory;

    [Header("Sell screen")]
    public GameObject SellButtonGo;
    public Button OpenBuyScreenButton;

    [Header("BuyScreen")]
    public GameObject BuyButtonGo;
    public List<ItemElement> DisplayItems;
    public Button OpenSellScreenButton;

    [Header("SwichCategoryButtons")]
    public Button HeadButton;
    public Button ShouldersButon;
    public Button ElbowButton;
    public Button Hand;
    public Button Torso;
    public Button Pelvis;
    public Button Legs;
    public Button Boots;

    [Header("Items")]
    public List<Item> HeadItems;
    public List<Item> ShoulderItems;
    public List<Item> ElbowItems;
    public List<Item> HandItems;
    public List<Item> TorsoItems;
    public List<Item> PelvisItems;
    public List<Item> LegItems;
    public List<Item> BootItems;

    private void OnEnable()
    {
        OpenBuyScreen();
    }

    public void OpenBuyScreen()
    {
        itemsCategory = ItemType.Torso;
        currentScreen = TradeScreenType.Buy;
        SellButtonGo.SetActive(false);
        BuyButtonGo.SetActive(true);
        DisplayItemUI.SetEmpty();
        OpenSellScreenButton.gameObject.SetActive(true);
        OpenBuyScreenButton.gameObject.SetActive(false);
        SetBuyItems();

    }
    public void OpenSellScreen()
    {
        itemsCategory = ItemType.Torso;
        currentScreen = TradeScreenType.Sell;
        SellButtonGo.SetActive(true);
        BuyButtonGo.SetActive(false);
        DisplayItemUI.SetEmpty();
        OpenSellScreenButton.gameObject.SetActive(false);
        OpenBuyScreenButton.gameObject.SetActive(true);
        warningText.gameObject.SetActive (false);
        SetSellItems();



    }
    public void CheckIfItemIsAvailableToBuy()
    {
        //checking if we alredy own it
        if (DataController.dataController.playerData.itemsOwned.Contains(((int)DisplayItemUI.item.Type, DisplayItemUI.item.Id)))
        {
            BuyButtonGo.SetActive(false);
            warningText.gameObject.SetActive(true);
            warningText.text = "Player Alredy Has This Item";
            return;
        }
        //checking if we have cash to buy
        if (DisplayItemUI.item.Price > DataController.dataController.playerData.playerBalance)
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

        if (DataController.dataController.playerData.itemsEquipped.Contains(((int)DisplayItemUI.item.Type, DisplayItemUI.item.Id)))
        {
            SellButtonGo.SetActive(false);
            warningText.gameObject.SetActive(true);
            warningText.text = "Can't sell an equipped item";
            return;
        }
        warningText.gameObject.SetActive(false);
    }

    public void SellItems()
    {
        //handeling data
        DataController.dataController.SetPlayerBalance(DisplayItemUI.item.Price);
        DataController.dataController.RemoveOwnedItem(DisplayItemUI.item);
        SellButtonGo.SetActive(false);
        DisplayItemUI.SetEmpty();
        audioSource.PlayOneShot(SellClip);
        UIController.SetPlayerBalance();
        SetSellItems();

    }
    public void BuyItems()
    {
        //handeling data
        DataController.dataController.AddToOwnedItems(DisplayItemUI.item);
        DataController.dataController.SetPlayerBalance(-DisplayItemUI.item.Price);

        audioSource.PlayOneShot(BuyAudioClips[UnityEngine.Random.Range(0, BuyAudioClips.Count)]);
        //UI update
        UIController.SetPlayerBalance();
        CheckIfItemIsAvailableToBuy();

    }



    public void SetCategory(int type)
    {
        itemsCategory = (ItemType)type;
        if (currentScreen == TradeScreenType.Buy)
        {

            SetBuyItems();
        }
        else
        {
            SetSellItems();
        }
    }
    private void SetBuyItems()
    {
        List<Item> items = GetCategotyItems();

        for (int i = 0; i < DisplayItems.Count; i++)
        {
            //Checking if there are items to display
            if (i < items.Count)
            {
                DisplayItems[i].gameObject.SetActive(true);
                DisplayItems[i].SetElement(items[i]);
            }
            else
            {
                DisplayItems[i].gameObject.SetActive(false);
            }
        }
    }

    private void SetSellItems()
    {
        List<int> items = DataController.dataController.GetItemsForSaleByCategory(itemsCategory);

        List<Item> allItems = GetCategotyItems();


        if (items.Count == 0)
        {
            for (int i = 0; i < DisplayItems.Count; i++)
            {
                DisplayItems[i].gameObject.SetActive(false);
            }
            SellButtonGo.SetActive(false);
            return;
        }
        SellButtonGo.SetActive(true);
        for (int i = 0; i < DisplayItems.Count; i++)
        {
            if (items.Contains(allItems[i].Id))
            {
                DisplayItems[i].gameObject.SetActive(true);
                DisplayItems[i].SetElement(allItems[i]);

            }
            else
            {
                DisplayItems[i].gameObject.SetActive(false);
            }
        }
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
