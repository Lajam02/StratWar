using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shrooms : KnightsAi
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask gag;
    [SerializeField] private Collider2D[] ConectedEnemy;
    [SerializeField] private bool sporing;
    
    

    public override void Update()
    {
        base.Update();


    }
    void realiseSpore()
    {
        sporing = !sporing;
    }


}
