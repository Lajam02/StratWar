using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightsAi : MonoBehaviour
{

    //base info
    [SerializeField] private int speed;
    [SerializeField] private float range;
    [SerializeField] private string enemyteam;
    public int damage;
    private bool isAlive;
    public bool facingRight;

    //combat
    [SerializeField] private GameObject[] enemy;
    private GameObject target;
    private bool haveAttacked;

    //Hp
    [SerializeField] private int maxHp;
    private int curentHp;

    // cooldown  
    [SerializeField] private float cdTimer;
    private float cd;

    [SerializeField] private Animator animator;



    // set all the base stats
    void Start()
    {
        DecideTeam();
        curentHp = maxHp;
        isAlive = true;
        searchEnemy();
        haveAttacked = false;
        cd = cdTimer;
    }

    // Update is called once per frame
    void Update()
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

    private void FixedUpdate()
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
    
    private void decideDirection()
    {
        Vector3 scale = transform.localScale;
        if (target.transform.position.x < gameObject.transform.position.x) {
            facingRight = true;
                }
        else
        {
            facingRight = false; 
        }

        scale.x = Mathf.Abs(scale.x) * (facingRight ? -1 : 1);
        transform.localScale = scale;
    }

    void DecideTeam()
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
    void searchEnemy()
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

    public void Attack()
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

    public void TakeDamage(int x)
    {
        Debug.Log("slå");
        curentHp = curentHp - x;
    }
}
