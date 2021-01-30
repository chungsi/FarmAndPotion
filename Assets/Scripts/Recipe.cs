using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    [FormerlySerializedAs("inputs")]
    [SerializeField] List<IngredientGroup> ingredientGroupInputs = new List<IngredientGroup>();
    // TODO: Should this be typecast to a Potion? to be more specific???
    [SerializeField] List<Item> results = new List<Item>();
    
    public List<IngredientGroup> GetInputs()
    {
        return ingredientGroupInputs;
    }

    public List<Item> GetResults()
    {
        return results;
    }
}
