using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OutHandExtentions
{    
    public static void TakeToHand(this Transform trans,Transform makeChild)
    {
        trans.parent = makeChild;
        trans.localPosition = Vector3.zero;
        trans.localEulerAngles = Vector3.zero;
        
    }
}
