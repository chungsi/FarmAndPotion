using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    [SerializeField] List<Ailment> ailmentRequirements = new List<Ailment>();
    [SerializeField] List<Item> results = new List<Item>();
    
    public List<Ailment> GetAilments()
    {
        return ailmentRequirements;
    }

    public List<Item> GetResults()
    {
        return results;
    }
}
