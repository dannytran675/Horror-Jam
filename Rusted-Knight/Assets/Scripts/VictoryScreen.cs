using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    bool finalScene;
    [SerializeField] private CharacterInfo DemonKing;
    // Update is called once per frame
    // void Update()
    // {
    //     if (DemonKing.hp == 0 && !finalScene)
    //     {
    //         finalScene = true;
    //         GameManager.Instance.FadeInSceneTransition();
    //         StartCoroutine(SceneSwitch(2.2f));
    //     }
    // }

    // IEnumerator SceneSwitch(float delay)
    // {
    //     yield return new WaitForSeconds(delay);
    //     GameManager.Instance.FadeOutSceneTransition();
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    // }
}
