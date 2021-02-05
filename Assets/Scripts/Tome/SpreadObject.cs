using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpreadType
{
    Item,
    Potion
}

public class SpreadObject : MonoBehaviour
{
    [SerializeField] SpreadType spreadType = new SpreadType();
    [SerializeField] List<PageObject> pageObjs = new List<PageObject>();

    // Setup for the spreads:
    // Set the spread type for fast travel,
    // Pass in page prefab to instantiate, and
    // Sets the page items.
    public void Instantiate(SpreadType spreadType, PageObject pagePrefab, Item item1, Item item2 = null)
    {
        this.spreadType = spreadType;
        CreatePageObjects(pagePrefab);
        SetPageItems(item1, item2);
    }

    public void SetActive(bool b)
    {
        this.gameObject.SetActive(b);
    }

    public SpreadType GetSpreadType()
    {
        return spreadType;
    }

    // Creates the prefabs within the spread object.
    // Could be one or two pages
    private void CreatePageObjects(PageObject pagePrefab)
    {
        for (int i = 0; i < 2; i++)
        {
            PageObject page = PageObject.Instantiate(pagePrefab, this.transform);
            page.SetActive(false); // disable by default
            pageObjs.Add(page);
        }
    }

    // Enable the pages if an item is valid;
    // Otherwise, leave it disabled.
    private void SetPageItems(Item i1, Item i2)
    {
        pageObjs[0].SetItem(i1);
        pageObjs[0].SetActive(true);

        // Check for empty item
        if (i2 != null)
        {
            pageObjs[1].SetItem(i2);
            pageObjs[1].SetActive(true);
        }
    }
}
