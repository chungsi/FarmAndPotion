using UnityEngine;
using UnityEngine.Events;

public class CraftingUI : MonoBehaviour
{
    public Transform itemsParent;
    [Space]
    public FloatVariable startSlotIndex;
    public FloatVariable dropSlotIndex;
    public FloatVariable floatingItemMasterIndex;
    [Space]
    public Inventory craftingList;
    [Space]    
    // public UnityEvent SuccessfulDropEvent;
    public UnityEvent SaveTheFloatingItemEvent;

    ItemSlot[] slots;

    void Start()
    {
        slots = itemsParent.GetComponentsInChildren<ItemSlot>();
        craftingList.ClearInventory();
    }

    #region EventResponses

    public void DropResponse()
    {
        ItemSlot dropSlot = slots[(int)dropSlotIndex.value];

        if (floatingItemMasterIndex.value >= 0) 
        {
            Item dropItem = craftingList.GetItemForIndex((int)floatingItemMasterIndex.value);

            // save the item originally in the dropSlot
            if (!dropSlot.isEmpty())
            {
                floatingItemMasterIndex.value = craftingList.GetIndexForItem(dropSlot.item);
                craftingList.RemoveItem(dropSlot.item);
                
                SaveTheFloatingItemEvent.Invoke();
            }

            dropSlot.AddItem(dropItem);
            craftingList.AddItem(dropItem);

            // SuccessfulDropEvent.Invoke();
        }
    }

    public void ResetSlotResponse() {
        ItemSlot slot = slots[(int)startSlotIndex.value];
        Item item = slot.item;
        floatingItemMasterIndex.value = craftingList.GetIndexForItem(item);

        slot.ClearSlot();
        craftingList.RemoveItem(item);
        SaveTheFloatingItemEvent.Invoke();
    }

    #endregion

}
