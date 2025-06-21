using System.Collections;
using UnityEngine;

public class DenaInteraction : NPC
{
    public NPCDialogue mayorFirst;
    //public NPCDialogue denaFlontInteraction;
    public GameObject exitButton;
    public bool startInteract, dialogueFinished, mayorDialogue;
    public float xOffset = 2.82031f, yOffset = -5.09192f;

    public override bool CanInteract()
    {
        return startInteract || !isDialogueActive;
    }
    public void Start()
    {
        exitButton.SetActive(false);
        startInteract = true;
        Interact();
    }

    public IEnumerator Teleport()
    {
        GameManager.Instance.FadeInSceneTransition();
        yield return new WaitForSeconds(2.3f);
        //Dena teleporting
        transform.position = new Vector3(15.81f - xOffset, 2.05f - yOffset, 0f);
        GameManager.Instance.FadeOutSceneTransition();
        dialogueFinished = false;
    }
    public override void Interact()
    {
        if (dialogueFinished || (GameManager.isFading && !startInteract) || dialogueData == null || (PauseController.IsGamePaused && !isDialogueActive))
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

    protected override void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            //Completes the line so it's fully on display
            dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        else if (++dialogueIndex < dialogueData.dialogueLines.Length)
        {
            //If another line, type next line
            StartCoroutine(TypeLine());
        }
        else
        {
            startInteract = false;
            EndDialogue();
            exitButton.SetActive(true);
            if (!mayorDialogue)
            {
                dialogueFinished = true;
                StartCoroutine(Teleport());
            }
            dialogueData = mayorFirst;
            mayorDialogue = true;
        }
    }



}
