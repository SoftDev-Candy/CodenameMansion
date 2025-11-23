using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class CakeScript : ItemBase
{
    private EventInstance handleCake;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemType = ItemType.BOTH;
        handleCake = AudioManager.instance.CreateEventInstance(FMODEvents.instance.handleCake);
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
    }

    public override void PickUp()
    {
        handleCake.setParameterByName("MoveCake", 1);
        handleCake.start();
        if (Inventory.Singleton != null) Inventory.Singleton.AddItem(itemData);
        Destroy(gameObject);
    }

}
