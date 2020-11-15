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
        if (XRInputDevices.RightController.isValid && XRInputDevices.LeftController.isValid)
        {
            return;
        }

        List<InputDevice> validDevices = new List<InputDevice>();
        InputDeviceCharacteristics validDeviceModel = InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(validDeviceModel, validDevices);

        for (int i = 0; i < validDevices.Count; i++)
        {
            InputDevice device = validDevices[i];
            Debug.Log(device.characteristics);
            if (device.characteristics.HasFlag(InputDeviceCharacteristics.Right) && !XRInputDevices.RightController.isValid)
            {
                Debug.Log("Right controller connected!");
                XRInputDevices.RightController = device;
            }

            if (device.characteristics.HasFlag(InputDeviceCharacteristics.Left) && !XRInputDevices.LeftController.isValid)
            {
                Debug.Log("Left controller connected!");
                XRInputDevices.LeftController = device;
            }
        }
    }
}
