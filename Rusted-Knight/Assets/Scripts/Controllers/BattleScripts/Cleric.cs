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
        characterName = "Dena";
        belief = 50;
        beliefCost1 = 20;
        beliefCost2 = 12;
        beliefCost3 = 50;
    }
    public override void Move1(CharacterInfo character) //heal
    {
        print("Bless!");
        if (belief >= beliefCost1)
        {
            DecreaseBelief(beliefCost1);
            if (character != null)
            {
                character.IncreaseHP((character.maxHP / 10) * 3); //Healed for 30% Max HP
            }

            usedMove = true;
        }
        
        ResetAccuracyDebuff();
        AddFam(); //Increases Fam Regardless of Hit or Miss
    }
    public override void Move2(CharacterInfo character) //ray
    {
        print("Surpress!");
        SetAccuracy(0.9f); //Put the base accuracy into the method, debuff applied by method
        
        if (CanHit(acc) && belief >= beliefCost2)
        {
            DecreaseBelief(beliefCost2);

            int damage = DamageDealt(150, IfCrit());

            //Attacking
            if (character != null)
            {
                character.ReduceHP(damage);
            }

            usedMove = true;
            ResetBoosts();
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
            belief = 100;
        }
        else
        {
            belief += beliefIncrease;
        }
    }

    public void DecreaseBelief(int beliefDecrease)
    {
        if (belief == 0)
        {
            return;
        }
        else if (belief - beliefDecrease < 0)
        {
            belief = 0;
        }
        else
        {
            belief -= beliefDecrease;
        }
    }

}