using UnityEngine;

public class UnloadedGun : ItemBase
{
    public override void DragAndDrop(ItemData item)
    {
        throw new System.NotImplementedException();
    }

    public override void Interact()
    {
        GameManager.instance.ShowDialogue(DialogueManager.Singleton.GetDialogueFromName(itemData.Item.Name));
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemType = ItemType.BOTH;   
    }

    public override void PickUp()
    {
        if (Inventory.Singleton != null)
        {
            Inventory.Singleton.AddItem(itemData);
        }
        AudioManager.instance.PlayOneShot(FMODEvents.instance.gunPickUp, transform.position);
        Destroy(gameObject);
    }
}
