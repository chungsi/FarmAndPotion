using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Add this file to a gameObject that is named the same as the scene it
/// should load as an overlay.
/// </summary>
public class Interactable_SceneLoadOverlay : MonoBehaviour, IInteractable
{
    bool isSceneLoaded = false;

    public void Interact(GameObject _other)
    {
        Debug.Log("Aight imma do some work");

        if (!isSceneLoaded)
        {
            SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
            isSceneLoaded = true;
        }
        else
        {
            SceneManager.UnloadSceneAsync(gameObject.name);
            isSceneLoaded = false;
        }
    }
}