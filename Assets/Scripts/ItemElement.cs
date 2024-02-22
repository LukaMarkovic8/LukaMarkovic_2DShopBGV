using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemElement : MonoBehaviour
{
    public DisplayItem DisplayItem;
    public Item item;
    public Image Image;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI PriceText;

   
    public void SetElement(Item item)
    {
        this.item = item;
        Image.sprite = item.Sprite;
        NameText.text = item.Name;
        PriceText.text = item.Price.ToString();
    }
   
    public void ChooseItem()
    {
        DisplayItem.SetElement(item);

    }

}
