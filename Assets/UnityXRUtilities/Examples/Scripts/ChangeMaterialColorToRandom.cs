using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ChangeMaterialColorToRandom : MonoBehaviour
{
    [SerializeField] private bool onTrigger = true;
    private Material mat;
    private void Start()
    {
        mat = GetComponent<Renderer>().sharedMaterial;
    }
    private void Update()
    {
        if (!onTrigger)
            return;

        XRInputDevices.RightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
        if (primaryButtonValue)
            ChangeColor();
    }

    public void ChangeColor()
    {
        if (mat == null)
            return;
        mat.color = Random.ColorHSV();
    }
}
