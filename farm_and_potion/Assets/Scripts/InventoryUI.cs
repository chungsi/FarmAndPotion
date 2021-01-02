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
    // public InventoryRuntimeSet inventorySet;

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
            ItemObject ui = ItemObject.Instantiate(itemObjectPrefab, slots[i].transform);
            Item uniqueItem = Object.Instantiate(inventory.startItems[i]);

            inventory.AddItem(uniqueItem); // setup start
            ui.SetItem(uniqueItem);
            slots[i].AddItem(uniqueItem);
        }
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

    #region EventResponses

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

        // Debug.Log("InventoryUI cleaned itself up; size is now " + inventory.GetItemCount());
    }

    // add the "floating item" to the inventory
    public void InsertAFloatingItem() 
    {
        Debug.Log("inventory is saving a floating item...");
        ItemObject newItem = GetFloatingItem();
        AddItem(newItem);

        foreach (ItemSlot slot in slots)
        {
            if (slot.isEmpty())
            {
                AddItemToSlot(newItem, slot);
                break;
            }
        }
    }

    #endregion
    
}
