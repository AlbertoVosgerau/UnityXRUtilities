using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Implements and easy way to interact with buttons.
/// onPressedLocalPosition will define the local position of the button after pressed. No need for animation
/// To use it, just grab the actual button collider to the colliders list and you are good to go
/// </summary>

[RequireComponent(typeof(Rigidbody))]
public class XRButtonInteractable : XRSimpleInteractable
{
    [Header("Button properties")]
    public float duration = 0.5f;

    [SerializeField] private Transform buttonMesh;
    [SerializeField] private Vector3 onPressedLocalPosition;

    public UnityEvent onButtonPressed;
    public UnityEvent onButtonReleased;

    private Vector3 originalLocalPosition;
    private void Start()
    {
        originalLocalPosition = buttonMesh.transform.localPosition;
    }
    private void OnEnable()
    {
        onHoverEntered.AddListener(OnHoverEnter);
        onHoverExited.AddListener(OnHoverExit);
    }
    private void OnDisable()
    {
        onHoverEntered.RemoveListener(OnHoverEnter);
        onHoverExited.RemoveListener(OnHoverExit);
    }
    private void OnHoverEnter(XRBaseInteractor xrBaseInteractor)
    {
        onButtonPressed.Invoke();
        StartCoroutine(UpdateTransform(onPressedLocalPosition));
    }
    private void OnHoverExit(XRBaseInteractor xrBaseInteractor)
    {
        onButtonReleased.Invoke();
        StartCoroutine(UpdateTransform(originalLocalPosition));
    }
    private IEnumerator UpdateTransform(Vector3 targetPosition)
    {
        float time = 0;
        Debug.Log("Time zero");
        do
        {
            buttonMesh.localPosition = Vector3.Lerp(buttonMesh.localPosition, targetPosition, time / duration);
            Debug.Log($"Pos: {buttonMesh.localPosition}");
            time += Time.deltaTime;
            Debug.Log("Time");
            yield return null;
        }
        while (time < duration);
    }
}
