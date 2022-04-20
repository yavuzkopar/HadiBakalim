using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OutHandExtentions
{
    public static void TakeToHand(this GameObject obj, Transform trans)
    {
        obj.transform.parent = trans;
    }
}
