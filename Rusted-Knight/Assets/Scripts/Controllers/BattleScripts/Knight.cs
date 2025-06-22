using UnityEngine;
public class Knight : CharacterInfo
{
    public int move1CD = 0, move2CD = 1, move3CD = 3;

    void Awake()
    {
        // Set the inherited field values here instead
        maxHP = 1300;
        hp = maxHP;
        fam = 1;
        crit = 0.125f;
        usedMove = false;
        downed = false;
        characterName = "The Knight";
    }
    public override void Move1(CharacterInfo character) //guard
    {
        if (move1CD == 0)
        {
            guarded = true;
            usedMove = true;
            move1CD = 4;
        }
        
        AddFam(); //Increases Fam Regardless of Hit or Miss
    }
    public override void Move2(CharacterInfo character) //attack
    {
        SetAccuracy(0.8f); //Put the base accuracy into the method, debuff applied by method
        
        if (CanHit(acc) && move2CD == 0)
        {
            int damage = DamageDealt(65, IfCrit());

            //Attacking
            character.ReduceHP(damage);

            usedMove = true;
            move2CD = 1;
            ResetBoosts();
        }
        
        AddFam(); //Increases Fam Regardless of Hit or Miss
    }

    public override void Move3(CharacterInfo character) //slice
    {
        SetAccuracy(0.9f); //Put the base accuracy into the method, debuff applied by method

        if (CanHit(acc) && move3CD == 0)
        {
            int damage = DamageDealt(200, IfCrit());

            //Attacking
            character.ReduceHP(damage);

            usedMove = true;
            move3CD = 3;
            ResetBoosts();
        }
        
        AddFam(); //Increases Fam Regardless of Hit or Miss
    }
}