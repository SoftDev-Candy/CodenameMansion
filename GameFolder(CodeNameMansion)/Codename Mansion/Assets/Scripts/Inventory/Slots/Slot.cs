using System;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField]
    private bool _isSlotFilled;

    
    public bool IsSlotFilled
    {
        get => _isSlotFilled;
        set
        {
            if (_isSlotFilled == value) return;
            _isSlotFilled = value;

        }
    }

    [HideInInspector]
    public ItemData CurrentHoldingItem;

    

    public virtual void FillSlot(GameObject item, ItemData itemData) 
    {
        CurrentHoldingItem = itemData;
        item.transform.SetParent(transform, false);
        RectTransform rTransformItem = item.GetComponent<RectTransform>();
        StretchImg(rTransformItem);
        IsSlotFilled = true;
    }

    public virtual void UnFillSlot()
    {
        IsSlotFilled = false;
        CurrentHoldingItem = null;

    }

    void StretchImg(RectTransform rT)
    {
        rT.offsetMin = Vector2.zero;
        rT.offsetMax = Vector2.zero;
        rT.anchorMax = new Vector2(1, 1);
        rT.anchorMin = new Vector2(0, 0);
    }
}
