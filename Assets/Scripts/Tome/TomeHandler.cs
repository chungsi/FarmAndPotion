using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomeHandler : MonoBehaviour
{
    [SerializeField] Transform pagesParent;
    [SerializeField] SpreadObject spreadPrefab;
    [SerializeField] PageObject pagePrefab;
    [SerializeField] PotionPageObject potionPagePrefab;

    private ItemHelper itemHelper = new ItemHelper();
    private List<SpreadObject> spreads = new List<SpreadObject>();
    private int currentIndex = 0;

    void Start()
    {
        CreateTomePages();
        SetFirstPagesVisible();
    }

    public void SetFirstPagesVisible()
    {
        spreads[0].SetActive(true);
    }

    private void CreateTomePages()
    {
        // Create ingredient pages
        CreateSpreads(
            spreadType: SpreadType.Item,
            pagePrefab: pagePrefab,
            itemList: GetSortedIngredientsList().Cast<Item>().ToList()
        );

        // Create potion pages
        CreateSpreads(
            spreadType: SpreadType.Potion,
            pagePrefab: potionPagePrefab,
            itemList: itemHelper.GetMasterPotionsList().Cast<Item>().ToList()
        );
    }
    
    // Creates a spread for every even number of pages and
    // calls the spread instantiate method to create the pages within it.
    private void CreateSpreads(SpreadType spreadType, PageObject pagePrefab, List<Item> itemList)
    {
        for (int i = 0; i < itemList.Count(); i+=2)
        {
            SpreadObject spread = SpreadObject.Instantiate(spreadPrefab, pagesParent);

            // Check if there is an even number of items left;
            // then instantiate the spread (with either next item or null value).
            Item i2 = null;
            if (i+1 < itemList.Count()) 
                i2 = itemList[i+1];
            spread.Instantiate(spreadType, pagePrefab, itemList[i], i2);
            
            spread.SetActive(false);
            spreads.Add(spread);
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

    private void SetActiveSpreadToIndex(int index)
    {
        spreads[currentIndex].SetActive(false);
        spreads[index].SetActive(true);
        currentIndex = index;
    }

    #region Event Responses

    public void HandleNextPageRequest()
    {
        int nextIndex = currentIndex + 1;
        if (spreads.Count() > nextIndex)
        {
            spreads[currentIndex].SetActive(false);
            spreads[nextIndex].SetActive(true);
            currentIndex = nextIndex;
        }
    }

    public void HandlePrevPageRequest()
    {
        int prevIndex = currentIndex - 1;
        if (prevIndex >= 0)
        {
            spreads[currentIndex].SetActive(false);
            spreads[prevIndex].SetActive(true);
            currentIndex = prevIndex;
        }
    }

    public void GoToIngredientBookmark()
    {
        int index = spreads.FindIndex(spread => spread.GetSpreadType() == SpreadType.Item);
        if (index >= 0) // check null
        {
            SetActiveSpreadToIndex(index);
        }
    }

    public void GoToPotionBookmark()
    {
        int index = spreads.FindIndex(spread => spread.GetSpreadType() == SpreadType.Potion);
        if (index >= 0) // check null
        {
            SetActiveSpreadToIndex(index);
        }
    }

    #endregion
}
