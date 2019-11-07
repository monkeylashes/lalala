using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyAi : AI
{
    public BunnyAi(Entity entity) : base(entity)
    {
        maxChaseTime = 6f;
        maxSightDistance = 40f;
    }

    //protected override void Search()
    //{
    //    moveLocation = new Vector3(Random.Range(1, 99), 0.0f, Random.Range(1, 99));
    //    var direction = moveLocation - entity.transform.position;
    //    //var toRotaion = Quaternion.FromToRotation(entity.transform.forward, direction);
    //    var toRotaion = Quaternion.LookRotation(entity.transform.forward, Vector3.up);
    //    entity.transform.rotation = Quaternion.Lerp(entity.transform.rotation, toRotaion, .7f * Time.deltaTime);
    //    if (entity.transform.rotation == toRotaion)
    //    {
    //        currentState = State.Moving;
    //        startTime = Time.time;
    //    }
    //}
}
