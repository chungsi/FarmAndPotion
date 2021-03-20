using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameObject gameManager;

    void Awake()
    {
        InstantiateGameManagerIfNoneExists();
    }

    void InstantiateGameManagerIfNoneExists()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }
    }
}
