using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAutoMainCameraSetter : MonoBehaviour
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
