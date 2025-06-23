using UnityEngine;
using System.Collections;

public class Boss : CharacterInfo
{
    [SerializeField] private CharacterInfo[] battlers = new CharacterInfo[3];// Used so that the boss can directly access their HPs
    Cleric dena;
    private CharacterInfo target;
    private CharacterInfo orignialTarget;





    void Awake()
    {
        dena = battlers[1] as Cleric;
        // Set the inherited field values here instead
        maxHP = 4000;
        hp = maxHP;
        fam = 0;
        crit = 0.125f;
        dmgMultiplier = 1;
        usedMove = false;
        downed = false;
        characterName = ColourText.BossColourString("The Demon King");
    }

    public void FindTarget()
    {
        int highestFam = -1;
        for (int i = 0; i < battlers.Length; i++)
        {
            CharacterInfo c = battlers[i];
            if (!c.downed && c.fam > highestFam)
            {
                highestFam = c.fam;
                target = c;
            }
        }
        if (target != orignialTarget)
        {
            print($"{characterName}'s eye is on {target.characterName}.");
            orignialTarget = target;
        }
    }

    public void BossTurn()
    {
        float rollMove = Random.Range(0f, 1f);

        // FindTarget();

        if (rollMove < 0.3f) // 1, 2, 3
        {
            FindTarget();
            Attack(target, 100);
        }
        else if (rollMove < 0.5f) // 4, 5
        {
            FindTarget();
            target.accuracyDebuffed = true; //Debuffing stat is handled on character's side
            print($"{characterName} reduced the accuracy of {target.characterName}'s next move by 3!");
        }
        else if (rollMove < 0.7f) // 6, 7
        {
            FindTarget();
            Attack(target, 200);
        }
        else if (rollMove < 0.9f)// 8, 9
        {
            FindTarget();
            Attack(target, 400);
        }
        else // 10
        {
            AttackAll(200);
        }

    }

    public void Attack(CharacterInfo character, int baseDamage)
    {
        if (!character.downed)
        {
            int damage = DamageDealt(baseDamage, IfCrit());

            if (character.guarded)
            {
                print($"{characterName} tried to hit {character.characterName} for {damage} damage but was blocked");
                character.guarded = false;
            }
            else
            {
                print($"{characterName} hit {character.characterName} for {damage} damage!");
                character.ReduceHP(damage);
                if (character.downed && !dena.downed)
                {
                    dena.DecreaseBelief(dena.belief / 2);
                }
                else if (!dena.downed)
                {
                    dena.DecreaseBelief(5);
                }
            }

        }
        else
        {
            print($"{characterName} tried to attack a dead person...");
        }
    }

    public void AttackAll(int baseDamage)
    {
        for (int i = 0; i < battlers.Length; i++)
        {
            CharacterInfo c = battlers[i];
            if (!c.downed)
            {
                Attack(battlers[i], baseDamage);
            }
        }
    }
    
}
