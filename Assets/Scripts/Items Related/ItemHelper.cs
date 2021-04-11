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

        string[] assetNames = AssetDatabase.FindAssets("t:Ingredient", new[] { "Assets/ScriptableObjects/Items" });
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

        string[] assetNames = AssetDatabase.FindAssets("t:Potion", new[] { "Assets/ScriptableObjects/Items" });
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

        string[] assetNames = AssetDatabase.FindAssets("t:Item", new[] { "Assets/ScriptableObjects/Items" });
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

        string[] assetNames = AssetDatabase.FindAssets("t:ItemStat", new[] { "Assets/ScriptableObjects/Items/Item Stats" });
        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var stat = AssetDatabase.LoadAssetAtPath<ItemStat>(SOpath);
            masterItemStatList.Add(stat);
        }

        return masterItemStatList;
    }

    // 
    public void FillEmptyItemStatValueList(List<ItemStatValue> _itemStatValueList, List<ItemStat> _masterItemStatList)
    {
        // loop through master attribute list to get attribute
        // check if attribute already exists
        // add new ItemStatvalue if it doesn't
        foreach (ItemStat statReference in _masterItemStatList)
        {
            if (!_itemStatValueList.Exists(s => s.stat == statReference))
            {
                _itemStatValueList.Add(new ItemStatValue() {
                    stat = statReference,
                    value = 0
                });

            }
        }
    }
}
