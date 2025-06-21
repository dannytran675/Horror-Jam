using UnityEngine;
public class Flont : CharacterInfo
{
    // Knight knightInstance = Object.FindFirstObjectByType<Knight>();
    // Cleric clericInstance = Object.FindFirstObjectByType<Cleric>();
    [SerializeField] private Knight knightInstance;
    [SerializeField] private Cleric clericInstance;
    public void Awake()
    {
        maxHP = 500;
        hp = 500;
        fam = 0;
        crit = 0.125f;
        usedMove = false;
        downed = false;
    }
    public override void Move1(CharacterInfo character) //atk boost
    {
        character.hp -= character.maxHP * 0.1;
        character.dmgMultiplier = 1.2;
        usedMove = true;
    }

    public override void Move2(CharacterInfo character) //heal
    {
        hp -= maxHP * 0.1;
        if (clericInstance != null)
        {
            Debug.Log("Cleric healed by Flont");
            clericInstance.hp += clericInstance.maxHP * 0.1;
        }
        if (knightInstance != null)
        {
            Debug.Log("Knight healed by Flont");
            knightInstance.hp += knightInstance.maxHP * 0.1;
        }
    }

    public override void Move3(CharacterInfo character) //lucky
    {
        character.hp -= character.maxHP * 0.2;
        character.crit *= 2;
        character.acc *= 2;
    }
}