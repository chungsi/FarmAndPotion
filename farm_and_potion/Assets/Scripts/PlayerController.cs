using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IPointerClickHandler
{
    // for the player game object
    // for now, treating the player as the cursor, without the rpg elements

    // for what items are clicked? in inventory/wild???
    public Interactable focus;

    void Start()
    {

    }

    void Update()
    {
        // if we press left mouse
        if (Input.GetMouseButtonDown(0)) {
            // Interactable interactable = 
        }

        // if we press right mouse
        if (Input.GetMouseButtonDown(1)) {

        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(name + " clicked");
    }
}
