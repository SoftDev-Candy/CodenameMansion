using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInventoryData", menuName = "Scriptable Objects/PlayerInventoryData")]
public class PlayerInventoryData : ScriptableObject
{
    public PlayerInventory CurrentPlayerInventory, UpdatedPlayerInventory;


    public void AddItemToInventory(ItemData item)
    {
        CurrentPlayerInventory.AddItemToInventory(item);
    }

    public void RemoveItemFromInventory(ItemData item)
    {
        CurrentPlayerInventory.RemoveItemFromInventory(item);
    }

    public void EmptyInventory()
    {
        CurrentPlayerInventory.RemoveAllItemsFromInventory();
    }

    public void SyncUpdatedInventory()
    {
        UpdatedPlayerInventory = CurrentPlayerInventory;
    }
}
