using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandAnimationController : MonoBehaviour
{
    [SerializeField] private InputType inputType;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        switch (inputType)
        {
            case InputType.RightController:
                UpdateAnimator(XRInputDevices.RightController);
                break;
            case InputType.LeftController:
                UpdateAnimator(XRInputDevices.LeftController);
                break;
        }
    }

    private void UpdateAnimator(InputDevice inputDevice)
    {
        if (!inputDevice.isValid)
            return;

        inputDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        inputDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue);

        animator.SetFloat("Trigger", triggerValue);
        animator.SetFloat("Grip", gripValue);
    }
}
