using System;
using UnityEngine.XR;

/// <summary>
/// Stores controllers as static objects for easy access from anywhere in the project
/// </summary>
public class XRInputDevices
{
    public static InputDevice RightController
    {
        get
        {
            return rightController;
        }
        set
        {
            rightController = value;
            if (onControllerConnected != null)
                onControllerConnected(rightController);
        }
    }
    public static InputDevice LeftController
    {
        get
        {
            return leftController;
        }
        set
        {
            leftController = value;
            if(onControllerConnected != null)
                onControllerConnected(leftController);
        }
    }
    private static InputDevice rightController;
    private static InputDevice leftController;

    public static Action<InputDevice> onControllerConnected;
}
