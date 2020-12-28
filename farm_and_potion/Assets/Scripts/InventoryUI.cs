using UnityEngine;
using UnityEngine.UI;
using TMPro;
// using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour 
{
    public Inventory inventory;
    public TextMeshProUGUI displayText;
    public Transform itemsParent;
    public ItemUI itemUIPrefab;

    public FloatVariable draggedSlotIndex;
    public FloatVariable dropSlotIndex;

    public Image draggedItem;
    // private CanvasGroup draggedItemCanvasGroup;

    private InventorySlot startDraggedSlot;
    private InventorySlot draggedSlot;

    InventorySlot[] slots;

    void Start()
    {
        Debug.Log("InventoryUI Start");
        if (inventory != null)
            inventory.onItemChangedCallback += SetupUI;
        
        // if (draggedItem != null)
        //     draggedItemCanvasGroup = draggedItem.GetComponent<CanvasGroup>();

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void SetupUI()
    {
        Debug.Log("InventoryUI is updating!");

        for (int i = 0; i < inventory.items.Count; i++)
        {
            // ItemUI ui = ItemUI.Instantiate(itemUIPrefab, slots[i].transform);
            // ui.SetItem(inventory.items[i]);
            slots[i].AddItem(inventory.items[i]);
        }
    }

    #region EventResponses

    public void DebugInventoryUI() 
    {
        Debug.Log("InventoryUI has detected an event from a listener!");
    }

    public void InventoryBeginDragResponse() 
    {
        int index = (int)draggedSlotIndex.value;
        startDraggedSlot = slots[index];

        draggedItem.enabled = true;
        draggedItem.sprite = startDraggedSlot.item.artwork;

        Debug.Log(startDraggedSlot.item.name + " is being dragged from slot " + index);
    }

    public void InventoryDragResponse() 
    {
        draggedItem.transform.position = Input.mousePosition;
    }

    public void InventoryDragEndResponse()
    {
        draggedItem.enabled = false;
    }

    public void InventoryDropResponse() 
    {
        InventorySlot dropSlot = slots[(int)dropSlotIndex.value];
        Debug.Log("Drop slot is index " + dropSlotIndex.value);

        if (startDraggedSlot != null && dropSlot.isEmpty()) 
        {
            Debug.Log(startDraggedSlot.item.name + " is being dropped into " + dropSlot.name);
            dropSlot.AddItem(startDraggedSlot.item);
            startDraggedSlot.ClearSlot();
        } 
        else if (startDraggedSlot != null && !dropSlot.isEmpty()) 
        {
            Item savedItem = dropSlot.item; // to save a temp before things get overridden

            dropSlot.AddItem(startDraggedSlot.item);
            startDraggedSlot.AddItem(savedItem);
        }

        startDraggedSlot = null;
        draggedItem.enabled = false;
    }

    #endregion
    
}
