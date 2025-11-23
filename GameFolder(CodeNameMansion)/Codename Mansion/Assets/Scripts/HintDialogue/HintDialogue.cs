using UnityEngine;

public class HintDialogue : ItemBase
{
    [SerializeField] ItemType typeOfItem;
    [SerializeField] string interactHint;
    [SerializeField] string pickUpHint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemType = typeOfItem;
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
        if (interactHint.Length == 0)
            return;

        GameManager.instance.ShowDialogue(interactHint);
    }

    public override void PickUp()
    {
        if (pickUpHint.Length == 0)
            return;

        GameManager.instance.ShowDialogue(pickUpHint);
    }
}
