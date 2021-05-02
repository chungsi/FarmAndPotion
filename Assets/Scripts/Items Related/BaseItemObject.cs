using UnityEngine;
using UnityEngine.UI;

public class BaseItemObject : MonoBehaviour
{
    [Header("Item Specs")]
    [SerializeField] protected Item item;
    protected Image artwork;
    
    [Space]
    
    [SerializeField] protected ItemObjectRuntimeSet itemObjectRuntimeSet;
    [SerializeField] protected FloatVariable floatingItemSetIndex;


    public Item Item
    {
        get => item;
        set
        {
            item = value;
            artwork.sprite = item.Artwork;
        }
    }

    
    void OnEnable()
    {
        if (item != null)
            artwork.sprite = item.Artwork;

        itemObjectRuntimeSet.Add(this);
    }

    void OnDisable()
    {
        // Debug.Log("disabling this ItemObject " + name + " " + this.item.name);
        itemObjectRuntimeSet.Remove(this);
    }
}