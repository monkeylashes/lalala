using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : Entity
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        StarvationLimit = 1000f;
        Ai = new PlantAi(this);
    }
    
    new private void FixedUpdate()
    {
        if (gameObject.transform.position.y > 1.5f)
        {
            gameObject.SetActive(false);
            Destroy(gameObject, 10f);
        }
    }
    // Update is called once per frame
    new void Update()
    {
        
    }

    public override void Die()
    {
        GetComponent<Plant>().Reset();
        gameObject.SetActive(false);
        GameManager.AddToDeadQueue(gameObject);
    }
}
