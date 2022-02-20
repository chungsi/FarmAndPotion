using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Speakable : MonoBehaviour, IInteractable
{
	[SerializeField] private InputReader _inputReader = default;
	[SerializeField] private string conversationStartNode;

	private bool isCurrentConversation = false;
	// public string characterName = "";
	// public string talkToNode = "";

	[Header("Optional")]

	private DialogueRunner dialogueRunner;

	void Start()
	{
		dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
		// dialogueRunner.onDialogueComplete.AddListener(EndConversation);
	}

	private void StartConversation()
	{
		isCurrentConversation = true;
		dialogueRunner.StartDialogue(conversationStartNode);
	}

	// To add functionality (if needed) when a conversation ends
	private void EndConversation()
	{
		if (isCurrentConversation)
		{
			isCurrentConversation = false;
		}

	}


	#region IInteractable

	public void Interact(GameObject _other)
	{
		if (dialogueRunner.IsDialogueRunning == true) { return; }

		if (conversationStartNode != null)
		{
			StartConversation();
		}
	}

	#endregion
}