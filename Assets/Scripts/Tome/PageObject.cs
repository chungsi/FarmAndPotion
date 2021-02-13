using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PageObject : MonoBehaviour
{
    // TODO: consider making this more strongly typed?
    public Item item;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI itemTypeText;
    public TextMeshProUGUI descripText;
    public Image artwork;
    
    public virtual void SetItem(Item newItem) {
        item = newItem;
        UpdatePage();
    }

    public virtual void UpdatePage() {
        nameText.text = item.Name;
        descripText.text = item.Description;
        artwork.sprite = item.Artwork;

        // TODO: change this
        if (item is Ingredient)
            itemTypeText.text = ((Ingredient)item).Group.Name;
    }

    public void SetActive(bool b)
    {
        gameObject.SetActive(b);
    }
}
