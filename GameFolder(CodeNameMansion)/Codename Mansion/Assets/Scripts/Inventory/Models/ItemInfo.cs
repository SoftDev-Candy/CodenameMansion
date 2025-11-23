using UnityEngine;

[System.Serializable]
public class ItemInfo
{
    public int ID;
    public string Name;
    public string Description;
    public bool CanBeCombined;
    public int CombinedItemId; // write -1 if item cannot be combined
    public int resultItemId; // write -1 if item cannot be combined
    public Sprite Icon;
    public GameObject itemObject;
}
