using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayItem : MonoBehaviour
{
    public TradeManager TradeManager;
    public Item emptyItem;

    public Item item;
    public Image Image;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI PriceText;


    private void OnEnable()
    {
        Image.sprite = emptyItem.Sprite;
        NameText.text = emptyItem.Name;
        PriceText.text = emptyItem.Price.ToString();
    }

    public void SetElement(Item item)
    {

        this.item = item;
        Image.sprite = item.Sprite;
        NameText.text = item.Name;
        PriceText.text = "$"+item.Price.ToString();
        TradeManager.CheckIfItemIsAvailableToBuy();

    }


}
