using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeToHandAction : OutHandAction
{
    

    public override void Doit()
    {       
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        TPoint.singleton.activeObject.transform.parent = PlayerInfo.singleton.rightHandTransform;
        TPoint.singleton.activeObject.transform.localPosition = Vector3.zero;
        TPoint.singleton.activeObject.transform.localEulerAngles = Vector3.zero;

    }
}
