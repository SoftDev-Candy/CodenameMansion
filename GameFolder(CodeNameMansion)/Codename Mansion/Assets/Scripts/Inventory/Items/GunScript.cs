using UnityEngine;

public class GunScript : ItemBase
{
    public int totalDamage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemType = ItemType.BOTH;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void PickUp()
    {
        Debug.Log("PICKED GUUNN");
        if (Inventory.Singleton != null) Inventory.Singleton.AddItem(itemData);
        Destroy(gameObject);
    }

    public override void DragAndDrop(ItemData item)
    {
        //Play shoot Animation
        //call zombie damage method
    }

    public override void Interact()
    {

    }
}
