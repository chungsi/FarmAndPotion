using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


// Might want to rename to ItemSlot later
public class ItemSlot : MonoBehaviour, IDropHandler
{
    public Item item;
    [Space]
    public FloatVariable dropSlotIndex;
    [Space]
    public UnityEvent DropEvent;

    private int indexWithinContainer;

    void Start() {
        indexWithinContainer = transform.GetSiblingIndex();
    }

    public void AddItem(Item newItem)
    {
    }

    public void ClearSlot()
    {
    }

    public bool isEmpty() {

        return transform.childCount <= 0;
    }

    public int GetIndexWithinContainer()
    {
        return indexWithinContainer;
    }

    public void OnDrop(PointerEventData eventData) 
    {
        // Debug.Log(name + " has detected a drop into itself; index is " + indexWithinContainer);

        dropSlotIndex.value = indexWithinContainer;

        DropEvent.Invoke();
    }

}
