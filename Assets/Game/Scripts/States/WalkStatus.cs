using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkStatus : Status
{
    [SerializeField] GameObject chase;
    [SerializeField] GameObject idle;
    [SerializeField] GameObject ye;
     [SerializeField] float speed;
    new void OnEnable()
    {
        agent.speed = speed;
        agent.isStopped = false;
        base.OnEnable();
    }

    
}
