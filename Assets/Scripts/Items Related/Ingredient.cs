using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Item/Ingredient")]
public class Ingredient : Item
{
    [Header("Ingredient-Specific Fields")]

    [SerializeField]
    private IngredientGroup ingredientGroup;

    public IngredientGroup Group => ingredientGroup;
}
