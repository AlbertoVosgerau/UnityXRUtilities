using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class XRScrollRectAxisInput : MonoBehaviour
{
    private ScrollRect scrollRect;

    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>(); 
    }

    private void Update()
    {
        XRInputDevices.RightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 test);

        Scroll(test);
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
