using UnityEngine;

public class CakeElevator : ItemBase
{
    [SerializeField] KeyElevator keyElev; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemType = ItemType.INTERACT;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void DragAndDrop(ItemData item)
    {
        //CHECK IF ITEMID IS CAKE'S ID
        //IF IT IS SOLVE THE CAKE AND CALL THE KEYELEVATOR TO BE SOLVED
        Debug.Log("TEST");
        if (item.Item.ID == (int)droppableItem)
        {
            Debug.Log("CAKE DROPPED");
            AudioManager.instance.PlayOneShot(FMODEvents.instance.cakeElevatorCrash,transform.position);
            keyElev.Solve();
            Inventory.Singleton.RemoveItem(item);
            //SOLVE KEY ELEVATOR
        }
        else
        {
            GameManager.instance.ShowDialogue("Cant use that here");
        }
    }

    public override void Interact()
    {
        GameManager.instance.ShowDialogue("I can put something in here");
    }

    public override void PickUp()
    {
        throw new System.NotImplementedException();
    }

}
