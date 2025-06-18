using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public static SceneController instance;

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

    //Loads next scene in the build profile (File>Build Profiles)
    public void NextScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Loads scene by scene name
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
