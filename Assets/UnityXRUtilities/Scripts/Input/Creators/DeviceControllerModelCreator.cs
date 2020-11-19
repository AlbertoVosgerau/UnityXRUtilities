﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Instantiates controller model into the scene and puts it inside the corresponding hand.
/// It verifies the device model and tries to attach the corresponding model to it.
/// If the device is not into the list, creates a default model.
/// The models are stored in a ScriptableObject that can be created from Project, Right Click > Create > UnityXRUtilities > VRControllers
/// </summary>
public class DeviceControllerModelCreator : MonoBehaviour
{
    public bool showControllersOnCreation = true;
    [SerializeField] private VRControllers controllers;
    [SerializeField] private XRController leftController;
    [SerializeField] private XRController rightController;

    public UnityEvent<GameObject> onControllerCreated;

    private void OnEnable()
    {
        XRInputDevices.onControllerConnected += OnControllerConnected;
    }
    private void OnDisable()
    {
        XRInputDevices.onControllerConnected -= OnControllerConnected;
    }
    private void OnControllerConnected(InputDevice inputDevice)
    {
        if(inputDevice.characteristics.HasFlag(InputDeviceCharacteristics.Right))
        {
            SolveControllerModel(inputDevice, rightController);
        }
        else
        {
            SolveControllerModel(inputDevice, leftController);
        }
    }
    private void SolveControllerModel(InputDevice inputDevice, XRController target)
    {
        if (!inputDevice.isValid)
            return;

        Transform[] targetChildren = target.GetComponentsInChildren<Transform>();

        for (int i = 0; i < targetChildren.Length; i++)
        {
            if(targetChildren[i].name == "controller")
            {
                return;
            }
        }

        GameObject newController = null;

        for (int i = 0; i < controllers.controllerNames.Count; i++)
        {
            if(controllers.controllerNames[i] == inputDevice.name)
            {
                newController = Instantiate(controllers.controllerPrefabs[i].gameObject, target.transform);
                newController.name = controllers.controllerNames[i];
                break;
            }
        }

        if(newController == null)
        {
            if(controllers.defaultController == null)
            {
                Debug.LogError("No default controller assigned. Please provide a controller");
                return;
            }
            newController = Instantiate(controllers.defaultController, target.transform);
            newController.name = controllers.defaultController.name;

        }

        onControllerCreated.Invoke(newController);

        if(!showControllersOnCreation)
        {
            newController.SetActive(false);
        }
    }
}