using UnityEngine;
using System.Collections;

public class StartingDialogue : NPC
{
    bool sceneChange;
    bool dialogueFinished;
    void Update()
    {
        if (isInteractKey())
        {
            Interact();
        }
        if (sceneChange)
        {
            GameManager.Instance.LoadScene();
            GameManager.Instance.FadeOutSceneTransition();
        }
    }

    void Start()
    {
        Interact();
    }
    public override void Interact()
    {
        if (dialogueFinished || dialogueData == null || (PauseController.IsGamePaused && !isDialogueActive))
        {
            return;
        }

        if (isDialogueActive)
        {
            NextLine();
        }
        else
        {
            StartDialogue();
        }
    }
    bool isInteractKey()
    {
        bool isInteractKey = Input.GetKeyUp("return") || Input.GetKeyUp("space");
        isInteractKey = isInteractKey || Input.GetKeyUp("e") || Input.GetKeyUp("z");
        return isInteractKey;
    }

    public override void EndDialogue()
    {
        //Stopping all 'threads' of text typing
        StopAllCoroutines();
        dialogueFinished = true;
        //End of dialogue
        isDialogueActive = false;
        isTyping = false;
        //Resetting text
        dialogueText.SetText("");
        //Hides dialogue box
        dialoguePanel.SetActive(false);
        //Unpauses game
        PauseController.SetPause(false);
        GameManager.Instance.FadeInSceneTransition();
        StartCoroutine(SceneLoadingDelay(2.3f));
    }

    public IEnumerator SceneLoadingDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        sceneChange = true;
    }
}
