using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
/// <summary>
/// Attach XRayForceGrabSolver to an X Ray Interactor, then use this as yout Grab Interactable to decide it it will force grab or not.
/// </summary>
public class XRGrabLocalInteractable : XRGrabInteractable
{
    public bool canGrabAtDistance;
}
