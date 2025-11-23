using FMODUnity;
using UnityEngine;

public class Chain : ItemBase
{
    [SerializeField] Door door;

    private StudioEventEmitter emitter;
    private float parameterValue;

    private void Start()
    {
        itemType = ItemType.INTERACT;
        door.isLocked = true;
        emitter = AudioManager.instance.InitializeEventEmitter(FMODEvents.instance.chain, gameObject);
    }
    //TEST
    public override void DragAndDrop(ItemData item)
    {
        if (item.Item.ID == (int)droppableItem)
        {
            parameterValue = 3f;  // Set parameter for cutting chain or interaction
            emitter.EventInstance.setParameterByName("ChainHandlin", parameterValue); // Update parameter before playing
            emitter.Play();
            door.isLocked = false;
            Inventory.Singleton.RemoveItem(item);
            Destroy(gameObject, 1);  // Optionally, destroy after 1 second
        }
        else
        {
            GameManager.instance.ShowDialogue("Can't use this here");
        }
    }

    public override void Interact()
    {
        GameManager.instance.ShowDialogue("I need to find something to cut the chains");
        parameterValue = 0f;  // Default parameter value for interaction state
        emitter.EventInstance.setParameterByName("ChainHandlin", parameterValue); // Update parameter before playing
        emitter.Play();
    }

    public override void PickUp()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        // Update parameter each frame if needed, only when the emitter is playing
        if (emitter.EventInstance.isValid())
        {
            emitter.EventInstance.setParameterByName("ChainHandlin", parameterValue);
        }
    }
}