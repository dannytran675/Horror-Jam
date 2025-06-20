using UnityEngine;
using System.Collections;

public class SaveFileButton : MonoBehaviour
{
    bool pressed;

    public void ButtonPress()
    {
        if (!GameManager.isFading && !pressed)
        {
            StartCoroutine(SaveFileButtonAction());
        }
    }

    private IEnumerator SaveFileButtonAction()
    {
        pressed = true;
        GameManager.Instance.FadeInSceneTransition();
        yield return new WaitForSeconds(2.2f);
        GameManager.Instance.LoadScene();
        GameManager.Instance.FadeOutSceneTransition();
    }
}
