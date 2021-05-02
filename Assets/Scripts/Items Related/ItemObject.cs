using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemObject : BaseItemObject, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IPointerEnterHandler
{
    // Use a UI element here instead of standard Sprite
    [SerializeField] private Image artwork;

    [Space]

    [SerializeField] private bool isDraggable = false;

    [Space]

    public UnityEvent DragBeginEvent;
    public UnityEvent DraggingEvent;
    public UnityEvent DragEndEvent;
    public UnityEvent OnItemRightClick;
    public UnityEvent OnItemLeftClick;
    public UnityEvent OnItemHover;
    
    private CanvasGroup canvasGroup;
    private Transform startParent;
    

    // TODO: Add some kind of nullity check
    public ItemSlot ParentItemSlot => startParent.GetComponent<ItemSlot>();
    

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Destroy()
    {
        Debug.Log(item.name + " is destroying itself");
        Destroy(this.gameObject);
    }

    #region Interface Interaction Handlers

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (isDraggable)
            {
                // sets the floating index of the item in the set
                floatingItemSetIndex.value = itemObjectRuntimeSet.GetIndex(this);
                startParent = transform.parent;

                // canvasGroup.alpha = .6f;
                canvasGroup.blocksRaycasts = false;

                DragBeginEvent.Invoke();
            }
        }

        public void OnDrag(PointerEventData eventData) 
        {
            if (isDraggable)
            {
                // uses the image/sprite renderer separately to just move an image
                artwork.transform.position = Input.mousePosition;

                DraggingEvent.Invoke();
            }
        }

        public void OnEndDrag(PointerEventData eventData) 
        {
            if (isDraggable)
            {
                // uses the image/sprite renderer separately to just move an image
                artwork.transform.localPosition = new Vector3(0,0,0);
                // canvasGroup.alpha = 1f;
                canvasGroup.blocksRaycasts = true;

                DragEndEvent.Invoke();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            // if right click
            if (Input.GetMouseButtonDown(1))
            {
                floatingItemSetIndex.value = itemObjectRuntimeSet.GetIndex(this);
                OnItemRightClick.Invoke();
            }
            // if left click
            else if (Input.GetMouseButton(0))
            {
                floatingItemSetIndex.value = itemObjectRuntimeSet.GetIndex(this);
                OnItemLeftClick.Invoke();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            // floatingItemSetIndex.value = itemObjectRuntimeSet.GetIndex(this);
            // OnItemHover.Invoke();
        }

    #endregion
}
