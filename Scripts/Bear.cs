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
        //this.Diet.Add("sheep");
        //this.Diet.Add("rabbit");
        Diet.Add("wolf");
        tag = "bear";
        
        //GetComponent<Renderer>().material.SetColor("_Color", Color.black);
        Speed = 125.0f;
        ChaseSpeed = 200.4f;
        StarvationLimit = 400f;
        nutrition = 7.5f;
    }

    public override void Die()
    {
        GetComponent<Bear>().Reset();
        gameObject.SetActive(false);
        GameManager.AddToDeadQueue(gameObject);
    }
}
