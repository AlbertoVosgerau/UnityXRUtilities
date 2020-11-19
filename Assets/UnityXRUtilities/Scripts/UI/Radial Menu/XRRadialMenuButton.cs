using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Add this to each item of a radial menu
/// </summary>
public class XRRadialMenuButton : XRRadialMenuItem
{
    public override void HoverEnter()
    {
        imageComponent.color = hoverColor;
        base.HoverEnter();
    }

    public override void HoverExit()
    {
        imageComponent.color = defaultColor;
        base.HoverExit();
    }

    public override void Select()
    {
        imageComponent.color = selectedColor;
        base.Select();
    }

    public override void Deselect()
    {
        imageComponent.color = defaultColor;
        base.Deselect();
    }
}
