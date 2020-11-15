using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class RegisterInputDevices : MonoBehaviour
{
    [SerializeField] private bool scanForDevicesOnUpdate = true;
    private void Start()
    {
        RegisterDevices();
    }
    private void Update()
    {
        if (!scanForDevicesOnUpdate)
            return;

        RegisterDevices();
    }
    public static void RegisterDevices()
    {
        Debug.Log("Search for devices");    
        List<InputDevice> validDevices = new List<InputDevice>();
        InputDeviceCharacteristics validDeviceModel = InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(validDeviceModel, validDevices);

        Debug.Log($"Valid devices count:{validDevices.Count}");
        foreach (var item in validDevices)
        {
            Debug.Log($"Device: {item.characteristics}");
        }

        for (int i = 0; i < validDevices.Count; i++)
        {
            InputDevice device = validDevices[i];
            Debug.Log(device.characteristics);
            if (device.characteristics.HasFlag(InputDeviceCharacteristics.Right) && XRInputDevices.RightController != null)
            {
                Debug.Log("Right device");
                XRInputDevices.RightController = device;
            }

            if (device.characteristics.HasFlag(InputDeviceCharacteristics.Left) && XRInputDevices.LeftController != null)
            {
                Debug.Log("Left device");
                XRInputDevices.LeftController = device;
            }

            if (XRInputDevices.RightController != null && XRInputDevices.LeftController != null)
            {
                break;
            }
        }
    }
}
