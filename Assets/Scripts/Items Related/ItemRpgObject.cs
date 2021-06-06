using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemRpgObject : ItemObject, IInteractable
{
    [Header("Interaction Setup")]
    public UnityEvent ItemPickUpEvent;
    

    public void Interact(GameObject _other)
    {
        floatingItemSetIndex.value = itemObjectRuntimeSet.GetIndex(this.GetComponent<ItemObject>());
        Debug.Log($"master set index for interacted item {item.name}: {floatingItemSetIndex.value}");
        ItemPickUpEvent.Invoke();
    }
}
