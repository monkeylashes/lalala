using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Entity: MonoBehaviour
{    
    public float Health { get; set; }
    public float Hunger { get; set; }
    public float Speed { get; set; }
    public float ChaseSpeed { get; set; }
    public float StarvationLimit { get; set; }
    public float nutrition { get; set; }
    public Rigidbody Rb { get; set; }
    public HashSet<string> Diet { get; set; }
    public HashSet<string> Predetors { get; set; }
    //public GameObject particle { get; set; }

    protected AI Ai { get; set; }
    
    public void Start()
    {
        Rb = GetComponent<Rigidbody>();
        Diet = new HashSet<string>();
        Predetors = new HashSet<string>();
        Hunger = 8f;
        StarvationLimit = 100f;
        nutrition = -10f;
    }

    public abstract void Die();

    public void Reset()
    {
        Hunger = 0f;
        gameObject.transform.position = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Diet == null || CompareTag("plant"))
            return;

        if (collision.gameObject.CompareTag("wall"))
        {
            Ai.SetState(AI.State.Searching);
            return;
        }

        if (Diet.Contains(collision.gameObject.tag))
        {
            //Debug.Log("found food: " + collision.gameObject);
            //Instantiate(particle, collision.gameObject.transform.position, Quaternion.identity);
            //collision.gameObject.SetActive(false);
            //Destroy(collision.gameObject, Random.Range(2f, 10f));
            collision.gameObject.GetComponent<Entity>().Die();
            Hunger -= nutrition;

            if(Hunger <= 0f)
            {
                //Instantiate(gameObject);
                GameManager.Resurrect(gameObject.tag, gameObject.transform);
                Hunger = 8f;
            }
            Ai.ClearState();
        }
        else
        {
            Ai.ClearState();
        }
    }

    public void Update()
    {
    }


    public void FixedUpdate()
    {
        //if (!gameObject.activeSelf) {
        //    Destroy(gameObject);
        //    this.enabled = false;
        //    return;
        //}
        //if(!gameObject.activeInHierarchy)        
        //    Destroy(gameObject);
     
        
        Ai.Process();
        Hunger += .04f;
        if(Hunger >= StarvationLimit)
        {
            //Instantiate(particle, gameObject.transform.position, Quaternion.identity);

            Hunger = StarvationLimit;
            //gameObject.SetActive(false);
            //Destroy(gameObject, Random.Range(2f,10f));

            Die();
        }
    }
}
