using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : Predetor
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        Ai = new PredetorAi(this);
       
        Diet.Add("wolf");
        Diet.Add("sheep");
        Diet.Add("rabbit");
        tag = "bear";
        
        //GetComponent<Renderer>().material.SetColor("_Color", Color.black);
        Speed = 125.0f;
        ChaseSpeed = 200.4f;
        StarvationLimit = 100f;
        nutrition = 7.5f;
    }

    public override void Die()
    {
        GetComponent<Bear>().Reset();
        gameObject.SetActive(false);
        GameManager.AddToDeadQueue(gameObject);
    }
}
