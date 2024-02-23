using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCustomization : MonoBehaviour
{
    public DisplayItem DisplayItem;
    public TradeManager TradeManager;

    public PlayerController PlayerController;
    public Button EquipButtonl;
    public ItemType itemsCategory;
    public List<ItemElement> DisplayItems;

    private void OnEnable()
    {
        SetCategory((int)ItemType.Torso);
    }

    public void SetCategory(int type)
    {
        itemsCategory = (ItemType)type;
        SetItems();
    }

    public void EquipItem()
    {
        PlayerController.EquipItem(DisplayItem.item);

        for (int i = 0; i < DataController.dataController.playerData.itemsEquipped.Count; i++)
        {
            if (DataController.dataController.playerData.itemsEquipped[i].Item1 == (int)DisplayItem.item.Type)
            {
                DataController.dataController.playerData.itemsEquipped[i] = new((int)DisplayItem.item.Type, DisplayItem.item.Id);
            }
        }
        DataController.dataController.SaveToJson();

    }


    private void SetItems()
    {
        List<int> itemIds = DataController.dataController.GetOwnedItemsByCategory(itemsCategory);
        List<Item> allItems = TradeManager.GetCategotyItems(itemsCategory);


        for (int i = 0; i < DisplayItems.Count; i++)
        {
            //Checking if there are items to display
            if (itemIds.Contains(allItems[i].Id))
            {
                DisplayItems[i].SetElement(allItems[i]);
                DisplayItems[i].gameObject.SetActive(true);

            }
            else
            {

                DisplayItems[i].gameObject.SetActive(false);
            }
        }
    }
}
