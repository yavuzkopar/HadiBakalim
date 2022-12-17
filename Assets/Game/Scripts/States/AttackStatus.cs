using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStatus : Status
{
    [SerializeField] GameObject idle;
    new void OnEnable()
    {
        agent.isStopped = true;
        base.OnEnable();
    }

}
