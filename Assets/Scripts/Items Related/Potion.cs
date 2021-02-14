using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe
{
    [SerializeField]
    private string prefix;

    [SerializeField]
    private List<ItemStat> statInputs;

    private Potion potion; // Auto-populated by the Potion SO.


    public string Prefix => prefix;

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
    [Header("Recipe Definitions")]

    [SerializeField]
    private List<Recipe> recipes = new List<Recipe>();

    public List<Recipe> Recipes => recipes;


    void OnValidate()
    {
        AutoPopulateRecipePotionWithSelf();
    }


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

    private void AutoPopulateRecipePotionWithSelf()
    {
        for (int i = 0; i < recipes.Count; i++)
        {
            if (recipes[i].Result != null)
            {
                recipes[i].Result = this;
            }
        }
    }
}
