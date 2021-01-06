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
[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject {

    new public string name = "New Item";
    public Sprite artwork = null;
    [TextArea]
    [SerializeField] string description;
    [SerializeField] List<Ailment> solvesAilments = new List<Ailment>();

    [Space]
    [SerializeField] List<StatValue> stats = new List<StatValue>(); // to be inspector populated
    // populated by code, used for internal traversals
    private Dictionary<Stat, int> statsDictionary = new Dictionary<Stat, int>();
    [SerializeField]
    private List<Stat> masterStatsList = new List<Stat>();

    void OnValidate()
    {
        PopulateMasterStatsList();
    }

    void OnEnable()
    {
        PopulateStatsDictionary();
        PopulateStatList();
    }

    private void PopulateMasterStatsList()
    {
        masterStatsList.Clear(); // clear existing first

        string[] assetNames = AssetDatabase.FindAssets("t:Stat", new[] { "Assets/Stats" });
        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var stat = AssetDatabase.LoadAssetAtPath<Stat>(SOpath);
            masterStatsList.Add(stat);
        }
    }

    private void PopulateStatList()
    {
        foreach (Stat statReference in masterStatsList)
        {
            if (!statsDictionary.ContainsKey(statReference))
            {
                stats.Add(new StatValue() {
                    stat = statReference,
                    value = 0
                });
            }
        }
    }

    private void PopulateStatsDictionary()
    {
        statsDictionary.Clear();
        if (stats != null)
        {
            foreach (StatValue stat in stats)
            {
                statsDictionary.Add(stat.stat, stat.value);
                // Debug.Log("testing statsDictionary insert: " + statsDictionary[stat.stat].ToString());
            }
            // Debug.Log("populating stats dictionary... size is now " + statsDictionary.Count);
        }
    }

    public virtual Item GetCopy()
    {
        return this;
    }

    public List<Ailment> GetAilments()
    {
        return solvesAilments;
    }

    public Dictionary<Stat, int> GetStats()
    {
        Dictionary<Stat, int> newlist = statsDictionary;
        return newlist;
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

}
