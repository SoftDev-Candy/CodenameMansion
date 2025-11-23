using UnityEngine;
using System.Collections.Generic;
using FMOD.Studio;

public class BowlScript : ItemBase
{

    [SerializeField] int _numberOfIngredients;

    [SerializeField] int _IngredientsCollected;

    private void Start()
    {
        itemType = ItemType.BOTH;
    }

    public override void DragAndDrop(ItemData item)
    {
        Debug.Log(item.Item.ID);
        if((int)droppableItem == item.Item.ID)
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.ingredient, transform.position);
            _IngredientsCollected++;
            Inventory.Singleton.RemoveItem(item);
            
        }
    }

    public override void Interact()
    {
        GameManager.instance.ShowDialogue(DialogueManager.Singleton.GetDialogueFromName(itemData.Item.Name));
    }

    public override void PickUp()
    {
        if(_IngredientsCollected == _numberOfIngredients)
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.grabBowl, transform.position);
            Inventory.Singleton.AddItem(itemData);
            Destroy(gameObject);
        }
    }

}
