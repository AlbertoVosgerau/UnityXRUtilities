using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class XRFade : MonoBehaviour
{
    [SerializeField] private float duration = 0.2f;
    [SerializeField] private float idleTime = 0.1f;
    [SerializeField] private float offsetFromCameraNearClip = 0.005f;
    [Tooltip("Optional parent for the created fade")]
    [SerializeField] private Transform fadeParent;

    [SerializeField] private List<GameObject> objectsToHideOnFade;

    private Canvas canvas;
    private CanvasGroup canvasGroup;

    public UnityEvent onFadeIn;
    public UnityEvent onWaitStart;
    public UnityEvent onWaitFinish;
    public UnityEvent onFadeOut;

    private void Update()
    {
        if (canvas.worldCamera.gameObject.activeInHierarchy)
            return;
    }

    private void UpdateCanvasCamera()
    {

    }

    public void CreateCanvas(float defaultAlpha)
    {
        Camera camera = FindObjectOfType<Camera>();

        if (camera == null)
            return;

        GameObject canvasObject = new GameObject("Fade");
        canvas = canvasObject.AddComponent<Canvas>();

        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = camera;
        canvas.planeDistance = camera.nearClipPlane + offsetFromCameraNearClip;

        canvasGroup = canvas.gameObject.AddComponent<CanvasGroup>();
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = defaultAlpha;

        RectTransform canvasTransform = canvas.GetComponent<RectTransform>();
        canvasTransform.localScale = new Vector3(100, 100, 100);
        GameObject newimage = new GameObject("Image");

        RectTransform imageTransform = newimage.AddComponent<RectTransform>();
        imageTransform.SetParent(canvasTransform);
        imageTransform.localScale = new Vector3(10, 10, 10);

        Image image = imageTransform.gameObject.AddComponent<Image>();
        image.color = Color.black;
        imageTransform.anchorMin = new Vector2(0, 0);
        imageTransform.anchorMax = new Vector2(1, 1);
        imageTransform.pivot = new Vector2(0.5f, 0.5f);

        if (fadeParent != null)
            canvas.transform.parent = fadeParent;
    }
    public void FadeInOut()
    {
        if(!CameraIsValid())
        {
            if(canvas != null)
            {
                Destroy(canvas.gameObject);
            }
            CreateCanvas(0);
        }
        StartCoroutine(FadeInOutRoutine());
    }

    public void FadeOut()
    {
        if (!CameraIsValid())
        {
            if (canvas != null)
            {
                Destroy(canvas.gameObject);
            }
            CreateCanvas(1);
        }
        StartCoroutine(FadeOutRoutine());
    }

    private IEnumerator FadeOutRoutine()
    {
        List<bool> objbectVisibilityStates = new List<bool>();
        canvasGroup.alpha = 1;

        for (int i = 0; i < objectsToHideOnFade.Count; i++)
        {
            objbectVisibilityStates.Add(objectsToHideOnFade[i].activeSelf);
            objectsToHideOnFade[i].SetActive(false);
        }
        float time = 0;

        onWaitStart.Invoke();

        yield return new WaitForSecondsRealtime(idleTime);
        onWaitFinish.Invoke();

        for (int i = 0; i < objectsToHideOnFade.Count; i++)
        {
            objectsToHideOnFade[i].SetActive(objbectVisibilityStates[i]);
        }

        do
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 0, time / (duration / 2));
            time += Time.deltaTime;
            yield return null;
        }
        while (time < duration / 2);
        onFadeOut.Invoke();
    }

    private IEnumerator FadeInOutRoutine()
    {
        List<bool> objbectVisibilityStates = new List<bool>();

        for (int i = 0; i < objectsToHideOnFade.Count; i++)
        {
            objbectVisibilityStates.Add(objectsToHideOnFade[i].activeSelf);
            objectsToHideOnFade[i].SetActive(false);
        }
        float time = 0;

        onFadeIn.Invoke();

        do
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 1, time / (duration / 2));
            time += Time.deltaTime;
            yield return null;
        }
        while (time < duration / 2);

        onWaitStart.Invoke();
        time = 0;
        
        yield return new WaitForSecondsRealtime(idleTime);
        onWaitFinish.Invoke();

        for (int i = 0; i < objectsToHideOnFade.Count; i++)
        {
            objectsToHideOnFade[i].SetActive(objbectVisibilityStates[i]);
        }

        do
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 0, time / (duration / 2));
            time += Time.deltaTime;
            yield return null;
        }
        while (time < duration / 2);
        onFadeOut.Invoke();
    }

    private bool CameraIsValid()
    {
        if (canvas == null)
            return false;
        if (canvas.worldCamera == null)
            return false;
        if(!canvas.worldCamera.gameObject.activeInHierarchy)
        {
            Camera newCam = Camera.FindObjectOfType<Camera>();
            canvas.worldCamera = newCam;
            return true;
        }

        return true;
    }
}
