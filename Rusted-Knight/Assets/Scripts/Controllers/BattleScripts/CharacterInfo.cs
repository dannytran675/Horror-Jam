using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    public int maxHP, hp;
    public int fam;
    public float crit, rnd;
    public float dmgMultiplier = 1;
    public static float critMultiplier = 2;
    public float acc;
    public bool luckBoosted;
    public bool guarded, usedMove, downed;

    public virtual void Move1(CharacterInfo character)
    {

    }

    public virtual void Move2(CharacterInfo character)
    {

    }

    public virtual void Move3(CharacterInfo character)
    {

    }

    public virtual void SetHP(int hp)
    {
        this.hp = hp;
    }

    public virtual void ReduceHP(int hp)
    {
        if (!downed)
        {
            this.hp -= hp;
            if (this.hp <= 0)
            {
                downed = true;
            }
        }

    }

    public virtual void IncreaseHP(int hp)
    {
        if (!downed)
        {
            this.hp += hp;
            if (hp > maxHP)
            {
                this.hp = maxHP;
            }
        }
    }

    public virtual void SetFam(int fam)
    {
        this.fam = fam;
    }

    public virtual void AddFam()
    {
        fam++;
    }

    public bool IfCrit()
    {
        rnd = Random.Range(0f, 1f);
        return (rnd <= crit);
    }

    public bool IfHit(float acc)
    {
        rnd = Random.Range(0f, 1f);
        return (rnd <= crit);
    }

    public bool CanHit(float acc)
    {
        return (luckBoosted || IfHit(acc));
    }

    public int DamageDealt(int baseDamage, bool critHit)
    {
        float damage = baseDamage * dmgMultiplier;

        if (critHit)
        {
            damage *= critMultiplier;
        }

        int roundedDmg = (int) Mathf.Ceil(damage);
        return roundedDmg;
    }

    public void ResetBoosts()
    {
        dmgMultiplier = 1;
        luckBoosted = false;
        crit = 0.125f;
    }
}
