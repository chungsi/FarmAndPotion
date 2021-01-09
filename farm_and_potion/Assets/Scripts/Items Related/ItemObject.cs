using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IPointerEnterHandler
{

    [SerializeField] Item item;
    public Image artwork;
    public ItemObjectRuntimeSet itemObjectRuntimeSet;
    public FloatVariable floatingItemSetIndex;
    [Space]
    public bool isDraggable = false;
    public UnityEvent DragBeginEvent;
    public UnityEvent DraggingEvent;
    public UnityEvent DragEndEvent;
    public UnityEvent OnItemRightClick;
    public UnityEvent OnItemLeftClick;
    public UnityEvent OnItemHover;
    
    private CanvasGroup canvasGroup;
    private Transform startParent;

    void OnEnable()
    {
        if (item != null)
            artwork.sprite = item.artwork;

        itemObjectRuntimeSet.Add(this);
    }

    void OnDisable()
    {
        itemObjectRuntimeSet.Remove(this);
    }

    void Start() 
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void SetItem(Item newItem) {
        item = newItem;
        artwork.sprite = item.artwork;
    }

    public Item GetItem()
    {
        return item;
    }

    public Dictionary<Stat, int> GetItemStats()
    {
        return item.GetStatsDictionary();
    }

    public void SetItemStats(Dictionary<Stat, int> newStats)
    {
        item.PopulateStats(newStats);
    }

    public Transform GetStartParentTransform()
    {
        return startParent;
    }

    public ItemSlot GetParentItemSlot()
    {
        return startParent.GetComponent<ItemSlot>();
    }

    public void Destroy()
    {
        Debug.Log(name + " is destroying itself");
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
            artwork.transform.position = Input.mousePosition;

            DraggingEvent.Invoke();
        }
    }

    public void OnEndDrag(PointerEventData eventData) 
    {
        if (isDraggable)
        {
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
