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

    string denaName, flontName;

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
    private bool startingNecromance;

    public bool bossDowned;


    void Start()
    {
        rustedKnight = battlers[0] as Knight;
        dena = battlers[1] as Cleric;
        denaName = dena.characterName;
        flontName = battlers[2].characterName;
        DisplayHPs();

        beliefText.SetText($"Belief: {dena.belief}%");

        //Knight Cooldowns
        DisplayKnightCD();

        //Dena Costs
        DisplayDenaCosts();

        //Flont all set to 0 since it costs HP
        DisplayFlontCosts();

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
            yield return new WaitUntil(() => turnsExecuted);
            //Deselect all buttons
            ResetAllMoveButtonSelections();

            //Reset all usedMove variables
            for (int i = 0; i < battlers.Length; i++)
            {
                battlers[i].usedMove = false;
            }

            //Boss turn
            if (!demonKing.downed)
            {
                print(ColourText.RedString("Boss turn"));

                yield return new WaitForSeconds(2);
                demonKing.BossTurn();
                yield return new WaitForSeconds(2);
                UpdateDisplay();
            }
            

            //Switching turns;
            playerAttacked = false;

        }

        if (bossDowned)
        {
            //Transition code
            GameManager.Instance.FadeInSceneTransition();
            yield return new WaitForSeconds(2);
            GameManager.Instance.LoadScene();
            GameManager.Instance.FadeOutSceneTransition();
        }


    }

    public bool canBattle()
    {
        bool allDowned = battlers[0].downed && battlers[1].downed && battlers[2].downed;

        bossDowned = demonKing.downed;

        bool canBattle = !allDowned && !bossDowned;

        return canBattle;
    }

    //Button interactions














    //Visual Methods
    public void DisplayKnightCD()
    {
        //Knight Cooldowns
        if (rustedKnight.downed)
        {
            CDTexts[0].SetText(ColourText.RedString($"[{rustedKnight.move1CD}]"));
            CDTexts[1].SetText(ColourText.RedString($"[{rustedKnight.move2CD}]"));
            CDTexts[2].SetText(ColourText.RedString($"[{rustedKnight.move3CD}]"));
        }
        else
        {
            CDTexts[0].SetText($"[{rustedKnight.move1CD}]");
            CDTexts[1].SetText($"[{rustedKnight.move2CD}]");
            CDTexts[2].SetText($"[{rustedKnight.move3CD}]");
        }
        
    }

    public void DisplayDenaCosts()
    {
        if (dena.downed)
        {
            CDTexts[3].SetText(ColourText.RedString($"[{dena.beliefCost1}%]"));
            CDTexts[4].SetText(ColourText.RedString($"[{dena.beliefCost2}%]"));
            CDTexts[5].SetText(ColourText.RedString($"[{dena.beliefCost3}%]"));
        }
        else
        {
            CDTexts[3].SetText($"[{dena.beliefCost1}%]");
            CDTexts[4].SetText($"[{dena.beliefCost2}%]");
            CDTexts[5].SetText($"[{dena.beliefCost3}%]");
        }
    }

    public void DisplayFlontCosts()
    {
        string cost = "[0]";
        if (battlers[2].downed)
        {
            cost = ColourText.RedString(cost);
        }
        else
        {
            cost = ColourText.GreenString(cost);
        }
        CDTexts[6].SetText(cost);
        CDTexts[7].SetText(cost);
        CDTexts[8].SetText(cost);
    }

    public void DisplayHPs()
    {

        for (int i = 0; i < 3; i++)
        {
            if (battlers[i].downed)
            {
                HPTexts[i].SetText(ColourText.RedString($"HP : {battlers[i].hp}"));
            }
            else
            {
                HPTexts[i].SetText($"HP : {battlers[i].hp}");
            }
        }

        //String formatting
        string demonHPString = $"{demonKing.hp}";
        // if (demonKing.hp > 999)
        // {
        //     demonHPString = $"{demonKing.hp / 1000} " + demonHPString.Substring(demonHPString.Length - 3);
        // }
        bossHPText.SetText($"HP : {demonHPString}");
    }

    public void DisplayBelief()
    {
        if (dena.downed)
        {
            beliefText.SetText(ColourText.RedString($"Belief: {dena.belief}%"));
        }
        else
        {
            beliefText.SetText($"Belief: {dena.belief}%");
        }
    }

    public void UpdateDisplay()
    {
        DisplayHPs();
        DisplayDenaCosts();
        DisplayKnightCD();
        DisplayFlontCosts();
        DisplayBelief();
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
        DenaButtonEnabler();
        DisableDownedButtons();
    }

    public void DisableDownedButtons()
    {
        for (int i = 0; i < 3; i++)
        {
            if (battlers[i].downed)
            {
                moveButtons[0 + 3 * i].interactable = false;
                moveButtons[1 + 3 * i].interactable = false;
                moveButtons[2 + 3 * i].interactable = false;
            }
        }
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
        buttonTexts[2].SetText("Altruistic Pierce");
        buttonTexts[3].SetText("Bless");
        buttonTexts[4].SetText("Suppress");
        buttonTexts[5].SetText("Necromance");
        buttonTexts[6].SetText("Coagulation");
        buttonTexts[7].SetText("Revitalize");
        buttonTexts[8].SetText("Adrenaline");
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

    public void AltruisticPierceButton()
    {
        ResetButtonTextsKnight();
        buttonTexts[2].SetText(ColourText.GreenString("Altruistic Pierce"));
        movesSelected[0, 2] = true;
    }

    public void ResetButtonTextsKnight()
    {
        buttonTexts[0].SetText("Guardian");
        buttonTexts[1].SetText("Flurry");
        buttonTexts[2].SetText("Altruistic Pierce");
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
        if (!startingNecromance)
        {
            EnablePlayerButtons();
            for (int i = 0; i < characterSelectButtons.Length; i++)
            {
                if (battlers[i].downed)
                {
                    characterSelectButtons[i].interactable = false;
                }
            }
            DisableMoveButtons();
            DisableEndTurnButton();
            print("Click on the icon of the character you want to Bless");
        }
        else
        {
            DisablePlayerButtons();
            for (int i = 0; i < characterSelectButtons.Length; i++)
            {
                if (battlers[i].downed)
                {
                    characterSelectButtons[i].interactable = true;
                }
            }
            DisableMoveButtons();
            DisableEndTurnButton();
            print("Click on the icon of the character you want to revive.");
        }
        
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

    public void SuppressButton()
    {
        ResetButtonTextsDena();
        buttonTexts[4].SetText(ColourText.GreenString("Suppress"));
        movesSelected[1, 1] = true;
    }

    public void NecromanceButton()
    {
        startingNecromance = true;
        ResetButtonTextsDena();
        buttonTexts[5].SetText(ColourText.GreenString("Necromance"));
        movesSelected[1, 2] = true;
        BlessTargetSelect();
    }

    public void ResetButtonTextsDena()
    {
        buttonTexts[3].SetText("Bless");
        buttonTexts[4].SetText("Suppress");
        buttonTexts[5].SetText("Necromance");
        for (int j = 0; j < 3; j++)
        {
            movesSelected[1, j] = false;
        }
    }

    public void DenaButtonEnabler()
    {
        if (dena.belief < dena.beliefCost3)
        {
            moveButtons[5].interactable = false;
            CDTexts[5].SetText(ColourText.RedString($"[{dena.beliefCost3}%]"));
        }
        else
        {
            moveButtons[5].interactable = true;
            CDTexts[5].SetText(ColourText.GreenString($"[{dena.beliefCost3}%]"));
        }

        //If neither teammate is dead, can't necromance
        if (!battlers[0].downed && !battlers[2].downed)
        {
            moveButtons[5].interactable = false;
        }

        if (dena.belief < dena.beliefCost1)
        {
            moveButtons[3].interactable = false;
            CDTexts[3].SetText(ColourText.RedString($"[{dena.beliefCost1}%]"));
        }
        else
        {
            moveButtons[3].interactable = true;
            CDTexts[3].SetText(ColourText.GreenString($"[{dena.beliefCost1}%]"));
        }

        if (dena.belief < dena.beliefCost2)
        {
            moveButtons[4].interactable = false;
            CDTexts[4].SetText(ColourText.RedString($"[{dena.beliefCost2}%]"));
        }
        else 
        {
            moveButtons[4].interactable = true;
            CDTexts[4].SetText(ColourText.GreenString($"[{dena.beliefCost2}%]"));
        }
    }

    //Flont Move Buttons

    public void FlontTargetSelect()
    {
        awaitingPlayerSelectFlont = true;
        EnablePlayerButtons();
        characterSelectButtons[2].interactable = false; //Disables flont's selection
        if (battlers[0].downed)
        {
            characterSelectButtons[0].interactable = false;
        }
        if (battlers[1].downed)
        {
            characterSelectButtons[1].interactable = false;
        }
        if (!battlers[0].downed || !battlers[1].downed)
        {
            DisableMoveButtons();
            DisableEndTurnButton();
            print("Click on the icon of the character you want to use this move on.");
        }
        else
        {
            print("You're doomed.");
        }
        
    }
    public void CoagulationButton()
    {
        ResetButtonTextsFlont();
        buttonTexts[6].SetText(ColourText.GreenString("Coagulation"));
        movesSelected[2, 0] = true;
        FlontTargetSelect();
    }

    public void RevitalizeButton()
    {
        ResetButtonTextsFlont();
        buttonTexts[7].SetText(ColourText.GreenString("Revitalize"));
        movesSelected[2, 1] = true;
        FlontTargetSelect();
    }

    public void AdrenalineButton()
    {
        ResetButtonTextsFlont();
        buttonTexts[8].SetText(ColourText.GreenString("Adrenaline"));
        movesSelected[2, 2] = true;
        FlontTargetSelect();
    }

    public void ResetButtonTextsFlont()
    {
        buttonTexts[6].SetText("Coagulation");
        buttonTexts[7].SetText("Revitalize");
        buttonTexts[8].SetText("Adrenaline");
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
                    if (!dena.downed && battlers[i].critLanded && battlers[i].usedMove)
                    {
                        dena.IncreaseBelief(14);
                    }
                    else if (!dena.downed && battlers[i].usedMove)
                    {
                        dena.IncreaseBelief(7);
                    }
                    UpdateDisplay();
                }
            }
            if (i == 0 && !characterMoved)
            {
                rustedKnight.CDUpdate(-1);
                UpdateDisplay();
            }
        }

        //Resetting the status for next turn;
        for (int i = 0; i < 3; i++)
        {
            battlers[i].usedMove = false;
            battlers[i].critLanded = false;
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
                    battler.Move3(demonKing); //Altruistic Pierce
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
                    battler.Move2(demonKing); //Suppress
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
                    battler.Move1(flontTarget); //Coagulation
                    break;
                case 1:
                    battler.Move2(flontTarget); //Revitalize
                    break;
                case 2:
                    battler.Move3(flontTarget); //Adrenalin
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
            if (startingNecromance)
            {
                print($"{denaName} will bring back {denaTarget.characterName}");
            }
            else
            {
                print($"{denaName} will Bless {denaTarget.characterName}.");
            }
            ExitTargetSelect();
        }
        else if (awaitingPlayerSelectFlont)
        {
            flontTarget = battlers[0];
            awaitingPlayerSelectFlont = false;
            print($"{flontName} will target {flontTarget.characterName}.");
            ExitTargetSelect();
        }

    }

    public void DenaPlayerSelect()
    {
        if (awaitingPlayerSelectDena)
        {
            denaTarget = battlers[1];
            awaitingPlayerSelectDena = false;
            if (startingNecromance)
            {
                print($"{denaName} will bring back {denaTarget.characterName}");
            }
            else
            {
                print($"{denaName} will Bless {denaTarget.characterName}.");
            }
            ExitTargetSelect();
        }
        else if (awaitingPlayerSelectFlont)
        {
            flontTarget = battlers[1];
            awaitingPlayerSelectFlont = false;
            print($"{flontName} will target {flontTarget.characterName}.");
            ExitTargetSelect();
        }

    }

    public void FlontPlayerSelect()
    {
        if (awaitingPlayerSelectDena)
        {
            denaTarget = battlers[2];
            awaitingPlayerSelectDena = false;
            if (startingNecromance)
            {
                print($"{denaName} will bring back {denaTarget.characterName}");
            }
            else
            {
                print($"{denaName} will Bless {denaTarget.characterName}.");
            }
            ExitTargetSelect();
        }
        else if (awaitingPlayerSelectFlont)
        {
            flontTarget = battlers[2];
            awaitingPlayerSelectFlont = false;
            print($"{flontName} will target {flontTarget.characterName}.");
            ExitTargetSelect();
        }

    }


}
