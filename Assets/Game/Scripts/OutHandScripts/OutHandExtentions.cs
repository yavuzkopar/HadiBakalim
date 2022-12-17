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
        if (trans.GetComponent<Rigidbody>() == null) return;
        
        trans.GetComponent<Rigidbody>().MakeRbDisable();
        
    }
    public static void MakeRbDisable(this Rigidbody rigidbody)
    {
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }
}
