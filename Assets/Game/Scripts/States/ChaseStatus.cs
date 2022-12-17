using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseStatus : Status
{
    [SerializeField] GameObject idle;
    [SerializeField] GameObject attack;
    [SerializeField] float speed;
    new void OnEnable()
    {
        agent.speed = speed;
        agent.isStopped = false;
        
        base.OnEnable();
    }
    
    
}
