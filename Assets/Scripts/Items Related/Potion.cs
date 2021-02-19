using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Recipe
{
    [SerializeField]
    private List<ItemStat> statInputs = new List<ItemStat>();

    private Potion potion; // Auto-populated by the Potion SO.


    public List<ItemStat> StatInputs => statInputs;

    public Potion Result
    {
        get => potion;
        set => potion = value;
    }
}


[CreateAssetMenu(menuName = "Item/Potion")]
public class Potion : Item
{
    [Header("Other Specs")]

    [Tooltip("If this is a variation of a parent potion, use this slot. Otherwise, leave it blank.")]
    [SerializeField]
    private Potion variationOfPotion;

    // [Tooltip("The stronger of the key attributes for sorting and categorization purposes.")]
    // [SerializeField]
    // private ItemStat statCategory;

    [Header("Recipe Definitions")]

    [SerializeField]
    private List<Recipe> recipes = new List<Recipe>();


    // Override base Item's read-only accessors for stats, as Potions will have
    // their main & secondary stats be set at runtime based on crafting.
    new public List<ItemStat> MainStats
    {
        get => base.MainStats;
        set => mainStats = value;
    }

    new public List<ItemStat> SecondaryStats
    {
        get => base.SecondaryStats;
        set => secondaryStats = value;
    }

    public Potion ParentPotion => variationOfPotion != null ? variationOfPotion : null; 

    public List<Recipe> Recipes => recipes;


    void OnValidate()
    {
        AutoPopulateRecipePotionWithSelf();
    }

    private void AutoPopulateRecipePotionWithSelf()
    {
        for (int i = 0; i < recipes.Count; i++)
        {
            recipes[i].Result = this;
        }
    }
}
