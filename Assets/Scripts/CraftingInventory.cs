using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu]
public class CraftingInventory : Inventory
{
    [SerializeField]
    private List<ItemStat> allItemStats = new List<ItemStat>();
    [SerializeField]
    private List<IngredientGroup> inputIngGroups = new List<IngredientGroup>();
    private List<Recipe> recipes = new List<Recipe>();

    private ItemHelper statHelper = new ItemHelper();

    public override void OnValidate()
    {
        base.OnValidate();
        FillCraftingList();
        GetMasterItemStatsList();
    }

    public override void OnEnable()
    {
        // Debug.Log("crafting inventory initialize");
    }

    // is there a better way to optimize this? see if a key exists first?
    private void FillCraftingList() 
    {
        // Debug.Log("Populating master list of recipes...");
        recipes.Clear();

        // string[] assetNames = AssetDatabase.FindAssets("t:Recipe", new[] { "Assets/Recipes" });
        // foreach (string SOName in assetNames)
        // {
        //     var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
        //     var recipe = AssetDatabase.LoadAssetAtPath<Recipe>(SOpath);

        //     recipes.Add(recipe);
        // }
    }

    private void GetMasterItemStatsList() 
    {
        allItemStats.Clear();
        allItemStats = statHelper.GetMasterItemStatsList();
    }

    public override bool AddItem(Item item)
    {
        if (items.Count >= space)
            return false;
        
        items.Add(item);
        // inputIngGroups = inputIngGroups.Concat(item.GetItemStats()).ToList();
        inputIngGroups.Add(((Ingredient)item).Group);
        return true;
    }

    public override bool RemoveItem(Item item)
    {
        if (!items.Contains(item))
            return false;

        // inputIngGroups = inputIngGroups.Except(item.GetItemStats()).ToList();
        inputIngGroups.Remove(((Ingredient)item).Group);
        items.Remove(item);
        return true;
    }

    public override void ClearInventory()
    {
        base.ClearInventory();
        inputIngGroups.Clear();
    }

    // For the current items in the inventory, will look for a possible recipe.
    // public Recipe GetRecipeForCurrent()
    // {
    //     foreach (Recipe recipe in recipes)
    //     {
    //         // List<ItemStat> recipeInputs = recipe.StatInputs;

    //         // One way: sort the two lists first, then check sequence equivalence.
    //         // recipeInputs.Sort();
    //         // inputIngGroups.Sort();
    //         // if (recipeInputs.SequenceEqual(inputIngGroups))
            
    //         // Other way: use hash sets and set equivalence.
    //         // if (new HashSet<ItemStat>(recipeInputs).SetEquals(inputIngGroups))
    //         // {
    //         //     return recipe;
    //         // }
    //     }

    //     return null;
    // }
    
    // Gets a list of recipe outputs for the current items in the inventory.
    public Potion GetRecipeOutputForCurrent()
    {
        // Recipe recipe = GetRecipeForCurrent();

        // if (recipe != null)
        //     return recipe.Result;

        return null;
    }

    public Dictionary<ItemStat, int> CalculateItemStats()
    {
        Dictionary<ItemStat, int> newItemStats = new Dictionary<ItemStat, int>();
        Debug.Log("calculating stats... ");

        // For each specific ItemStat (attribute)
        // foreach (ItemStat stat in allItemStats)
        // {
        //     int statValue = 0;

        //     // looping through list of crafting items because could be an indefinite amount
        //     foreach (Item item in items)
        //     {
        //         if (item.StatDict.ContainsKey(stat))
        //         {
        //             statValue += item.StatDict[stat];
        //             // could somehow add modifiers & bonuses stuff here?
        //         }
        //     }
            
        //     newItemStats.Add(stat, statValue);
        // }

        return newItemStats;
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
