﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fire : MonoBehaviour, IDropHandler
{
    public ItemObjectRuntimeSet masterInventorySet;
    public FloatVariable floatingItemMasterIndex;

    public void OnDrop(PointerEventData eventData) 
    {
        Debug.Log("Throwing an item into the fire!");
        ItemObject item = masterInventorySet.GetItem((int)floatingItemMasterIndex.value);
        item.Destroy();
    }
}