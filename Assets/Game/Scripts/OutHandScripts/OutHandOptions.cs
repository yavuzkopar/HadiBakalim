using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutHandOptions : MonoBehaviour
{
    public static Animator anim;

    private void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
       
    }

    public OutHandScriptible[] options;
}
