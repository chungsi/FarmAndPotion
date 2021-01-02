using System.Collections.Generic;
using UnityEngine;

public abstract class RuntimeSet<T> : ScriptableObject
{
    public List<T> items = new List<T>();

    public void Add(T t)
    {
        if (!items.Contains(t)) items.Add(t);
    }

    public void Remove(T t)
    {
        if (items.Contains(t)) items.Remove(t);
    }

    public int GetIndex(T t)
    {
        if (items.Contains(t)) 
            return items.FindIndex(item => item.Equals(t));
        else return -1;
    }

    public T GetItem(int index)
    {
        if (items.Count > index)
            return items[index];
        return default(T);
    }
}
