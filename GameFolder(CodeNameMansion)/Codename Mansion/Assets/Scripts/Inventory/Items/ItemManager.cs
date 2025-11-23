using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance; 

    public List<ItemData> AllItems = new List<ItemData>(); 
    private Dictionary<string, ItemData> _itemLookup = new Dictionary<string, ItemData>(); 

    void Awake()
    {
  
        if (Instance == null)
        {
            Instance = this;
            LoadAllItems();
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    private void LoadAllItems()
    {
        
        ItemData[] items = Resources.LoadAll<ItemData>("Items");
        
        AllItems.Clear();
        _itemLookup.Clear();

        foreach (ItemData item in items)
        {
            AllItems.Add(item);
            _itemLookup[item.Item.ID.ToString()] = item; 
        }
    }

    public ItemData GetItemByID(string itemID)
    {
        if (_itemLookup.ContainsKey(itemID))
        {
            return _itemLookup[itemID];
        }
        return null;
    }

}
