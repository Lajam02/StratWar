using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shrooms : KnightsAi
{
    [SerializeField] private GameObject spore;
    private GameObject mySporeRain;
    public bool UwU;

    // the ranged attack
    [SerializeField] private float maxRange;
    [SerializeField] private float minRange;
    [SerializeField] private GameObject sporeCloud;
    private GameObject CurrentSporeCloud;
    [SerializeField] private float rangedAttackCd;
    public Rigidbody2D rb;
    public float projecilSpeed;
    private float lastSpawnTime;
    public Vector2 direction;
 

    public override void Start()
    {
        base.Start();

        lastSpawnTime = rangedAttackCd;       
    }

    public override void Update()
    {
        base.Update();
        Rangedattack();

    }

    public override void isDead()
    {
        base.isDead();
        mySporeRain = Instantiate(spore, gameObject.transform.position, Quaternion.identity);
        if (enemyteam == "redTeam")
        {
            UwU = true;
        }
        else if (enemyteam == "blueTeam")
        {
            UwU = false;
        }
        mySporeRain.GetComponent<Testfield>().Findenemy(UwU);
    }

    private void Rangedattack()
    {
        if ((Vector2.Distance(gameObject.transform.position, target.transform.position) < maxRange) && (Vector2.Distance(gameObject.transform.position, target.transform.position) > minRange) && isAlive == true)
        {
            speed = 0;

            
            
                rangedAttackCd -= Time.fixedDeltaTime;
                if (rangedAttackCd <= 0)
                {
                direction = (target.transform.position - transform.position).normalized;
                CurrentSporeCloud = Instantiate(sporeCloud, transform.position, Quaternion.identity);
                rb = CurrentSporeCloud.GetComponent<Rigidbody2D>();
                rb.velocity = direction * projecilSpeed;
                rangedAttackCd = lastSpawnTime;

                }

        }
        else if ((Vector2.Distance(gameObject.transform.position, target.transform.position) > maxRange) || (Vector2.Distance(gameObject.transform.position, target.transform.position) < minRange))
        {
            speed = 3;
        }
    }

    private void InstantiateObject()
    {
        Instantiate(sporeCloud, gameObject.transform.position, Quaternion.identity);
    }
}
