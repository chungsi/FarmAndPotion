using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Ingredient")]
public class Ingredient : Item
{
    [SerializeField] IngredientGroup ingredientGroup;

    public IngredientGroup GetIngredientGroup()
    {
        return ingredientGroup;
    }
}
