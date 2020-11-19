using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Management;

/// <summary>
/// Toggles between VR and non VR mode and fires events corresponding to the action
/// </summary>
public class VRModeToggle : MonoBehaviour
{
    public bool startInVRMode;
    public UnityEvent onEnableVR;
    public UnityEvent onDisableVR;

    private void Awake()
    {
        ToggleVR(startInVRMode);
    }
    public void ToggleVR(bool isEnabled)
    {
        if(isEnabled)
        {
            EnableVR();
        }
        else
        {
            DisableVR();
        }
    }
    public void EnableVR()
    {
        VRModeController.EnableVR();
        onEnableVR.Invoke();
    }
    public void DisableVR()
    {
        VRModeController.DisableVR();
        onDisableVR.Invoke();
    }
}