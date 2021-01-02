using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu]
public class CraftingInventory : Inventory
{
    [SerializeField]
    private List<string> craftingInputNames = new List<string>();
    private List<Recipe> recipes = new List<Recipe>();

    public override void OnValidate()
    {
        base.OnValidate();

        recipes.Clear();
        craftingInputNames.Clear();

        PopulateCraftingList();
    }

    public override void OnEnable()
    {
        // Debug.Log("crafting inventory initialize");
    }

    // is there a better way to optimize this? see if a key exists first?
    private void PopulateCraftingList() 
    {
        // Debug.Log("Populating master list of recipes...");

        string[] assetNames = AssetDatabase.FindAssets("t:Recipe", new[] { "Assets/Recipes" });
        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var recipe = AssetDatabase.LoadAssetAtPath<Recipe>(SOpath);

            recipes.Add(recipe);
        }
    }

    public override bool AddItem(Item item)
    {
        if (items.Count >= space)
            return false;
        
        items.Add(item);
        craftingInputNames.Add(item.name);
        // Debug.Log("adding item to crafting list: " + item.name);

        return true;
    }

    public override bool RemoveItem(Item item)
    {
        if (!items.Contains(item))
            return false;

        craftingInputNames.Remove(item.name);
        items.Remove(item);
        return true;
    }

    public override void ClearInventory()
    {
        base.ClearInventory();
        craftingInputNames.Clear();
    }

    // public bool ContainsRecipeForItems(List<Item> inputs)
    // {
    //     Debug.Log("checking if recipe and inputs match");
    //     Recipe possibleRecipe = GetRecipeForItems(inputs);

    //     return possibleRecipe != null;
    // }

    public Recipe GetRecipeForCurrent()
    {
        string str = "checking if recipe exists for : ";
        foreach (string name in craftingInputNames)
        {
            str += name + " ";
        }
        Debug.Log(str);

        foreach (Recipe recipe in recipes)
        {
            List<string> recipeIngredients = recipe.GetInputNames();

            Debug.Log("list counts; recipe ingredients: " + recipeIngredients.Count + "; crafting: " + craftingInputNames.Count);

            if (Enumerable.SequenceEqual(recipeIngredients.OrderBy(s => s).ToList(), 
                craftingInputNames.OrderBy(t => t).ToList()))
                return recipe;
            
            // if (recipeIngredients.Count == craftingInputNames.Count &&
            //     recipeIngredients.All(craftingInputNames.Contains))
            // {
            //     return recipe;
            // }
        }

        return null;
    }

    public List<Item> GetRecipeOutputsForCurrent()
    {
        Recipe recipe = GetRecipeForCurrent();

        if (recipe != null)
            return recipe.results;

        return null;
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
