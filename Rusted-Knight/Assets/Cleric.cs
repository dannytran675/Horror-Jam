using UnityEngine;
public class Cleric : CharacterInfo
{
    public int belief;
    public override void move1(CharacterInfo character) //heal
    {
        if (belief >= 25)
        {
            hp -= maxHP * 0.25;
            character.hp += maxHP * 0.3;
            if (character.hp >= character.maxHP)
            {
                character.hp = character.maxHP;
            }
            usedMove = true;
        }
    }
    public override void move2(CharacterInfo character) //attack
    {
        if (belief >= 10)
        {

        }
    }

    public override void move3(CharacterInfo character) //slice
    {
        if (belief >= 65)
        {

        }
    }

}