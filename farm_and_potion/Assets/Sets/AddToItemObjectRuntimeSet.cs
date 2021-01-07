using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToItemObjectRuntimeSet : MonoBehaviour
{
    public ItemObjectRuntimeSet itemObjectRuntimeSet;
    public ItemObject itemObject;

    void OnEnable()
    {
        itemObjectRuntimeSet.Add(itemObject);
    }

    void OnDisable()
    {
        itemObjectRuntimeSet.Remove(itemObject);
    }
}
