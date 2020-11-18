using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class XRScrollRectAxisInput : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private ScrollRect scrollRect;
    private bool isOverRectTransform;

    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    private void Update()
    {
        //if (!isOverRectTransform)
        //    return;

        Vector2 result;

        XRInputDevices.RightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out result);
        Scroll(result);
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log($"Hover: {eventData.ToString()}");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}
