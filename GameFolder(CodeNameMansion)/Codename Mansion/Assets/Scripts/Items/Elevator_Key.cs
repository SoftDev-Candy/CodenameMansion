using UnityEngine;

public class Elevator_Key : ItemBase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemType = ItemType.BOTH;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void DragAndDrop(ItemData item)
    {
        throw new System.NotImplementedException();
    }
    public override void Interact()
    {
        GameManager.instance.ShowDialogue(DialogueManager.Singleton.GetDialogueFromName(itemData.Item.Name));
        Destroy(gameObject);
    }
    public override void PickUp()
    {
        if (Inventory.Singleton != null)
        {
            Inventory.Singleton.AddItem(itemData);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.pickUpKey, transform.position);
        }
    }
}
