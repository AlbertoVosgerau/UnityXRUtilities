using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Add this to each item of a radial menu
/// </summary>
public class XRRadialMenuItem : MonoBehaviour
{
    public bool IsSelected { get; private set; }
    public bool isToggle;

    [SerializeField] private Color hoverColor = Color.gray;
    [SerializeField] private Color selectedColor = Color.white;

    private Image imageComponent;
    private Color defaultColor = Color.white;

    public UnityEvent onHoverEnter;
    public UnityEvent onHoverExit;
    public UnityEvent onSelect;
    public UnityEvent onDeselect;
    public UnityEvent<bool> onToggle;
    private void Awake()
    {
        imageComponent = GetComponent<Image>();
        defaultColor = imageComponent.color;
    }

    public void HoverEnter()
    {
        imageComponent.color = hoverColor;
        onHoverEnter.Invoke();
    }

    public void HoverExit()
    {
        onHoverExit.Invoke();
        if(isToggle)
        {
            if(IsSelected)
            {
                imageComponent.color = selectedColor;
            }
            else
            {
                imageComponent.color = defaultColor;
            }
        }
        else
        {
            imageComponent.color = defaultColor;
        }
    }

    public void Select()
    {
        IsSelected = isToggle? !IsSelected : true;

        if(isToggle)
        {
            Toggle();
            return;
        }
        imageComponent.color = selectedColor;
        onSelect.Invoke();
    }

    public void Deselect()
    {
        if (isToggle && IsSelected)
            return;

        imageComponent.color = defaultColor;
        onDeselect.Invoke();
    }


    public void Toggle()
    {
        if (!isToggle)
            return;

        onToggle.Invoke(IsSelected);
        if (!IsSelected)
        {
            Deselect();
        }
        else
        {
            imageComponent.color = selectedColor;
            onSelect.Invoke();
        }
    }
}
