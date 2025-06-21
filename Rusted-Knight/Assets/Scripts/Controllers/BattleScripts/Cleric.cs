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
    public override void Move1(CharacterInfo character) //heal
    {
        if (belief >= 25)
        {
            belief -= 25;
            character.IncreaseHP((character.maxHP / 10) * 3); //Healed for 30% Max HP

            usedMove = true;
            belief -= 25;
        }
    }
    public override void Move2(CharacterInfo character) //ray
    {
        acc = 0.9f;
        if (CanHit(acc) && belief >= 10)
        {
            belief -= 10;

            int damage = DamageDealt(150, IfCrit());

            //Attacking
            character.ReduceHP(damage);

            usedMove = true;
            belief -= 10;
            ResetBoosts();
        }
    }

    public override void Move3(CharacterInfo character) //revive
    {
        if (belief >= 65)
        {
            if (character.hp <= 0)
            {
                character.downed = false;
                character.SetHP(character.maxHP/2); //Start with 50% Max HP
            }
            belief -= 65;
        }
        usedMove = true;
    }

}