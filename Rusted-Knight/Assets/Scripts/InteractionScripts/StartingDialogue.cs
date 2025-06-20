using UnityEngine;
using System.Collections;

public class StartingDialogue : NPC
{
    bool sceneChange;
    bool dialogueFinished;
    bool firstDialogue;
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
        firstDialogue = true;
        Interact();
    }
    public override void Interact()
    {
        if ((GameManager.isFading && !firstDialogue)|| dialogueFinished || dialogueData == null || (PauseController.IsGamePaused && !isDialogueActive))
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

    protected override void StartDialogue()
    {
        firstDialogue = false;
        isDialogueActive = true;
        dialogueIndex = 0;

        nameText.SetText(dialogueData.npcName);

        dialoguePanel.SetActive(true);
        if (gameObject.transform.CompareTag("Interactable"))
        {
            DialogueHelper.ShowOnlyDialogue();
        }
        PauseController.SetPause(true);

        StartCoroutine(TypeLine());
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
