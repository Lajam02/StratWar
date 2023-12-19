using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testfield : MonoBehaviour
{
    public GameObject target;
    public float testRange;
    public Collider2D coler;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
         
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("uWu");
    }


}
