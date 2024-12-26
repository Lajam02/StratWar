using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shrooms : KnightsAi
{
    [SerializeField] private GameObject spore;
    private GameObject mySporeRain;
    public bool isBlue;

    // the ranged attack
    [SerializeField] private float maxRange;
    [SerializeField] private float minRange;
    [SerializeField] private GameObject sporeCloud;
    private GameObject CurrentSporeCloud;
    [SerializeField] private float rangedAttackCd;
    public Rigidbody2D rb;
    public float projecilSpeed;
     [SerializeField] private float lastSpawnTime;
    public Vector2 direction;
    public Transform firePos;


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
            isBlue = true;
        }
        else if (enemyteam == "blueTeam")
        {
            isBlue = false;
        }
        mySporeRain.GetComponent<Testfield>().Findenemy(isBlue);
    }

    private void Rangedattack()
    {
        if ((Vector2.Distance(gameObject.transform.position, target.transform.position) < maxRange) && (Vector2.Distance(gameObject.transform.position, target.transform.position) > minRange) && isAlive == true)
        {
            speed = 0;



            rangedAttackCd -= Time.fixedDeltaTime;
            if (rangedAttackCd <= 0)
            {
                ShootSporeCloud();
                rangedAttackCd = Time.fixedTime + 1f / lastSpawnTime; // Ställ in nästa tidpunkt för skott
            }

        }
        else if ((Vector2.Distance(gameObject.transform.position, target.transform.position) > maxRange) || (Vector2.Distance(gameObject.transform.position, target.transform.position) < minRange))
        {
            speed = 3;
        }
    }

    void ShootSporeCloud()
    {
        GameObject projectile = Instantiate(sporeCloud, firePos.position, firePos.rotation);

        // Lägg till fysik (Rigidbody) om det inte redan finns
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Skjut projektilen med en kraft i den riktning NPC:n pekar (forward)
            rb.AddForce(firePos.right * projecilSpeed, ForceMode2D.Impulse);
        }
    }

    private void InstantiateObject()
    {
        Instantiate(sporeCloud, gameObject.transform.position, Quaternion.identity);
    }
}
