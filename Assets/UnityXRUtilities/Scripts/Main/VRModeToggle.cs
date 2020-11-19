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
    public bool useFade = true;
    [Tooltip("Optional XR Fade")]
    public XRFade xRFade;
    public UnityEvent onEnableVR;
    public UnityEvent onDisableVR;

    private void Start()
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
    private void DoEnableVR()
    {
        EventSystem.current.SetSelectedGameObject(null);
        VRModeController.EnableVR();
        onEnableVR.Invoke();

        if (xRFade != null && useFade)
        {
            xRFade.onWaitStart.RemoveListener(DoEnableVR);
        }
    }

    private void DoDisableVR()
    {
        EventSystem.current.SetSelectedGameObject(null);
        VRModeController.DisableVR();
        onDisableVR.Invoke();
        if (xRFade != null && useFade)
        {
            xRFade.onWaitStart.RemoveListener(DoDisableVR);
        }
    }

    public void EnableVR()
    {
#if !UNITY_ANDROID
        
        if(xRFade != null && useFade)
        {
            xRFade.onWaitStart.AddListener(DoEnableVR);
            xRFade.FadeInOut();
            return;
        }
        DoEnableVR();
#endif
    }
 
    public void DisableVR()
    {
#if !UNITY_ANDROID        
        
        if (xRFade != null && useFade)
        {
            xRFade.onWaitStart.AddListener(DoDisableVR);
            xRFade.FadeInOut();
            return;
        }
        DoDisableVR();
#endif
    }

}