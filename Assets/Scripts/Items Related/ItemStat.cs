using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Item Stat")]
public class ItemStat : ScriptableObject
{
    [SerializeField] ItemStatGroup statGroup;
}