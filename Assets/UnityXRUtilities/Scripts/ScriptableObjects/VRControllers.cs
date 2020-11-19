using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "VRControllers", menuName = "UnityXRUtilities/VR Controllers")]
public class VRControllers : ScriptableObject
{
    public GameObject defaultController;
    public List<string> controllerNames;
    public List<GameObject> controllerPrefabs;
}