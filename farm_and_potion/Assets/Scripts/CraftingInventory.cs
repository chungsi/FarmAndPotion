using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu]
public class CraftingInventory : Inventory
{
    private List<Recipe> recipes = new List<Recipe>();

    public override void OnValidate()
    {
        base.OnValidate();

        // is there a better way to optimize this? see if a key exists first?
        recipes.Clear();
        PopulateCraftingList();
    }

    public override void OnEnable()
    {
        Debug.Log("crafting inventory initialize");
    }

    private void PopulateCraftingList() 
    {
        Debug.Log("Populating master list of recipes...");

        string[] assetNames = AssetDatabase.FindAssets("t:Recipe", new[] { "Assets/Recipes" });
        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var recipe = AssetDatabase.LoadAssetAtPath<Recipe>(SOpath);

            recipes.Add(recipe);
        }
    }

    public bool ContainsRecipeForItems(List<Item> inputs)
    {
        Debug.Log("checking if recipe and inputs match");
        Recipe possibleRecipe = GetRecipeForItems(inputs);

        return possibleRecipe != null;
    }

    public Recipe GetRecipeForItems(List<Item> inputs)
    {
        foreach (Recipe recipe in recipes)
        {
            List<Item> recipeIngredients = recipe.ingredients;
            List<Item> craftingInputs = inputs;

            if (recipeIngredients.Count == craftingInputs.Count &&
                recipeIngredients.All(inputs.Contains))
                return recipe;
        }

        return null;

        // IEnumerable<Recipe> recipeQuery = 
        //     from recipe in recipes
        //     where recipe.ingredients.Equals(inputs)
        //     select recipe;
        
        // List<Recipe> list = recipeQuery.ToList<Recipe>();

        // Debug.Log("printing.... " + recipeQuery.Count() + "; list count... " + list.Count);

        // return recipeQuery.FirstOrDefault();
    }

}
