using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
// using UnityEngine.EventSystems;

public class InventoryUI : ItemContainerUI 
{
    public TextMeshProUGUI displayText;

    // public Inventory inventory;
    // public Transform itemsParent;
    // [Space]
    // public FloatVariable draggedSlotIndex;
    // public FloatVariable dropSlotIndex;
    // public FloatVariable floatingItemMasterIndex;
    // public ItemObjectRuntimeSet inventorySet;

    // private ItemSlot startDraggedSlot;
    // private ItemSlot draggedSlot;

    // ItemSlot[] slots;

    protected override void Start()
    {
        base.Start();

        SetupUI();
    }

    void SetupUI()
    {
        Debug.Log("InventoryUI is updating!");

        for (int i = 0; i < inventory.startItems.Count; i++)
        {
            InstantiateAndAddUniqueItem(inventory.startItems[i], slots[i].transform);
        }
    }

    ItemObject InstantiateAndAddUniqueItem(Item item, Transform parent)
    {
        ItemObject ui = ItemObject.Instantiate(itemObjectPrefab, parent);
        Item uniqueItem = Object.Instantiate(item);

        inventory.AddItem(uniqueItem); // setup start
        ui.SetItem(uniqueItem);
        return ui;
    }

    ItemSlot GetFirstEmptySlot()
    {
        foreach (ItemSlot slot in slots)
        {
            if (slot.isEmpty())
            {
                Debug.Log("empty slot index: " + slot.GetIndexWithinContainer());
                return slot;
            }
        }
        return null;
    }

    void AddToFirstEmptySlot(ItemObject newItem)
    {
        ItemSlot slot = GetFirstEmptySlot();
        AddItemToSlot(newItem, slot);
    }

    // public void AddItem(ItemObject itemObject)
    // {
    //     inventory.AddItem(itemObject.GetItem());
    //     inventorySet.Add(itemObject);
    // }

    // public void RemoveItem(ItemObject itemObject)
    // {
    //     inventory.RemoveItem(itemObject.GetItem());
    //     inventorySet.Remove(itemObject);
    // }

    // private ItemObject GetFloatingItem()
    // {
    //     return inventorySet.GetItem((int)floatingItemMasterIndex.value);
    // }

    // private void AddItemToSlot(ItemObject item, ItemSlot slot)
    // {
    //     item.transform.SetParent(slot.transform);
    //     item.transform.localPosition = new Vector3(0,0,0);
    // }

    #region Event Responses

    public void DropResponse() 
    {
        ItemSlot dropSlot = slots[(int)dropSlotIndex.value];
        ItemObject floatingItem = GetFloatingItem();

        // Just drop the item when dropSlot is empty
        if (dropSlot.isEmpty()) 
        {
            AddItemToSlot(floatingItem, dropSlot);
        } 
        // Swap the two items if the dropSlot isn't empty
        else if (!dropSlot.isEmpty())
        {
            // save the existing item first
            // and get the start slot from the itemObject; maybe not use GetComponent?
            ItemSlot startSlot = floatingItem.GetParentItemSlot();
            ItemObject existingItem = dropSlot.GetComponentInChildren<ItemObject>();

            AddItemToSlot(floatingItem, dropSlot);
            AddItemToSlot(existingItem, startSlot);
        }
    }

    // this means the item has successfully been dropped into a crafting slot
    public void CraftingPanelDropResponse()
    {
        ItemObject item = GetFloatingItem();
        RemoveItem(item);
    }

    // add the "floating item" to the inventory
    public void InsertAFloatingItem() 
    {
        Debug.Log("inventory is saving a floating item...");
        ItemObject newItem = GetFloatingItem();

        // only add to the inventory if the item doesn't already exist
        if (!inventory.ContainsItem(newItem.GetItem()))
            AddItem(newItem);

        AddToFirstEmptySlot(newItem);
    }

    // add an item that requires instantiating a new object
    public void AddWildItem()
    {
        ItemSlot slot = GetFirstEmptySlot();

        // just in case check both inventory and available slots
        if (!inventory.isFull() && slot != null)
        {
            ItemObject floatingItem = GetFloatingItem();
            ItemObject newItem = InstantiateAndAddUniqueItem(floatingItem.GetItem(), slot.transform);
            AddItemToSlot(newItem, slot);

            floatingItem.Destroy(); // removes the wildItem because replaced by "tamed"
        }
    }

    public void DisplayItemName()
    {
        ItemObject newItem = GetFloatingItem();
        displayText.SetText(newItem.GetItem().name);
    }

    #endregion
    
}
