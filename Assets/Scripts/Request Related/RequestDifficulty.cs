using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct RequestStatRequirementPoints
{
    public RequestStatRequirement requirement;
    public int points;
}

[CreateAssetMenu(menuName = "Request/Difficulty Level")]
public class RequestDifficulty : ScriptableObject
{
    [SerializeField]
    List<RequestStatRequirementPoints> statRequirementPoints = new List<RequestStatRequirementPoints>();

    private Dictionary<RequestStatRequirement, int> statRequirementPointsDict = new Dictionary<RequestStatRequirement, int>();


    public Dictionary<RequestStatRequirement, int> StatRequirementPointsDict
    {
        get {
            if (statRequirementPointsDict.Count != 0)
            {
                return statRequirementPointsDict;
            }
            else
            {
                MakeStatRequirementPointsDict();
                return statRequirementPointsDict;
            }
        }
    }
    

    void OnEnable()
    {
        // TODO: for some reason, the OnEnable is only called after the first loop
        // through the requests items on initialization... weird?
        MakeStatRequirementPointsDict();
    }
    
    
    private void MakeStatRequirementPointsDict()
    {
        var str = new StringBuilder();
        str.Append($"Request difficulty dictionary initiating...");

        foreach (RequestStatRequirementPoints requirementPoints in statRequirementPoints)
        {
            if (statRequirementPointsDict.ContainsKey(requirementPoints.requirement))
            {
                statRequirementPointsDict[requirementPoints.requirement] = requirementPoints.points;
            }
            else
            {
                statRequirementPointsDict.Add(requirementPoints.requirement, requirementPoints.points);
            }
            str.Append($"\n {requirementPoints.requirement} with value {requirementPoints.points}");
        }
        Debug.Log(str);
    }
}
