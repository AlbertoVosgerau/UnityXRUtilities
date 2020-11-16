using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class XRButtonInteractor : MonoBehaviour
{
    [SerializeField] private Transform buttonMesh;
    [SerializeField] private Vector3 onPressedLocalPosition;

    private Vector3 originalLocalPosition;

    public UnityEvent onButtonPressed;
    public UnityEvent onButtonReleased;

    private void Awake()
    {
        originalLocalPosition = buttonMesh.transform.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Hands"))
            return;

        onButtonPressed.Invoke();

        if (buttonMesh == null)
            return;

        StartCoroutine(UpdateTransform(onPressedLocalPosition));
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Hands"))
            return;

        onButtonReleased.Invoke();

        if (buttonMesh == null)
            return;

        StartCoroutine(UpdateTransform(originalLocalPosition));
    }

    private IEnumerator UpdateTransform(Vector3 targetPosition)
    {
        float time = 0;
        Debug.Log("Time zero");
        do
        {
            buttonMesh.localPosition = Vector3.Lerp(buttonMesh.localPosition, targetPosition, time / 0.5f);
            Debug.Log($"Pos: {buttonMesh.localPosition}");
            time += Time.deltaTime;
            Debug.Log("Time");
            yield return null;
        }
        while (time < 0.5f);
    }
}
