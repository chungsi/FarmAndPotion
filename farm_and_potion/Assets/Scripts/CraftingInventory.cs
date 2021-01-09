using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu]
public class CraftingInventory : Inventory
{
    [SerializeField]
    private List<Stat> allStats = new List<Stat>();
    [SerializeField]
    private List<Ailment> craftingItemAilments = new List<Ailment>();
    private List<Recipe> recipes = new List<Recipe>();

    public override void OnValidate()
    {
        base.OnValidate();
        PopulateCraftingList();
        PopulateStatList();
    }

    public override void OnEnable()
    {
        // Debug.Log("crafting inventory initialize");
    }

    // is there a better way to optimize this? see if a key exists first?
    private void PopulateCraftingList() 
    {
        // Debug.Log("Populating master list of recipes...");
        recipes.Clear();

        string[] assetNames = AssetDatabase.FindAssets("t:Recipe", new[] { "Assets/Recipes" });
        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var recipe = AssetDatabase.LoadAssetAtPath<Recipe>(SOpath);

            recipes.Add(recipe);
        }
    }

    private void PopulateStatList() 
    {
        // Debug.Log("Populating master list of recipes...");
        allStats.Clear();

        string[] assetNames = AssetDatabase.FindAssets("t:Stat", new[] { "Assets/Stats" });
        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var stat = AssetDatabase.LoadAssetAtPath<Stat>(SOpath);

            allStats.Add(stat);
        }
    }

    public override bool AddItem(Item item)
    {
        if (items.Count >= space)
            return false;
        
        items.Add(item);
        craftingItemAilments = craftingItemAilments.Concat(item.GetAilments()).ToList();

        return true;
    }

    public override bool RemoveItem(Item item)
    {
        if (!items.Contains(item))
            return false;

        craftingItemAilments = craftingItemAilments.Except(item.GetAilments()).ToList();
        items.Remove(item);
        return true;
    }

    public override void ClearInventory()
    {
        base.ClearInventory();
        craftingItemAilments.Clear();
    }

    public Recipe GetRecipeForCurrent()
    {
        foreach (Recipe recipe in recipes)
        {
            List<Ailment> recipeAilments = recipe.GetAilments();
            
            // checks for size equivalence first because .All method specs.... but change?
            if (recipeAilments.Count == craftingItemAilments.Count &&
                recipeAilments.All(craftingItemAilments.Contains))
            {
                return recipe;
            }
        }

        return null;
    }

    public List<Item> GetRecipeOutputsForCurrent()
    {
        Recipe recipe = GetRecipeForCurrent();

        if (recipe != null)
            return recipe.GetResults();

        return null;
    }

    public Dictionary<Stat, int> CalculateStats()
    {
        Dictionary<Stat, int> newStats = new Dictionary<Stat, int>();

        Debug.Log("calculating stats... ");

        foreach (Stat stat in allStats)
        {
            int statValue = 0;

            // looping through list of crafting items because could be an indefinite amount
            foreach (Item item in items)
            {
                if (item.GetStatsDictionary().ContainsKey(stat))
                {
                    statValue += item.GetStatsDictionary()[stat];
                    // could somehow add modifiers & bonuses stuff here?
                }
            }
            
            newStats.Add(stat, statValue);
        }

        return newStats;
    }


    // public Recipe GetRecipeForItems(List<Item> inputs)
    // {
    //     foreach (Recipe recipe in recipes)
    //     {
    //         List<Item> recipeIngredients = recipe.ingredients;
    //         List<Item> craftingInputs = inputs;

    //         if (recipeIngredients.Count == craftingInputs.Count &&
    //             recipeIngredients.All(inputs.Contains))
    //             return recipe;
    //     }

    //     return null;

    //     // IEnumerable<Recipe> recipeQuery = 
    //     //     from recipe in recipes
    //     //     where recipe.ingredients.Equals(inputs)
    //     //     select recipe;
        
    //     // List<Recipe> list = recipeQuery.ToList<Recipe>();

    //     // Debug.Log("printing.... " + recipeQuery.Count() + "; list count... " + list.Count);

    //     // return recipeQuery.FirstOrDefault();
    // }

}
