using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
public class SceneController : MonoBehaviour
{

    public static SceneController instance;
    public GameObject SceneTransition;
    [SerializeField] Animator transitionAnim;
    //Upon game loading, SceneController creates and instance of itself so it can run the methods
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //Ensures SceneController does not delete upon loading a new scene(Allows for re-use)
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Destroys any existing SceneController that isn't this one
            Destroy(gameObject);
        }
    }

    
    public void NextScene()
    {
        StartCoroutine(LoadScene());
    }

    //Loads next scene in the build profile (File>Build Profiles)
    public IEnumerator LoadScene()
    {
        SceneTransition.SetActive(true);
        transitionAnim.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        transitionAnim.SetTrigger("FadeOut");
        SceneTransition.SetActive(false);
    }
}
