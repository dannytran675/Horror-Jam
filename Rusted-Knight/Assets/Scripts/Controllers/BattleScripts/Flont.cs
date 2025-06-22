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
        if (character != null)
        {
            print($"{characterName} used Coagulation on {character.characterName}!");
            int hpOff = character.maxHP / 10;
            character.ReduceHP(hpOff);//10% Max HP Removed
            character.dmgMultiplier = 1.2f;
            print($"{character.characterName} lost {hpOff} HP");
            print($"{character.characterName}'s next attack will deal 20% more damage");
        }
        
        usedMove = true;
        
        ResetAccuracyDebuff();
        AddFam(); //Increases Fam Regardless of Hit or Miss
    }

    public override void Move2(CharacterInfo character) //heal
    {
        ReduceHP(maxHP / 10); //10% Max HP Removed
        print($"{characterName} lost {maxHP / 10} health using Revitalize.");
        if (character != null)
        {
            int heal = character.maxHP / 10;
            int hpBef = character.hp;
            string personName = character.characterName;
            character.IncreaseHP(character.maxHP / 10); //10% Max HP Healed
            if (character.hp - hpBef > 0)
            {
                print($"{characterName} healed {personName} for {heal} health!");
            }
            else
            {
                print($"{characterName} tried to heal {personName} for {heal} health. But they're too healthy!");
            }
        }

        usedMove = true;
        
        ResetAccuracyDebuff();
        AddFam(); //Increases Fam Regardless of Hit or Miss
    }

    public override void Move3(CharacterInfo character) //lucky
    {
        if (character != null)
        {
            print($"{characterName} used Adrenaline on {character.characterName}!");
            int hpOff = character.maxHP / 5;
            character.ReduceHP(hpOff); //20% Max HP Removed
            character.crit *= 2;
            character.luckBoosted = true;
            print($"{character.characterName} lost {hpOff} HP");
            print($"{character.characterName} feels lucky");
        }

        usedMove = true;
        
        ResetAccuracyDebuff();
        AddFam(); //Increases Fam Regardless of Hit or Miss
    }
}