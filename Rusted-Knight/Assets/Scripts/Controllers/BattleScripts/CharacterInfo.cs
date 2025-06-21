using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    public double maxHP, hp;
    public int fam;
    public float crit, rnd;
    public static double dmgMultiplier = 1;
    public double acc;
    public bool guarded, usedMove, downed;

    public virtual void move1(CharacterInfo character)
    {

    }

    public virtual void move2(CharacterInfo character)
    {

    }

    public virtual void move3(CharacterInfo character)
    {

    }

    public virtual void setHP(double hp)
    {
        this.hp = hp;
    }

    public virtual void setFam(int fam)
    {
        this.fam = fam;
    }

    public virtual void addFam()
    {
        fam++;
    }

    public bool ifCrit()
    {
        rnd = Random.Range(0f, 1f);
        if (rnd > crit)
        {
            return false;
        }
        return true;
    }

    public bool ifHit(double acc)
    {
        rnd = Random.Range(0f, 1f);
        if (rnd > acc)
        {
            return false;
        }
        return true;
    }
}
