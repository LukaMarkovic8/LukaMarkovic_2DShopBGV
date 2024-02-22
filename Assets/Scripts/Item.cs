using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item")]
public class Item : ScriptableObject
{
    public int Id;
    public string Name;
    public ItemType Type;
    public bool isDouble = false;
    public Sprite Sprite;
    public Sprite SpriteL;
    public Sprite SpriteR;

    public int Price;
}

