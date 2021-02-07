using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Request/Request")]
public class Request : ScriptableObject
{
    [SerializeField] string requestBy;
    [TextArea(10, 15)] public string description;
    [Space]
    [SerializeField] RequestDifficulty difficulty;
    [SerializeField] RequestEvaluation lowestEval;
    [Space]
    [SerializeField] Potion potion;
    [SerializeField] List<ItemStatValue> statReqs = new List<ItemStatValue>();
    [Space]
    [SerializeField] bool isCompleted = false;
    [SerializeField] RequestEvaluation completedEval;
    
    private ItemHelper statHelper = new ItemHelper();
    private List<ItemStat> masterItemStatsList = new List<ItemStat>();

    // Only contains attributes required (no zero-value stats)
    private Dictionary<ItemStat, int> statReqsDict = new Dictionary<ItemStat, int>();

    // Corresponding Evaluation to each individual ItemStat
    private Dictionary<ItemStat, RequestEvaluation> statEvalsDict = new Dictionary<ItemStat, RequestEvaluation>();

    void OnValidate()
    {
        FillMasterItemStatsList();
        statHelper.FillEmptyItemStatValueList(statReqs, masterItemStatsList);
    }

    void OnEnable()
    {
        // Get a dictionary with only the non-zero attribute requirements
        statReqsDict = statHelper.GetNonZeroItemStatsDictionary(statReqs);
        // Clear dictionary data and such
        statEvalsDict.Clear();
    }

    public string GetRequestBy()
    {
        return requestBy;
    }

    public RequestDifficulty GetDifficulty()
    {
        return difficulty;
    }

    public void SetIsCompleted(bool b)
    {
        isCompleted = b;
    }

    public bool IsCompleted()
    {
        return isCompleted;
    }

    public RequestEvaluation GetLowestEval()
    {
        return lowestEval;
    }

    public RequestEvaluation GetCompletedEval()
    {
        return completedEval;
    }

    public void SetCompletedEval(RequestEvaluation eval)
    {
        completedEval = eval;
    }

    public Dictionary<ItemStat, int> GetItemStatRequirements()
    {
        return statReqsDict;
    }

    public Dictionary<ItemStat, RequestEvaluation> GetItemStatEvals(Dictionary<ItemStat, int> solutionStats)
    {
        Dictionary<RequestEvaluation, int> evalMargins = difficulty.GetEvaluationMargins();

        // For each required stat of the request:
        foreach (KeyValuePair<ItemStat, int> reqStat in statReqsDict)
        {
            // If value for a stat on the item given is 0 => auto-fail (or lowest) evaluation;
            if (solutionStats[reqStat.Key] == 0)
            {
                statEvalsDict.Add(reqStat.Key, lowestEval);
                Debug.Log("The stat " + reqStat.Key.name + " is zero on the given item.");
                continue;
            }

            // Stat value of the solution
            int solStatValue = solutionStats[reqStat.Key];
            // TODO: if using words to describe what's wrong, might get rid of abs
            //          to show "too much" or "too little"
            int statDifference = Mathf.Abs(reqStat.Value - solStatValue);

            // Check the evaluation margins for this stat;
            // Adding only the first condition to be met (the best score)
            foreach (KeyValuePair<RequestEvaluation, int> evalMargin in evalMargins)
            {
                if (statDifference <= evalMargin.Value)
                {
                    statEvalsDict.Add(reqStat.Key, evalMargin.Key);
                    Debug.Log("The " + reqStat.Key.name + " stat is being evaluated... " + statEvalsDict[reqStat.Key]);
                    break;
                }
            }
        }

        return statEvalsDict;
    }

    public RequestEvaluation GetLowestEval(Dictionary<ItemStat, int> solutionStats)
    {
        List<RequestEvaluation> evals = GetItemStatEvals(solutionStats).Values.ToList();
        evals.Sort(); // sorts by descending order
        RequestEvaluation lowestEval = evals[evals.Count-1];

        Debug.Log("Lowest eval: " + lowestEval.name);
        return lowestEval;
    }

    private void FillMasterItemStatsList()
    {
        masterItemStatsList.Clear(); // clear existing first
        masterItemStatsList = statHelper.GetMasterItemStatsList();
    }

}
