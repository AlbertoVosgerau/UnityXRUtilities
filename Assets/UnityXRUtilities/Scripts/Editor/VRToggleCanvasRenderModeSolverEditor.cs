using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[CustomEditor(typeof(VRToggleCanvasRenderModeSolver))]
public class VRToggleCanvasRenderModeSolverEditor : Editor
{
    VRToggleCanvasRenderModeSolver canvasRenderModeSolver;
    CanvasRenderEditMode editMode;
    RectTransform rectTransform;
    private void OnEnable()
    {
        canvasRenderModeSolver = target as VRToggleCanvasRenderModeSolver;
        Canvas localCanvas = canvasRenderModeSolver.GetComponent<Canvas>();
        XRRig targetTransform = FindObjectOfType<XRRig>();
        if (canvasRenderModeSolver.CanvasRenderModeTarget == null && localCanvas != null)
            canvasRenderModeSolver.CanvasRenderModeTarget = localCanvas;
        if (canvasRenderModeSolver.parent == null && targetTransform != null)
            canvasRenderModeSolver.parent = targetTransform.transform;

        rectTransform = canvasRenderModeSolver.GetComponent<RectTransform>();

        if (canvasRenderModeSolver.CanvasRenderModeTarget.renderMode == RenderMode.WorldSpace)
        {
            rectTransform.SetParent(canvasRenderModeSolver.parent);
            canvasRenderModeSolver.worldSpaceModeLocalPosition = rectTransform.localPosition;
            canvasRenderModeSolver.worldSpaceModeScale = rectTransform.localScale;
        }
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Canvas on VR Mode"))
        {
            canvasRenderModeSolver.Awake();
            canvasRenderModeSolver.OnEnableVR();
            editMode = CanvasRenderEditMode.WorldSpace;
        }
        if (GUILayout.Button("Canvas on PC Mode"))
        {
            canvasRenderModeSolver.Awake();
            canvasRenderModeSolver.OnDisableVR();
            editMode = CanvasRenderEditMode.ScreenSpace;
        }
        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        GUILayout.Label($"Current Edit mode:");
        editMode = (CanvasRenderEditMode)EditorGUILayout.EnumPopup(editMode);
        GUILayout.EndHorizontal();

        
        switch (editMode)
        {
            case CanvasRenderEditMode.ScreenSpace:
                if (canvasRenderModeSolver.CanvasRenderModeTarget.renderMode != RenderMode.ScreenSpaceCamera)
                {
                    canvasRenderModeSolver.OnDisableVR();
                }
                break;
            case CanvasRenderEditMode.WorldSpace:
                if (canvasRenderModeSolver.CanvasRenderModeTarget.renderMode != RenderMode.WorldSpace)
                {
                    canvasRenderModeSolver.OnEnableVR();
                }
                if (GUILayout.Button("Store Transforms"))
                {
                    rectTransform.SetParent(canvasRenderModeSolver.parent);
                    canvasRenderModeSolver.worldSpaceModeLocalPosition = rectTransform.localPosition;
                    canvasRenderModeSolver.worldSpaceModeScale = rectTransform.localScale;
                }
                break;
        }
    }
}
