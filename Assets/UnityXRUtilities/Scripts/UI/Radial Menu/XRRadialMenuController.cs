using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.Events;

public class XRRadialMenuController : MonoBehaviour
{
    [SerializeField] private XRNode controllerNode;
    [SerializeField] private float inputDeadZone = 0.4f;
    [SerializeField] private int activeSections = 8;
    [SerializeField] private List<XRRadialMenuItem> menuItems;

    private XRRadialMenuItem currentMenuItem;
    private bool haveMenuItemSelected;

    public TextMeshProUGUI debugText;

    private void Awake()
    {
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = new Vector3(90, 0, 0);
    }
    private void Update()
    {
        bool triggerButton;
        InputDevice inputDevice = controllerNode == XRNode.LeftHand ? XRInputDevices.LeftController : XRInputDevices.RightController;

        if(haveMenuItemSelected)
        {
            inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButton);

            if (triggerButton) 
                return;

            debugText.text = $"Deselected item: {currentMenuItem}";
            currentMenuItem.Deselect();
        }

        inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 controllerAxis2D);

        if (controllerAxis2D.magnitude < inputDeadZone)
        {
            currentMenuItem.HoverExit();
            currentMenuItem = null;
            return;
        }

        float angle = Vector2ToAngle(controllerAxis2D);
        HoveredSection(angle, out int hoveredSection);
        Debug.Log($"Selecting: {hoveredSection}");

        if(currentMenuItem != null)
        {
            inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool isSelected);
            haveMenuItemSelected = isSelected;
            if(haveMenuItemSelected)
            {
                currentMenuItem.Select();
                debugText.text = $"Selected item: {currentMenuItem}";
            }
        }

        if (currentMenuItem == menuItems[hoveredSection])
            return;

        if(currentMenuItem != null)
        {
            currentMenuItem.HoverExit();
        }

        currentMenuItem = menuItems[hoveredSection];
        menuItems[hoveredSection].HoverEnter();
        debugText.text = $"Hover item: {currentMenuItem}";

    }

    private void SelectItem()
    {

    }

    private void DeselectItem()
    {

    }
    
    public float Vector2ToAngle(Vector2 input)
    {
        float valueInradians = Mathf.Atan2(input.y, input.x);
        float valueInDegrees = valueInradians * 180 / Mathf.PI;
        float finalValue = valueInDegrees < 0 ? (180 - Mathf.Abs(valueInDegrees)) + 180 : valueInDegrees;

        return finalValue;
    }

    public int HoveredSection(float angle, out int section)
    {
        float sectionLenght = 360 / activeSections;

        for (int i = 0; i < activeSections; i++)
        {
            float sectionStart = i *sectionLenght;
            float sectionFinish = (i + 1) * sectionLenght;
            Debug.Log($"Length: {sectionLenght}, Angle: {angle}, Section Start: {sectionStart}, Section End {sectionFinish}");

            if(angle > sectionStart && angle < sectionFinish)
            {
                return section = i;
            }
        }
        return section = 0;
    }
}
