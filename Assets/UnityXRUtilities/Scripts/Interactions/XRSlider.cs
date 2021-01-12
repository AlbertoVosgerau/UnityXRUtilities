using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSlider : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Transform _handler;
    [SerializeField] private Transform _upLimit;
    [SerializeField] private Transform _downLimit;
    [SerializeField] private MyFloatEvent _onValueChange;
    [SerializeField] private MyFloatEvent _onValueChangeEnd;

    private float _currentValue;
    private float _maxDistance;
    private void Start()
    {
        _maxDistance = Vector3.Distance(_downLimit.position, _upLimit.position);
        _currentValue = GetCurrentValue();

        if (_handler.gameObject.TryGetComponent(out XRBaseInteractable interactable))
        {
            interactable.onDeactivate.AddListener(EndGrab);
        }

        if (_text != null)
            _text.text = (_currentValue * 100).ToString("N0");
    }

    private void EndGrab(XRBaseInteractor interactor)
    {
        _onValueChangeEnd.Invoke(_currentValue);
    }

    void Update()
    {
        _handler.position = VectorUtils.ClampPoint(_handler.position, _upLimit.position, _downLimit.position);
        var newValue = GetCurrentValue();
        if (_currentValue != newValue)
        {
            _currentValue = newValue;
            _onValueChange.Invoke(_currentValue);
            if (_text != null)
                _text.text = (_currentValue * 100).ToString("N0");
        }
    }

    private float GetCurrentValue()
    {
        return Vector3.Distance(_downLimit.position, _handler.position) / _maxDistance;
    }
}

[System.Serializable]
public class MyFloatEvent : UnityEvent<float>
{
}