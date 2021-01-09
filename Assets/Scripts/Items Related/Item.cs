using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Serialization;

[Serializable]
public struct StatValue 
{
    public Stat stat;
    public int value;
}

// blueprint class for all items
[CreateAssetMenu(fileName = "New Item", menuName = "Item/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite artwork = null;
    [SerializeField] ItemType itemType;
    [TextArea]
    [SerializeField] string description = "";
    [SerializeField] List<Ailment> solvesAilments = new List<Ailment>();

    [Space]
    [SerializeField] List<StatValue> stats = new List<StatValue>(); // to be inspector populated
    
    // helper functions to get stats stuff
    private StatHelper statHelper = new StatHelper();
    // populated by code, used for internal traversals
    private Dictionary<Stat, int> statsDictionary = new Dictionary<Stat, int>();
    private List<Stat> masterStatsList = new List<Stat>();

    void OnValidate()
    {
        PopulateMasterStatsList();
        statHelper.PopulateEmptyStatValueList(stats, masterStatsList);
    }

    void OnEnable()
    {
        statHelper.PopulateStatsDictionary(statsDictionary, stats);
    }

    public virtual Item GetCopy()
    {
        return this;
    }

    public List<Ailment> GetAilments()
    {
        return solvesAilments;
    }

    public Dictionary<Stat, int> GetStatsDictionary()
    {
        Dictionary<Stat, int> newlist = statsDictionary;
        return newlist;
    }

    public string GetDescription()
    {
        return description;
    }

    public ItemType GetItemType()
    {
        return itemType;
    }

    public void PopulateStats(Dictionary<Stat, int> newStats)
    {
        statsDictionary = newStats;

        stats.Clear();
        foreach (KeyValuePair<Stat, int> stat in newStats)
        {
            stats.Add(new StatValue() { stat = stat.Key, value = stat.Value });
        }
    }

    private void PopulateMasterStatsList()
    {
        masterStatsList.Clear(); // clear existing first
        masterStatsList = statHelper.GetMasterStatsList();
    }

}
