using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : Prey
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        Ai = new PreyAi(this);
        this.Diet.Add("plant");
        //this.Diet.Add("tree");
        this.Predetors.Add("wolf");
        this.Predetors.Add("bear");
        Speed = 105.0f;
        ChaseSpeed = 160.0f;
        nutrition = 38f;
    }

    public override void Die()
    {
        GetComponent<Sheep>().Reset();
        gameObject.SetActive(false);
        GameManager.AddToDeadQueue(gameObject);
    }
}
