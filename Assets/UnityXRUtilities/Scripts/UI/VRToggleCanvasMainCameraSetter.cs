using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// When you toggle between VR and non VR mode, you might need to define the Canvase's worldCamera, so it interacts correctly with the UI.
/// This script get the current active camera for interaction;
/// </summary>
public class VRToggleCanvasMainCameraSetter : MonoBehaviour
{
    private Canvas canvas;
    private void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    private void Update()
    {
        if (canvas.worldCamera == null)
            return;

        if (canvas.worldCamera.gameObject.activeInHierarchy)
            return;

        Camera newCam = FindObjectOfType<Camera>();

        if (newCam == null)
            return;

        canvas.worldCamera = newCam;

    }
}
