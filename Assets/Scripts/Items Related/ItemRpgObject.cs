using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemRpgObject : BaseItemObject, IInteractable
{
    [Header("Interaction Setup")]
    public UnityEvent ItemPickUpEvent;
    

    public void Interact(GameObject _other)
    {
        ItemPickUpEvent.Invoke();
    }
}
