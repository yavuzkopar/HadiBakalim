using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAction : OutHandAction
{
    

    public override void Doit()
    {
        anim.SetTrigger("push");
        Debug.Log("Push action");
    }

    
}
