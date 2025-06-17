using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;


public class GateInteraction : NPC
{
    public NPCDialogue gateOpeningDialogue;
    public bool gateOpenable, gateOpened, finalMessageDisplayed;
    public GameObject answerObjects;
    public TMP_Text questionText;
    public GameObject dialogueObjects;
    public GameObject answerButtons;

    public override void Interact()
    {
        if (finalMessageDisplayed || dialogueData == null || (PauseController.IsGamePaused && !isDialogueActive))
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
        isDialogueActive = true;
        dialogueIndex = 0;

        nameText.SetText(dialogueData.npcName);

        dialoguePanel.SetActive(true);
        PauseController.SetPause(true);

        if (gateOpenable)
        {
            //Switch to display the yes/no options
            answerObjects.SetActive(true);
            dialogueObjects.SetActive(false);
        }

        StartCoroutine(TypeLine());
    }

    protected override void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            //Completes the line so it's fully on display
            if (gateOpenable)
            {
                questionText.SetText(gateOpeningDialogue.dialogueLines[dialogueIndex]);
            }
            else
            {
                dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
            }
            isTyping = false;
        }
        else if (!gateOpenable && ++dialogueIndex < dialogueData.dialogueLines.Length)
        {
            //If another line, type next line
            StartCoroutine(TypeLine());
        }
        else if (gateOpenable && ++dialogueIndex < gateOpeningDialogue.dialogueLines.Length - 1)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }
    protected override IEnumerator TypeLine()
    {
        isTyping = true;

        if (gateOpenable)
        {
            questionText.SetText("");

            foreach (char letter in gateOpeningDialogue.dialogueLines[dialogueIndex])
            {
                questionText.text += letter;
                yield return new WaitForSeconds(gateOpeningDialogue.typingSpeed);
            }
        }
        else
        {
            dialogueText.SetText("");

            foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(dialogueData.typingSpeed);
            }
        }

        isTyping = false;

        //Once at end of dialogue display options

        //Auto progress code
        // if (dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        // {
        //     yield return new WaitForSeconds(dialogueData.autoProgressDelay);
        //     NextLine();
        // }
    }

    public override void EndDialogue()
    {
        //Stopping all 'threads' of text typing
        StopAllCoroutines();
        //End of dialogue
        isDialogueActive = false;
        isTyping = false;
        //Resetting text
        dialogueText.SetText("");
        if (gateOpenable)
        {
            questionText.SetText("");
        }
        //Hides dialogue box
        dialoguePanel.SetActive(false);
        //Unpauses game
        PauseController.SetPause(false);
    }

    public IEnumerator FinalMessage(string finalMessage, bool yes)
    {
        //Prevent any interaction during this phase
        finalMessageDisplayed = true; //first if-statement in Interact()
        //Close the answer buttons
        answerButtons.SetActive(false);
        //Type out the final message
        isTyping = true;

        if (gateOpenable)
        {
            questionText.SetText("");

            foreach (char letter in finalMessage)
            {
                questionText.text += letter;
                yield return new WaitForSeconds(gateOpeningDialogue.typingSpeed);
            }
        }

        isTyping = false;

        //Wait a second then close the box
        yield return new WaitForSeconds(1);
        EndDialogue();
        if (!yes)
        {
            //Bringing back the answer buttons for when you want to answer again
            answerButtons.SetActive(true);
        }
        finalMessageDisplayed = false;
    }


    public void NoInteraction()
    {
        StopAllCoroutines();
        //Display text saying "I will be waiting"
        //End dialogue
        if (!finalMessageDisplayed)
        {
            StartCoroutine(FinalMessage("I will be waiting", false));
        }
        //StartCoroutine(FinalMessage("I will be waiting", false));
    }

    public void YesInteraction()
    {
        StopAllCoroutines();
        //Display text saying "Good luck"
        //End dialogue
        StartCoroutine(FinalMessage("Good luck!", true));
        //Fade in
        //Glitch
        //Switch scenes
        //Fade out
    }
}
