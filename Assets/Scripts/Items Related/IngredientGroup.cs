using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Item/Ingredient Group")]
public class IngredientGroup : ScriptableObject, IComparable, IComparer
{
    [FormerlySerializedAs("displayText")]
    [SerializeField] string _name;

    #region Custom Comparisons Interface Implementation

    int IComparable.CompareTo(object obj)
    {
        IngredientGroup eval = (IngredientGroup) obj;
        return Name.CompareTo(eval.Name);
    }

    int IComparer.Compare(object x, object y)
    {
        IngredientGroup ingr1 = (IngredientGroup) x;
        IngredientGroup ingr2 = (IngredientGroup) y;
        return String.Compare(ingr1.Name, ingr2.Name);
    }

    #endregion

    public string Name => _name;
}
