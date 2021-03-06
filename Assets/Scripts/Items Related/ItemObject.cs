using UnityEngine;
using UnityEngine.UI;

public class ItemObject : MonoBehaviour
{
    [Header("Item Specs")]
    [SerializeField] protected Item item;
    [SerializeField] private GameObject spriteGO;
    
    [Space]
    
    [SerializeField] protected ItemObjectRuntimeSet itemObjectRuntimeSet;
    [SerializeField] protected FloatVariable floatingItemSetIndex;


    public Item Item
    {
        get => item;
        set
        {
            item = value;
            spriteGO.GetComponent<SpriteRenderer>().sprite = item.Artwork;
        }
    }

    
    protected void OnEnable()
    {
        if (item != null)
            spriteGO.GetComponent<SpriteRenderer>().sprite = item.Artwork;

        itemObjectRuntimeSet.Add(this);
    }

    void OnDisable()
    {
        // Debug.Log("disabling this ItemObject " + name + " " + this.item.name);
        itemObjectRuntimeSet.Remove(this);
    }

    public void Destroy()
    {
        Debug.Log(item.name + " is destroying itself");
        this.gameObject.SetActive(false);
        // Destroy(this.gameObject);
    }
}