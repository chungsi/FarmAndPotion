using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Potion")]
public class Potion : Item
{
    [Space]
    [SerializeField] List<IngredientGroup> ingredientGroupInputs = new List<IngredientGroup>();
    
    public List<IngredientGroup> GetInputs()
    {
        return ingredientGroupInputs;
    }
}
