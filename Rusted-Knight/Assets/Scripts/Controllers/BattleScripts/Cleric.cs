using UnityEngine;
public class Cleric : CharacterInfo
{
    public int belief = 50;
    public int beliefCost1 = 20;
    public int beliefCost2 = 12;
    public int beliefCost3 = 50;
    public void Awake()
    {
        maxHP = 700;
        hp = maxHP;
        fam = 0;
        crit = 0.125f;
        usedMove = false;
        downed = false;
        characterName = ColourText.DenaColourString("Dena");
        belief = 50;
        beliefCost1 = 20;
        beliefCost2 = 12;
        beliefCost3 = 50;
    }
    public override void Move1(CharacterInfo character) //heal
    {
        if (belief >= beliefCost1)
        {
            DecreaseBelief(beliefCost1);
            if (character != null)
            {
                int heal = (character.maxHP / 10) * 3;
                int hpBef = character.hp;
                character.IncreaseHP(heal); //Healed for 30% Max HP
                heal = character.hp - hpBef;
                bool healed = (heal > 0);
                if (healed)
                {
                    print($"{characterName} healed {character.characterName} for {heal} health using Bless");
                }
                else
                {
                    heal = (character.maxHP / 10) * 3;
                    print($"{characterName} tried to heal {character.characterName} for {heal} health. But they're too healthy!");
                }
            }

            usedMove = true;
        }
        
        ResetAccuracyDebuff();
        AddFam(); //Increases Fam Regardless of Hit or Miss
    }
    public override void Move2(CharacterInfo character) //ray
    {
        SetAccuracy(0.9f); //Put the base accuracy into the method, debuff applied by method

        if (CanHit(acc) && belief >= beliefCost2)
        {
            DecreaseBelief(beliefCost2);

            critLanded = IfCrit();
            int damage = DamageDealt(150, critLanded);

            //Attacking
            if (character != null)
            {
                print($"{characterName} dealt {damage} damage to {character.characterName} using Surpress!");
                character.ReduceHP(damage);
            }

            usedMove = true;
            ResetBoosts();
        }
        else
        {
            if (character != null)
            {
                print($"{characterName} tried to use Surpress against {character.characterName} but missed...");
                critLanded = false;
            }
        }
        
        ResetAccuracyDebuff();
        AddFam(); //Increases Fam Regardless of Hit or Miss
    }

    public override void Move3(CharacterInfo character) //revive
    {
        print("Necromance!");
        if (belief >= beliefCost3)
        {
            DecreaseBelief(beliefCost3);

            if (character != null && character.hp <= 0)
            {
                character.downed = false;
                character.SetHP(character.maxHP / 2); //Start with 50% Max HP
                print($"{characterName} revived {character.characterName} to half health!");
            }
            usedMove = true;
        }
        
        ResetAccuracyDebuff();
        AddFam(); //Increases Fam Regardless of Hit or Miss
    }

    public void IncreaseBelief(int beliefIncrease)
    {
        if (belief == 100)
        {
            return;
        }
        else if (belief + beliefIncrease > 100)
        {
            int effectiveGain = 100 - belief;
            belief = 100;
            print(ColourText.GrayString($"{characterName} gained {effectiveGain}% belief."));
        }
        else
        {
            belief += beliefIncrease;
            print(ColourText.GrayString($"{characterName} gained {beliefIncrease}% belief."));
        }
    }

    public void DecreaseBelief(int beliefDecrease)
    {
        if (belief == 0)
        {
            return;
        }
        else if (belief - beliefDecrease <= 0)
        {
            belief = 0;
            hp = 0;
            downed = true;
            print(ColourText.DarkRedString("Dena has lost all hope. She has given up."));
        }
        else
        {
            belief -= beliefDecrease;
            print(ColourText.GrayString($"{characterName} has lost {beliefDecrease}% belief."));
        }
    }

}