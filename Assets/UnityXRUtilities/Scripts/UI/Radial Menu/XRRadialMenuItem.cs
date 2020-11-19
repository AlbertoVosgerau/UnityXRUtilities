using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
/// <summary>
/// Base Menu item for Radial Menus
/// </summary>
public class XRRadialMenuItem : MonoBehaviour
{
    [SerializeField] protected Color hoverColor = Color.gray;
    [SerializeField] protected Color selectedColor = Color.white;

    protected Image imageComponent;
    protected Color defaultColor = Color.white;

    public UnityEvent onHoverEnter;
    public UnityEvent onHoverExit;
    public UnityEvent onSelect;
    public UnityEvent onDeselect;
    public virtual void Awake()
    {
        imageComponent = GetComponent<Image>();
        defaultColor = imageComponent.color;
    }

    public virtual void HoverEnter()
    {
        onHoverEnter.Invoke();
    }

    public virtual void HoverExit()
    {
        onHoverExit.Invoke();
    }

    public virtual void Select()
    {
        onSelect.Invoke();
    }

    public virtual void Deselect()
    {
        onDeselect.Invoke();
    }
}
