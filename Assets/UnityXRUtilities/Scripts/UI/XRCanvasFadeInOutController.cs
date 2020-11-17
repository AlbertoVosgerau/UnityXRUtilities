using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class XRCanvasFadeInOutController : MonoBehaviour
{
    public float duration = 0.2f;
    public float idleTime = 0.1f;
    public CanvasGroup canvasGroup;

    public UnityEvent onFadeIn;
    public UnityEvent onFadeOut;

    public void FadeInOut()
    {
        StartCoroutine(FadeInOutRoutine());
    }

    private IEnumerator FadeInOutRoutine()
    {
        float time = 0;
        onFadeIn.Invoke();
        do
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 1, time / (duration/2));
            time += Time.deltaTime;
            yield return null;
        }
        while (time < duration/2);

        time = 0;

        yield return new WaitForSecondsRealtime(idleTime);

        do
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 0, time / (duration /2));
            time += Time.deltaTime;
            yield return null;
        }
        while (time < duration/2);
        onFadeOut.Invoke();
    }
}
