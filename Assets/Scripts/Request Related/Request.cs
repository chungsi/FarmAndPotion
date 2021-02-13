using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Request/Request")]
public class Request : ScriptableObject
{
    [SerializeField]
    private string requester;

    [TextArea(10, 15)]
    [SerializeField]
    private string description;

    [Space]

    [SerializeField]
    private RequestDifficulty difficulty;

    [SerializeField]
    private RequestEvaluation lowestEval;

    [Space]
    [SerializeField]
    private Potion potion;

    [SerializeField]
    private List<ItemStatValue> statReqs = new List<ItemStatValue>();

    [Space]

    [SerializeField]
    private bool isCompleted = false;

    [SerializeField]
    private RequestEvaluation completedEval;

    private ItemHelper statHelper = new ItemHelper();
    
    private List<ItemStat> masterItemStatsList = new List<ItemStat>();
    
    // Only contains attributes required (no zero-value stats).
    private Dictionary<ItemStat, int> statReqsDict = new Dictionary<ItemStat, int>();

    // Corresponding Evaluation to each individual ItemStat.
    private Dictionary<ItemStat, RequestEvaluation> statEvalsDict = new Dictionary<ItemStat, RequestEvaluation>();


    public string Requester => requester;
    public string Description => description;
    public RequestDifficulty Difficulty => difficulty;
    public RequestEvaluation LowestPossibleEval => lowestEval;
    
    public RequestEvaluation CompletionEvaluation
    {
        get => completedEval;
        set => completedEval = value;
    }

    public bool Completed 
    { 
        get => isCompleted; 
        set => isCompleted = value;
    }

    public Dictionary<ItemStat, int> StatRequirements => statReqsDict;


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

    // Clears data related to calculating the evaluation and whatnot,
    // to reset the status for another chance.
    public void ResetRequestToDefault()
    {
        isCompleted = false;
        statEvalsDict.Clear();
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
        RequestEvaluation lowestEval = evals[evals.Count()-1];

        Debug.Log("Lowest eval: " + lowestEval.name);
        return lowestEval;
    }

    private void FillMasterItemStatsList()
    {
        masterItemStatsList.Clear(); // clear existing first
        masterItemStatsList = statHelper.GetMasterItemStatsList();
    }

}
