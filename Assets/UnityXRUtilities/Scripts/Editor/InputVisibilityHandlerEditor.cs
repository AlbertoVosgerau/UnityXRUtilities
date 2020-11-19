using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InputModelsVisibilityHandler))]
public class InputModelsVisibilityHandlerEditor : Editor
{
    InputModelsVisibilityHandler visibilityHandler;
    private void OnEnable()
    {
        visibilityHandler = target as InputModelsVisibilityHandler;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Show Controllers"))
        {
            if (Application.isPlaying)
                visibilityHandler.ShowControllers();
        }
        if (GUILayout.Button("Show Hands"))
        {
            if (Application.isPlaying)
                visibilityHandler.ShowHands();
        }
    }
}