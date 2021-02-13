using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    [SerializeField]
    private Potion result;
    
    [SerializeField]
    private List<ItemStat> statInputs = new List<ItemStat>();
    
    public Potion Result => result;
    public List<ItemStat> StatInputs => statInputs;
}
