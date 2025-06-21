using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    public double maxHP, hp;
    public int fam;
    public float crit, rnd;
    public double dmgMultiplier = 1;
    public static double critMultiplier = 2;
    public double acc;
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

    public virtual void SetHP(double hp)
    {
        this.hp = hp;
    }

    public virtual void ReduceHP(double hp)
    {
        this.hp -= hp;
        if (this.hp <= 0)
        {
            downed = true;
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

    public bool IfHit(double acc)
    {
        rnd = Random.Range(0f, 1f);
        return (rnd <= crit);
    }
}
