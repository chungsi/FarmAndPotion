using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct RequestEvaluationMargin
{
    public RequestEvaluation evaluation;
    public int margin;
}

[CreateAssetMenu(menuName = "Request/Difficulty Level")]
public class RequestDifficulty : ScriptableObject
{
    [SerializeField] List<RequestEvaluationMargin> evalMargins = new List<RequestEvaluationMargin>();
    private Dictionary<RequestEvaluation, int> evalMarginDict = new Dictionary<RequestEvaluation, int>();

    void OnEnable()
    {
        // populate dictionary with inspector values
        evalMarginDict.Clear();
        foreach (RequestEvaluationMargin refMargin in evalMargins)
        {
            evalMarginDict.Add(refMargin.evaluation, refMargin.margin);
        }
    }

    public Dictionary<RequestEvaluation, int> GetEvaluationMargins()
    {
        return evalMarginDict;
    }
}
