using UnityEngine;

public class Interactable : MonoBehaviour
{

    // master class for all things that players can interact with

    public float radius = 20f;

    // This draws a sphere for the interactable area around the object
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public virtual void Interact()
    {
        // this method is meant to be overwritten
        Debug.Log("Interacting with " + transform.name);
    }

}
