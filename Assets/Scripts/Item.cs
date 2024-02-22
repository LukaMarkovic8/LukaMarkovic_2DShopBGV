using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item")]
public class Item : ScriptableObject
{
    public string Name;
    public string Description;
    public ItemType Type;
    public Sprite Sprite;
    public int Price;

}

