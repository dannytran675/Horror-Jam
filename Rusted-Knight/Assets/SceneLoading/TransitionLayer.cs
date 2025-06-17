using UnityEngine;
using System;

public abstract class TransitionLayer : MonoBehaviour
{
    public float Progress { get; protected set; }
    public float IsDone { get; protected set; }
    public Action OnComplete { get; protected set; }
    public abstract void Show(float time, float delay);
    public abstract void ShowImmediately();
    public abstract void Hide(float time, float delay);
    public abstract void HideImmediately();

    public void InvokeAndClearCallBack()
    {
        Action callback = OnComplete;
        OnComplete = null;
        callback?.Invoke();
    }
}
