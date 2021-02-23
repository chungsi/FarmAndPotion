using System;
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


    public Dictionary<RequestStatRequirement, int> StatRequirementPointsDict => statRequirementPointsDict;
    

    void OnEnable()
    {
        MakeStatRequirementPointsDict();
    }
    
    
    private void MakeStatRequirementPointsDict()
    {
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
        }
    }
}
