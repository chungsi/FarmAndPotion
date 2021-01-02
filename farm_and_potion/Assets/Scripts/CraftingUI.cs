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
    public Item floatingItemVar;
    [Space]
    public CraftingInventory craftingList;
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

    private Item GetFloatingItem()
    {
        Item newItem = Object.Instantiate(floatingItemVar);
        // craftingList.GetItemForIndex((int)floatingItemMasterIndex.value)
        return newItem;
    }

    private void SaveFloatingItem(Item newItem)
    {
        floatingItemVar.OverwriteItemWithItem(newItem);
    }

    #region EventResponses

    /* 
        When an item is dropped onto a crafting slot
     */
    public void DropResponse()
    {
        ItemSlot dropSlot = slots[(int)dropSlotIndex.value];

        // if (floatingItemMasterIndex.value >= 0) 
        // {
            Item dropItem = GetFloatingItem();
            Debug.Log("getting floating item: " + dropItem.name);

            if (!dropSlot.isEmpty())
            {
                // save the item originally in the dropSlot
                // floatingItemMasterIndex.value = craftingList.GetIndexForItem(dropSlot.item);
                SaveFloatingItem(dropSlot.item);
                craftingList.RemoveItem(dropSlot.item);
                
                SaveTheFloatingItemEvent.Invoke();
            }

            dropSlot.AddItem(dropItem);
            craftingList.AddItem(dropItem);

            // SuccessfulDropEvent.Invoke();
        // }
    }

    /*
        clears the slot of the item that was in it
     */    
    public void ResetSlotResponse() 
    {
        ItemSlot slot = slots[(int)startSlotIndex.value];
        Item item = slot.item;

        Debug.Log("Should be saving this item into floating var: " + item.name);

        SaveFloatingItem(item);

        slot.ClearSlot();
        craftingList.RemoveItem(item);

        SaveTheFloatingItemEvent.Invoke();
    }

    /* When a crafting request comes in */
    public void CraftRequestResponse()
    {
        // TODO: will need to change how the results are output, to account for stats
        Recipe possibleRecipe = craftingList.GetRecipeForItems(craftingList.items);

        if (!craftingList.isEmpty() && 
            possibleRecipe != null)
        {
            Debug.Log("recipe exists! creating " + possibleRecipe.name);

            // invokes the event for each recipe result
            foreach (Item item in possibleRecipe.results)
            {
                SaveFloatingItem(item);
                SaveTheFloatingItemEvent.Invoke();
            }

            ClearCraftingSlots();
        }
    }

    #endregion

}
