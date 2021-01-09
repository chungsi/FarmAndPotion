using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BookPage : MonoBehaviour
{

    public Item item;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descripText;
    public Image artwork;
    [Space]
    public ItemObjectRuntimeSet inventorySet;
    public FloatVariable itemSetIndex;

    void Start()
    {
        if (item != null)
            updatePage();
    }

    public void setItem(Item newItem) {
        item = newItem;
    }

    public void updatePage() {
        nameText.text = item.name;
        descripText.text = item.GetDescription();
        artwork.sprite = item.artwork;
    }

    #region Event Responses

    public void ShowInfoOnClickedItem()
    {
        ItemObject newItem = inventorySet.GetItem((int)itemSetIndex.value);
        this.item = newItem.GetItem();
        updatePage();
    }

    #endregion

}
