using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    // the delegate allows other scripts & classes to subscribe to when this is invoked
    // can take inputs and return outputs
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 12;

    public List<Item> items;
    public Transform itemsParent;

    void Start() 
    {
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public bool AddItem(Item item)
    {
        // check if inventory is full; could change to a function
        if (items.Count >= space)
        {
            Debug.Log("Not enough space in the inventory!!");
            return false;
        }

        Debug.Log("Adding item to inventory");
        items.Add(item);

        // if(onItemChangedCallback != null) {
        //     onItemChangedCallback.Invoke();
        //     Debug.Log("invoked the onItemCHangedCallback");
        // }

        return true;
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);

        // if (onItemChangedCallback != null)
        //     onItemChangedCallback.Invoke();
    } 

}
