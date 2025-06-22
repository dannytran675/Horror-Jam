using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System.Threading.Tasks;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private CharacterInfo[] battlers = new CharacterInfo[3];

    Knight rustedKnight;
    Cleric dena;

    CharacterInfo denaTarget;
    CharacterInfo flontTarget;

    [SerializeField] private Boss demonKing;
    [SerializeField] private TMP_Text[] HPTexts = new TMP_Text[3];
    [SerializeField] private TMP_Text bossHPText;

    //For all cooldowns or percentages
    [SerializeField] private TMP_Text[] CDTexts = new TMP_Text[9];

    //Buttons for each player's moves
    [SerializeField] private Button[] moveButtons = new Button[9];
    [SerializeField] private TMP_Text[] buttonTexts = new TMP_Text[9];
    [SerializeField] private Button[] characterSelectButtons = new Button[3];
    [SerializeField] private Button endTurnButton;

    //Belief text
    [SerializeField] private TMP_Text beliefText;

    bool playerAttacked;

    private bool[,] movesSelected = new bool[3, 3];
    private bool singleTurnExecuted;
    private bool turnsExecuted;

    private bool awaitingPlayerSelectDena, awaitingPlayerSelectFlont;


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

            //Enable all buttons except for player selection
            EnableAllButtons();
            //Ensure moves you can't perform are being disabled
            UpdateMoveButtons();
            yield return new WaitUntil(() => playerAttacked);

            //Ensures you can't press buttons after confirming your turn is over
            DisableAllButtons();

            //remove green highlight
            ResetAllMoveButtonsText();

            print("Player moves: ");
            StartCoroutine(EndOfTurnSequence(2));
            Debug.Log(turnsExecuted);
            yield return new WaitUntil(() => turnsExecuted);
            Debug.Log("hi");
            //Deselect all buttons
            ResetAllMoveButtonSelections();

            //Boss turn
            print(ColourText.RedString("Boss turn"));

            yield return new WaitForSeconds(2);
            demonKing.BossTurn();
            yield return new WaitForSeconds(2);
            UpdateDisplay();

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
        // if (demonKing.hp > 999)
        // {
        //     demonHPString = $"{demonKing.hp / 1000} " + demonHPString.Substring(demonHPString.Length - 3);
        // }
        bossHPText.SetText($"HP : {demonHPString}");
    }

    public void UpdateDisplay()
    {
        DisplayHPs();
        DisplayDenaCosts();
        DisplayKnightCD();
        beliefText.SetText($"Belief: {dena.belief}%");
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
        DisablePlayerButtons();
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

    public void UpdateMoveButtons()
    {
        KnightButtonEnabler();
    }

    public void ResetAllMoveButtonSelections()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                movesSelected[i, j] = false;
            }
        }
    }
    public void ResetAllMoveButtonsText()
    {
        buttonTexts[0].SetText("Guardian");
        buttonTexts[1].SetText("Flurry");
        buttonTexts[2].SetText("Altrutistic Pierce");
        buttonTexts[3].SetText("Bless");
        buttonTexts[4].SetText("Surpress");
        buttonTexts[5].SetText("Necromance");
        buttonTexts[6].SetText("Coagulation");
        buttonTexts[7].SetText("Revitalize");
        buttonTexts[8].SetText("Adrenalin");
    }

    public void EnableEndTurnButton()
    {
        endTurnButton.interactable = true;
    }

    //Knight Move Buttons
    public void GuardianButton()
    {
        ResetButtonTextsKnight();
        buttonTexts[0].SetText(ColourText.GreenString("Guardian"));
        movesSelected[0, 0] = true;
    }

    public void FlurryButton()
    {
        ResetButtonTextsKnight();
        buttonTexts[1].SetText(ColourText.GreenString("Flurry"));
        movesSelected[0, 1] = true;
    }

    public void AltrutisticPierceButton()
    {
        ResetButtonTextsKnight();
        buttonTexts[2].SetText(ColourText.GreenString("Altrutistic Pierce"));
        movesSelected[0, 2] = true;
    }

    public void ResetButtonTextsKnight()
    {
        buttonTexts[0].SetText("Guardian");
        buttonTexts[1].SetText("Flurry");
        buttonTexts[2].SetText("Altrutistic Pierce");
        for (int j = 0; j < 3; j++)
        {
            movesSelected[0, j] = false;
        }
    }

    public void KnightButtonEnabler()
    {
        //Disable/Enable Knight move 1
        if (rustedKnight.move1CD == 0)
        {
            moveButtons[0].interactable = true;
        }
        else
        {
            moveButtons[0].interactable = false;
        }

        //Disable/Enable Knight move 2
        if (rustedKnight.move2CD == 0)
        {
            moveButtons[1].interactable = true;
        }
        else
        {
            moveButtons[1].interactable = false;
        }

        //Disable/Enable Knight move 3
        if (rustedKnight.move3CD == 0)
        {
            moveButtons[2].interactable = true;
        }
        else
        {
            moveButtons[2].interactable = false;
        }
    }

    //Dena Move Buttons
    public void BlessTargetSelect()
    {
        awaitingPlayerSelectDena = true;
        EnablePlayerButtons();
        DisableMoveButtons();
        DisableEndTurnButton();
        print("Click on the character you want to Bless");
    }

    public void ExitTargetSelect()
    {
        DisablePlayerButtons();
        EnableMoveButtons();
        UpdateMoveButtons(); //Ensure no cooldowned moves are enabled
        EnableEndTurnButton();
    }

    public void BlessButton()
    {
        ResetButtonTextsDena();
        buttonTexts[3].SetText(ColourText.GreenString("Bless"));
        movesSelected[1, 0] = true;
        BlessTargetSelect();
    }

    public void SurpressButton()
    {
        ResetButtonTextsDena();
        buttonTexts[4].SetText(ColourText.GreenString("Surpress"));
        movesSelected[1, 1] = true;
    }

    public void NecromanceButton()
    {
        ResetButtonTextsDena();
        buttonTexts[5].SetText(ColourText.GreenString("Necromance"));
        movesSelected[1, 2] = true;
    }

    public void ResetButtonTextsDena()
    {
        buttonTexts[3].SetText("Bless");
        buttonTexts[4].SetText("Surpress");
        buttonTexts[5].SetText("Necromance");
        for (int j = 0; j < 3; j++)
        {
            movesSelected[1, j] = false;
        }
    }

    //Flont Move Buttons

    public void CoagulationButton()
    {
        ResetButtonTextsFlont();
        buttonTexts[6].SetText(ColourText.GreenString("Coagulation"));
        movesSelected[2, 0] = true;
    }

    public void RevitalizeButton()
    {
        ResetButtonTextsFlont();
        buttonTexts[7].SetText(ColourText.GreenString("Revitalize"));
        movesSelected[2, 1] = true;
    }

    public void AdrenalinButton()
    {
        ResetButtonTextsFlont();
        buttonTexts[8].SetText(ColourText.GreenString("Adrenalin"));
        movesSelected[2, 2] = true;
    }

    public void ResetButtonTextsFlont()
    {
        buttonTexts[6].SetText("Coagulation");
        buttonTexts[7].SetText("Revitalize");
        buttonTexts[8].SetText("Adrenalin");
        for (int j = 0; j < 3; j++)
        {
            movesSelected[2, j] = false;
        }
    }

    public IEnumerator EndOfTurnSequence(int delayBetweenMoves)
    {
        turnsExecuted = false;
        for (int i = 0; i < 3; i++)
        {
            bool characterMoved = false;
            for (int j = 0; j < 3; j++)
            {
                if (movesSelected[i, j])
                {
                    singleTurnExecuted = false;
                    characterMoved = true;
                    StartCoroutine(MoveExecutionScenariosCoroutine(i, j, delayBetweenMoves));
                    UpdateDisplay();
                    yield return new WaitUntil(() => singleTurnExecuted);
                }
            }
            if (i == 0 && !characterMoved)
            {
                rustedKnight.CDUpdate(-1);
                UpdateDisplay();
            }
        }
        turnsExecuted = true;
    }

    public IEnumerator MoveExecutionScenariosCoroutine(int characterIndex, int moveNum, int delay)
    {
        CharacterInfo battler = battlers[characterIndex];
        //Knight's move executions
        if (characterIndex == 0)
        {
            switch (moveNum)
            {
                case 0:
                    battler.Move1(demonKing); //Guard
                    break;
                case 1:
                    battler.Move2(demonKing); //Flurry
                    break;
                case 2:
                    battler.Move3(demonKing); //Altrutistic Pierce
                    break;
            }
        }

        //Dena's move executions
        else if (characterIndex == 1)
        {
            switch (moveNum)
            {
                case 0:
                    battler.Move1(denaTarget); //Bless
                    break;
                case 1:
                    battler.Move2(demonKing); //Surpress
                    break;
                case 2:
                    battler.Move3(denaTarget); //Necromance
                    break;
            }
        }

        //Flont's move executions
        else if (characterIndex == 2)
        {
            switch (moveNum)
            {
                case 0:
                    battler.Move1(flontTarget); //Bless
                    break;
                case 1:
                    battler.Move2(demonKing); //Surpress
                    break;
                case 2:
                    battler.Move3(flontTarget); //Necromance
                    break;
            }
        }
        yield return new WaitForSeconds(delay);
        singleTurnExecuted = true;
    }

    //Player Select Buttons

    public void KnightPlayerSelect()
    {
        if (awaitingPlayerSelectDena)
        {
            denaTarget = battlers[0];
            awaitingPlayerSelectDena = false;
            print($"Dena will Bless {denaTarget.characterName}.");
            ExitTargetSelect();
        }
        else if (awaitingPlayerSelectFlont)
        {
            flontTarget = battlers[0];
            awaitingPlayerSelectFlont = false;
            ExitTargetSelect();
        }

    }


}
