using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private CharacterInfo[] battlers = new CharacterInfo[3];

    Knight rustedKnight;
    Cleric dena;

    [SerializeField] private Boss demonKing;
    [SerializeField] private TMP_Text[] HPTexts = new TMP_Text[3];
    [SerializeField] private TMP_Text bossHPText;

    //For all cooldowns or percentages
    [SerializeField] private TMP_Text[] CDTexts = new TMP_Text[9];

    //Buttons for each player's moves
    [SerializeField] private Button[] moveButtons = new Button[9];
    [SerializeField] private Button[] characterSelectButtons = new Button[3];
    [SerializeField] private Button endTurnButton;

    //Belief text
    [SerializeField] private TMP_Text beliefText;

    bool playerAttacked;


    void Start()
    {
        rustedKnight = battlers[0] as Knight;
        dena = battlers[1] as Cleric;

        DisplayHPs();

        beliefText.SetText($"Belief: {dena.belief}%");

        //Knight Cooldowns
        DisplayKnightCD();

        //Dena Costs
        DisplayDenaCosts();

        //Flont all set to 0 since it costs HP
        CDTexts[6].SetText("[0]");
        CDTexts[7].SetText("[0]");
        CDTexts[8].SetText("[0]");

        DisableAllButtons();
        //Adding functionality to end turn button
        endTurnButton.onClick.AddListener(() => playerAttacked = true);

        print("Welcome to the battle sequence!");
        StartCoroutine(BattleSequence());

    }

    //Battle!
    public IEnumerator BattleSequence()
    {
        yield return new WaitForSeconds(2);
        print("Please click on the moves you want each character to perfom.");
        yield return new WaitForSeconds(2);
        print("To confirm the actions, please press the end turn button.");
        yield return new WaitForSeconds(2);

        //Battle loop
        while (canBattle())
        {

            //Player turn
            print(ColourText.BlueString("Player turn"));
            EnableAllButtons();
            yield return new WaitUntil(() => playerAttacked);


            //Boss turn
            print(ColourText.RedString("Boss turn"));
            DisableAllButtons();
            yield return new WaitForSeconds(2);

            //Switching turns;
            playerAttacked = false;

        }


    }

    public bool canBattle()
    {
        bool allDowned = battlers[0].downed && battlers[1].downed && battlers[2].downed;

        bool bossDowned = demonKing.downed;

        bool canBattle = !allDowned && !bossDowned;

        return canBattle;
    }

    //Button interactions














    //Visual Methods
    public void DisplayKnightCD()
    {
        //Knight Cooldowns
        CDTexts[0].SetText($"[{rustedKnight.move1CD}]");
        CDTexts[1].SetText($"[{rustedKnight.move2CD}]");
        CDTexts[2].SetText($"[{rustedKnight.move3CD}]");
    }

    public void DisplayDenaCosts()
    {
        CDTexts[3].SetText($"[{dena.beliefCost1}%]");
        CDTexts[4].SetText($"[{dena.beliefCost2}%]");
        CDTexts[5].SetText($"[{dena.beliefCost3}%]");
    }

    public void DisplayHPs()
    {
        HPTexts[0].SetText($"HP : {battlers[0].hp}");
        HPTexts[1].SetText($"HP : {battlers[1].hp}");
        HPTexts[2].SetText($"HP : {battlers[2].hp}");

        //String formatting
        string demonHPString = $"{demonKing.hp}";
        if (demonKing.hp > 999)
        {
            demonHPString = $"{demonKing.hp / 1000} " + demonHPString.Substring(demonHPString.Length - 3);
        }
        bossHPText.SetText($"HP : {demonHPString}");
    }


    public void print(string s)
    {
        TextInstantiator.Instance.AddText(s);
    }










    //Button Enabling and Disabling
    public void DisableAllButtons()
    {
        DisablePlayerButtons();
        DisableMoveButtons();
        DisableEndTurnButton();
    }

    public void DisablePlayerButtons()
    {
        for (int i = 0; i < characterSelectButtons.Length; i++)
        {
            characterSelectButtons[i].interactable = false;
        }
    }

    public void DisableMoveButtons()
    {
        for (int i = 0; i < moveButtons.Length; i++)
        {
            moveButtons[i].interactable = false;
        }
    }

    public void DisableEndTurnButton()
    {
        endTurnButton.interactable = false;
    }

    public void EnableAllButtons()
    {
        EnablePlayerButtons();
        EnableMoveButtons();
        EnableEndTurnButton();
    }

    public void EnablePlayerButtons()
    {
        for (int i = 0; i < characterSelectButtons.Length; i++)
        {
            characterSelectButtons[i].interactable = true;
        }
    }

    public void EnableMoveButtons()
    {
        for (int i = 0; i < moveButtons.Length; i++)
        {
            moveButtons[i].interactable = true;
        }
    }

    public void EnableEndTurnButton()
    {
        endTurnButton.interactable = true;
    }
    

}
