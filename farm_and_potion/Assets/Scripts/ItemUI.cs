using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour
//, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    public Item item;
    public Image artwork;

    public UnityEvent DragBeginEvent;
    public UnityEvent DraggingEvent;
    public UnityEvent DragEndEvent;

    private Vector3 startPosition;
    private Transform startParent;
    private CanvasGroup iconCanvasGroup;

    void OnValidate() {
        if (item != null)
            artwork.sprite = item.artwork;
    }

    void Start() {
        iconCanvasGroup = artwork.GetComponent<CanvasGroup>();
    }

    public void SetItem(Item newItem) {
        item = newItem;
        artwork.sprite = item.artwork;
    }

    // public void OnBeginDrag(PointerEventData eventData) 
    // {
    //     startPosition = this.transform.position;
    //     canvasGroup.alpha = .6f;

    //     DragBeginEvent.Invoke();
    // }

    // public void OnDrag(PointerEventData eventData) 
    // {
    //     eventData.pointerDrag.transform.position = Input.mousePosition;

    //     DraggingEvent.Invoke();
    // }

    // public void OnEndDrag(PointerEventData eventData) 
    // {
    //     // eventData.pointerDrag.transform.position = startPosition;
    //     canvasGroup.alpha = 1f;

    //     DragEndEvent.Invoke();
    // }

}
