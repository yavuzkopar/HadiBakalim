using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStatus : Status
{
    [SerializeField] GameObject chase;
    [SerializeField] GameObject hunger;

    new void OnEnable()
    {
        
        agent.isStopped = true;
        base.OnEnable();
    }

    
}
