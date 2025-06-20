using UnityEngine;
public class Knight : CharacterInfo
{
    public int move1CD = 0, move2CD = 1, move3CD = 3;
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
        if (move2CD == 0)
        {
            if (ifCrit())
            {
                character.hp -= 65;
            }
            else
            {
                character.hp -= 130;
            }
            usedMove = true;
            move2CD = 1;
        }
    }

    public override void move3(CharacterInfo character) //slice
    {
        if (move3CD == 0)
        {
            if (ifCrit())
            {
                character.hp -= 200;
            }
            else
            {
                character.hp -= 400;
            }
            usedMove = true;
            move3CD = 3;
        }
    }

}