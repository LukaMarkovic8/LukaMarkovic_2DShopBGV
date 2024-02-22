using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item")]
public class Item : ScriptableObject
{
    public int Id;
    public string Name;
    public ItemType Type;
    public Sprite Sprite;
    public int Price;
}

