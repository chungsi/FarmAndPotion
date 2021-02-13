using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Potion")]
public class Potion : Item
{
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
}
