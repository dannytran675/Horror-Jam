using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    public int hp, fam;
    public float crit, rnd;
    public bool guarded, usedMove;

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
}
