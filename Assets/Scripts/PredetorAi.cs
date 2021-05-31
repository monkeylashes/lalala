using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredetorAi : AI
{

    public PredetorAi(Entity entity) : base(entity)
    {
        //maxChaseTime = Random.Range(5.0f, 10.0f);
        maxChaseTime = 5f;
        maxSightDistance = 50f;
    }

    //new public void ClearState()
    //{
    //    target = null;
    //    currentState = State.Idle;
    //    moveLocation = Vector3.zero;
    //    //currentState = Random.Range(0, 10) <= 5 ? State.Searching : State.Idle;
    //    //currentState = State.Waiting;
    //}

    //protected override void Idle()
    //{
    //    LookAround();
    //    GetClosestTarget();
    //    if (target == null)
    //    {
    //        currentState = State.Searching;
    //        startTime = Time.time;
    //    }
    //    else if (entity.Diet.Contains(target.tag))
    //    {
    //        currentState = State.Chasing;
    //        startTime = Time.time;
    //    }
    //    else if (entity.Predetors.Contains(target.tag))
    //    {
    //        currentState = State.RunningAway;
    //    }
    //    else
    //    {
    //        currentState = State.Searching;
    //        startTime = Time.time;
    //    }
    //}

    protected override void Search()
    {
        if(moveLocation == Vector3.zero)
            moveLocation = new Vector3(Random.Range(1, 200), 0.0f, Random.Range(1, 200));

        var direction = moveLocation - entity.transform.position;
        //var toRotaion = Quaternion.FromToRotation(entity.transform.forward, direction);
        var toRotaion = Quaternion.LookRotation(entity.transform.forward, Vector3.up);
        entity.transform.rotation = Quaternion.Lerp(entity.transform.rotation, toRotaion, .5f * Time.deltaTime);
        if (entity.transform.rotation == toRotaion)
        {
            currentState = State.Moving;
            startTime = Time.time;
        }
    }
}
