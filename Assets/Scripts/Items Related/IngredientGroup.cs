using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Ingredient Group")]
public class IngredientGroup : ScriptableObject, IComparable, IComparer
{
    [SerializeField] string displayText;

    public string GetDisplayText()
    {
        return displayText;
    }

    #region Custom Comparisons

    int IComparable.CompareTo(object obj)
    {
        IngredientGroup eval = (IngredientGroup) obj;
        return displayText.CompareTo(eval.displayText);
    }

    int IComparer.Compare(object x, object y)
    {
        IngredientGroup ingr1 = (IngredientGroup) x;
        IngredientGroup ingr2 = (IngredientGroup) y;
        return String.Compare(ingr1.displayText, ingr2.displayText);
    }

    #endregion
}
