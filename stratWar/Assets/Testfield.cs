using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testfield : MonoBehaviour
{
    public GameObject target;
    public float testRange;
    public Collider2D coler;
    public Rigidbody2D rb;
    public bool grog;
    [SerializeField] private string enemy;
    public float fielDuration;

    
    public float tickInterval;
    public float duration;
    public int tickDamage;
    

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, fielDuration);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
         
    }

    public void Findenemy(bool fog)
    {
        if(fog == true)
        {
            enemy = "redTeam";
        }
        else if(fog == false)
        {
            enemy = "blueTeam";
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        // if(col.gameObject.tag == enemy) {
        Debug.Log(col.gameObject);
            col.gameObject.GetComponent<KnightsAi>().Sporedcondition(tickInterval,duration,tickDamage);
        //}
    }


}
