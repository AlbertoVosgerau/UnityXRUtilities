using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Instantiates hand model into the scene and puts it inside the corresponding hand
/// </summary>
public class HandModelCreator : MonoBehaviour
{
    public bool showHandsOnCreation = true;
    [SerializeField] private GameObject leftHandPrefab;
    [SerializeField] private GameObject rightHandPrefab;
    [SerializeField] private XRController leftController;
    [SerializeField] private XRController rightController;

    public UnityEvent<GameObject> onHandCreated;

    private void Start()
    {
        CreateHands();
    }
    public void CreateHands()
    {
        CreateHand(out GameObject leftHand, leftHandPrefab, leftController);
        CreateHand(out GameObject rightHand, rightHandPrefab, rightController);

        if (!showHandsOnCreation)
        {
            leftHand?.SetActive(false);
            rightHand?.SetActive(false);
        }
    }
    private void CreateHand(out GameObject newHand, GameObject prefab, XRController controller)
    {
        Transform[] children = controller.GetComponentsInChildren<Transform>();

        for (int i = 0; i < children.Length; i++)
        {
            if (children[i].name == "hand")
            {
                newHand = null;
                return;
            }
        }

        newHand = Instantiate(prefab, controller.transform);
        newHand.name = "hand";
        onHandCreated.Invoke(newHand);
    }
}
