using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class StatHelper
{
    /* 
        Methods to help when getting a list of Stat objects and populating
        the lists and dictionaries when working with stats.
     */
    

    // Gets a list of all possible stat attributes from the asset database
    public List<Stat> GetMasterStatsList()
    {
        List<Stat> masterStatsList = new List<Stat>();

        string[] assetNames = AssetDatabase.FindAssets("t:Stat", new[] { "Assets/Stats" });
        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var stat = AssetDatabase.LoadAssetAtPath<Stat>(SOpath);
            masterStatsList.Add(stat);
        }

        return masterStatsList;
    }

    // 
    public void PopulateEmptyStatValueList(List<StatValue> statValueList, List<Stat> masterStatsList)
    {
        // loop through master stat list to get stat
        // check if stat already exists
        // add new statvalue if it doesn't

        foreach (Stat statReference in masterStatsList)
        {
            if (!statValueList.Exists(s => s.stat == statReference))
            {
                statValueList.Add(new StatValue() {
                    stat = statReference,
                    value = 0
                });

            }
        }
    }

    public void PopulateStatsDictionary(Dictionary<Stat, int> statsDictionary, List<StatValue> statValueList)
    {
        if (statValueList != null) // double check null
        {
            // loop through user defined stats
            foreach(StatValue statValue in statValueList) 
            {
                if (statsDictionary.ContainsKey(statValue.stat))
                    statsDictionary[statValue.stat] = statValue.value;
                else
                    statsDictionary.Add(statValue.stat, statValue.value);
            }
        }
    }
}
