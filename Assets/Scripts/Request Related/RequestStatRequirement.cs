using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Request/Stat Condition")]
public class RequestStatRequirement : ScriptableObject, IComparable, IComparer
{
    [SerializeField]
    private int order;


    public int Order => order;


    #region Custom Comparisons

    // Compare Request Stat Requirement objects by their "order" value
    int IComparable.CompareTo(object obj)
    {
        RequestStatRequirement requirement = (RequestStatRequirement) obj;
        return order.CompareTo(requirement.order);
    }

    int IComparer.Compare(object x, object y)
    {
        RequestStatRequirement requirement1 = (RequestStatRequirement) x;
        RequestStatRequirement requirement2 = (RequestStatRequirement) y;
        if (requirement1.order > requirement2.order)
            return 1;
        if (requirement1.order < requirement2.order)
            return -1;
        else
            return 0;
    }

    #endregion
}
