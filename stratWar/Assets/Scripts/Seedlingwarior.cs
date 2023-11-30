using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seedlingwarior : KnightsAi
{
    [SerializeField] private float xp = 0;
    [SerializeField] private float xpLimit;
    [SerializeField] private int megaDmg;
    [SerializeField] private bool spsAtt;

    public override void decideDirection()
    {
        base.decideDirection();
        spsAtt = false;
    }
    public override void Attack()
    {
        if (xp <= xpLimit && spsAtt == false)
        {
            haveAttacked = true;
            animator.SetBool("attack", true);
            if (haveAttacked == true)
            {
                cd -= Time.fixedDeltaTime;
                if (cd <= 0)
                {

                    target.GetComponent<KnightsAi>().TakeDamage(damage);
                    xp += 10;
                    cd = cdTimer;
                    haveAttacked = false;


                }


            }

        }
        
    }
    public override void TakeDamage(int x)
    {
        if (xp >= xpLimit)
        {
            spsAtt = true;
            animator.SetBool("attack", false);
            animator.SetBool("speAttack", true);
        }
        else if(xp <= xpLimit && spsAtt == false)
        {
            base.TakeDamage(x);
            xp = xp + 10;
        }

    }

    void SpecialAttack()
    {
        target.GetComponent<KnightsAi>().TakeDamage(megaDmg);
        animator.SetBool("speAttack", false);
        xp = 0;
        spsAtt = false;
    }
}
