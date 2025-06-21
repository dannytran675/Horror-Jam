using UnityEngine;
public class Flont : CharacterInfo
{
    Knight knightInstance = Object.FindFirstObjectByType<Knight>();
    Cleric clericInstance = Object.FindFirstObjectByType<Cleric>();
    public void Awake()
    {
        maxHP = 500;
        hp = 500;
        fam = 0;
        crit = 0.125f;
        usedMove = false;
        downed = false;
    }
    public override void move1(CharacterInfo character) //atk boost
    {
        character.hp -= character.maxHP * 0.1;
        dmgMultiplier = 1.2;
        usedMove = true;
    }

    public override void move2(CharacterInfo character) //heal
    {
        hp -= maxHP * 0.1;
        knightInstance.hp += knightInstance.maxHP * 0.1;
        clericInstance.hp += clericInstance.maxHP * 0.1;
    }

    public override void move3(CharacterInfo character) //lucky
    {
        character.hp -= character.maxHP * 0.2;
        character.crit *= 2;
        character.acc *= 2;
    }
}