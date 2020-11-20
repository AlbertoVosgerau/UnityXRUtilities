using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.UI;
using UnityEngine.XR.Management;

/// <summary>
/// Toggles between VR and non VR mode and fires events corresponding to the action
/// </summary>
public class VRModeToggle : MonoBehaviour
{
    public bool startInVRMode;
    public bool useFade = true;

    // FIXME: Not working properly, just breaks the whole transition. Figure out why
    [Tooltip("Optional XR Fade")]
    private XRFade xRFade;

    public List<GameObject> VRModeContent;
    public List<GameObject> NonVRModeContent;

    public UnityEvent onEnableVR;
    public UnityEvent onDisableVR;

    private bool isVRMode;

    private void Start()
    {
        if (startInVRMode)
        {
            EnableVR();
            return;
        }

        DisableVR();
    }
    public void ToggleVR()
    {
        isVRMode = !isVRMode;
        if(isVRMode)
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
        if(EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
        VRModeController.EnableVR();

        onEnableVR.Invoke();
        isVRMode = true;
        SetObjectVisibilities();

        if (xRFade != null && useFade)
        {
            xRFade.onWaitStart.RemoveListener(DoEnableVR);
        }
    }

    private void DoDisableVR()
    {
        if(EventSystem.current != null)
        {
        EventSystem.current.SetSelectedGameObject(null);
        }
        VRModeController.DisableVR();

        onDisableVR.Invoke();
        isVRMode = false;
        SetObjectVisibilities();
        if (xRFade != null && useFade)
        {
            //xRFade.onWaitStart.RemoveListener(DoDisableVR);
        }
    }

    public void EnableVR()
    {
#if !UNITY_ANDROID
        
        if(xRFade != null && useFade)
        {
            DoEnableVR();
            //xRFade.onWaitStart.AddListener(DoEnableVR);
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
            DoDisableVR();
            //xRFade.onWaitStart.AddListener(DoDisableVR);
            xRFade.FadeInOut();
            return;
        }
        DoDisableVR();
#endif
    }

    private void SetObjectVisibilities()
    {
        for (int i = 0; i < VRModeContent.Count; i++)
        {
            if (VRModeContent[i] != null)
                VRModeContent[i].SetActive(isVRMode);
        }

        for (int i = 0; i < NonVRModeContent.Count; i++)
        {
            if (NonVRModeContent[i] != null)
                NonVRModeContent[i].SetActive(!isVRMode);
        }
    }
}