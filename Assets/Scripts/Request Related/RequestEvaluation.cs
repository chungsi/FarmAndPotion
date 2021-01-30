using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Request/Evaluation")]
public class RequestEvaluation : ScriptableObject, IComparable, IComparer
{
    [SerializeField] string displayText;
    [SerializeField] int order;

    public string GetDisplayText()
    {
        return displayText;
    }

    #region Custom Comparisons

    // Compare RequestEvaluation objects by their "order" value
    int IComparable.CompareTo(object obj)
    {
        RequestEvaluation eval = (RequestEvaluation) obj;
        return order.CompareTo(eval.order);
    }

    int IComparer.Compare(object x, object y)
    {
        RequestEvaluation eval1 = (RequestEvaluation) x;
        RequestEvaluation eval2 = (RequestEvaluation) y;
        if (eval1.order > eval2.order)
            return 1;
        if (eval1.order < eval2.order)
            return -1;
        else
            return 0;
    }

    #endregion
}