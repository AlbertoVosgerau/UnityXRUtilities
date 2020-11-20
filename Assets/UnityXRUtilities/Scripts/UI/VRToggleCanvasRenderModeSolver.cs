using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRToggleCanvasRenderModeSolver : MonoBehaviour
{
    public VRModeToggle vRToggle;
    public Canvas CanvasRenderModeTarget;
    public Camera vrCamera;
    public Transform xRParent;
    public Transform PCParent;
    public Vector3 worldSpaceModeLocalPosition = new Vector3(0, 1.25f, 0.75f);
    public Vector3 worldSpaceModeScale = new Vector3(0.0025f, 0.0025f, 0.0025f);


    public void Awake()
    {
        vRToggle = FindObjectOfType<VRModeToggle>();
        CanvasRenderModeTarget = GetComponent<Canvas>();
        if(xRParent != null && vrCamera == null)
            vrCamera = xRParent.GetComponentInChildren<Camera>(true);
    }

    private void OnEnable()
    {
        vRToggle.onEnableVR.AddListener(OnEnableVR);
        vRToggle.onDisableVR.AddListener(OnDisableVR);
    }

    private void OnDisable()
    {
        vRToggle.onEnableVR.RemoveListener(OnEnableVR);
        vRToggle.onDisableVR.RemoveListener(OnDisableVR);
    }

    public void OnEnableVR()
    {
        Debug.Log("Enabled VR");
        if(PCParent == null)
            PCParent = CanvasRenderModeTarget.transform.parent;

        if (xRParent != null && vrCamera == null)
            vrCamera = xRParent.GetComponentInChildren<Camera>(true);

        CanvasRenderModeTarget.renderMode = RenderMode.WorldSpace;
        if (xRParent == null)
            return;

        CanvasRenderModeTarget.worldCamera = vrCamera;
        CanvasRenderModeTarget.transform.SetParent(xRParent);
        CanvasRenderModeTarget.transform.localPosition = worldSpaceModeLocalPosition;
        CanvasRenderModeTarget.transform.localScale = worldSpaceModeScale;
    }
    public void OnDisableVR()
    {
        Debug.Log("Disabled VR");
        CanvasRenderModeTarget.worldCamera = null;
        CanvasRenderModeTarget.transform.SetParent(PCParent);
        CanvasRenderModeTarget.renderMode = RenderMode.ScreenSpaceCamera;
    }
}
