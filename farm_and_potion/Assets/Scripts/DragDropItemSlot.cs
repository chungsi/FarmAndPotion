using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropItemSlot : MonoBehaviour, IDropHandler
{

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(name + " on drop");

        // maybe here detect what has been dropped and update the slot(s) themselves with new data

        // if (eventData.pointerDrag != null) {
        //     // Vector2 slotPos = GetComponent<RectTransform>().anchoredPosition;

        //     Debug.Log("item pos: " + eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition);
        //     Debug.Log("slot pos: " + GetComponent<RectTransform>().anchoredPosition);

        //     eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        // }
    }

}
