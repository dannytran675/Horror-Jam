using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public void Load(string scene)
    {
        StartCoroutine(LoadAsync(scene));
    }


    private IEnumerator LoadAsync(string scene)
    {
        //Creates a scene loader that loads the scenes in the background rather than halting the game boot up
        AsyncOperation aop = SceneManager.LoadSceneAsync(scene);

        //Disables scene activation until nearly loaded
        aop.allowSceneActivation = false;

        //Fade in
        while (aop.progress < 0.9f)
        {
            yield return null;
        }

        //Fade out

        //Once it's near 90% loaded scene activation is re-enabled
        aop.allowSceneActivation = true;

    }
}
