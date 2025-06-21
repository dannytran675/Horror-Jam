using System.Collections;
using UnityEngine;

public class MayorInteraction : NPC
{
    public NPCDialogue talkToDena;
    public static bool doneMayorInteraction;
    public override void Interact()
    {
        if (GameManager.isFading || dialogueData == null || (PauseController.IsGamePaused && !isDialogueActive))
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
            EndDialogue();
            doneMayorInteraction = true;
            dialogueData = talkToDena;
        }
    }

}
