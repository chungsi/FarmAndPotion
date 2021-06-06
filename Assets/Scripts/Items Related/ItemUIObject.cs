using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemUIObject : ItemObject, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IPointerEnterHandler
{
    // Use a UI element here instead of standard Sprite
    [SerializeField] private Image artwork;

    [Header("Input things")]

    [SerializeField] private InputReader inputReader = default;
    [SerializeField] private bool isDraggable = false;

    [Header("Events")]

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


    new public Item Item
    {
        get => item;
        set
        {
            item = value;
            artwork.sprite = item.Artwork;
        }
    }

    new void OnEnable()
    {
        if (item != null)
            artwork.sprite = item.Artwork;

        base.OnEnable();
    }

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
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
