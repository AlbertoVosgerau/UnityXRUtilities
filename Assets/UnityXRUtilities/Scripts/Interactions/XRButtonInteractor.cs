using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class XRButtonInteractor : MonoBehaviour
{
    public UnityEvent onButtonPressed;
    public UnityEvent onButtonReleased;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Hands"))
            return;

        onButtonPressed.Invoke();
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Hands"))
            return;

        onButtonReleased.Invoke();
    }
}
