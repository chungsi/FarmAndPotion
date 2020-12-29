using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
// using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour 
{
    public Inventory inventory;
    public TextMeshProUGUI displayText;
    public Transform itemsParent;
    public Image draggedItem;
    public ItemUI itemUIPrefab;
    [Space]
    public FloatVariable draggedSlotIndex;
    public FloatVariable dropSlotIndex;
    public FloatVariable floatingItemMasterIndex;

    // private CanvasGroup draggedItemCanvasGroup;
    private ItemSlot startDraggedSlot;
    private ItemSlot draggedSlot;

    ItemSlot[] slots;

    void Start()
    {
        Debug.Log("InventoryUI Start");

        // if (draggedItem != null)
        //     draggedItemCanvasGroup = draggedItem.GetComponent<CanvasGroup>();
        
        slots = itemsParent.GetComponentsInChildren<ItemSlot>();

        SetupUI();
    }

    void SetupUI()
    {
        Debug.Log("InventoryUI is updating!");

        for (int i = 0; i < inventory.GetItemCount(); i++)
        {
            // ItemUI ui = ItemUI.Instantiate(itemUIPrefab, slots[i].transform);
            // ui.SetItem(inventory.items[i]);
            slots[i].AddItem(inventory.items[i]);
        }
    }

    private void EnableDragUIForItem(Item i)
    {
        draggedItem.enabled = true;
        draggedItem.sprite = i.artwork;
    }

    private void ResetDragUI()
    {
        draggedItem.enabled = false;
        // floatingItemMasterIndex.value = -1f;
        startDraggedSlot = null;
    }

    #region EventResponses

    public void BeginDragResponse() 
    {
        int index = (int)draggedSlotIndex.value;
        startDraggedSlot = slots[index];
        Item item = startDraggedSlot.item;

        floatingItemMasterIndex.value = inventory.GetIndexForItem(item);

        EnableDragUIForItem(item);

        Debug.Log(item.name + " is being dragged from slot " + index);
    }

    public void DragResponse() 
    {
        draggedItem.transform.position = Input.mousePosition;
    }

    public void DragEndResponse()
    {
        // only disable the dragging image
        draggedItem.enabled = false;
    }

    public void DropResponse() 
    {
        ItemSlot dropSlot = slots[(int)dropSlotIndex.value];
        Debug.Log("Drop slot is index " + dropSlotIndex.value);

        // Just drop the item when dropSlot is empty
        if (startDraggedSlot != null && dropSlot.isEmpty()) 
        {
            Debug.Log(startDraggedSlot.item.name + " is being dropped into " + dropSlot.name);
            dropSlot.AddItem(startDraggedSlot.item);
            startDraggedSlot.ClearSlot();
        } 
        // Swap the two items if the dropSlot isn't empty
        else if (startDraggedSlot != null && !dropSlot.isEmpty()) 
        {
            Item savedItem = dropSlot.item;

            dropSlot.AddItem(startDraggedSlot.item);
            startDraggedSlot.AddItem(savedItem);
        }

        ResetDragUI();
    }

    // this means the item has successfully been dropped into a crafting slot
    public void CraftingPanelDropResponse()
    {
        inventory.RemoveItem(startDraggedSlot.item);
        startDraggedSlot.ClearSlot();

        Debug.Log("InventoryUI cleaned itself up; size is now " + inventory.GetItemCount());
        
        ResetDragUI();
    }

    // add the "floating item" to the inventory
    public void InsertAFloatingItem() {
        Debug.Log($"{inventory.name}'s size is currently: { inventory.GetItemCount() }");
        Item newItem = inventory.GetItemForIndex((int)floatingItemMasterIndex.value);
        inventory.AddItem(newItem);

        foreach (ItemSlot slot in slots)
        {
            if (slot.isEmpty())
            {
                slot.AddItem(newItem);
                break;
            }
        }
    }

    #endregion
    
}
