using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


// Might want to rename to ItemSlot later
public class ItemSlot : MonoBehaviour, IDropHandler
{
    public FloatVariable dropSlotIndex;

    public UnityEvent DropEvent;

    private int indexWithinContainer = -1;


    public bool IsEmpty => transform.childCount <= 0;

    public int IndexWithinContainer => indexWithinContainer;


    void Start()
    {
        indexWithinContainer = transform.GetSiblingIndex();
    }

    public void OnDrop(PointerEventData eventData) 
    {
        dropSlotIndex.value = indexWithinContainer;
        DropEvent.Invoke();
    }
}