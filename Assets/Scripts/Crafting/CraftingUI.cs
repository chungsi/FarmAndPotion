using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CraftingUI : ItemContainerUI
{
    [Space]

    public CraftingInventory craftingList; // hrmmm takes its own inventory....
    public ItemObjectRuntimeSet craftingSubset;
    public FloatVariable numCraftingResults;

    [Space]

    public UnityEvent SaveTheFloatingItemEvent;
    public UnityEvent OnCraftingSuccessfulEvent;

    // ItemSlot[] slots;

    protected override void Start()
    {
        base.Start();
        ClearCraftingInventory();
    }

    protected void OnDisable()
    {
        // clear crafting subset when done
        ClearCraftingInventory();
    }

    protected override void AddItem(ItemObject itemObject)
    {
        craftingList.AddItem(itemObject.Item);
        craftingSubset.Add(itemObject);
    }

    protected override void RemoveItem(ItemObject itemObject)
    {
        craftingList.RemoveItem(itemObject.Item);
        craftingSubset.Remove(itemObject);
    }

    void ClearCraftingInventory() 
    {
        craftingList.ClearInventory();

        for (int i = craftingSubset.items.Count - 1; i >= 0; i--)
        {
            ItemObject item = (ItemObject)craftingSubset.items[i];
            craftingSubset.Remove(item);

            item.Destroy();
        }
    }

    #region EventResponses

    // When an item is dropped onto a crafting slot
    public void DropResponse()
    {
        ItemSlot dropSlot = slots[(int)dropSlotIndex.value];
        ItemObject floatingItem = GetFloatingItem();

        // Just drop the item when dropSlot is empty
        if (dropSlot.IsEmpty && (floatingItem.Item is Ingredient))
        {
            AddItem(floatingItem);
            AddItemToSlot(floatingItem, dropSlot);
        } 
        // Swap the two items if the dropSlot isn't empty
        else if (!dropSlot.IsEmpty)
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

    // clears the slot of the item that was in it.
    public void ResetSlotResponse() 
    {
        ItemObject item = GetFloatingItem();
        RemoveItem(item);
        
        SaveTheFloatingItemEvent.Invoke();
    }

    // When a crafting request comes in.
    public void CraftRequestResponse()
    {
        Debug.Log("a craft request has come in.");
        Recipe possibleRecipe = craftingList.GetRecipeForCurrentIngredients();

        if (possibleRecipe != null)
        {
            Debug.Log("recipe exists! creating " + possibleRecipe.Result.Name);

            // Instantiate instances of both the game object and scriptable object.
            ItemObject itemObject = ItemObject.Instantiate(itemObjectPrefab, slots[0].transform);
            Item uniqueItem = Object.Instantiate(possibleRecipe.Result);
            itemObject.Item = uniqueItem;

            // TODO: Should be a better way to instantiate these potions... should be handled elsewhere?
            ((Potion)itemObject.Item).MainStats = craftingList.GetCurrentIngredientsMainStats();
            ((Potion)itemObject.Item).SecondaryStats = craftingList.GetOutputSecondaryItemStats();

            SaveFloatingItem(itemObject);
            SaveTheFloatingItemEvent.Invoke();

            OnCraftingSuccessfulEvent.Invoke();

            ClearCraftingInventory();
        }
    }

    #endregion

}
