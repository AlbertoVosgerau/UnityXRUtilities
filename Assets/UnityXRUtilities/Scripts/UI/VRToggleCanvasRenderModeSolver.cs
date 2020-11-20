using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRToggleCanvasRenderModeSolver : MonoBehaviour
{
    public Canvas CanvasRenderModeTarget;
    public Transform parent;
    public Vector3 worldSpaceModeLocalPosition = new Vector3(0, 1.25f, 0.75f);
    public Vector3 worldSpaceModeScale = new Vector3(0.0025f, 0.0025f, 0.0025f);

    private VRModeToggle vRToggle;
    private Transform storedParent;

    public void Awake()
    {
        CanvasRenderModeTarget = GetComponent<Canvas>();
        vRToggle = FindObjectOfType<VRModeToggle>();
    }

    private void OnEnable()
    {
        if (vRToggle == null)
            return;

        vRToggle.onEnableVR.AddListener(OnEnableVR);
        vRToggle.onDisableVR.AddListener(OnDisableVR);
    }

    private void OnDisable()
    {
        if (vRToggle == null)
            return;
        vRToggle.onEnableVR.RemoveListener(OnEnableVR);
        vRToggle.onDisableVR.RemoveListener(OnDisableVR);
    }

    public void OnEnableVR()
    {
        storedParent = CanvasRenderModeTarget.transform.parent;
        CanvasRenderModeTarget.renderMode = RenderMode.WorldSpace;
        if (parent == null)
            return;
        CanvasRenderModeTarget.transform.SetParent(parent);
        CanvasRenderModeTarget.transform.localPosition = worldSpaceModeLocalPosition;
        CanvasRenderModeTarget.transform.localScale = worldSpaceModeScale;
    }
    public void OnDisableVR()
    {
        CanvasRenderModeTarget.transform.SetParent(storedParent);
        CanvasRenderModeTarget.renderMode = RenderMode.ScreenSpaceCamera;
    }
}
