using UnityEngine;
using UnityEngine.EventSystems;

public class DropHandler : MonoBehaviour, IDropHandler
{

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(name + " on drop");

        // maybe here detect what has been dropped and update the slot(s) themselves with new data

        if (eventData.pointerDrag != null) {
            // set position of dragged item to the position of the drop container
            eventData.pointerDrag.transform.position = this.transform.position;
        }
    }

}
