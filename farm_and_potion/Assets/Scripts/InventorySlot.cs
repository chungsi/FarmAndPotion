using UnityEngine;
using UnityEngine.UI;

// Might want to rename to ItemSlot later
public class InventorySlot : MonoBehaviour
{

    public Image icon;
    public Item item;


    GameObject currentItemObject;

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.artwork;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
    }

}
