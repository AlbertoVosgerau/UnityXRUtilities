using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class DeviceControllerModelSolver : MonoBehaviour
{
    [SerializeField] private bool showControllersOnCreation = true;
    [SerializeField] private VRControllers controllers;
    [SerializeField] private XRController leftController;
    [SerializeField] private XRController rightController;
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
            if(targetChildren[i].name == inputDevice.name)
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

        target.modelPrefab = newController.transform;

        if(!showControllersOnCreation)
        {
            newController.SetActive(false);
        }
    }
}