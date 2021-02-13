using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Serialization;

[Serializable]
public struct ItemStatValue
{
    public ItemStat stat;
    public int value;
}

public class Item : ScriptableObject
{
    // Unity Inspector visible fields.
    [SerializeField]
    new private string name = "Item";

    [SerializeField]
    private Sprite artwork = null;

    [TextArea(7, 15)]
    [SerializeField]
    private string description = "";

    [Header("Stat Definitions")]

    [SerializeField]
    protected List<ItemStat> mainStats;

    [SerializeField]
    protected List<ItemStat> secondaryStats;

    // Properties for accessing.
    public string Name => name;
    public string Description => description;
    public Sprite Artwork => artwork;
    public List<ItemStat> MainStats => mainStats;
    public List<ItemStat> SecondaryStats => secondaryStats;


    void OnValidate()
    {
    }

    void OnEnable()
    {
    }

    public virtual Item GetCopy()
    {
        return this;
    }
}
