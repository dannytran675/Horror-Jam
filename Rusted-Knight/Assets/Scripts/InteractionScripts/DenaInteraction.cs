using System.Collections;
using UnityEngine;

public class DenaInteraction : NPC
{
    public NPCDialogue mayorFirst, denaFlontInteraction;
    public GameObject exitButton, flont;
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

    public IEnumerator FlontAppearance()
    {
        GameManager.Instance.FadeInSceneTransition();
        yield return new WaitForSeconds(2.3f);
        //Flont Appearing
        flont.SetActive(true);
        GameManager.Instance.FadeOutSceneTransition();
    }
    public override void Interact()
    {
        if (MayorInteraction.doneMayorInteraction && dialogueData == mayorFirst)
        {
            dialogueData = denaFlontInteraction;
            exitButton.SetActive(false);
        }
        if (dialogueFinished || (GameManager.isFading && !startInteract) || dialogueData == null || (PauseController.IsGamePaused && !isDialogueActive))
        {
            return;
        }

        if (isDialogueActive)
        {
            CheckName();
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
                dialogueData = mayorFirst;
                mayorDialogue = true;
            }
            if (dialogueData == denaFlontInteraction)
            {
                dialogueFinished = true;
                GateInteraction.gateOpenable = true;
                exitButton.SetActive(true);
            }
        }
    }

    public void CheckName()
    {
        if (!isTyping)
        {
            switch (dialogueData.dialogueLines[dialogueIndex])
            {
                case "With the power of the holy spirit by my side, defeating the Demon King will be easy!":
                    StartCoroutine(FlontAppearance());
                    nameText.SetText("Flont");
                    break;
                case "Flont! Yup, I’m going to accompany this knight here on his journey to kill the demon king!":
                    nameText.SetText("Flont");
                    break;
                case "I- I’ll come with-":
                    nameText.SetText("Flont");
                    break;
                case "No, you won’t! We’re going to be battling many dangerous monsters, and you’re terrified of blood!":
                    nameText.SetText("Flont");
                    break;
                case "I, I don’t care.":
                    nameText.SetText("Flont");
                    break;
                case "Fine! But only if you listen to us at all times!":
                    nameText.SetText("Flont");
                    break;
                default:
                    nameText.SetText("Dena");
                    break;
            }
        }
    }



}
