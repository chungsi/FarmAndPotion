using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Speakable : MonoBehaviour, IInteractable
{
    [SerializeField] private InputReader _inputReader = default;
    
    public string characterName = "";
    public string talkToNode = "";

    [Header("Optional")]
    public YarnProgram scriptToLoad;

    private DialogueRunner dialogueRunner;

    void Start()
    {
        if (scriptToLoad != null)
        {
            dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
            dialogueRunner.Add(scriptToLoad);
        }
    }

    #region IInteractable

    public void Interact(GameObject _other)
    {
        if (dialogueRunner.IsDialogueRunning == true)
        {
            return;
        }

        dialogueRunner.StartDialogue(talkToNode);
    }

    #endregion
}