using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class Interactor : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = default;
    [SerializeField] private GameObject interactionHintSprite;
    private IInteractable currentInteractable = null;

    private DialogueUI dialogueUI;

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
        dialogueUI = FindObjectOfType<Yarn.Unity.DialogueUI>();
    }

    private void OnInteraction()
    {
        if (currentInteractable == null) { return; }

        currentInteractable.Interact(transform.root.gameObject);
    }

    private void OnAdvanceDialogue()
    {
        if (dialogueUI != null)
        {
            dialogueUI.MarkLineComplete();
        }
    }

    #region Triggers

        private void OnTriggerEnter(Collider _other)
        {
            var interactable = _other.GetComponent<IInteractable>();

            if (interactable == null) { return; }

            // There is a valid interactable object within our range
            currentInteractable = interactable;
            interactionHintSprite.SetActive(true);
        }

        private void OnTriggerExit(Collider _other)
        {
            var interactable = _other.GetComponent<IInteractable>();

            if (interactable == null) { return; }
            if (interactable != currentInteractable) { return; }

            // We left the trigger box of a valid interactable
            currentInteractable = null;
            interactionHintSprite.SetActive(false);
        }

    #endregion
}
