using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemContainerUI : MonoBehaviour
{
    public ItemObject itemObjectPrefab;
    public Transform itemsParent;
    protected ItemSlot[] slots;
    [Space]
    public Inventory inventory;
    public InventoryRuntimeSet inventorySet;
    [Space]
    public FloatVariable startSlotIndex;
    public FloatVariable dropSlotIndex;
    public FloatVariable floatingItemMasterIndex;


    protected virtual void Start()
    {
        slots = itemsParent.GetComponentsInChildren<ItemSlot>();
    }

    protected virtual void AddItem(ItemObject itemObject)
    {
        inventory.AddItem(itemObject.GetItem());
    }

    protected virtual void RemoveItem(ItemObject itemObject)
    {
        inventory.RemoveItem(itemObject.GetItem());
    }

    protected virtual ItemObject GetFloatingItem()
    {
        return inventorySet.GetItem((int)floatingItemMasterIndex.value);
    }

    protected virtual void AddItemToSlot(ItemObject item, ItemSlot slot)
    {
        item.transform.SetParent(slot.transform);
        item.transform.localPosition = new Vector3(0,0,0);
    }

    protected virtual void SaveFloatingItem(ItemObject item)
    {
        inventorySet.Add(item);
        floatingItemMasterIndex.value = inventorySet.GetIndex(item);
    }
}
