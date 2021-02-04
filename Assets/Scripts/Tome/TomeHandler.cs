using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomeHandler : MonoBehaviour
{
    [SerializeField] PageObject pagePrefab;
    [SerializeField] PotionPageObject potionPagePrefab;
    [SerializeField] Transform pagesParent;

    private ItemHelper itemHelper = new ItemHelper();
    private List<PageObject> pageObjects = new List<PageObject>();
    private int currentEvenIndex = 0;

    void Start()
    {
        GetTomePages();
        SetFirstPagesVisible();
    }

    public void SetFirstPagesVisible()
    {
        pageObjects[0].SetActive(true);
        pageObjects[1].SetActive(true);
    }

    private void GetTomePages()
    {
        List<Ingredient> ingredients = GetSortedIngredientsList();
        foreach (Ingredient ingredient in ingredients)
        {
            PageObject page = PageObject.Instantiate(pagePrefab, pagesParent);
            page.setItem((Item)ingredient);
            page.SetActive(false); // disable it to be invisible first
            pageObjects.Add(page);
        }

        List<Potion> potions = itemHelper.GetMasterPotionsList();
        foreach (Potion potion in potions)
        {
            PotionPageObject page = PotionPageObject.Instantiate(potionPagePrefab, pagesParent);
            page.setItem((Item)potion);
            page.SetActive(false);
            pageObjects.Add(page);
        }
    }

    // Sort the ingredients list by the group first.
    // Need some queries and traversals, so it's own method.
    private List<Ingredient> GetSortedIngredientsList()
    {
        List<Ingredient> ingredients = new List<Ingredient>();
        var ingrGroups = 
            itemHelper.GetMasterIngredientsList()
            .GroupBy(ingr => ingr.GetIngredientGroup())
            .OrderBy(ingr => ingr.Key)
            .ToList();
        
        // Loop through the ingredient grouping
        foreach (var ingrGroup in ingrGroups)
        {
            // Loop through each ingredient within that group
            foreach (Ingredient ingr in ingrGroup)
            {
                ingredients.Add(ingr);
            }
        }

        return ingredients;
    }

    #region Event Responses

    public void HandleNextPageRequest()
    {
        // Check if next index exists
        int nextEvenIndex = currentEvenIndex + 2;
        if (pageObjects.Count() > nextEvenIndex)
        {
            pageObjects[currentEvenIndex].SetActive(false);
            pageObjects[nextEvenIndex].SetActive(true);

            // Disable right page regardless because there could be odd num of pages
            if (pageObjects.Count() > currentEvenIndex+1)
                pageObjects[currentEvenIndex+1].SetActive(false);

            int nextOddIndex = currentEvenIndex + 3;
            if (pageObjects.Count() > nextOddIndex)
                pageObjects[nextOddIndex].SetActive(true);

            currentEvenIndex = nextEvenIndex;
        }
    }

    public void HandlePrevPageRequest()
    {
        // If the previous even index is greater than zero,
        // it's guaranteed there's an odd numbered page too
        int prevEvenIndex = currentEvenIndex - 2;
        if (prevEvenIndex >= 0)
        {
            pageObjects[currentEvenIndex].SetActive(false);

            // Double check if there's an existing odd numbered page to disable
            if (pageObjects.Count() > currentEvenIndex+1)
                pageObjects[currentEvenIndex+1].SetActive(false);

            pageObjects[prevEvenIndex].SetActive(true);
            pageObjects[prevEvenIndex+1].SetActive(true);

            currentEvenIndex = prevEvenIndex;
        }
    }

    #endregion
}
