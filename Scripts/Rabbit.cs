using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : Prey
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        Ai = new BunnyAi(this);
        this.Diet.Add("plant");
        //this.Diet.Add("tree");
        Predetors.Add("bear");
        Predetors.Add("wolf");
        tag = "rabbit";
        Speed = 125.0f;
        ChaseSpeed = 179.4f;
        StarvationLimit = 45f;
        nutrition = 22f;
    }

    public override void Die()
    {
        GetComponent<Rabbit>().Reset();
        gameObject.SetActive(false);
        GameManager.AddToDeadQueue(gameObject);
    }
}
