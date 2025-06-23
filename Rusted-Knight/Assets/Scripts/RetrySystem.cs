using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class RetrySystem : NPC
{
    [SerializeField] private CharacterInfo Knight;
    [SerializeField] private CharacterInfo Cleric;
    [SerializeField] private CharacterInfo Flont;
    public NPCDialogue retryDialogue;
    public bool retryMessageDisplayed;
    public TMP_Text questionText;
    public GameObject answerButtons;
    // Update is called once per frame
    void Update()
    {
        if (GameManager.isFading || dialogueData == null || isDialogueActive)
        {
            return;
        }

        if (Knight.downed && Cleric.downed && Flont.downed)
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

        //Switch to display the yes/no options
        DialogueHelper.ShowOnlyQuestion();

        StartCoroutine(TypeLine());
    }

    protected override IEnumerator TypeLine()
    {
        isTyping = true;

        if (Knight.downed && Cleric.downed && Flont.downed)
        {
            questionText.SetText("");

            foreach (char letter in retryDialogue.dialogueLines[dialogueIndex])
            {
                questionText.text += letter;
                yield return new WaitForSeconds(retryDialogue.typingSpeed);
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
    }


    public IEnumerator RetryMessage(string retryMessage, bool yes)
    {
        //Prevent any interaction during this phase
        retryMessageDisplayed = true; //first if-statement in Interact()
        //Close the answer buttons
        answerButtons.SetActive(false);
        //Wait a second then close the box
        yield return new WaitForSeconds(1);
        EndDialogue();
        if (yes)
        {
            retryMessageDisplayed = true;
            GameManager.Instance.FadeInSceneTransition();
            StartCoroutine(PlayerRetry(2.2f));
        }
    }

    IEnumerator PlayerRetry(float delay)
    {
        Debug.Log("Player retried.");
        yield return new WaitForSeconds(delay);
        GameManager.Instance.FadeOutSceneTransition();
        SceneManager.LoadScene("BattleScene");
    }
    public void YesInteraction()
    {
        StopAllCoroutines();
        //Display text saying "Good luck"
        //End dialogue
        StartCoroutine(RetryMessage("Good luck!", true));
        //Fade in
        //Glitch
        //Switch scenes
        //Fade out
    }
}
