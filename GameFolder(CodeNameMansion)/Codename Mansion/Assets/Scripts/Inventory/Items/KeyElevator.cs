using UnityEngine;

public class KeyElevator : ItemBase
{
    public bool isSolved;
    [SerializeField] private ItemData keyItem;
    private bool hasTakenKey;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hasTakenKey = false;
        isSolved = false;
        itemType = ItemType.INTERACT;       
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
        if (isSolved)
        {
            if(hasTakenKey)
            {
                GameManager.instance.ShowDialogue("Nothing more in the elevator");
                return;
            }

            //ADD KEY ITEM TO INVENTORY 
            if (Inventory.Singleton != null)
            {
                hasTakenKey = true;
                AudioManager.instance.PlayOneShot(FMODEvents.instance.pickUpKey, transform.position);
                Inventory.Singleton.AddItem(keyItem);
                GameManager.instance.ShowDialogue(DialogueManager.Singleton.GetDialogueFromName(keyItem.Item.Name));
            }
        }
        else
        {
            GameManager.instance.ShowDialogue("I need something to push it up");
        }
    }

    public void Solve()
    {
        isSolved = true;
    }

    public override void PickUp()
    {
        throw new System.NotImplementedException();
    }
}
