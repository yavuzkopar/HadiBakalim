using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitAction : OutHandAction
{
    
    public override void Doit()
    {
      //  Animator animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
      //  animator.SetTrigger("sit");
      anim.SetTrigger("sit");
        Debug.Log("Sit action");
    }
    
}
