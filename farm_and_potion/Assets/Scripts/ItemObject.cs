using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler
{

    [SerializeField] Item item;
    public Image artwork;
    public InventoryRuntimeSet inventoryRuntimeSet;
    public FloatVariable floatingItemSetIndex;
    [Space]
    public UnityEvent DragBeginEvent;
    public UnityEvent DraggingEvent;
    public UnityEvent DragEndEvent;
    public UnityEvent OnItemRightClick;
    
    private CanvasGroup canvasGroup;
    private Transform startParent;

    // void OnValidate() {
    // }

    void OnEnable()
    {
        if (item != null)
            artwork.sprite = item.artwork;

        inventoryRuntimeSet.Add(this);
    }

    void OnDisable()
    {
        inventoryRuntimeSet.Remove(this);
    }

    void Start() 
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetItem(Item newItem) {
        item = newItem;
        artwork.sprite = item.artwork;
    }

    public Item GetItem()
    {
        return item;
    }

    public Dictionary<Stat, int> GetItemStats()
    {
        return item.GetStats();
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

    #region Interface Interaction Handlers

    public void OnBeginDrag(PointerEventData eventData) 
    {
        // sets the floating index of the item in the set
        floatingItemSetIndex.value = inventoryRuntimeSet.GetIndex(this);
        startParent = transform.parent;

        // canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;

        DragBeginEvent.Invoke();
    }

    public void OnDrag(PointerEventData eventData) 
    {
        artwork.transform.position = Input.mousePosition;

        DraggingEvent.Invoke();
    }

    public void OnEndDrag(PointerEventData eventData) 
    {
        artwork.transform.localPosition = new Vector3(0,0,0);
        // canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        DragEndEvent.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // if right click
        if (Input.GetMouseButtonDown(1))
        {
            floatingItemSetIndex.value = inventoryRuntimeSet.GetIndex(this);
            OnItemRightClick.Invoke();
        }
    }

    #endregion
}
