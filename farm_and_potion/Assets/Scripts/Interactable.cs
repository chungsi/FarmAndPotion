using UnityEngine;

public class Interactable : MonoBehaviour
{

    // master class for items & things that players can interact with

    public virtual void Interact()
    {
        // this method is meant to be overwritten
        Debug.Log("Interacting with " + transform.name);
    }

}
