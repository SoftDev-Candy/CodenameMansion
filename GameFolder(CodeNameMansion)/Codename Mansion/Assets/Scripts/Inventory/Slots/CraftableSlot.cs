using UnityEngine;

public class CraftableSlot : Slot
{
    public Slot OtherCraftableSlot;
    public ResultSlot resultSlot;

    public bool ShowDialog = false;

    public delegate void CraftingEventHandler(ItemData resultItem);
    public static CraftingEventHandler OnItemCrafted;


    private void Start()
    {
        if (resultSlot != null)
        {
            resultSlot.OnSlotFilledChanged += HandleResultSlotFillChange;
        }
        ResultSlot.OnResultCrafted += DestroyImageAndItem;
    }

    public override void FillSlot(GameObject itemObject, ItemData item)
    {
        base.FillSlot(itemObject, item);
        TryCraftItem();
    }


    void HandleResultSlotFillChange(bool val)
    {
        IsSlotFilled = val;
        ShowDialog = val;
    }

    private void TryCraftItem()
    {

        if (OtherCraftableSlot.IsSlotFilled)
        {
            if (CurrentHoldingItem.Item.CombinedItemId == OtherCraftableSlot.CurrentHoldingItem.Item.ID)
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.mergeSucces, transform.position);
                ItemData resultItem = ItemManager.Instance.GetItemByID(CurrentHoldingItem.Item.resultItemId.ToString());
                OnItemCrafted?.Invoke(resultItem);
                Inventory.Singleton.AddItem(resultItem, false);

            }

            else
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.mergeFailed, transform.position);
            }


        }
    }

    void DestroyImageAndItem()
    {
        Inventory.Singleton.RemoveItem(CurrentHoldingItem);
        base.UnFillSlot();
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }


}
