using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ChangeMaterialColorToRandom : MonoBehaviour
{
    private Material mat;
    private void Start()
    {
        mat = GetComponent<Renderer>().sharedMaterial;
    }
    private void Update()
    {
        if (mat == null)
            return;

        XRInputDevices.RightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
        if (primaryButtonValue)
            mat.color = Random.ColorHSV();
    }
}
