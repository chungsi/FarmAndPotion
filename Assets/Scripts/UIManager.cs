using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = default;
    [SerializeField] private GameObject inventoryUIObject;
    [SerializeField] private Inventory playerInventory;
    

    private void OnEnable()
    {
        _inputReader.openInventoryEvent += OnToggleInventory;
    }

    private void OnDisable()
    {
        _inputReader.openInventoryEvent -= OnToggleInventory;
    }


    private void OnToggleInventory()
    {
        if (!inventoryUIObject.gameObject.activeInHierarchy)
        {
            inventoryUIObject.gameObject.SetActive(true);
        } else {
            inventoryUIObject.gameObject.SetActive(false);
        }
    }
}