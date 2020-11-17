using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class XRScrollRectAxisInput : MonoBehaviour
{
    private XRController xRController;
    private XRRayInteractor interactor;
    private ScrollRect scrollRect;
    private bool isOverRectTransform;

    private void Start()
    {
        interactor = GetComponent<XRRayInteractor>();
        xRController = GetComponent<XRController>();
    }

    private void Update()
    {
        if (!isOverRectTransform)
            return;

        Vector2 result;
        if(xRController.controllerNode == XRNode.RightHand)
        {
            XRInputDevices.RightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out result);
        }
        else
        {
            XRInputDevices.RightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out result);
        }
        Scroll(result);
    }
    private void OnEnable()
    {
        interactor.onHoverEntered.AddListener(OnHoverEnter);
    }
    private void OnDisable()
    {
        interactor.onHoverEntered.RemoveListener(OnHoverEnter);
    }
    private void OnHoverEnter(XRBaseInteractable interactable)
    {
        if (interactable.GetType() == typeof(XRGrabLocalInteractable))
        {
            XRGrabLocalInteractable grabInteractable = (XRGrabLocalInteractable)interactable;
            interactor.useForceGrab = grabInteractable.canGrabAtDistance;
        }
        else
        {
            //interactor.useForceGrab = storedForceGrab;
        }
    }

    public void Scroll(Vector2 scrollValue)
    {
        scrollValue = scrollValue / 10;

        if (scrollRect == null)
            return;

        if (scrollRect.horizontal)
        {
            scrollRect.verticalNormalizedPosition += scrollValue.y;
        }

        if (scrollRect.vertical)
        {
            scrollRect.verticalNormalizedPosition += scrollValue.y;
        }
    }
}
