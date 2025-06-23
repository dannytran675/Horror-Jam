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
        characterName = ColourText.KnightColourString("The Knight");
        move1CD = 0;
        move2CD = 1;
        move3CD = 3;
    }
    public override void Move1(CharacterInfo character) //guard
    {
        print("Guard!");
        if (move1CD == 0)
        {
            guarded = true;
            usedMove = true;
            CDUpdate(0);
            print($"{characterName} used Guardian against {character.characterName}!");
        }

        ResetAccuracyDebuff();
        AddFam(); //Increases Fam Regardless of Hit or Miss
    }
    public override void Move2(CharacterInfo character) //attack
    {
        print("Flurry!");
        SetAccuracy(0.8f); //Put the base accuracy into the method, debuff applied by method

        if (CanHit(acc) && move2CD == 0)
        {
            critLanded = IfCrit();
            int damage = DamageDealt(65, critLanded);

            //Attacking
            if (character != null)
            {
                print($"{characterName} dealt {damage} damage to {character.characterName}!");
                character.ReduceHP(damage);
            }

            usedMove = true;
            ResetBoosts();
        }
        else
        {
            if (character != null)
            {
                print($"{characterName} tried to use Flurry against {character.characterName} but missed...");
            }
            critLanded = false;
            usedMove = false;
        }

        CDUpdate(1);
        ResetAccuracyDebuff();
        AddFam(); //Increases Fam Regardless of Hit or Miss
    }

    public override void Move3(CharacterInfo character) //slice
    {
        print("Altrutistic Pierce!");
        SetAccuracy(0.9f); //Put the base accuracy into the method, debuff applied by method

        if (CanHit(acc) && move3CD == 0)
        {
            critLanded = IfCrit();
            int damage = DamageDealt(200, critLanded);

            //Attacking
            if (character != null)
            {
                print($"{characterName} dealt {damage} damage to {character.characterName}!");
                character.ReduceHP(damage);
            }


            usedMove = true;
            ResetBoosts();
        }
        else
        {
            if (character != null)
            {
                print($"{characterName} tried to use Altruistic Pierce against {character.characterName} but missed...");
            }
            usedMove = false;
            critLanded = false;
        }

        CDUpdate(2);
        ResetAccuracyDebuff();
        AddFam(); //Increases Fam Regardless of Hit or Miss
    }

    public void CDUpdate(int moveNum)
    {
        if (moveNum == 0)
        {
            Debug.Log("Move1");
            move1CD = 4;
            if (move2CD > 0) move2CD -= 1;
            if (move3CD > 0) move3CD -= 1;
        }

        else if (moveNum == 1)
        {
            Debug.Log("Move2");
            move2CD = 1;
            if (move1CD > 0) move1CD -= 1;
            if (move3CD > 0) move3CD -= 1;
        }

        else if (moveNum == 2)
        {
            Debug.Log("Move3");
            move3CD = 3;
            if (move1CD > 0) move1CD -= 1;
            if (move2CD > 0) move2CD -= 1;
        }

        else
        {
            if (move1CD > 0) move1CD -= 1;
            if (move2CD > 0) move2CD -= 1;
            if (move3CD > 0) move3CD -= 1;
        }
    }
}