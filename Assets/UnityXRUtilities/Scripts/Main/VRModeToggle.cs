using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
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
#if !UNITY_ANDROID
        // Deselect the button that fired this, to avoid a bug that the current EventSystem will hide.
        EventSystem.current.SetSelectedGameObject(null);
        VRModeController.EnableVR();
        onEnableVR.Invoke();
#endif
    }
    public void DisableVR()
    {
#if !UNITY_ANDROID        
        EventSystem.current.SetSelectedGameObject(null);
        VRModeController.DisableVR();
        onDisableVR.Invoke();
#endif
    }
}