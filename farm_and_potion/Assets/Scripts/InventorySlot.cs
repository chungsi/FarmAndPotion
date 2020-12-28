using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


// Might want to rename to ItemSlot later
public class InventorySlot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    public Image icon;
    public Item item;

    public FloatVariable draggedSlotIndex;
    public FloatVariable dropSlotIndex;
    
    public UnityEvent DragBeginEvent;
    public UnityEvent DraggingEvent;
    public UnityEvent DragEndEvent;
    public UnityEvent DropEvent;

    private int indexWithinContainer;

    void Start() {
        indexWithinContainer = transform.GetSiblingIndex();
    }

    public void AddItem(Item newItem)
    {
        Debug.Log(newItem.name + " is being added to this inventory slot");
        item = newItem;

        Debug.Log("item's name is now: " + item.name);

        icon.sprite = item.artwork;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
    }

    public bool isEmpty() {
        return item == null;
    }

    public void OnBeginDrag(PointerEventData eventData) 
    {
        if (item != null) 
        {
            Debug.Log(name + " has detected a drag on itself; index is " + indexWithinContainer);
            draggedSlotIndex.value = indexWithinContainer;
            DragBeginEvent.Invoke();
        }
    }

    public void OnDrag(PointerEventData eventData) 
    {
        if (item != null) {
            DraggingEvent.Invoke();
        }
    }

    public void OnEndDrag(PointerEventData eventData) 
    {
        if (item != null) {
            DragEndEvent.Invoke();
        }
    }

    public void OnDrop(PointerEventData eventData) 
    {
        Debug.Log(name + " has detected a drop into itself; index is " + indexWithinContainer);
        dropSlotIndex.value = indexWithinContainer;
        DropEvent.Invoke();
    }

}
