using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    [SerializeField] Potion result;
    [Space]
    [SerializeField] List<IngredientGroup> ingredientGroupInputs = new List<IngredientGroup>();
    
    public List<IngredientGroup> GetInputs()
    {
        return ingredientGroupInputs;
    }

    public Potion GetResult()
    {
        return result;
    }
}
