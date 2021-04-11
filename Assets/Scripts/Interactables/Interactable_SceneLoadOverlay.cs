using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable_SceneLoadOverlay : MonoBehaviour, IInteractable
{
    bool isSceneLoaded = false;
    public void Interact(GameObject _other)
    {
        Debug.Log("Aight imma head out for real");

        if (!isSceneLoaded)
        {
            SceneManager.LoadScene("Crafting", LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.UnloadSceneAsync("Crafting");
        }
        isSceneLoaded = !isSceneLoaded;
    }
}