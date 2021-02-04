using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PageObject : MonoBehaviour
{

    public Item item;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI itemTypeText;
    public TextMeshProUGUI descripText;
    public Image artwork;

    void Start()
    {
        if (item != null)
            updatePage();
    }

    public void setItem(Item newItem) {
        item = newItem;
        updatePage();
    }

    public void updatePage() {
        nameText.text = item.name;
        descripText.text = item.GetDescription();
        artwork.sprite = item.artwork;

        if (item is Ingredient)
            itemTypeText.text = ((Ingredient)item).GetIngredientGroup().GetDisplayText();
    }

    public void SetActive(bool b)
    {
        gameObject.SetActive(b);
    }
}
