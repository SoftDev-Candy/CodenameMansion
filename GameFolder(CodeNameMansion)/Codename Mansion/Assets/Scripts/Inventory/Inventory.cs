using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class Inventory : MonoBehaviour
{
    
    public static Inventory Singleton;
    private List<StorageSlot> _storageSlotList;
    private List<Slot> _slotList;

    public GameObject OuterRingUI, ItemHolder;

    [SerializeField]
    private PlayerInventoryData _playerInventoryData;


    private void Start()
    {
        Singleton = this;

        _storageSlotList = new List<StorageSlot>(FindObjectsByType<StorageSlot>(FindObjectsInactive.Include, FindObjectsSortMode.None));
        _slotList = new List<Slot>(FindObjectsByType<Slot>(FindObjectsInactive.Include, FindObjectsSortMode.None));

        _playerInventoryData.EmptyInventory();

        SyncInventories();

        DragableImage.OnItemDragged += UnFillSlot;

        Debug.Log(_slotList.Count);
    }


    public void SyncInventories()
    {
        _playerInventoryData.SyncUpdatedInventory();

    }

    private void UnFillSlot(ItemData item)
    {
        foreach (Slot slot in _slotList)
        {
            if (slot.CurrentHoldingItem == item)
            {
                slot.UnFillSlot();
                return;
            }
        }
    }

    private void OnDestroy()
    {
        DragableImage.OnItemDragged -= UnFillSlot;
    }

    public void AddItem(ItemData _item, bool spaceShowImage =true)
    {
        _playerInventoryData.AddItemToInventory(_item);

        if (!spaceShowImage)
        {
            return;
        }
        

        GameObject imageGO = new GameObject($"{_item.Item.Name}Image", typeof(DragableImage), typeof(Image));
        Image img = imageGO.GetComponent<Image>();
        img.sprite = _item.Item.Icon;
        imageGO.GetComponent<DragableImage>().CurrentItem = _item;

        AssignItemToSlot(_item, imageGO);

        
    }

    public void RemoveItem(ItemData _item)
    {

        _playerInventoryData.RemoveItemFromInventory(_item);
        DestroyItemImage(_item);
        UnFillSlot(_item);
        

    }

    void DestroyItemImage(ItemData item)
    {
        foreach (Slot slot in _slotList)
        {
            if (slot.CurrentHoldingItem == item)
            {
                foreach(Transform image in slot.transform)
                {
                    Destroy(image.gameObject);
                }
            }
        }
    } // need more workk

    private void AssignItemToSlot(ItemData item, GameObject imageGO)
    {
        foreach(StorageSlot slot in _storageSlotList)
        {
            if (!slot.IsSlotFilled)
            {

                slot.FillSlot(imageGO, item);
                imageGO.GetComponent<DragableImage>().Setup(slot);
                break;
            }
        }
    }

}
