using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Interactable : MonoBehaviour
{
    private void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }
    public abstract void Interact();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // other.GetComponent<PlayerMovement>().OpenInteractableIcon();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // other.GetComponent<PlayerMovement>().CloseInteractableIcon();
    }
}
