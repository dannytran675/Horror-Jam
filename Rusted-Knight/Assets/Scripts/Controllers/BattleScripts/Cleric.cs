using UnityEngine;
public class Cleric : CharacterInfo
{
    public int belief = 0;
    public void Awake()
    {
        maxHP = 600;
        hp = 600;
        fam = 0;
        crit = 0.125f;
        usedMove = false;
        downed = false;
        characterName = "Dena";
    }
    public override void Move1(CharacterInfo character) //heal
    {
        if (belief >= 20)
        {
            DecreaseBelief(20);

            character.IncreaseHP((character.maxHP / 10) * 3); //Healed for 30% Max HP

            usedMove = true;
        }
        
        ResetAccuracyDebuff();
        AddFam(); //Increases Fam Regardless of Hit or Miss
    }
    public override void Move2(CharacterInfo character) //ray
    {
        SetAccuracy(0.9f); //Put the base accuracy into the method, debuff applied by method
        
        if (CanHit(acc) && belief >= 12)
        {
            DecreaseBelief(12);

            int damage = DamageDealt(150, IfCrit());

            //Attacking
            character.ReduceHP(damage);

            usedMove = true;
            ResetBoosts();
        }
        
        ResetAccuracyDebuff();
        AddFam(); //Increases Fam Regardless of Hit or Miss
    }

    public override void Move3(CharacterInfo character) //revive
    {
        if (belief >= 50)
        {
            DecreaseBelief(50);

            if (character.hp <= 0)
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