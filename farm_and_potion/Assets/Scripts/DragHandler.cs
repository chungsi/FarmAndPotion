using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    private CanvasGroup draggedItemCanvasGroup;
    Vector3 startPosition;
    Transform startParent;

    private void Start()
    {
        draggedItemCanvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        startParent = transform.parent;
        
        draggedItemCanvasGroup.alpha = .6f;
        draggedItemCanvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        eventData.pointerDrag.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        draggedItemCanvasGroup.alpha = 1f;
        draggedItemCanvasGroup.blocksRaycasts = true;

        // check if it's been dropped in a position with a drop handler
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(name + " clicked");
    }

}
