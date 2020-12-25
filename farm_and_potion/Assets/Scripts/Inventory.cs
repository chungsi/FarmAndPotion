using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    #region Singleton

    // following Brackeys tutorial, makes one instance of an inventory
    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!!!");
            return;
        }

        instance = this;
    }

    #endregion

    // the delegate allows other scripts & classes to subscribe to when this is invoked
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 12;

    public List<Item> items;
    public List<Item> startItems;
    public GameObject itemPrefab;

    void Start() {
        foreach (Item item in startItems)
        {
            AddItem(item);
        }
    }

    public bool AddItem(Item item)
    {
        if (items.Count >= space)
        {
            Debug.Log("Not enough space in the inventory!!");
            return false;
        }

        Debug.Log("Adding item to inventory");
        items.Add(item);

        if(onItemChangedCallback != null) {
            onItemChangedCallback.Invoke();
            Debug.Log("invoked the onItemCHangedCallback");
        }

        return true;
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    } 

}
