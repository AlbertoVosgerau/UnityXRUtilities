using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class XRRadialMenuToggle : XRRadialMenuItem
{
    [Header("Toggle")]
    public bool isSelected;
    [Tooltip("Optional toggle object that will be enagled when selected")]
    public RectTransform toggleObject;
    public UnityEvent<bool> onToggle;

    public override void Awake()
    {
        base.Awake();
        if (toggleObject != null)
            toggleObject.gameObject.SetActive(isSelected);
    }
    public override void HoverEnter()
    {
        imageComponent.color = hoverColor;
        base.HoverEnter();
    }
    public override void HoverExit()
    {
        base.HoverExit();
        if (isSelected)
        {
            imageComponent.color = selectedColor;
        }
        else
        {
            imageComponent.color = defaultColor;
        }
    }
    public override void Select()
    {
        isSelected = !isSelected;
        if (isSelected)
        {
            imageComponent.color = selectedColor;
        }
        else
        {
            imageComponent.color = defaultColor;
        }
        onToggle.Invoke(isSelected);
        if(toggleObject != null)
            toggleObject.gameObject.SetActive(isSelected);
    }
    public override void Deselect()
    {
        if (isSelected)
            return;

        imageComponent.color = defaultColor;
        base.Deselect();
    }
}
