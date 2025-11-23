using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    public ItemData itemData;
    protected ItemType itemType;
    [SerializeField] protected DragAndDropItems droppableItem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public ItemType GetItemType()
    {
        return itemType;
    }
    public abstract void DragAndDrop(ItemData item);
    public abstract void PickUp();

    public abstract void Interact();
}
