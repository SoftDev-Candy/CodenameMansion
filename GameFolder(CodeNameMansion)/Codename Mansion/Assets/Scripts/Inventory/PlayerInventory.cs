using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PlayerInventory
{
    public List<ItemData> InventoryItems = new List<ItemData>();

    public void AddItemToInventory(ItemData item)
    {
        InventoryItems.Add(item);
    }

    public void RemoveItemFromInventory(ItemData item)
    {
        InventoryItems.Remove(item);
    }

    public void RemoveAllItemsFromInventory()
    {
        InventoryItems.Clear();
    }
}
