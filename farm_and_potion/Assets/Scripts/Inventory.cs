﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu]
public class Inventory : ScriptableObject {

    public int space;
    [Space]
    public List<Item> items;
    public List<Item> startItems;
    [Space]
    public bool useStartItemsOnInitialize;

    private List<Item> masterList = new List<Item>();

    void OnValidate()
    {
        masterList.Clear();

        // populate the Master list of all available Items
        PopulateMasterList();

    }

    void OnEnable()
    {
        // reset list of items to be the startItems on new start
        if (startItems.Count > 0 && useStartItemsOnInitialize)
        {
            Debug.Log("resetting inventory to use startItems");
            items.Clear();
            foreach (Item item in startItems)
            {
                items.Add(item);
            }
        }
    }

    public bool AddItem(Item item)
    {
        Debug.Log($"available space in the inventory: {space}; items count: {items.Count}");
        if (items.Count >= space)
        {
            Debug.Log("Not enough space in the inventory!!");
            return false;
        }

        items.Add(item);

        return true;
    }

    public bool RemoveItem(Item item)
    {
        return items.Remove(item);
    } 

    public int GetMaxSpace()
    {
        return space;
    }

    public int GetItemCount()
    {
        return items.Count;
    }

    public int GetIndexForItem(Item item)
    {
        return masterList.IndexOf(item);
    }

    public Item GetItemForIndex(int i)
    {
        return masterList[i];
    }

    public void ClearInventory()
    {
        items.Clear();
    }

    private void PopulateMasterList()
    {
        string[] assetNames = AssetDatabase.FindAssets("t:Item", new[] { "Assets/Items" });
        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var item = AssetDatabase.LoadAssetAtPath<Item>(SOpath);
            masterList.Add(item);
        }
    }

}
