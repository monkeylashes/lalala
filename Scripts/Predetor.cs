using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Predetor : Entity
{    
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();                
        //GetComponent<Renderer>().material.SetColor("_Color", Color.black);
        //Speed = 115.0f;
        //ChaseSpeed = 125.4f;
        //StarvationLimit = 150f;
        //nutrition = 25f;
        //this.particle = predetorDieParticle;
    }
}
