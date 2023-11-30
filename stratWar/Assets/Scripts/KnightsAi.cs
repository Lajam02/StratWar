using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightsAi : MonoBehaviour
{

    //base info
     [SerializeField] protected private int speed;
    [SerializeField] protected private float range;
    [SerializeField] protected private string enemyteam;
    public int damage;
    protected private bool isAlive;
    [SerializeField] protected private bool facingRight;

    //combat
    [SerializeField] protected private GameObject[] enemy;
    protected private GameObject target;
    protected private bool haveAttacked;

    //Hp
    [SerializeField] protected private int maxHp;
    [SerializeField]  protected private int curentHp;

    // cooldown  
    [SerializeField] protected private float cdTimer;
    protected private float cd;

    [SerializeField] protected private Animator animator;



    // set all the base stats
    public virtual void Start()
    {
        DecideTeam();
        curentHp = maxHp;
        isAlive = true;
        searchEnemy();
        haveAttacked = false;
        cd = cdTimer;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
        if (curentHp <= 0)
        {
            gameObject.tag = "Dead";
            //Destroy(gameObject);
        }
        else {
            decideDirection();
        }


        if (target.tag == "Dead")
        {
            enemy = null;
            animator.SetBool("attack", false);
            searchEnemy();

        }

        if (gameObject.tag == "Dead")
        {
            animator.SetBool("targetfound", false);
            animator.SetBool("attack", false);
            animator.SetBool("die", true);
            isAlive = false;
        }
    }

    public virtual  void FixedUpdate()
    {

        if (isAlive == true)
        {
            if (Vector3.Distance(gameObject.transform.position, target.transform.position) < range)
            {
                animator.SetBool("targetfound", false);
                Attack();


            }
            else
            {
                animator.SetBool("attack", false);
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.fixedDeltaTime);
                if (speed > 0)
                {
                    animator.SetBool("targetfound", true);
                }
                else if (speed <= 0)
                {
                    animator.SetBool("targetfound", false);
                }

            }
        }

    }
    
    public virtual void decideDirection()
    {
        Vector3 scale = transform.localScale;
        if (target.transform.position.x < gameObject.transform.position.x) {
            facingRight = true;
                }
        else
        {
            facingRight = false; 
        }

        scale.x = Mathf.Abs(scale.x) * -1 * (facingRight ? 1 : -1);
        transform.localScale = scale;
    }

    public virtual void DecideTeam()
    {
        if(gameObject.tag == "blueTeam")
        {
            
            enemyteam = "redTeam";
        }
        else if(gameObject.tag == "redTeam")
        {
            
            enemyteam = "blueTeam";
        }
    }
    public virtual void  searchEnemy()
    {
        enemy = GameObject.FindGameObjectsWithTag(enemyteam);
        GameObject firstEnemyList = enemy[0];
        foreach (GameObject fuu in enemy)
        {
            //distance = Vector3.Distance(transform.position, fuu.transform.position);
            if (Vector3.Distance(transform.position, firstEnemyList.transform.position) > Vector3.Distance(transform.position, fuu.transform.position))
            {
                firstEnemyList = fuu;
                enemy[0] = fuu;
            }

        }

        target = enemy[0];
    }

    public virtual void Attack()
    {

        haveAttacked = true;
        animator.SetBool("attack", true);
        if (haveAttacked == true)
        {
            cd -= Time.fixedDeltaTime;
            if (cd <= 0)
            {

                target.GetComponent<KnightsAi>().TakeDamage(damage);
                cd = cdTimer;
                haveAttacked = false;

            }


        }

    }

    public virtual void TakeDamage(int x)
    {
        Debug.Log("slå");
        curentHp = curentHp - x;
    }
}
