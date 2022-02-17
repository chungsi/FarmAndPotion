using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class Interactor : MonoBehaviour
{
    [Header("General Input")]
    [SerializeField] private InputReader _inputReader = default;
    [SerializeField] private GameObject interactionHintSprite;

    [Header("Player Inventory Related")]
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private FloatVariable itemIndexInMasterRuntimeSet;
    [SerializeField] private ItemObjectRuntimeSet masterItemsRuntimeSet;
    [SerializeField] private GameEvent triggerInventoryUIUpdate;

    private IInteractable currentInteractable = null;
    private Collider currentInteractableGO = null;

    // private DialogueUI dialogueUI;


    private void OnEnable()
    {
        _inputReader.interactEvent += OnInteraction;
        _inputReader.advanceDialogueEvent += OnAdvanceDialogue;
    }

    private void OnDisable()
    {
        _inputReader.interactEvent -= OnInteraction;
        _inputReader.advanceDialogueEvent -= OnAdvanceDialogue;
    }

    void Start()
    {
        // dialogueUI = FindObjectOfType<Yarn.Unity.DialogueUI>();
    }


    // Actually interact with whatever is interactable.
    private void OnInteraction()
    {
        if (currentInteractable == null) { return; }

        currentInteractable.Interact(transform.root.gameObject);
    }


    private void OnAdvanceDialogue()
    {
        // if (dialogueUI != null)
        // {
        //     dialogueUI.MarkLineComplete();
        // }
    }


    public void OnItemPickup()
    {
        if (!(currentInteractable is ItemRpgObject)) { return; }

        // Save interactable item index
        ItemObject itemObject = (ItemRpgObject)currentInteractable;
        itemIndexInMasterRuntimeSet.value = masterItemsRuntimeSet.GetIndex(itemObject);

        if (!playerInventory.isFull())
        {
            triggerInventoryUIUpdate.Raise();
            currentInteractableGO.gameObject.SetActive(false);
            ResetInteractableCollider();
        } else {
            // add something for cases when inventory is full. letting the player know.
            Debug.Log($"can't pick up item {itemObject.Item.name} because inventory is full!");
        }
    }


    private void ResetInteractableCollider()
    {
        // We left the trigger box of a valid interactable
        currentInteractable = null;
        currentInteractableGO = null;
        interactionHintSprite.SetActive(false);
    }


    #region Triggers

        private void OnTriggerEnter(Collider _other)
        {
            var interactable = _other.GetComponent<IInteractable>();

            if (interactable == null) { return; }

            // There is a valid interactable object within our range
            currentInteractable = interactable;
            currentInteractableGO = _other;
            interactionHintSprite.SetActive(true);
        }

        private void OnTriggerExit(Collider _other)
        {
            var interactable = _other.GetComponent<IInteractable>();

            if (interactable == null) { return; }
            if (interactable != currentInteractable) { return; }

            ResetInteractableCollider();
        }

    #endregion
}
