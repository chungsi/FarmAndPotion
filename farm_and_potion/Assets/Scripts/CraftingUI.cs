using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CraftingUI : ItemContainerUI
{
    // public Transform itemsParent;
    // [Space]
    // public FloatVariable startSlotIndex;
    // public FloatVariable dropSlotIndex;
    // public FloatVariable floatingItemMasterIndex;
    // public InventoryRuntimeSet inventorySet;
    // [Space]
    public CraftingInventory craftingList; // hrmmm takes its own inventory....
    public InventoryRuntimeSet craftingSubset;
    [Space]    
    public UnityEvent SaveTheFloatingItemEvent;

    // ItemSlot[] slots;

    protected override void Start()
    {
        base.Start();
        craftingList.ClearInventory();
    }

    protected override void AddItem(ItemObject itemObject)
    {
        craftingList.AddItem(itemObject.GetItem());
        craftingSubset.Add(itemObject);
    }

    protected override void RemoveItem(ItemObject itemObject)
    {
        craftingList.RemoveItem(itemObject.GetItem());
        craftingSubset.Remove(itemObject);
    }

    void ClearCraftingInventory() 
    {
        craftingList.ClearInventory();

        for (int i = craftingSubset.items.Count - 1; i >= 0; i--)
        {
            ItemObject item = craftingSubset.items[i];
            inventorySet.Remove(item);
            craftingSubset.Remove(item);

            Destroy(item.gameObject);
        }
    }

    #region EventResponses

    /* 
        When an item is dropped onto a crafting slot
     */
    public void DropResponse()
    {
        ItemSlot dropSlot = slots[(int)dropSlotIndex.value];
        ItemObject floatingItem = GetFloatingItem();


        // Just drop the item when dropSlot is empty
        if (dropSlot.isEmpty()) 
        {
            AddItem(floatingItem);
            AddItemToSlot(floatingItem, dropSlot);
        } 
        // Swap the two items if the dropSlot isn't empty
        else if (!dropSlot.isEmpty())
        {
            // save the existing item first
            // and get the start slot from the itemObject; maybe not use GetComponent?

            ItemObject existingItem = dropSlot.GetComponentInChildren<ItemObject>();
            floatingItemMasterIndex.value = inventorySet.GetIndex(existingItem); // save item

            RemoveItem(existingItem);
            AddItem(floatingItem);

            AddItemToSlot(floatingItem, dropSlot);

            SaveTheFloatingItemEvent.Invoke();
        }
    }

    /*
        clears the slot of the item that was in it
     */    
    public void ResetSlotResponse() 
    {
        ItemObject item = GetFloatingItem();
        RemoveItem(item);
        
        SaveTheFloatingItemEvent.Invoke();
    }

    /* When a crafting request comes in */
    public void CraftRequestResponse()
    {
        // TODO: will need to change how the results are output, to account for stats
        // Recipe possibleRecipe = craftingList.GetRecipeForItems(craftingList.items);

        Recipe possibleRecipe = craftingList.GetRecipeForCurrent();

        if (possibleRecipe != null)
        {
            Debug.Log("recipe exists! creating " + possibleRecipe.name);
            foreach (Item item in possibleRecipe.GetResults())
            {
                ItemObject itemObject = ItemObject.Instantiate(itemObjectPrefab, slots[0].transform);
                Item uniqueItem = Object.Instantiate(item);
                itemObject.SetItem(uniqueItem);

                Dictionary<Stat, int> newStats = craftingList.CalculateStats();
                itemObject.SetItemStats(newStats);

                SaveFloatingItem(itemObject);
                SaveTheFloatingItemEvent.Invoke();
            }

            ClearCraftingInventory();
        }
    }

    #endregion

}
