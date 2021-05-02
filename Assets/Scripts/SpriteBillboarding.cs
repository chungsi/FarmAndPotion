using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBillboarding : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        // transform.LookAt(mainCamera.transform);

        Debug.Log("camera rotation: " + mainCamera.transform.rotation.eulerAngles.x);

        transform.rotation = Quaternion.Euler(mainCamera.transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f);
    }
}