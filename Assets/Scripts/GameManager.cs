using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // singleton
    public static GameManager instance = null;

    void Awake()
    {
        EnsureOnlyOneInstanceExists();
        DontDestroyOnLoad(gameObject);
    }

    void EnsureOnlyOneInstanceExists()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
