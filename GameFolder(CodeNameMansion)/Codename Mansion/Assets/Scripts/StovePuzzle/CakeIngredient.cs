using UnityEngine;

public class CakeIngredient : ItemBase
{
    private void Start()
    {
        itemType = ItemType.BOTH;
    }

    public override void DragAndDrop(ItemData item)
    {
        return;
    }

    public override void Interact()
    {
        GameManager.instance.ShowDialogue(DialogueManager.Singleton.GetDialogueFromName(itemData.Item.Name));
    }

    public override void PickUp()
    {
        Inventory.Singleton.AddItem(itemData);
        Destroy(gameObject);
    }

    
}
