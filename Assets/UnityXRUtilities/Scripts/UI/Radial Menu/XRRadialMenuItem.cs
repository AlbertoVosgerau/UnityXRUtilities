using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class XRRadialMenuItem : MonoBehaviour
{
    [SerializeField] private Color hoverColor = Color.gray;
    [SerializeField] private Color selectedColor = Color.white;


    private Image imageComponent;
    private Color defaultColor = Color.white;
    private bool isSelected;

    public UnityEvent onHoverEnter;
    public UnityEvent onHoverExit;
    public UnityEvent onSelect;
    public UnityEvent onDeselect;
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
        imageComponent.color = defaultColor;
        onHoverExit.Invoke();
    }

    public void Select()
    {
        imageComponent.color = selectedColor;
        onSelect.Invoke();
    }

    public void Deselect()
    {
        imageComponent.color = defaultColor;
        onDeselect.Invoke();
    }
}
