using UnityEngine;
using TMPro;

public class BattleManager : MonoBehaviour
{
    CharacterInfo[] battlers = new CharacterInfo[3];
    [SerializeField] private TMP_Text[] HPTexts = new TMP_Text[3];

    //For all cooldowns or percentages
    [SerializeField] private TMP_Text[] CDTexts = new TMP_Text[9];

    //Belief text
    [SerializeField] private TMP_Text beliefText;


    void Start()
    {
        HPTexts[0].SetText("HP : 1000");
        HPTexts[1].SetText("HP : 600");
        HPTexts[2].SetText("HP : 500");

        beliefText.SetText("Belief: 10%");

        //Knight Cooldowns
        CDTexts[0].SetText("[0]");
        CDTexts[1].SetText("[1]");
        CDTexts[2].SetText("[3]");

        //Dena Costs
        CDTexts[3].SetText("[25%]");
        CDTexts[4].SetText("[10%]");
        CDTexts[5].SetText("[65%]");

        //Flont all set to 0 since it costs HP
        CDTexts[6].SetText("[0]");
        CDTexts[7].SetText("[0]");
        CDTexts[8].SetText("[0]");

    }
}
