using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour {

    public Transform itemsParent;

    Inventory inventory;

    InventorySlot[] slots;

    public Image draggedItem;
    private InventorySlot draggedSlot;

    void Start()
    {
        Debug.Log("InventoryUI Start");
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        UpdateUI();
    }

    void UpdateUI()
    {
        Debug.Log("Updating UI");
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
                GameObject gameObject = Instantiate(
                    inventory.itemPrefab, 
                    itemsParent.GetChild(i).transform.position, 
                    Quaternion.identity);
                gameObject.transform.SetParent(itemsParent.GetChild(i));
                /* TODO: investigate if it's an issue that the prefab is instantiating at a weird scale
                         I've manually set it in the code, but it just so happens that it seems the prefab
                         does this when dragged into a parent that's not the "Canvas"...
                         using .SetParent(..., false) fixes it, but then the position is wonky... :'(
                 */
                gameObject.transform.localScale = new Vector3(1,1,1);
                /* 
                    Object pooling 
                    finite set of items that could enable/disable at run time
                    calling the GetComponent a lot is resource heavy, so should be careful to call
                    only in Start/Awake if possible
                 */
                var temp = gameObject.GetComponent<ItemHandler>();
                temp.SetItem(inventory.items[i]);

                // Item tempItem = (Item)gameObject.GetComponent("item");
            } else {
                slots[i].ClearSlot();
            }
        }
    }
    
}
