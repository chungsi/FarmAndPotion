using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


// Might want to rename to ItemSlot later
public class ItemSlot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IPointerDownHandler
{
    public Image icon;
    public Item item;
    [Space]
    public FloatVariable startSlotIndex;
    public FloatVariable dropSlotIndex;
    [Space]
    public UnityEvent DragBeginEvent;
    public UnityEvent DraggingEvent;
    public UnityEvent DragEndEvent;
    public UnityEvent DropEvent;
    public UnityEvent OnItemRightClick;

    private int indexWithinContainer;
    private CanvasGroup iconCanvasGroup;

    void Start() {
        indexWithinContainer = transform.GetSiblingIndex();
        iconCanvasGroup = icon.GetComponent<CanvasGroup>();
    }

    public void AddItem(Item newItem)
    {
        // Debug.Log(newItem.name + " is being added to this inventory slot");
        item = newItem;

        // Debug.Log("item's name is now: " + item.name);

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
            // Debug.Log(name + " has detected a drag on itself; index is " + indexWithinContainer);
            startSlotIndex.value = indexWithinContainer;
            iconCanvasGroup.alpha = .6f;

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
        iconCanvasGroup.alpha = 1f;

        if (item != null) {
            DragEndEvent.Invoke();
        }
    }

    public void OnDrop(PointerEventData eventData) 
    {
        // Debug.Log(name + " has detected a drop into itself; index is " + indexWithinContainer);
        dropSlotIndex.value = indexWithinContainer;
        iconCanvasGroup.alpha = 1f;

        DropEvent.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // if right click
        if (item != null && Input.GetMouseButtonDown(1))
        {
            startSlotIndex.value = indexWithinContainer;
            Debug.Log($"Right click on a filled slot detected; index of crafting slot is {indexWithinContainer}");
            OnItemRightClick.Invoke();
        }
    }

}
