using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Add this file to a gameObject that is named the same as the scene it
/// should load.
/// </summary>
public class Interactable_SceneChanger : MonoBehaviour, IInteractable
{
    public void Interact(GameObject _other)
    {
        Debug.Log("Aight imma head out for real");
        SceneManager.LoadSceneAsync(gameObject.name);
    }
}