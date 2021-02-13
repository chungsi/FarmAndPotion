using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PotionPageObject : PageObject
{
    [SerializeField] TextMeshProUGUI recipeText;

    private Potion potion;

    public override void SetItem(Item newItem)
    {
        // base.SetItem(newItem);
        item = newItem;
        itemTypeText.text = "Potion";

        if (newItem is Potion)
            potion = (Potion)newItem;

        UpdatePage();
    }

    public override void UpdatePage()
    {
        base.UpdatePage();

        string recipeString = "";
        // if (potion != null)
        // {
        //     foreach (IngredientGroup ingrGroup in potion.StatInputs)
        //     {
        //         recipeString += ingrGroup.Name + "\n";
        //     }
        // }
        recipeText.text = recipeString;
    }
}
