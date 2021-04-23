using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = default;
    private DialogueUI dialogueUI;

    // private void OnEnable()
    // {
    //     _inputReader.advanceDialogueEvent += OnSpeak;
    // }

    // private void OnDisable()
    // {
    //     _inputReader.advanceDialogueEvent -= OnSpeak;
    // }

    // void Start()
    // {
    //     dialogueUI = FindObjectOfType<Yarn.Unity.DialogueUI>();
    // }

    // private void OnSpeak()
    // {
    //     if (dialogueUI != null)
    //     {
    //         dialogueUI.MarkLineComplete();
    //     }
    // }

    // private void OnInteract()
    // {
    //     base.OnInteract();
    //     // _inputReader.EnableDialogueInput();
    // }
}