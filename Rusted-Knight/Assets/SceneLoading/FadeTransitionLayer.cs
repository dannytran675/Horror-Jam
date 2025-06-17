using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class FadeTransitionLayer : TransitionLayer
{
    private CanvasGroup cover;
    private Coroutine tween;

    public override void Show(float time, float delay)
    {

    }

    public override void ShowImmediately()
    {

    }
    public override void Hide(float time, float delay)
    {

    }
    public override void HideImmediately()
    {

    }

    void Awake()
    {
        cover = GetComponent<CanvasGroup>();
    }

    private IEnumerator ShowTween(float time, float delay)
    {
        float t = 0.0f;

        cover.alpha = 0.0f;
        yield return WaitForSecondsRealtime(delay);

        while (t < time)
        {
            t += Time.unscaledDeltaTime;
            Progress = Mathf.Clamp0(t / time);
            cover.alpha = Progress;
            yield return null;
        }

        cover.alpha = 1.0f;
        Progress = 1.0f;
        IsDone = true;
        tween = null;
        InvokeAndClearCallback();
    }
}
