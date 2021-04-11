using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable_SceneChanger : MonoBehaviour, IInteractable
{
    public void Interact(GameObject _other)
    {
        Debug.Log("Aight imma head out for real");
        SceneManager.LoadScene("RPG");
    }
}