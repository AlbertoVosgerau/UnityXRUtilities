using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.Events;

/// <summary>
/// Easily implements a radial menu.
/// </summary>
public class XRRadialMenuController : MonoBehaviour
{
    [SerializeField] private XRNode controllerNode;
    [SerializeField] private float inputDeadZone = 0.4f;
    [Tooltip("The number of sections you want to interact with")]
    [SerializeField] private int activeSections = 8;
    [Tooltip("The visual representation of the sections. If your item number is lower than active rections, you can have partial radial menus")]
    [SerializeField] private List<XRRadialMenuItem> menuItems;

    private XRRadialMenuItem currentHoveredMenuItem;
    private bool haveMenuItemSelected;

    public UnityEvent onEnable;
    public UnityEvent onDisable;
    public UnityEvent<XRRadialMenuItem> onHoverEnterItem;
    public UnityEvent<XRRadialMenuItem> onHoverExitItem;
    public UnityEvent<XRRadialMenuItem> onSelectItem;
    public UnityEvent<XRRadialMenuItem> onDeselectItem;

    public TextMeshProUGUI debugText;

    private void Awake()
    {
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = new Vector3(90, 0, 0);
    }
    private void Update()
    {
        InputDevice inputDevice = controllerNode == XRNode.LeftHand ? XRInputDevices.LeftController : XRInputDevices.RightController;

        if(haveMenuItemSelected && currentHoveredMenuItem != null)
        {
            inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out haveMenuItemSelected);

            if (haveMenuItemSelected) 
                return;

            DeselectItem();
        }

        inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 controllerAxis2D);

        if (controllerAxis2D.magnitude < inputDeadZone)
        {
            if(currentHoveredMenuItem != null)
            {
                HoverExitItem();                
            }
            return;
        }

        float angle = Vector2ToAngle(controllerAxis2D);
        HoveredSection(angle, out int hoveredSection);

        if(currentHoveredMenuItem != null)
        {
            inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out haveMenuItemSelected);
            if (haveMenuItemSelected)
            {
                SelectItem();
            }
        }

        if (currentHoveredMenuItem == menuItems[hoveredSection])
            return;

        if(currentHoveredMenuItem != null)
        {
            HoverExitItem();
        }

        HoverEnterItem(hoveredSection);
    }
    private void HoverEnterItem(int item)
    {
        currentHoveredMenuItem = menuItems[item];
        onHoverEnterItem.Invoke(currentHoveredMenuItem);
        menuItems[item].HoverEnter();
        debugText.text = $"Hover item: {currentHoveredMenuItem}";
    }
    private void HoverExitItem()
    {        
        debugText.text = $"Leaving item: {currentHoveredMenuItem}";
        onHoverEnterItem.Invoke(currentHoveredMenuItem);
        currentHoveredMenuItem.HoverExit();
        currentHoveredMenuItem = null;
    }
    private void SelectItem()
    {
        onSelectItem.Invoke(currentHoveredMenuItem);
        currentHoveredMenuItem.Select();
        if(currentHoveredMenuItem.isToggle)
            debugText.text = $"Toggled item: {currentHoveredMenuItem} to {currentHoveredMenuItem.IsSelected}";
        else
            debugText.text = $"Selected item: {currentHoveredMenuItem}";
    }
    private void DeselectItem()
    {
        onDeselectItem.Invoke(currentHoveredMenuItem);
        currentHoveredMenuItem.Deselect();
        if (currentHoveredMenuItem.isToggle)
            debugText.text = $"Toggled item: {currentHoveredMenuItem} to {currentHoveredMenuItem.IsSelected}";
        else
            debugText.text = $"Deselected item: {currentHoveredMenuItem}";
    }

    public void SetXRNode(XRNode node)
    {
        controllerNode = node;
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
