using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    public double maxHP, hp;
    public int fam;
    public float crit, rnd;
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

    public void setHP(int hp)
    {
        this.hp = hp;
    }

    public void setFam(int fam)
    {
        this.fam = fam;
    }

    public void addFam()
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
}
