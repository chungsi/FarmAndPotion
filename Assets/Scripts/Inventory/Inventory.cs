﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject {

    public int space;
    [Space]
    public List<Item> items = new List<Item>();
    public List<Item> startItems = new List<Item>();
    [Space]
    public bool useStartItemsOnInitialize;
    [SerializeField]
    private List<Item> masterList = new List<Item>();
    private ItemHelper itemHelper = new ItemHelper();

    public virtual void OnValidate()
    {
        masterList.Clear();

        // populate the Master list of all available Items
        masterList = itemHelper.GetMasterItemsList();
    }

    public virtual void OnEnable()
    {
        // reset list of items to be the startItems on new start
        // if (useStartItemsOnInitialize && startItems.Count > 0)
        // {
        //     Debug.Log("resetting inventory to use startItems");
            items.Clear();
        //     foreach (Item item in startItems)
        //     {
        //         items.Add(item);
        //     }
        // }
    }

    public virtual bool AddItem(Item item)
    {
        if (items.Count >= space)
        {
            Debug.Log("Not enough space in the inventory!!");
            return false;
        }

        items.Add(item);

        return true;
    }

    public virtual bool RemoveItem(Item item)
    {
        return items.Remove(item);
    }
    
    public bool isEmpty()
    {
        return items.Count <= 0;
    }

    public bool isFull()
    {
        return items.Count >= space;
    }

    public int GetMaxSpace()
    {
        return space;
    }

    public int GetItemCount()
    {
        return items.Count;
    }

    public bool ContainsItem(Item item)
    {
        return items.Contains(item);
    }

    public List<Item> GetMasterList()
    {
        return masterList;
    }

    public int GetIndexForItem(Item item)
    {
        return masterList.IndexOf(item);
    }

    public Item GetItemForIndex(int i)
    {
        return masterList[i];
    }

    public virtual void ClearInventory()
    {
        // Debug.Log(name + " is clearing itself out");
        items.Clear();
    }
}
