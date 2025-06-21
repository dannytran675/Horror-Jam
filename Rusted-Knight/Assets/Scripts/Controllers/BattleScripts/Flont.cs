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
        character.ReduceHP(character.maxHP / 10);//10% Max HP Removed
        character.dmgMultiplier = 1.2f;
        usedMove = true;
        
        AddFam(); //Increases Fam Regardless of Hit or Miss
    }

    public override void Move2(CharacterInfo character) //heal
    {
        ReduceHP(maxHP / 10); //10% Max HP Removed
        if (clericInstance != null)
        {
            Debug.Log("Cleric healed by Flont");
            clericInstance.IncreaseHP(clericInstance.maxHP / 10); //10% Max HP Healed
        }
        if (knightInstance != null)
        {
            Debug.Log("Knight healed by Flont");
            knightInstance.IncreaseHP(knightInstance.maxHP / 10); //10% Max HP Healed
        }
        
        AddFam(); //Increases Fam Regardless of Hit or Miss
    }

    public override void Move3(CharacterInfo character) //lucky
    {
        character.ReduceHP(character.maxHP / 5); //20% Max HP Removed
        character.crit *= 2;
        character.luckBoosted = true;
        
        AddFam(); //Increases Fam Regardless of Hit or Miss
    }
}