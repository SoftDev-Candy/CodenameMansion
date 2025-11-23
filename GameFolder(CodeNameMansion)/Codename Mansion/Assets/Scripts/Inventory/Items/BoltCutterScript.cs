using UnityEngine;

public class BoltCutterScript : PickableItems
{
    public BoltCutterScript OtherBoltCutterPart;
    public bool otherPieceFound = false;
    
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
        if (OtherBoltCutterPart != null)
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.boltCutterPickUp, transform.position);
            OtherBoltCutterPart.otherPieceFound = true;
        }
        base.PickUp();
    }

    public override void Interact()
    {
        if (!otherPieceFound)
        {
            GameManager.instance.ShowDialogue(DialogueManager.Singleton.GetDialogueFromName("BoltCutter_Piece1"));
        }
        else GameManager.instance.ShowDialogue(DialogueManager.Singleton.GetDialogueFromName("BoltCutter_Piece2"));
    }
}
