using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRCanvasFadeInOutController))]
public class TeleportationFadeController : MonoBehaviour
{
    public TeleportationProvider teleportationProvider;
    private XRCanvasFadeInOutController canvasFadeInOutController;

    private void Awake()
    {
        canvasFadeInOutController = GetComponent<XRCanvasFadeInOutController>();
    }
    private void OnEnable()
    {
        teleportationProvider.startLocomotion += (x) => canvasFadeInOutController.FadeInOut();
        canvasFadeInOutController.onFadeIn.AddListener(() => teleportationProvider.enabled = false);
        canvasFadeInOutController.onFadeOut.AddListener(() => teleportationProvider.enabled = true);
    }
}
