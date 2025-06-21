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
    }
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
            belief -= 25;
        }
    }
    public override void move2(CharacterInfo character) //ray
    {
        acc = 0.9;
        if (ifHit(acc) && belief >= 10)
        {
            if (ifCrit())
            {
                character.hp -= 300;
            }
            else
            {
                character.hp -= 150;
            }
            usedMove = true;
            belief -= 10;
        }
    }

    public override void move3(CharacterInfo character) //revive
    {
        if (belief >= 65)
        {
            if (character.hp <= 0)
            {
                character.downed = false;
                character.hp += character.maxHP * 0.3;
            }
            belief -= 65;
        }
        usedMove = true;
    }

}