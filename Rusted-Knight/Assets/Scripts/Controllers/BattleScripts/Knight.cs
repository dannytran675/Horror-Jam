using UnityEngine;
public class Knight : CharacterInfo
{
    public int move1CD = 0, move2CD = 1, move3CD = 3;

    void Awake()
    {
        // Set the inherited field values here instead
        maxHP = 1000;
        hp = 1000;
        fam = 0;
        crit = 0.125f;
        usedMove = false;
        downed = false;
    }
    public override void move1(CharacterInfo character) //guard
    {
        if (move1CD == 0)
        {
            guarded = true;
            usedMove = true;
            move1CD = 4;
        }
    }
    public override void move2(CharacterInfo character) //attack
    {
        acc = 0.8;
        if (ifHit(acc) && move2CD == 0)
        {
            if (ifCrit())
            {
                character.hp -= 65 * dmgMultiplier;
            }
            else
            {
                character.hp -= 130 * dmgMultiplier;
            }
            usedMove = true;
            move2CD = 1;
        }
    }

    public override void move3(CharacterInfo character) //slice
    {
        acc = 0.9;
        if (ifHit(acc) && move3CD == 0)
        {
            if (ifCrit())
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