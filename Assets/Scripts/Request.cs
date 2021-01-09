using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Request : ScriptableObject
{
    [TextArea] public string description;
    [SerializeField] List<Ailment> ailmentRequirements = new List<Ailment>();
    [SerializeField] List<StatValue> minStatRequirements = new List<StatValue>();
    [SerializeField] bool isCompleted = false;
    
    private int satisfactionPoints;

    private StatHelper statHelper = new StatHelper();
    private List<Stat> masterStatsList = new List<Stat>();
    private Dictionary<Stat, int> statReqsDictionary = new Dictionary<Stat, int>();

    void OnValidate()
    {
        PopulateMasterStatsList();
        statHelper.PopulateEmptyStatValueList(minStatRequirements, masterStatsList);
    }

    void OnEnable()
    {
        statHelper.PopulateStatsDictionary(statReqsDictionary, minStatRequirements);
        GenerateSatisfactionPoints(); // call after getting changes from dictionary
    }

    public void SetIsCompleted(bool b)
    {
        isCompleted = b;
    }

    public bool IsCompleted()
    {
        return isCompleted;
    }

    public List<Ailment> GetAilmentRequirements()
    {
        return ailmentRequirements;
    }

    public Dictionary<Stat, int> GetStatRequirements()
    {
        return statReqsDictionary;
    }

    public int GetSatisfactionPoints()
    {
        return satisfactionPoints;
    }

    private void GenerateSatisfactionPoints()
    {
        satisfactionPoints = 0; // reset to account for changes

        // add points from stat reqs
        foreach (int statValue in statReqsDictionary.Values)
        {
            satisfactionPoints += statValue;
        }
        // add points for each ailment requirement
        satisfactionPoints += ailmentRequirements.Count;
    }

    private void PopulateMasterStatsList()
    {
        masterStatsList.Clear(); // clear existing first
        masterStatsList = statHelper.GetMasterStatsList();
    }

}
