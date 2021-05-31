using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Predetor
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        Ai = new PredetorAi(this);
        this.Diet.Add("sheep");
        this.Diet.Add("rabbit");
        Predetors.Add("bear");
        tag = "wolf";
        //GetComponent<Renderer>().material.SetColor("_Color", new Color(.7f,.7f,.7f, 1f));
        Speed = 135.0f;
        ChaseSpeed = 189.6f;
        StarvationLimit = 210f;
        nutrition = 12f;
    }

    public override void Die()
    {
        GetComponent<Wolf>().Reset();
        gameObject.SetActive(false);
        GameManager.AddToDeadQueue(gameObject);
    }
}
