using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Adds fade behavior to Teleport. Just add this to LocomotionSystem and fill the inspector.
/// Since I found no way to delay or prevent TeleportationProvider's queue to teleport, this scripts implements a workaround.
/// On startLocomotion it stores initial transform, on endLocomotion it stores final transform, then starts the fade, reverts to initial position
/// Finally, when fade is on waiting stage, it moves the rig to the final position.
/// </summary>
public class TeleportationFadeController : MonoBehaviour
{
    [SerializeField] private XRRig xRRig;
    [SerializeField] private TeleportationProvider teleportationProvider;
    [SerializeField] private XRFade xRFade;
 
    private Vector3 initialPosition;
    private Vector3 finalPosition;
    private Quaternion initialRotation;
    private Quaternion finalRotation;

    private void Start()
    {
        if (teleportationProvider == null)
            teleportationProvider = GetComponent<TeleportationProvider>();

        LocomotionSystem locomotionSystem = GetComponent<LocomotionSystem>();

        if (locomotionSystem != null && xRRig == null)
        {
            xRRig = locomotionSystem.xrRig;
        }
        xRFade.CreateCanvas();
    }
    private void OnEnable()
    {
        teleportationProvider.startLocomotion += (x) => initialPosition = xRRig.transform.position;
        teleportationProvider.startLocomotion += (x) => initialRotation = xRRig.transform.rotation;
        teleportationProvider.endLocomotion += (x) => finalPosition = xRRig.transform.position;
        teleportationProvider.endLocomotion += (x) => finalRotation = xRRig.transform.rotation;
        teleportationProvider.endLocomotion += (x) => xRFade.FadeInOut();

        xRFade.onFadeIn.AddListener(OnFadeIn);
        xRFade.onFadeOut.AddListener(OnFadeOut);
        xRFade.onWaitStart.AddListener(OnWaitStart);
        xRFade.onWaitFinish.AddListener(OnWaitFinish);
    }

    private void OnDisable()
    {
        xRFade.onFadeIn.RemoveListener(OnFadeIn);
        xRFade.onFadeOut.RemoveListener(OnFadeOut);
        xRFade.onWaitStart.RemoveListener(OnWaitStart);
        xRFade.onWaitFinish.RemoveListener(OnWaitFinish);
    }

    private void OnFadeIn()
    {
        xRRig.transform.position = initialPosition;
        xRRig.transform.rotation = initialRotation;
    }

    private void OnWaitStart()
    {
        xRRig.transform.position = finalPosition;
        xRRig.transform.rotation = finalRotation;
    }

    private void OnWaitFinish()
    {

    }

    private void OnFadeOut()
    {

    }
}
