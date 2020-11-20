using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Management;

/// <summary>
/// Enable/Disable VR Mode
/// </summary>
public class VRModeController : MonoBehaviour
{
    public static void EnableVR()
    {
        XRGeneralSettings.Instance.Manager.StartSubsystems();
        XRGeneralSettings.Instance.Manager.InitializeLoaderSync();
    }
    public static void DisableVR()
    {
        if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
        {
            XRGeneralSettings.Instance.Manager.StopSubsystems();
            XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        }
    }
}
