using UnityEngine;

public class CabinetDoor : ItemBase
{
    [SerializeField] Animator animator;
    private bool isOpen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isOpen = false;
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
        if(isOpen)
        {
            isOpen = false;
        }
        else
        {
            isOpen = true;
        }

        animator.SetBool("IsOpen", isOpen);
    }

    public override void PickUp()
    {
        throw new System.NotImplementedException();
    }
}
