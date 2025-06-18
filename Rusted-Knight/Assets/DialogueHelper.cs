using UnityEngine;

public class DialogueHelper : MonoBehaviour
{
    //Used to created a public instance of DialogueHelper
    public static DialogueHelper Instance { get; private set; }


    public GameObject dialogueObjects;
    public GameObject answerObjects;

    void Awake()
    {
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

    public static void ShowOnlyDialogue()
    {
        if (Instance != null)
        {
            Instance.dialogueObjects.SetActive(true);
            Instance.answerObjects.SetActive(false);
        }
    }

    public static void ShowOnlyQuestion()
    {
        if (Instance != null)
        {
            Instance.dialogueObjects.SetActive(false);
            Instance.answerObjects.SetActive(true);
        }
    }
}
