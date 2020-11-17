﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRayForceGrabSolver : MonoBehaviour
{    
    private XRRayInteractor interactor;
    private bool storedForceGrab;

    private void Awake()
    {
        interactor = GetComponent<XRRayInteractor>();        
        storedForceGrab = interactor.useForceGrab;
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
            interactor.useForceGrab = storedForceGrab;
        }
    }
}
