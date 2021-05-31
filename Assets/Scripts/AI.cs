using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class AI
{
    public enum State
    {
        Searching,
        Moving,
        RunningAway,
        Chasing,
        Eating,
        Idle,
        Waiting
    }

    protected Entity entity;
    protected GameObject previousTarget;
    protected GameObject target;
    protected State currentState;
    protected List<RaycastHit> hits;
    protected float maxSightDistance;
    protected Vector3 moveLocation;
    protected float startTime;
    protected float maxChaseTime;
    
    public AI(Entity entity)
    {
        this.entity = entity;
        currentState = State.Idle;
        hits = new List<RaycastHit>();
        maxSightDistance = 20f;
        moveLocation = new Vector3(Random.Range(1.0f, 99.0f), 0.0f, Random.Range(1.0f, 99.0f));
        entity.transform.LookAt(moveLocation, Vector3.up);
        maxChaseTime = 5.0f;
    }

    public void Process()
    {

        // entity.transform.GetChild(0)?.gameObject.GetComponent<TextMeshPro>()?.SetText(currentState.ToString() + " " + (int)entity.Hunger + "/" + (int)entity.StarvationLimit);

        CheckDanger();

        switch (currentState)
        {
            case State.Idle:                
                Idle();
                break;
            case State.Searching:                
                Search();
                break;
            case State.Moving:
                Move();
                break;
            case State.Chasing:
                Chase();
                break;
            case State.RunningAway:
                Run();
                break;
            case State.Waiting:
                Wait();
                break;
            default:
                currentState = State.Idle;
                break;
        }
    }
    protected void CheckDanger()
    {
        LookAround();
        GetClosestTarget();
        if (target != null)
        {
            if (entity.Predetors.Contains(target.tag))
            {
                currentState = State.RunningAway;
            }
            else if (entity.Diet.Contains(target.tag))
            {
                currentState = State.Chasing;
            }
            else if(previousTarget != null)
            {
                target = previousTarget;
            }
        }
    }

    public void SetState(State state)
    {
        currentState = state;
    }

    public void ClearState()
    {
        target = null;
        currentState = State.Idle;
        //currentState = Random.Range(0, 10) <= 5 ? State.Searching : State.Idle;
        //currentState = State.Waiting;
        moveLocation = Vector3.zero;
    }

    protected virtual void Idle()
    {
        LookAround();
        GetClosestTarget();
        if(target == null)
        {
            currentState = State.Searching;
            startTime = Time.time;
        }
        else if (entity.Diet.Contains(target.tag))
        {
            currentState = State.Chasing;
            startTime = Time.time;
        }
        else if (entity.Predetors.Contains(target.tag))
        {
            currentState = State.RunningAway;
        }
        else
        {
            currentState = State.Waiting;
            startTime = Time.time;
        }
    }

    //protected virtual void Search()
    //{
    //    if(moveLocation == Vector3.zero)
    //        moveLocation = new Vector3(Random.Range(1, 99), 0.0f, Random.Range(1, 99));
    //    entity.transform.LookAt(moveLocation, Vector3.up);        
    //    currentState = State.Moving;        
    //}

    protected virtual void Search()
    {
        moveLocation = new Vector3(Random.Range(1, 99), 0.0f, Random.Range(1, 99));
        var direction = moveLocation - entity.transform.position;
        var toRotaion = Quaternion.LookRotation(entity.transform.forward, Vector3.up);
        entity.transform.rotation = Quaternion.Lerp(entity.transform.rotation, toRotaion, .5f * Time.deltaTime);
        if (entity.transform.rotation == toRotaion)
        {
            currentState = State.Moving;
            startTime = Time.time;
        }
    }

    protected virtual void Wait()
    {
        //entity.Rb.velocity = Vector3.zero;
        entity.Rb.angularVelocity = Vector3.zero;

        if (Time.time - startTime > Random.Range(1.0f, 4.0f))
        {
            currentState = Random.Range(0, 10) <= 8 ? State.Searching : State.Idle;
        }
    }

    protected virtual void Move()
    {        
        entity.Rb.velocity = (moveLocation - entity.transform.position).normalized * entity.Speed * Time.deltaTime;
        entity.transform.LookAt(moveLocation, Vector3.up);

        //LookAround();
        
        //if (hits.Count > 0)
        //{
        //    GetClosestTarget();
        //    //Debug.Log("ENTITY: " + entity);
        //    if(target != null)
        //    {
        //        if (entity.Predetors.Contains(target.tag))
        //        {
        //            currentState = State.RunningAway;
        //        }
        //        else if (entity.Diet.Contains(target.tag))
        //        {
        //            currentState = State.Chasing;
        //        }
        //        //currentState = State.Idle;
        //    }
        //}

        if (Time.time - startTime > maxChaseTime || (entity.transform.position - moveLocation).magnitude <= 1f)
        {
            startTime = Time.time;
            this.ClearState();
        }
    }

    
    protected virtual void Chase()
    {       
        if(target == null)
        {
            this.ClearState();
            return;
        }

        entity.transform.LookAt(target.transform.position, Vector3.up);        
        entity.Rb.velocity = (target.transform.position - entity.transform.position).normalized * entity.ChaseSpeed * Time.deltaTime;
        if(Time.time - startTime > maxChaseTime || target == null || !Physics.Raycast(entity.transform.position, (target.transform.position - entity.transform.position).normalized, maxSightDistance))
        {
            startTime = Time.time;
            this.ClearState();
        }        

    }

    protected virtual void Run()
    {        
        entity.Rb.velocity = -(target.transform.position - entity.transform.position).normalized * entity.ChaseSpeed * Time.deltaTime;
        LookAround();

        if (hits.Count == 0)
        {
            this.ClearState();
            return;
        }

        var closest = GetClosestTarget();

        if (entity.Predetors.Contains(closest.tag))
        {
            entity.transform.LookAt(closest.transform.position, Vector3.up);
            entity.transform.Rotate(new Vector3(0, 180f, 0));
        }
        else
        {
            this.ClearState();
        }
    }
          
    protected List<RaycastHit> LookAround()
    {
        hits.Clear();

        // do the raycasting
        hits.AddRange(Physics.RaycastAll(entity.transform.position, entity.transform.right, maxSightDistance));
        hits.AddRange(Physics.RaycastAll(entity.transform.position, entity.transform.right + entity.transform.forward, maxSightDistance));
        hits.AddRange(Physics.RaycastAll(entity.transform.position, entity.transform.forward, maxSightDistance));
        hits.AddRange(Physics.RaycastAll(entity.transform.position, (-entity.transform.right) + entity.transform.forward, maxSightDistance));
        hits.AddRange(Physics.RaycastAll(entity.transform.position, -entity.transform.right, maxSightDistance));
        hits.AddRange(Physics.RaycastAll(entity.transform.position, -entity.transform.forward, maxSightDistance));

        return hits;
    }

    protected GameObject GetClosestTarget()
    {
        previousTarget = target;
        foreach (var hit in hits)
        {
            if (entity.transform.CompareTag(hit.transform.tag))
            {
                continue;
            }

            if (target == null)
            {
                target = hit.transform.gameObject;
                continue;
            }

            if ((hit.transform.position - entity.transform.position).magnitude < (target.transform.position - entity.transform.position).magnitude)
            {
                target = hit.transform.gameObject;
            }
        }

        return target;
    }

}
