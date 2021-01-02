using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    // inputs
    // to consider: will more complex recipes just take more inputs at once?
    public List<Item> ingredients;
    // public List<Item> ingredients = new List<Item>();
    // public Item input1;
    // public Item input2;

    // output(s)
    public List<Item> results;

    [SerializeField]
    List<string> ingredientNames = new List<string>();
    
    void OnEnable()
    {
        ingredientNames.Clear();
        
        foreach (Item item in ingredients)
        {
            ingredientNames.Add(item.name);
        }
    }

    public List<string> GetInputNames()
    {
        return ingredientNames;
    }
}
