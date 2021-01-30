using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CraftingSuccessPanel : MonoBehaviour, IPointerDownHandler
{
    // public GameObject itemsDisplayContainer;
    // public 
    public Image icon;
    public TextMeshProUGUI resultName;
    public Image uiContainer;
    [Space]
    public FloatVariable floatingItemIndex;
    public FloatVariable numCraftingResults;
    public ItemObjectRuntimeSet inventorySet;

    [SerializeField] List<ItemObject> craftedResults = new List<ItemObject>();

    void Update()
    {

    }

    void SetDisplay(Item item)
    {
        icon.sprite = item.artwork;
        resultName.text = item.name;
    }

    void ShowUI()
    {
        uiContainer.gameObject.SetActive(true);
    }

    void HideUI()
    {
        uiContainer.gameObject.SetActive(false);
    }

    private void FillCraftedResults()
    {
        int numInventoryItems = inventorySet.items.Count;
        int difference = numInventoryItems - (int)numCraftingResults.value;
        for (int i = numInventoryItems-1; i >= difference; i--)
        {
            craftedResults.Add(inventorySet.GetItem(i));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        HideUI();
    }

    #region Event Responses

    public void ActivateSuccessPanel()
    {
        ShowUI();
        ItemObject item = inventorySet.GetItem((int)floatingItemIndex.value);
        SetDisplay(item.GetItem());
    }

    #endregion
}
