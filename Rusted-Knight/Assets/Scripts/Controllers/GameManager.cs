using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static bool isFading;
    [SerializeField] private GameObject _fadeInSceneTransition;
    [SerializeField] private GameObject _fadeOutSceneTransition;

    private void Awake()
    {
        // Ensure only one GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        FadeOutSceneTransition();
    }

    public IEnumerator DisableFadeInSceneTransition(float time)
    {
        yield return new WaitForSeconds(time);
        _fadeInSceneTransition.SetActive(false);
        isFading = false;
    }

    public IEnumerator DisableFadeOutSceneTransition(float time)
    {
        yield return new WaitForSeconds(time);
        _fadeOutSceneTransition.SetActive(false);
        isFading = false;
    }

    public void FadeInSceneTransition()
    {
        _fadeInSceneTransition.SetActive(true);
        isFading = true;
        StartCoroutine(DisableFadeInSceneTransition(2.3f));
    }

    public void FadeOutSceneTransition()
    {
        _fadeOutSceneTransition.SetActive(true);
        isFading = true;
        StartCoroutine(DisableFadeOutSceneTransition(2.3f));
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
