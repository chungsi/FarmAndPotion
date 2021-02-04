using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class ItemHelper
{
    /* 
        Methods to get a list of item assets.
     */
     
    public List<Ingredient> GetMasterIngredientsList()
    {
        List<Ingredient> masterList = new List<Ingredient>();

        string[] assetNames = AssetDatabase.FindAssets("t:Ingredient", new[] { "Assets/Items" });
        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var item = AssetDatabase.LoadAssetAtPath<Ingredient>(SOpath);
            masterList.Add(item);
        }
        return masterList;
    }

    public List<Potion> GetMasterPotionsList()
    {
        List<Potion> masterList = new List<Potion>();

        string[] assetNames = AssetDatabase.FindAssets("t:Potion", new[] { "Assets/Items" });
        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var item = AssetDatabase.LoadAssetAtPath<Potion>(SOpath);
            masterList.Add(item);
        }
        return masterList;
    }
    
    public List<Item> GetMasterItemsList()
    {
        List<Item> masterList = new List<Item>();

        string[] assetNames = AssetDatabase.FindAssets("t:Item", new[] { "Assets/Items" });
        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var item = AssetDatabase.LoadAssetAtPath<Item>(SOpath);
            masterList.Add(item);
        }
        return masterList;
    }

    /* 
        Methods to help when getting a list of ItemStat objects and populating
        the lists and dictionaries when working with stats.
     */
    
    // Gets a list of all possible attribute attributes from the asset database
    public List<ItemStat> GetMasterItemStatsList()
    {
        List<ItemStat> masterItemStatList = new List<ItemStat>();

        string[] assetNames = AssetDatabase.FindAssets("t:ItemStat", new[] { "Assets/Items/Item Stats" });
        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var stat = AssetDatabase.LoadAssetAtPath<ItemStat>(SOpath);
            masterItemStatList.Add(stat);
        }

        return masterItemStatList;
    }

    // 
    public void FillEmptyItemStatValueList(List<ItemStatValue> itemStatValueList, List<ItemStat> masterItemStatList)
    {
        // loop through master attribute list to get attribute
        // check if attribute already exists
        // add new ItemStatvalue if it doesn't
        foreach (ItemStat statReference in masterItemStatList)
        {
            if (!itemStatValueList.Exists(s => s.stat == statReference))
            {
                itemStatValueList.Add(new ItemStatValue() {
                    stat = statReference,
                    value = 0
                });

            }
        }
    }

    // Fill the given dictionary with the Item Stat values.
    public void FillItemStatsDictionary(Dictionary<ItemStat, int> itemStatDict, List<ItemStatValue> itemStatValueList)
    {
        if (itemStatValueList != null) // double check null
        {
            // loop through user defined stats
            foreach(ItemStatValue statValue in itemStatValueList) 
            {
                // If the key exists already, just update the value
                if (itemStatDict.ContainsKey(statValue.stat))
                    itemStatDict[statValue.stat] = statValue.value;
                else
                    itemStatDict.Add(statValue.stat, statValue.value);
            }
        }
    }

    // Returns a dictionary of all Item Stat keys and values.
    public Dictionary<ItemStat, int> GetItemStatsDictionary(List<ItemStatValue> itemStatValueList)
    {
        Dictionary<ItemStat, int> dict = new Dictionary<ItemStat, int>();
        if (itemStatValueList != null)
        {
            // Loop through a list of structs (ItemStatValue), which means it's user defined data
            foreach(ItemStatValue statValue in itemStatValueList)
            {
                dict.Add(statValue.stat, statValue.value);
            }
        }
        return dict;
    }

    // Returns a dictionary ignoring zero-value Item Stats; all stats are >= 1.
    public Dictionary<ItemStat, int> GetNonZeroItemStatsDictionary(List<ItemStatValue> itemStatValueList)
    {
        Dictionary<ItemStat, int> dict = new Dictionary<ItemStat, int>();
        if (itemStatValueList != null)
        {
            // Loop through a list of structs (ItemStatValue), which means it's user defined data
            foreach(ItemStatValue statValue in itemStatValueList)
            {
                if (statValue.value != 0)
                    dict.Add(statValue.stat, statValue.value);
            }
        }
        return dict;
    }
}
