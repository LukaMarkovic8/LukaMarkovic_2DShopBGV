using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemElement : MonoBehaviour
{
    public Item item;
    public Image Image;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI PriceText;

    private void Start()
    {
        SetElement();
    }
    public void SetElement()
    {
        Image.sprite = item.Sprite;
        NameText.text = item.Name;
        PriceText.text = item.Price.ToString();
    }
}
