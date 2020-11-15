using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "VRControllers", menuName = "UnityXRUtilities/VRControllers")]
public class VRControllers : ScriptableObject
{
    public GameObject defaultController;
    public List<string> controllerNames;
    public List<GameObject> controllerPrefabs;
}

[CustomEditor(typeof(VRControllers))]
public class VRControllersEditor : Editor
{
    VRControllers vrControllers;
    private void OnEnable()
    {
        vrControllers = target as VRControllers;
    }
    public override void OnInspectorGUI()
    {
        GUILayout.Label("Default Controller");
        vrControllers.defaultController = EditorGUILayout.ObjectField(vrControllers.defaultController, typeof(GameObject), true) as GameObject;
        if (vrControllers.controllerNames.Count == 0)
        {
            if (GUILayout.Button("Add controller"))
            {
                vrControllers.controllerPrefabs.Add(null);
                vrControllers.controllerNames.Add(null);
            }
            return;
        }
        GUILayout.Label($"{vrControllers.controllerNames.Count} Controllers");
        GUILayout.BeginVertical();
        for (int i = 0; i < vrControllers.controllerNames.Count; i++)
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("x", GUILayout.Width(20), GUILayout.Height(20)))
            {
                vrControllers.controllerPrefabs.RemoveAt(i);
                vrControllers.controllerNames.RemoveAt(i);
                return;
            }
            vrControllers.controllerNames[i] = GUILayout.TextField(vrControllers.controllerNames[i]);
            vrControllers.controllerPrefabs[i] = EditorGUILayout.ObjectField(vrControllers.controllerPrefabs[i], typeof(GameObject), true) as GameObject;
            if (GUILayout.Button("+", GUILayout.Width(20), GUILayout.Height(20)))
            {
                vrControllers.controllerPrefabs.Insert(i+1, null);
                vrControllers.controllerNames.Insert(i+1, null);
            }
            GUILayout.EndHorizontal();

            if(string.IsNullOrEmpty(vrControllers.controllerNames[i]) && vrControllers.controllerPrefabs[i] != null)
            {
                vrControllers.controllerNames[i] = vrControllers.controllerPrefabs[i].name;
            }
        }
        GUILayout.EndVertical();
    }
}