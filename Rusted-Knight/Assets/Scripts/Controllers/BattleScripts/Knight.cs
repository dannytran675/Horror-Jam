using UnityEngine;
public class Knight : CharacterInfo
{
    public int move1CD = 0, move2CD = 1, move3CD = 3;

    void Awake()
    {
        // Set the inherited field values here instead
        maxHP = 1000;
        hp = 1000;
        fam = 1;
        crit = 0.125f;
        usedMove = false;
        downed = false;
    }
    public override void Move1(CharacterInfo character) //guard
    {
        if (move1CD == 0)
        {
            guarded = true;
            usedMove = true;
            move1CD = 4;
        }
    }
    public override void Move2(CharacterInfo character) //attack
    {
        acc = 0.8;
        if (IfHit(acc) && move2CD == 0)
        {
            double damage = 65 * dmgMultiplier;
            if (IfCrit())
            {
                damage *= critMultiplier;
            }

            //Attacking
            character.ReduceHP(damage);

            usedMove = true;
            move2CD = 1;
        }
    }

    public override void Move3(CharacterInfo character) //slice
    {
        acc = 0.9;
        if (IfHit(acc) && move3CD == 0)
        {
            if (IfCrit())
            {
                character.hp -= 200 * dmgMultiplier;
            }
            else
            {
                character.hp -= 400 * dmgMultiplier;
            }
            usedMove = true;
            move3CD = 3;
        }
    }
}