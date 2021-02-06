using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Serialization;

[Serializable]
public struct ItemStatValue
{
    public ItemStat stat;
    public int value;
}

// [CreateAssetMenu(menuName = "Item/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    // [SerializeField] ItemType itemType;
    public Sprite artwork = null;

    [TextArea(7, 15)] 
    [SerializeField] string description = "";
    [Space]
    [SerializeField] List<ItemStatValue> itemStats = new List<ItemStatValue>(); // to be inspector populated
    
    // populated by code, used for internal traversals
    private Dictionary<ItemStat, int> statDict = new Dictionary<ItemStat, int>();
    private List<ItemStat> masterStatList = new List<ItemStat>();
    
    // helper functions to get stats stuff
    private ItemHelper statHelper = new ItemHelper();
    

    void OnValidate()
    {
        // Gets a list of all item stats and populates the struct list with all stats.
        // Newly defined stats will get added to the end of the list...
        FillMasterItemStatsList();
        statHelper.FillEmptyItemStatValueList(itemStats, masterStatList);
    }

    void OnEnable()
    {
        // Fill the private dictionary with the values from the inspector.
        statHelper.FillItemStatsDictionary(statDict, itemStats);
    }

    public virtual Item GetCopy()
    {
        return this;
    }

    public Dictionary<ItemStat, int> GetItemStatsDictionary()
    {
        Dictionary<ItemStat, int> newlist = statDict;
        return newlist;
    }

    public string GetDescription()
    {
        return description;
    }

    // Fill the items itemStats dictionary with a new one.
    // Useful when instantiating new items with programmatically defined itemStats.
    public void SetItemStats(Dictionary<ItemStat, int> newItemStats)
    {
        statDict = newItemStats;

        // Also populates the inspector-visible list for easier debugging
        itemStats.Clear();
        foreach (KeyValuePair<ItemStat, int> newStat in newItemStats)
        {
            itemStats.Add(new ItemStatValue() { stat = newStat.Key, value = newStat.Value });
        }
    }

    private void FillMasterItemStatsList()
    {
        masterStatList.Clear(); // clear existing first
        masterStatList = statHelper.GetMasterItemStatsList();
    }

}
