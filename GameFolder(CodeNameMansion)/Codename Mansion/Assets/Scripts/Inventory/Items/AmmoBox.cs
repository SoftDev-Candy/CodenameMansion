using UnityEngine;

public class AmmoBox : ItemBase
{

    private void Start()
    {
        itemType = ItemType.BOTH;
    }
    public override void DragAndDrop(ItemData item)
    {
        throw new System.NotImplementedException();
    }

    public override void Interact()
    {
        GameManager.instance.ShowDialogue(DialogueManager.Singleton.GetDialogueFromName(itemData.Item.Name));
    }

    public override void PickUp()
    {
        
        if (Inventory.Singleton != null) 
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.ammoPickUp,transform.position);
            Inventory.Singleton.AddItem(itemData);
        }
        Destroy(gameObject);
    }
}
