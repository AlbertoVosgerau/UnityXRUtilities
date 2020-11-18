using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Management;

public class VRModeController : MonoBehaviour
{
    
    
    public static void EnableVR()
    {
        XRGeneralSettings.Instance.Manager.InitializeLoaderSync();
        XRGeneralSettings.Instance.Manager.StartSubsystems();

        //List<XRDisplaySubsystemDescriptor> displaysDescs = new List<XRDisplaySubsystemDescriptor>();
        //List<XRDisplaySubsystem> displays = new List<XRDisplaySubsystem>();
        ////displays.Clear();
        ////SubsystemManager.GetInstances(displays);
        ////foreach (var displaySubsystem in displays)
        ////{
        ////    if (displaySubsystem.running)
        ////    {
        ////        Debug.Log($"VR Running: {displaySubsystem.running}");
        ////        break;
        ////    }
        ////}
        //XRGeneralSettings.Instance.Manager.InitializeLoader();
        ////XRSettings.enabled = true;
        ////XRDisplaySubsystem.Start();

        //Debug.Log("Will enable");
        //displays.Clear();
        //SubsystemManager.GetInstances(displays);
        //foreach (var displaySubsystem in displays)
        //{
        //    if (displaySubsystem.running)
        //    {
        //        Debug.Log($"VR Running: {displaySubsystem.running}");
        //        break;
        //    }
        //}
        //XRDisplaySubsystem.Start();
    }
    public static void DisableVR()
    {
        //List<XRDisplaySubsystemDescriptor> displaysDescs = new List<XRDisplaySubsystemDescriptor>();
        //List<XRDisplaySubsystem> displays = new List<XRDisplaySubsystem>();

        //XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        ////XRSettings.enabled = false;
        //displays.Clear();
        //SubsystemManager.GetInstances(displays);
        //foreach (var displaySubsystem in displays)
        //{
        //    if (displaySubsystem.running)
        //    {
        //        Debug.Log($"VR Running: {displaySubsystem.running}");
        //        break;
        //    }
        //}

        if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
        {
            XRGeneralSettings.Instance.Manager.StopSubsystems();
            XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        }
    }
}
