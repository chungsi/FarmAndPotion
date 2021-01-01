using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CraftingUI : MonoBehaviour
{
    public Transform itemsParent;
    [Space]
    public FloatVariable startSlotIndex;
    public FloatVariable dropSlotIndex;
    public FloatVariable floatingItemMasterIndex;
    [Space]
    public CraftingInventory craftingList;
    public Button craftButton;
    [Space]    
    // public UnityEvent SuccessfulDropEvent;
    public UnityEvent SaveTheFloatingItemEvent;

    ItemSlot[] slots;

    void Start()
    {
        slots = itemsParent.GetComponentsInChildren<ItemSlot>();
        craftingList.ClearInventory();
    }

    void ClearCraftingSlots() {
        craftingList.ClearInventory();
        foreach (ItemSlot slot in slots)
            slot.ClearSlot();
    }

    #region EventResponses

    /* 
        When an item is dropped onto a crafting slot
     */
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

    /*
        clears the slot of the item that was in it
     */    
    public void ResetSlotResponse() 
    {
        ItemSlot slot = slots[(int)startSlotIndex.value];
        Item item = slot.item;
        floatingItemMasterIndex.value = craftingList.GetIndexForItem(item);

        slot.ClearSlot();
        craftingList.RemoveItem(item);

        SaveTheFloatingItemEvent.Invoke();
    }

    /* When a crafting request comes in */
    public void CraftRequestResponse()
    {
        Recipe possibleRecipe = craftingList.GetRecipeForItems(craftingList.items);

        if (!craftingList.isEmpty() && 
            possibleRecipe != null)
        {
            Debug.Log("recipe exists! creating " + possibleRecipe.name);

            // invokes the event for each recipe result
            foreach (Item item in possibleRecipe.results)
            {
                floatingItemMasterIndex.value = craftingList.GetIndexForItem(item);
                SaveTheFloatingItemEvent.Invoke();
            }

            ClearCraftingSlots();
        }
    }

    #endregion

}
