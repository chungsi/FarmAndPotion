using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemContainerUI : MonoBehaviour
{
    public ItemObject itemObjectPrefab;
    public Transform itemsParent;
    [Space]
    public Inventory inventory;
    public ItemObjectRuntimeSet inventorySet;
    public FloatVariable floatingItemMasterIndex;
    [Space]
    public FloatVariable startSlotIndex;
    public FloatVariable dropSlotIndex;

    protected ItemSlot[] slots;

    protected virtual void Start()
    {
        slots = itemsParent.GetComponentsInChildren<ItemSlot>();
    }

    protected virtual void AddItem(ItemObject itemObject)
    {
        inventory.AddItem(itemObject.Item);
    }

    protected virtual void RemoveItem(ItemObject itemObject)
    {
        inventory.RemoveItem(itemObject.Item);
    }

    protected virtual ItemObject GetFloatingItem()
    {
        return (ItemObject)inventorySet.GetItem((int)floatingItemMasterIndex.value);
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
