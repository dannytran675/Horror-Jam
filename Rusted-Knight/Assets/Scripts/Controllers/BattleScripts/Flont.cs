using UnityEngine;
public class Flont : CharacterInfo
{
    // Knight knightInstance = Object.FindFirstObjectByType<Knight>();
    // Cleric clericInstance = Object.FindFirstObjectByType<Cleric>();
    [SerializeField] private Knight knightInstance;
    [SerializeField] private Cleric clericInstance;
    public void Awake()
    {
        maxHP = 1000;
        hp = maxHP;
        fam = 0;
        crit = 0.125f;
        usedMove = false;
        downed = false;
        characterName = "Flont";
    }
    public override void Move1(CharacterInfo character) //atk boost
    {
        print("Coagulation!");
        if (character != null)
        {
            character.ReduceHP(character.maxHP / 10);//10% Max HP Removed
            character.dmgMultiplier = 1.2f;
        }
        
        usedMove = true;
        
        ResetAccuracyDebuff();
        AddFam(); //Increases Fam Regardless of Hit or Miss
    }

    public override void Move2(CharacterInfo character) //heal
    {
        ReduceHP(maxHP / 10); //10% Max HP Removed
        print($"{characterName} lost {maxHP / 10} health using Revitalize.");
        if (clericInstance != null)
        {
            Debug.Log("Cleric healed by Flont");
            int heal = clericInstance.maxHP / 10;
            int hpBef = clericInstance.hp;
            string personName = clericInstance.characterName;
            clericInstance.IncreaseHP(clericInstance.maxHP / 10); //10% Max HP Healed
            if (clericInstance.hp - hpBef > 0)
            {
                print($"{characterName} healed {personName} for {heal} health!");
            }
            else
            {
                print($"{characterName} tried to heal {personName} for {heal} health. But they're too healthy!");
            }
        }
        if (knightInstance != null)
        {
            Debug.Log("Knight healed by Flont");
            int heal = knightInstance.maxHP / 10;
            int hpBef = knightInstance.hp;
            string personName = knightInstance.characterName;
            knightInstance.IncreaseHP(knightInstance.maxHP / 10); //10% Max HP Healed
            if (knightInstance.hp - hpBef > 0)
            {
                print($"{characterName} healed {personName} for {heal} health!");
            }
            else
            {
                print($"{characterName} tried to heal {personName} for {heal} health. But they're too healthy!");
            }
        }
        
        ResetAccuracyDebuff();
        AddFam(); //Increases Fam Regardless of Hit or Miss
    }

    public override void Move3(CharacterInfo character) //lucky
    {
        print("Adrenalin!");
        if (character != null)
        {
            character.ReduceHP(character.maxHP / 5); //20% Max HP Removed
            character.crit *= 2;
            character.luckBoosted = true;
        }
        
        ResetAccuracyDebuff();
        AddFam(); //Increases Fam Regardless of Hit or Miss
    }
}