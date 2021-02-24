using System;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public struct RequestStatRequirementDefinitions
{
    public RequestStatRequirement requirement;
    public List<ItemStat> stats;
}

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

    [Space]

    [SerializeField]
    private List<RequestStatRequirementDefinitions> statRequirementDefinitions = new List<RequestStatRequirementDefinitions>();
    
    [Space]
    
    [SerializeField]
    private List<RequestStatRequirementDefinitions> posStatRequirementDefinitions = new List<RequestStatRequirementDefinitions>();
    
    [Space]

    [SerializeField]
    private List<RequestStatRequirementDefinitions> negStatRequirementDefinitions = new List<RequestStatRequirementDefinitions>();

    [Space]

    [SerializeField]
    private int perfectScore = 0;

    [SerializeField]
    private int finalScore = 0;

    [SerializeField]
    private bool isCompleted = false;

    [SerializeField]
    private RequestEvaluation completedEval;

    private ItemHelper statHelper = new ItemHelper();
    private List<ItemStat> masterItemStatsList = new List<ItemStat>();
    
    // Dictionary object for internal traversal and use.
    private Dictionary<RequestStatRequirement, List<ItemStat>> posStatRequirementsDefinitionsDict = new Dictionary<RequestStatRequirement, List<ItemStat>>();
    private Dictionary<RequestStatRequirement, List<ItemStat>> negStatRequirementsDefinitionsDict = new Dictionary<RequestStatRequirement, List<ItemStat>>();
    
    private Dictionary<RequestStatRequirement, List<ItemStat>> statRequirementsDefinitionsDict = new Dictionary<RequestStatRequirement, List<ItemStat>>();


    public int FinalScore => finalScore;
    public int PerfectScore => perfectScore;
    public string Requester => requester;
    public string Description => description;
    public RequestDifficulty Difficulty => difficulty;
    
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


    void OnValidate()
    {
        masterItemStatsList.Clear(); // clear existing first
        masterItemStatsList = statHelper.GetMasterItemStatsList();
    }

    void OnEnable()
    {
        MakeStatRequirementsDictionary();
        CalculatePerfectScore();
    }

    // Clears data related to calculating the evaluation and whatnot,
    // to reset the status for another chance.
    public void ResetRequestToDefault()
    {
        isCompleted = false;
    }

    public void CalculateSolutionPoints(Potion _potion)
    {
        int solutionScore = 0;
        Dictionary<RequestStatRequirement, int> statReqPointsDef = difficulty.StatRequirementPointsDict;

        // Each stat requirement definition, get the score from the difficulty setting.
        foreach (KeyValuePair<RequestStatRequirement, List<ItemStat>> thisStatReqDef in statRequirementsDefinitionsDict)
        {
            int thisStatsPoints = 0;
            if (statReqPointsDef.ContainsKey(thisStatReqDef.Key))
            {
                thisStatsPoints = statReqPointsDef[thisStatReqDef.Key];
            }

            // Now loop through each item stat within this stat requirement.
            List<ItemStat> theseDefStats = thisStatReqDef.Value;
            List<ItemStat> allPotionStats = _potion.AllStats;

            Debug.Log($"Checking for stats within the {thisStatReqDef.Key.name} category... list size: {theseDefStats.Count}");
            var debugStr = new StringBuilder();

            foreach (ItemStat stat in theseDefStats)
            {
                debugStr.Append($"Potion allStats size: {allPotionStats.Count}; Does potion contain {stat}? \n");
                if (allPotionStats.Contains(stat))
                {
                    debugStr.Append($"Yes \n");
                    solutionScore += thisStatsPoints;
                }
            }
            Debug.Log(debugStr);
        }

        finalScore = solutionScore;
    }

    private void MakeStatRequirementsDictionary()
    {
        // For all.
        List<RequestStatRequirementDefinitions> allStatReqsList = new List<RequestStatRequirementDefinitions>();
        allStatReqsList.AddRange(posStatRequirementDefinitions);
        allStatReqsList.AddRange(negStatRequirementDefinitions);

        foreach (RequestStatRequirementDefinitions statReq in allStatReqsList)
        {
            if (statRequirementsDefinitionsDict.ContainsKey(statReq.requirement))
            {
                statRequirementsDefinitionsDict[statReq.requirement] = statReq.stats;
            } 
            else
            {
                statRequirementsDefinitionsDict.Add(statReq.requirement, statReq.stats);
            }
        }

        // For positive stat requirements.
        foreach (RequestStatRequirementDefinitions statReq in posStatRequirementDefinitions)
        {
            if (posStatRequirementsDefinitionsDict.ContainsKey(statReq.requirement))
            {
                posStatRequirementsDefinitionsDict[statReq.requirement] = statReq.stats;
            } 
            else
            {
                posStatRequirementsDefinitionsDict.Add(statReq.requirement, statReq.stats);
            }
        }

        // For negative stat requirements.
        foreach (RequestStatRequirementDefinitions statReq in negStatRequirementDefinitions)
        {
            if (negStatRequirementsDefinitionsDict.ContainsKey(statReq.requirement))
            {
                negStatRequirementsDefinitionsDict[statReq.requirement] = statReq.stats;
            } 
            else
            {
                negStatRequirementsDefinitionsDict.Add(statReq.requirement, statReq.stats);
            }
        }
    }

    private void CalculatePerfectScore()
    {
        int calculatingPerfectScore = 0;

        // Loop through positive list of stat requirements and get the point
        // value from the request difficulty;
        // Then multiply the point value with how many attributes are in that
        // requirement category.
        foreach (KeyValuePair<RequestStatRequirement, List<ItemStat>> currentStatReqDef in posStatRequirementsDefinitionsDict)
        {
            RequestStatRequirement currentStatReq = currentStatReqDef.Key;
            List<ItemStat> currentReqStatsList = currentStatReqDef.Value;
            Dictionary<RequestStatRequirement, int> difficultyStatReqPointsDict = difficulty.StatRequirementPointsDict;

            Debug.Log($"{requester} - {currentStatReq.name}'s list size: {currentReqStatsList.Count}; difficulty {difficulty.name} with size {difficultyStatReqPointsDict.Count}");

            if (difficultyStatReqPointsDict.ContainsKey(currentStatReq))
            {
                int thisStatPoints = difficultyStatReqPointsDict[currentStatReq];
                calculatingPerfectScore += thisStatPoints * currentReqStatsList.Count();
            }
        }

        perfectScore = calculatingPerfectScore;
    }
}
