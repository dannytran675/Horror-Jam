using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;


public class GateInteraction : NPC
{
    public NPCDialogue gateOpeningDialogue;
    public bool gateOpenable;
    public GameObject answerObjects;
    public GameObject exitButton;


    protected override void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;

        nameText.SetText(dialogueData.npcName);

        dialoguePanel.SetActive(true);
        PauseController.SetPause(true);

        StartCoroutine(TypeLine());
    }

    public void NoInteraction()
    {
        //Display text saying "I will be waiting"
        //End dialogue
    }

    public void YesInteraction()
    {
        //Display text saying "Good luck"
        //End dialogue
        //Fade in
        //Glitch
        //Switch scenes
        //Fade out
    }
}
