using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyAi : AI
{
    public PreyAi(Entity entity):base(entity)
    {
        maxChaseTime = 5f;
    }

}
