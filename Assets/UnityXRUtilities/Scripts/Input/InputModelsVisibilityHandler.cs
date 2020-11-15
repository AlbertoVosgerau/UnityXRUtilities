﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
