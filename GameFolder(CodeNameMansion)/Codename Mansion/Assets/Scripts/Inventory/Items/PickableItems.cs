using UnityEngine;

public class PickableItems : ItemBase
{
    void Start()
    {
        itemType = ItemType.BOTH;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void PickUp()
    {
        
        if (Inventory.Singleton != null) Inventory.Singleton.AddItem(itemData);
        Destroy(gameObject);
    }

    public override void DragAndDrop(ItemData item)
    {
        
    }

    public override void Interact()
    {
        GameManager.instance.ShowDialogue(DialogueManager.Singleton.GetDialogueFromName(itemData.Item.Name));
    }
}
