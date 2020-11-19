using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Handles input visual models visibility when using XR Controller. It will enable or disable controller, or hand model.
/// User this script only if you have hands and controllers in the same scene and want to easily toggle between them.
/// </summary>
public class InputModelsVisibilityHandler : MonoBehaviour
{
    [SerializeField] private bool showControllersByDefault = true;
    [SerializeField] private DeviceControllerModelCreator deviceControllerCreator;
    [SerializeField] private HandModelCreator handCreator;

    private List<GameObject> controllers = new List<GameObject>();
    private List<GameObject> hands = new List<GameObject>();
    private void Awake()
    {
        deviceControllerCreator.showControllersOnCreation = showControllersByDefault;
        handCreator.showHandsOnCreation = !showControllersByDefault;
    }

    private void OnEnable()
    {
        deviceControllerCreator.onControllerCreated.AddListener(OnControllerCreated);
        handCreator.onHandCreated.AddListener(OnHandCreated);
    }
    private void OnDisable()
    {
        deviceControllerCreator.onControllerCreated.RemoveListener(OnControllerCreated);
        handCreator.onHandCreated.RemoveListener(OnHandCreated);
    }
    private void OnControllerCreated(GameObject controller)
    {
        controllers.Add(controller);
    }
    private void OnHandCreated(GameObject hand)
    {
        hands.Add(hand);
    }
    public void ShowControllers()
    {
        deviceControllerCreator.showControllersOnCreation = true;
        handCreator.showHandsOnCreation = false;

        foreach (var controller in controllers)
        {
            controller.SetActive(true);
        }

        foreach (var hand in hands)
        {
            hand.SetActive(false);
        }
    }
    public void ShowHands()
    {
        deviceControllerCreator.showControllersOnCreation = false;
        handCreator.showHandsOnCreation = true;

        foreach (var controller in controllers)
        {
            controller.SetActive(false);
        }

        foreach (var hand in hands)
        {
            hand.SetActive(true);
        }
    }
}

[CustomEditor(typeof(InputModelsVisibilityHandler))]
public class InputModelsVisibilityHandlerEditor : Editor
{
    InputModelsVisibilityHandler visibilityHandler;
    private void OnEnable()
    {
        visibilityHandler = target as InputModelsVisibilityHandler;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Show Controllers"))
        {
            if(Application.isPlaying)
                visibilityHandler.ShowControllers();
        }
        if (GUILayout.Button("Show Hands"))
        {
            if (Application.isPlaying)
                visibilityHandler.ShowHands();
        }
    }
}
