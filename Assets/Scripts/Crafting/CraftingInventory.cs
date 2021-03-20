using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu]
public class CraftingInventory : Inventory
{
    // private List<ItemStat> inputIngredientMainStats = new List<ItemStat>();

    private List<Recipe> recipes = new List<Recipe>();

    private ItemHelper itemHelper = new ItemHelper();


    public List<Recipe> Recipes => recipes;


    public override void OnEnable()
    {
        GetAllPotionRecipes();
    }

    public override bool AddItem(Item item)
    {
        if (items.Count >= space)
        {
            return false;
        }
        
        items.Add(item);
        return true;
    }

    public override bool RemoveItem(Item item)
    {
        if (!items.Contains(item))
        {
            return false;
        }

        items.Remove(item);
        return true;
    }

    // For the current items in the inventory, will look for a possible recipe.
    public Recipe GetRecipeForCurrentIngredients()
    {
        List<ItemStat> ingredientMainStats = GetCurrentIngredientsMainStats();
        
        foreach (Recipe recipe in recipes)
        {
            List<ItemStat> recipeInputs = recipe.StatInputs;

            // One way: sort the two lists first, then check sequence equivalence.
            // recipeInputs.Sort();
            // ingredientMainStats.Sort();
            // if (recipeInputs.SequenceEqual(inputIngGroups))
            
            // Other way: use hash sets and set equivalence.
            if (new HashSet<ItemStat>(recipeInputs).SetEquals(ingredientMainStats))
            {
                return recipe;
            }
        }
        return null;
    }
    
    // Gets a list of recipe outputs for the current items in the inventory.
    public Potion GetRecipeOutputForCurrentIngredients()
    {
        Recipe recipe = GetRecipeForCurrentIngredients();
        if (recipe != null)
        {
            return recipe.Result;
        }
        return null;
    }

    // TODO: The current functionality with secondary stats is to remove duplicates.
    // This may be changed in the future, like if duplicates makes that attribute stronger...
    public List<ItemStat> GetOutputSecondaryItemStats()
    {
        List<ItemStat> tempSecondaryStats = new List<ItemStat>();
        foreach (Item item in items)
        {
            tempSecondaryStats.AddRange(item.SecondaryStats);
        }

        // Cast to a HashSet and back to quickly remove duplicates.
        List<ItemStat> noDupesSecondaryStats = new HashSet<ItemStat>(tempSecondaryStats).ToList();
        
        // Remove the duplicate stats with the main stats.
        List<ItemStat> noDupesWithMainStats = RemoveDuplicateStatsAndReturnNew(noDupesSecondaryStats, GetCurrentIngredientsMainStats());
        return noDupesWithMainStats;
    }

    // Converse to the secondary stats, main stats can have duplicates.
    public List<ItemStat> GetCurrentIngredientsMainStats()
    {
        var itemsString = new StringBuilder();
        List<ItemStat> currentMainStats = new List<ItemStat>();
        foreach (Item item in items)
        {
            currentMainStats.AddRange(item.MainStats);
            itemsString.Append(item.Name + ", ");
        }

        var stringOutput = new StringBuilder();
        foreach (ItemStat stat in currentMainStats)
        {
            stringOutput.Append(stat.name + ", ");
        }
        Debug.Log("Curent ingredients: " + itemsString + "\n  Main ingredients: " + stringOutput);

        return currentMainStats;
    }

    // Remove the stats in the _dupeStatList from the _primaryStatList if they exist.
    // This is useful for removing stats from the secondary stats that exist in the main stats.
    private List<ItemStat> RemoveDuplicateStatsAndReturnNew(List<ItemStat> _primaryStatList, List<ItemStat> _dupeStatList)
    {
        List<ItemStat> newStatList = _primaryStatList;
        
        foreach (ItemStat stat in _dupeStatList)
        {
            // stat exists in the primary list
            if (newStatList.IndexOf(stat) >= 0)
            {
                newStatList.Remove(stat);
            }
        }

        return newStatList;
    }

    // private void RemoveItemMainStatsFromList(Item item)
    // {
    //     foreach (ItemStat stat in item.MainStats)
    //     {
    //         inputIngredientMainStats.Remove(stat);
    //     }
    // }

    // TODO: Is there a better way to optimize this? see if a key exists first?
    private void GetAllPotionRecipes() 
    {
        recipes.Clear();

        List<Potion> allPotions = itemHelper.GetMasterPotionsList();
        foreach (Potion potion in allPotions)
        {
            foreach (Recipe recipe in potion.Recipes)
            {
                recipes.Add(recipe);
            }
        }
    }
}
