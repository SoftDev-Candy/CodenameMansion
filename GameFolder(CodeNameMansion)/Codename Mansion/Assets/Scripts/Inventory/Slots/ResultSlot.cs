using System;
using UnityEngine;
using UnityEngine.UI;

public class ResultSlot : Slot
{
    public delegate void CraftingResultEventHandler();
    public static CraftingResultEventHandler OnResultCrafted;
    public event Action<bool> OnSlotFilledChanged;

    private void Start()
    {
        CraftableSlot.OnItemCrafted += ShowResult;
    }

    void ShowResult(ItemData item)
    {
        GameObject imageGO = new GameObject($"{item.Item.Name}Image", typeof(DragableImage), typeof(Image));
        Image img = imageGO.GetComponent<Image>();
        img.sprite = item.Item.Icon;
        imageGO.GetComponent<DragableImage>().CurrentItem = item;

        imageGO.GetComponent<DragableImage>().Setup(this.GetComponent<Slot>());

        FillSlot(imageGO, item);
        
        Debug.Log(item.Item.Name);
        
    }

    public override void FillSlot(GameObject item, ItemData itemData)
    {
        base.FillSlot(item, itemData);
        OnResultCrafted?.Invoke();
        OnSlotFilledChanged?.Invoke(IsSlotFilled);
    }

    public override void UnFillSlot()
    {
        base.UnFillSlot();
        OnSlotFilledChanged?.Invoke(IsSlotFilled);
    }
}
