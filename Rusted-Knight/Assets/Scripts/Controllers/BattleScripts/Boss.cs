using UnityEngine;

public class Boss : CharacterInfo
{
    [SerializeField] private CharacterInfo[] battlers = new CharacterInfo[3];// Used so that the boss can directly access their HPs

    private CharacterInfo target;





    void Awake()
    {
        // Set the inherited field values here instead
        maxHP = 40000;
        hp = maxHP;
        fam = 0;
        crit = 0.125f;
        dmgMultiplier = 1;
        usedMove = false;
        downed = false;
        characterName = "Boss";
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
    }

    public void BossTurn()
    {
        float rollMove = Random.Range(0f, 1f);

        FindTarget();

        if (rollMove < 0.3f) // 1, 2, 3
        {
            Attack(target, 100);
        }
        else if (rollMove < 0.5f) // 4, 5
        {
            target.accuracyDebuffed = true; //Debuffing stat is handled on character's side
        }
        else if (rollMove < 0.7f) // 6, 7
        {
            Attack(target, 200);
        }
        else if (rollMove < 0.9f)// 8, 9
        {
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
                Debug.Log($"{characterName} tried to hit {character.characterName} for {damage} damage but was blocked");
            }
            else
            {
                character.ReduceHP(damage);
                Debug.Log($"{characterName} hit {character.characterName} for {damage} damage!");
            }

        }
        else
        {
            Debug.Log($"{characterName} tried to attack a dead person...");
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
