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

    ItemUIObject InstantiateAndAddUniqueItem(Item item, Transform parent)
    {
        // Casting to more specificity
        ItemUIObject ui = (ItemUIObject) ItemObject.Instantiate(itemObjectPrefab, parent);
        Item uniqueItem = Object.Instantiate(item);

        inventory.AddItem(uniqueItem); // setup start
        ui.Item = uniqueItem;
        return ui;
    }

    ItemSlot GetFirstEmptySlot()
    {
        foreach (ItemSlot slot in slots)
        {
            if (slot.IsEmpty)
            {
                Debug.Log("empty slot index: " + slot.IndexWithinContainer);
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

    #region Event Responses

    public void DropResponse() 
    {
        ItemSlot dropSlot = slots[(int)dropSlotIndex.value];
        ItemUIObject floatingItem = (ItemUIObject) GetFloatingItem();

        // Just drop the item when dropSlot is empty
        if (dropSlot.IsEmpty) 
        {
            AddItemToSlot(floatingItem, dropSlot);
        } 
        // Swap the two items if the dropSlot isn't empty
        else if (!dropSlot.IsEmpty)
        {
            // save the existing item first
            // and get the start slot from the itemObject; maybe not use GetComponent?
            ItemSlot startSlot = floatingItem.ParentItemSlot;
            ItemObject existingItem = dropSlot.GetComponentInChildren<ItemObject>();

            AddItemToSlot(floatingItem, dropSlot);
            AddItemToSlot(existingItem, startSlot);
        }
    }

    // this means the item has successfully been dropped into a crafting slot
    public void RemoveItemDroppedIntoCraftingContainer()
    {
        ItemObject item = GetFloatingItem();
        if (item.Item is Ingredient)
        {
            RemoveItem(item);
        }
    }

    public void RemoveItemDroppedIntoRequestBoard()
    {
        ItemObject item = GetFloatingItem();
        if (item.Item is Potion)
        {
            RemoveItem(item);
        }
    }

    // add the "floating item" to the inventory
    public void InsertAFloatingItem() 
    {
        Debug.Log("inventory is saving a floating item...");
        ItemObject newItem = GetFloatingItem();

        // only add to the inventory if the item doesn't already exist
        if (!inventory.ContainsItem(newItem.Item))
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
            ItemObject newItem = InstantiateAndAddUniqueItem(floatingItem.Item, slot.transform);
            AddItemToSlot(newItem, slot);

            floatingItem.Destroy(); // removes the wildItem because replaced by "tamed"
        }
    }

    public void DisplayItemName()
    {
        ItemObject newItem = GetFloatingItem();
        displayText.SetText(newItem.Item.name);
    }

    #endregion
    
}
