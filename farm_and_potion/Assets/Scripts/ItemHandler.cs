using UnityEngine;
using UnityEngine.UI;

public class ItemHandler : Interactable
{


    // temp: when an item is clicked, and it's "wild", add to inventory

    public Item item;
    public Image artwork;

    bool isPickedUp;

    void OnValidate() {
        if (item != null)
            artwork.sprite = item.artwork;
    }

    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picking up " + item.name);
        bool wasPickedUp = Inventory.instance.AddItem(item);

        if (wasPickedUp)
            Destroy(gameObject);
    }

    public void SetItem(Item newItem) {
        item = newItem;
        artwork.sprite = item.artwork;
    }

}
