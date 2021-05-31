using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Prey : Entity
{
    
    //public GameObject preyDieParticle;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        Ai = new PreyAi(this);
        this.Diet.Add("tree");
        this.Predetors.Add("wolf");
        this.Predetors.Add("bear");
        //GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        Speed = 105.0f;
        ChaseSpeed = 128.0f;
        nutrition = 35f;
        //this.particle = preyDieParticle;
    }
}
